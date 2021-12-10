using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WayfinderServer.Entities;

namespace WayfinderServer.Services
{
    public interface IVirtualObjectService
    {
        public int AddVirtualObject(VirtualObject virtualObject, int targetId);
        public Task AddImageToVirtualObject(IFormFile file, int virtualObjectId);
        public Task AddVideoToVirtualObject(IFormFile file, int virtualObjectId);
        public Task AddObjToVirtualObject(IFormFile file, int virtualObjectId);
        public Task AddTextureToVirtualObject(IFormFile file, int virtualObjectId);
        public int EditVirtualObjectCoordinates(int virtualObjectId, VirtualObject virtualObject);
        public IEnumerable<VirtualObject> GetAllVirtualObjectsByTarget(int targetId);

    }
    public class VirtualObjectService : IVirtualObjectService
    {
        private WayfinderContext _context;

        public VirtualObjectService(WayfinderContext context)
        {
            _context = context;
        }

        public int AddVirtualObject(VirtualObject virtualObject, int targetId)
        {
            var target = _context.Targets.SingleOrDefault(x => x.Id == targetId);

            var type = _context.VirtualObjectTypes.SingleOrDefault(x => x.Id == virtualObject.Type.Id);
            virtualObject.Target = target;
            virtualObject.Type = type;

            if (target.VirtualObjects == null)
            {
                target.VirtualObjects = new List<VirtualObject>();
            }
            target.VirtualObjects.Add(virtualObject);

            var addResult = _context.VirtualObjects.AddAsync(virtualObject);
            var updateResult = _context.Targets.Update(target);

            _context.SaveChanges();

            return virtualObject.Id;
        }


        public async Task AddImageToVirtualObject(IFormFile file, int virtualObjectId)
        {
            var filePath = "Resources/VirtualObjects/" + virtualObjectId + ".jpg";

            using (var stream = System.IO.File.Create(filePath))
            {
                var virtualObject = _context.VirtualObjects.SingleOrDefault(x => x.Id == virtualObjectId);


                virtualObject.FileLocation = filePath;

                
                var updateResult = _context.VirtualObjects.Update(virtualObject);

                await file.CopyToAsync(stream);
                _context.SaveChanges();

            }
        }

        public async Task AddVideoToVirtualObject(IFormFile file, int virtualObjectId)
        {
            var filePath = "Resources/VirtualObjects/" + virtualObjectId + ".mp4";

            using (var stream = System.IO.File.Create(filePath))
            {
                var virtualObject = _context.VirtualObjects.SingleOrDefault(x => x.Id == virtualObjectId);


                virtualObject.FileLocation = filePath;


                var updateResult = _context.VirtualObjects.Update(virtualObject);

                await file.CopyToAsync(stream);
                _context.SaveChanges();

            }
        }

        public int EditVirtualObjectCoordinates(int id, VirtualObject virtualObject)
        {
            var result = _context.VirtualObjects.SingleOrDefault(x => x.Id == id);
            result.XCoordinate = virtualObject.XCoordinate;
            result.YCoordinate = virtualObject.YCoordinate;
            result.ZCoordinate = virtualObject.ZCoordinate;
            result.XRotation = virtualObject.XRotation;
            result.YRotation = virtualObject.YRotation;
            result.ZRotation = virtualObject.ZRotation;
            result.XScale = virtualObject.XScale;
            result.YScale = virtualObject.YScale;
            result.ZScale = virtualObject.ZScale;

            var updated = _context.VirtualObjects.Update(result);
            _context.SaveChanges();
            //return result.IsCompletedSuccessfully;
            return result.Id;
        }


        public IEnumerable<VirtualObject> GetAllVirtualObjectsByTarget(int targetId)
        {
            return _context.VirtualObjects.Include(f => f.Target).Include(x => x.Type).Where(f => f.Target.Id == targetId).ToList();

        }

        public async Task AddObjToVirtualObject(IFormFile file, int virtualObjectId)
        {
            var filePath = "Resources/VirtualObjects/" + virtualObjectId + ".obj";

            using (var stream = System.IO.File.Create(filePath))
            {
                var virtualObject = _context.VirtualObjects.SingleOrDefault(x => x.Id == virtualObjectId);


                virtualObject.FileLocation = filePath;


                var updateResult = _context.VirtualObjects.Update(virtualObject);

                await file.CopyToAsync(stream);
                _context.SaveChanges();

            }

        }

        public async Task AddTextureToVirtualObject(IFormFile file, int virtualObjectId)
        {
            var filePath = "Resources/VirtualObjects/" + virtualObjectId + ".jpg";

            using (var stream = System.IO.File.Create(filePath))
            {
                var virtualObject = _context.VirtualObjects.SingleOrDefault(x => x.Id == virtualObjectId);


                virtualObject.TextureLocation = filePath;


                var updateResult = _context.VirtualObjects.Update(virtualObject);

                await file.CopyToAsync(stream);
                _context.SaveChanges();

            }

        }

    }



}
