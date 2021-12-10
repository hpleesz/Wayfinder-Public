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
    public class TargetsController : ControllerBase
    {

        private ITargetService _targetService;
        private IVirtualObjectService _virtualObjectService;
        private ITargetDistanceService _targetDistanceService;

        private IMapper _mapper;
        public TargetsController(ITargetService targetService, IVirtualObjectService virtualObjectService, ITargetDistanceService targetDistanceService, IMapper mapper)
        {
            _targetService = targetService;
            _virtualObjectService = virtualObjectService;
            _targetDistanceService = targetDistanceService;
            _mapper = mapper;
        }

        //VIRTUAL OBJECT
        [HttpPost("{id}/virtual-objects")]
        public IActionResult AddVirtualObjectToTarget([FromBody] VirtualObjectNewDTO virtualObject, int id)
        {
            var virtualObjectId = _virtualObjectService.AddVirtualObject(_mapper.Map<VirtualObject>(virtualObject), id);
            if (virtualObjectId > 0)
            {
                return Ok(virtualObjectId);
            }
            else
            {
                return BadRequest();
            }

        }

        [HttpGet("{id}/virtual-objects")]
        public IActionResult GetVirtualObjectsByTarget(int id)
        {
            var virtualObjects = _mapper.Map<IEnumerable<VirtualObjectDTO>>(_virtualObjectService.GetAllVirtualObjectsByTarget(id));
            return Ok(new { virtualObjects = virtualObjects });
        }

        //TARGET
        [HttpPost("anchortest")]
        public void AddTargetToFloor([FromBody] string cloudAnchor)
        {
            //string cloudAnchor2 = "";
        }

        [HttpGet("{id}")]
        public IActionResult GetTarget(int id)
        {
            var target = _mapper.Map<TargetDTO>(_targetService.GetTarget(id));
            if (target != null)
            {
                return Ok(target);
            }
            else
            {
                return BadRequest();
            }
        
       }

        [HttpPut("{id}")]
        public IActionResult EditTarget(int id, [FromBody] TargetNewDTO target)
        {
            var placeId = _targetService.EditCoordinates(id, _mapper.Map<Target>(target));
            if (placeId > 0)
            {
                return Ok(placeId);
            }
            else
            {
                return BadRequest();
            }

        }

        [HttpPut("{id}/floor-switcher-point-distances")]
        public IActionResult AddFloorSwithcerPointDistancesToTarget(int id, [FromBody] TargetDistanceListDTO targetDistanceList)
        {
            var placeId = _targetDistanceService.AddFloorSwithcerPointDistancesToTarget(_mapper.Map<List<TargetDistance>>(targetDistanceList.targetDistances), id);
            if (placeId > 0)
            {
                return Ok(placeId);
            }
            else
            {
                return BadRequest();
            }

        }

        
        [HttpPost("{id}/path")]
        public IActionResult GetTargetDistance(int id, [FromBody] PathStepListDTO pathDistances)
        {
            

            TargetPathStepsDTO targetPathSteps = _targetService.GetPathToTarget(id, pathDistances.pathSteps);
            return Ok(targetPathSteps);

        }
        
    }
}
