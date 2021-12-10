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
    public class FloorSwitchersController : ControllerBase
    {

        private IFloorSwitcherService _floorSwitcherService;
        private IFloorSwitcherPointService _floorSwitcherPointService;

        private IMapper _mapper;
        public FloorSwitchersController(IFloorSwitcherService floorSwitcherService, IFloorSwitcherPointService floorSwitcherPointService, IMapper mapper)
        {
            _floorSwitcherService = floorSwitcherService;
            _floorSwitcherPointService = floorSwitcherPointService;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public IActionResult GetFloorSwitcher(int id)
        {
            var floorSwitcher = _mapper.Map<FloorSwitcherDTO>(_floorSwitcherService.GetFloorSwitcher(id));
            if (floorSwitcher != null)
            {
                return Ok(floorSwitcher);
            }
            else
            {
                return BadRequest();
            }

        }

        [HttpPut("{id}/floor-switcher-points")]
        public IActionResult AddFloorSwitcherPointsToFloorSwitcher(int id, [FromBody] FloorSwitcherPointListDTO floorSwitcherPointList)
        {
            var placeId = _floorSwitcherPointService.AddFloorSwitcherPointsToFloorSwitcher(_mapper.Map<List<FloorSwitcherPoint>>(floorSwitcherPointList.floorSwitcherPoints), id);
            if (placeId > 0)
            {
                return Ok(placeId);
            }
            else
            {
                return BadRequest();
            }

        }

        [HttpGet("{id}/floor-switcher-points")]
        public IActionResult GetFloorSwitcherPointsByFloorSwitcher(int id)
        {
            var floorSwitcherPoints = _mapper.Map<IEnumerable<FloorSwitcherPointDTO>>(_floorSwitcherPointService.GetFloorSwitcherPointsByFloorSwitcher(id));

            if (floorSwitcherPoints != null)
            {
                return Ok(new { floorswitcherpoints = floorSwitcherPoints });
            }
            else
            {
                return BadRequest();
            }

        }

        [HttpGet("{id}/free-floor-switcher-points")]
        public IActionResult GetFreeFloorSwitcherPointsByFloorSwitcher(int id)
        {
            var floorSwitcherPoints = _mapper.Map<IEnumerable<FloorSwitcherPointDTO>>(_floorSwitcherPointService.GetFreeFloorSwitcherPointsAndByFloorSwitcher(id));

            if (floorSwitcherPoints != null)
            {
                return Ok(new { floorswitcherpoints = floorSwitcherPoints });
            }
            else
            {
                return BadRequest();
            }

        }

    }
}
