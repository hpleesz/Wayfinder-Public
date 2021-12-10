using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WayfinderServer.DTOs;
using WayfinderServer.Entities;
using WayfinderServer.Services;

namespace WayfinderServer.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class VirtualObjectsController : ControllerBase
    {

        private IVirtualObjectService _virtualObjectService;

        private IMapper _mapper;
        public VirtualObjectsController(IVirtualObjectService virtualObjectService, IMapper mapper)
        {
            _virtualObjectService = virtualObjectService;
            _mapper = mapper;
        }



        //2D MAP
        [HttpPost("{id}/image")]
        public async Task<IActionResult> AddImageToVirtualObject(IFormFile file, int id)
        {
            await _virtualObjectService.AddImageToVirtualObject(file, id);
            return Ok();

        }

        //2D MAP
        [HttpPost("{id}/video")]
        public async Task<IActionResult> AddVideoToVirtualObject(IFormFile file, int id)
        {
            await _virtualObjectService.AddVideoToVirtualObject(file, id);
            return Ok();

        }

        [HttpPost("{id}/obj")]
        public async Task<IActionResult> AddObjToVirtualObject(IFormFile file, int id)
        {
            await _virtualObjectService.AddObjToVirtualObject(file, id);
            return Ok(id);

        }

        [HttpPost("{id}/texture")]
        public async Task<IActionResult> AddTextureToVirtualObject(IFormFile file, int id)
        {
            await _virtualObjectService.AddTextureToVirtualObject(file, id);
            return Ok(id);

        }



        [HttpPut("{id}")]
        public IActionResult EditVirtualObject(int id, [FromBody] VirtualObjectCoordinatesDTO virtualObject)
        {
            var virtualObjectId = _virtualObjectService.EditVirtualObjectCoordinates(id, _mapper.Map<VirtualObject>(virtualObject));
            if (virtualObjectId > 0)
            {
                return Ok(virtualObjectId);
            }
            else
            {
                return BadRequest();
            }

        }

        

    }
}
