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
    public class VirtualObjectTypesController : ControllerBase
    {

        private IVirtualObjectTypeService _virtualObjectTypeService;

        private IMapper _mapper;
        public VirtualObjectTypesController(IVirtualObjectTypeService virtualObjectTypeService, IMapper mapper)
        {
            _virtualObjectTypeService = virtualObjectTypeService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAllVirtualObjectTypes()
        {
            var virtualObjectTypes = _mapper.Map<IEnumerable<VirtualObjectTypeDTO>>(_virtualObjectTypeService.GetAllVirtualObjectTypes());
            return Ok(new { virtualObjectTypes = virtualObjectTypes });
        }

    }
}
