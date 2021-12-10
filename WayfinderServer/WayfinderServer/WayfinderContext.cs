using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WayfinderServer.Entities;

namespace WayfinderServer
{
    public class WayfinderContext : DbContext
    {
        public WayfinderContext(DbContextOptions<WayfinderContext> options)
            : base(options)
        {
        }

        //public DbSet<TodoItem> TodoItems { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Floor> Floors { get; set; }
        public DbSet<FloorPlan2D> FloorPlan2Ds { get; set; }
        public DbSet<FloorPlan3D> FloorPlan3Ds { get; set; }
        public DbSet<Marker> Markers { get; set; }
        public DbSet<Place> Places { get; set; }
        public DbSet<Target> Targets { get; set; }
        public DbSet<VirtualObject> VirtualObjects { get; set; }
        public DbSet<VirtualObjectType> VirtualObjectTypes { get; set; }
        public DbSet<FloorSwitcher> FloorSwitchers { get; set; }
        public DbSet<FloorSwitcherPoint> FloorSwitcherPoints { get; set; }
        public DbSet<FloorSwitcherPointDistance> FloorSwitcherPointDistances { get; set; }
        public DbSet<TargetDistance> TargetDistances { get; set; }



    }
}
