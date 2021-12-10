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
using WayfinderServer.Helpers;
using WayfinderServer.Services;

namespace WayfinderServer.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MarkersController : ControllerBase
    {

        private IMarkerService _markerService;

        private IMapper _mapper;
        public MarkersController(IMarkerService markerService, IMapper mapper)
        {
            _markerService = markerService;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public IActionResult GetMarker(int id)
        {
            var marker = _mapper.Map<MarkerDTO>(_markerService.GetMarker(id));
            if (marker != null)
            {
                return Ok(marker);
            }
            else
            {
                return BadRequest();
            }

        }

        [HttpPut("{id}")]
        public IActionResult EditMarker(int id, [FromBody] MarkerNewDTO marker)
        {
            var markerId = _markerService.EditCoordinates(id, _mapper.Map<Marker>(marker));
            if (markerId > 0)
            {
                return Ok(markerId);
            }
            else
            {
                return BadRequest();
            }

        }

    }
}
