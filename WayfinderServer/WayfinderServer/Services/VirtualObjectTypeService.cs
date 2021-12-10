using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WayfinderServer.Entities;

namespace WayfinderServer.Services
{
    public interface IVirtualObjectTypeService
    {
        public IEnumerable<VirtualObjectType> GetAllVirtualObjectTypes();

    }
    public class VirtualObjectTypeService : IVirtualObjectTypeService
    {
        private WayfinderContext _context;

        public VirtualObjectTypeService(WayfinderContext context)
        {
            _context = context;
        }


        public IEnumerable<VirtualObjectType> GetAllVirtualObjectTypes()
        {
            return _context.VirtualObjectTypes.ToList();

        }
    }



}
