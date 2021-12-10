using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WayfinderServer.Services;
using Microsoft.Extensions.FileProviders;
using System.IO;
using Microsoft.AspNetCore.StaticFiles;

namespace WayfinderServer
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<WayfinderContext>(opt =>opt.UseSqlServer(Configuration.GetConnectionString("WayfinderDatabase")));

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WayfinderServer", Version = "v1" });
            });

            services.AddAutoMapper(typeof(Startup));
            services.AddScoped<IPlaceService, PlaceService>();
            services.AddScoped<IFloorService, FloorService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<ITargetService, TargetService>();
            services.AddScoped<IVirtualObjectService, VirtualObjectService>();
            services.AddScoped<IVirtualObjectTypeService, VirtualObjectTypeService>();
            services.AddScoped<IFloorSwitcherPointService, FloorSwitcherPointService>();
            services.AddScoped<IFloorSwitcherService, FloorSwitcherService>();
            services.AddScoped<ITargetDistanceService, TargetDistanceService>();
            services.AddScoped<IFloorSwitcherPointDistanceService, FloorSwitcherPointDistanceService>();
            services.AddScoped<IMarkerService, MarkerService>();

            services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins,
                                  builder =>
                                  {
                                      builder.AllowAnyOrigin();
                                  });
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WayfinderServer v1"));

                var provider = new FileExtensionContentTypeProvider();
                provider.Mappings[".obj"] = "application/octet-stream";
                app.UseStaticFiles(); // For the wwwroot folder
                app.UseStaticFiles(new StaticFileOptions
                {
                    FileProvider = new PhysicalFileProvider(
                                Path.Combine(Directory.GetCurrentDirectory(), "Resources")),
                    RequestPath = "/Resources",
                    ContentTypeProvider = provider
                });
                /*
                app.UseStaticFiles(new StaticFileOptions
                {
                    FileProvider = new PhysicalFileProvider(
                    Path.Combine(Directory.GetCurrentDirectory(), "Models")),
                    RequestPath = "/Models"
                });*/
            }
            else
            {
                var provider = new FileExtensionContentTypeProvider();
                provider.Mappings[".obj"] = "application/octet-stream";

                app.UseStaticFiles(new StaticFileOptions
                {
                    FileProvider = new PhysicalFileProvider(
                     Path.Combine(env.ContentRootPath, "Resources")),
                        RequestPath = "/Resources",
                        ContentTypeProvider = provider
                });
                /*
                app.UseStaticFiles(new StaticFileOptions
                {
                    FileProvider = new PhysicalFileProvider(
                    Path.Combine(env.ContentRootPath, "Models")),
                    RequestPath = "/Models"
                });*/
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });




        }
    }
}
