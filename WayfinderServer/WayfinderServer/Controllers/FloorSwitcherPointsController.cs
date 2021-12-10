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
    public class FloorSwitcherPointsController : ControllerBase
    {

        private IFloorSwitcherPointService _floorSwitcherPointService;
        private IFloorSwitcherPointDistanceService _floorSwitcherPointDistanceService;

        private IMapper _mapper;
        public FloorSwitcherPointsController(IFloorSwitcherPointService floorSwitcherPointService, IFloorSwitcherPointDistanceService floorSwitcherPointDistanceService, IMapper mapper)
        {
            _floorSwitcherPointService = floorSwitcherPointService;
            _floorSwitcherPointDistanceService = floorSwitcherPointDistanceService;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public IActionResult GetFloorSwitcherPoint(int id)
        {
            var floorSwitcherPoint = _mapper.Map<FloorSwitcherPointDTO>(_floorSwitcherPointService.GetFloorSwitcherPoint(id));
            if (floorSwitcherPoint != null)
            {
                return Ok(floorSwitcherPoint);
            }
            else
            {
                return BadRequest();
            }

        }

        //name + floor switcher?
        [HttpPut("{id}")]
        public IActionResult EditFloorSwitcherPoint(int id, [FromBody] FloorSwitcherPointNewDTO floorSwitcherPoint)
        {
            var floorSwitcherPointId = _floorSwitcherPointService.EditCoordinates(id, _mapper.Map<FloorSwitcherPoint>(floorSwitcherPoint));
            if (floorSwitcherPointId > 0)
            {
                return Ok(floorSwitcherPointId);
            }
            else
            {
                return BadRequest();
            }

        }

        [HttpPut("{id}/floor-switcher-point-distances")]
        public IActionResult AddFloorSwithcerPointDistancesToFloorSwitcherPoint(int id, [FromBody] FloorSwitcherPointDistanceListDTO floorSwitcherPointDistanceList)
        {
            var placeId = _floorSwitcherPointDistanceService.AddFloorSwithcerPointDistancesToFloorSwitcherPoint(_mapper.Map<List<FloorSwitcherPointDistance>>(floorSwitcherPointDistanceList.floorSwitcherPointDistances), id);
            if (placeId > 0)
            {
                return Ok(placeId);
            }
            else
            {
                return BadRequest();
            }

        }

        [HttpPut("{id}/target-distances")]
        public IActionResult AddTargetDistancesToFloorSwitcherPoint(int id, [FromBody] TargetDistanceListDTO targetDistanceList)
        {
            var placeId = _floorSwitcherPointDistanceService.AddTargetDistancesToFloorSwitcherPoint(_mapper.Map<List<TargetDistance>>(targetDistanceList.targetDistances), id);
            if (placeId > 0)
            {
                return Ok(placeId);
            }
            else
            {
                return BadRequest();
            }

        }
    }
}
