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
    public class FloorsController : ControllerBase
    {

        private IFloorService _floorService;
        private ITargetService _targetService;
        private IMarkerService _markerService;
        private IFloorSwitcherPointService _floorSwitcherPointService;

        private IMapper _mapper;
        public FloorsController(IFloorService floorService, ITargetService targetService, IFloorSwitcherPointService floorSwitcherPointService, IMarkerService markerService, IMapper mapper)
        {
            _floorService = floorService;
            _targetService = targetService;
            _markerService = markerService;
            _floorSwitcherPointService = floorSwitcherPointService;
            _mapper = mapper;
        }


        [HttpGet("{id}")]
        public IActionResult GetFloor(int id)
        {
            var floor = _mapper.Map<FloorDTO>(_floorService.GetFloor(id));
            if (floor != null)
            {
                return Ok(floor);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPut("{id}")]
        public IActionResult EditFloor(int id, [FromBody] FloorNewDTO floor)
        {
            var floorId = _floorService.EditFloor(id, _mapper.Map<Floor>(floor));
            if (floorId > 0)
            {
                return Ok(floorId);
            }
            else
            {
                return BadRequest();
            }


        }
        //3D MODEL
        [HttpPost("{id}/3d-model")]
        public async Task<IActionResult> AddFloorPlan3DToFloor([FromBody] string fileContents, int id)
        {
            await _floorService.AddFloorPlan3D(fileContents, id);
            return Ok();

        }

        //2D MAP
        [HttpPost("{id}/2d-map")]
        public async Task<IActionResult> AddFloorPlan2DToFloor(IFormFile file, int id)
        {
            await _floorService.AddFloorPlan2D(file, id);
            return Ok();

        }

        //TEMPLATE
        [HttpPost("{id}/template")]
        public async Task<IActionResult> AddFloorPlanTemplateImage(IFormFile file, int id)
        {
            await _floorService.AddFloorPlanTemplateImage(file, id);
            return Ok();

        }

        //TARGET
        [HttpPost("{id}/targets")]
        public IActionResult AddTargetToFloor([FromBody] TargetNewDTO target, int id)
        {
            var categoryId = _targetService.AddTarget(_mapper.Map<Target>(target), id);
            if (categoryId > 0)
            {
                return Ok(id);
            }
            else
            {
                return BadRequest();
            }

        }

        //MARKER
        [HttpPost("{id}/markers")]
        public IActionResult AddMarkerToFloor([FromBody] MarkerNewDTO marker, int id)
        {
            var markerId = _markerService.AddMarker(_mapper.Map<Marker>(marker), id);
            if (markerId > 0)
            {
                return Ok(markerId);
            }
            else
            {
                return BadRequest();
            }

        }

        //TARGET
        [HttpPost("{id}/floor-switcher-points")]
        public IActionResult AddFloorSwitcherPointToFloor([FromBody] FloorSwitcherPointNewDTO floorSwitcherPoint , int id)
        {
            var floorSwitcherPointId = _floorSwitcherPointService.AddFloorSwitcherPoint(_mapper.Map<FloorSwitcherPoint>(floorSwitcherPoint), id);
            if (floorSwitcherPointId > 0)
            {
                return Ok(floorSwitcherPointId);
            }
            else
            {
                return BadRequest();
            }

        }


        //get floor switcher points
        [HttpGet("{id}/floor-switcher-points")]
        public IActionResult GetAllFloorSwitcherPoints(int id)
        {
            var points = _mapper.Map<IEnumerable<FloorSwitcherPointDTO>>(_floorSwitcherPointService.GetAllFloorSwitcherPointsByFloor(id));
            return Ok(new { floorswitcherpoints = points });
        }

        //get floor switcher points search
        [HttpGet("{id}/floor-switcher-points/search")]
        public IActionResult GetFloorSwitcherPointsSearch([FromQuery] string term, int id)
        {
            var points = _mapper.Map<IEnumerable<FloorSwitcherPointDTO>>(_floorSwitcherPointService.GetFloorSwitcherPointsByFloorSearch(term, id));
            return Ok(new { floorswitcherpoints = points });
        }


        //get floor switcher points
        [HttpGet("{id}/targets")]
        public IActionResult GetTargets(int id)
        {
            var targets = _mapper.Map<IEnumerable<Target>>(_targetService.GetAllTargetsByFloor(id));
            return Ok(new { targets = targets });
        }


        [HttpGet("{id}/markers")]
        public IActionResult GetMarkers(int id)
        {
            var markers = _mapper.Map<IEnumerable<Marker>>(_markerService.GetAllMarkersByFloor(id));
            return Ok(new { markers = markers });
        }
    }
}
