using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
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
    public class PlacesController : ControllerBase
    {
        private IPlaceService _placeService;
        private IFloorService _floorService;
        private ICategoryService _categoryService;
        private ITargetService _targetService;
        private IMarkerService _markerService;
        private IFloorSwitcherService _floorSwitcherService;
        private IFloorSwitcherPointService _floorSwitcherPointService;

        private IMapper _mapper;
        public PlacesController(IPlaceService placeService, IFloorService floorService, ICategoryService categoryService, ITargetService targetService, IMarkerService markerService, IFloorSwitcherService floorSwitcherService, IFloorSwitcherPointService floorSwitcherPointService, IMapper mapper)
        {
            _placeService = placeService;
            _floorService = floorService;
            _categoryService = categoryService;
            _targetService = targetService;
            _markerService = markerService;
            _floorSwitcherService = floorSwitcherService;
            _floorSwitcherPointService = floorSwitcherPointService;

            _mapper = mapper;
        }

        [HttpPost]
        public IActionResult AddPlace([FromBody] PlaceNewDTO place)
        {

            var placeId = _placeService.AddPlace(_mapper.Map<Place>(place));
            if(placeId > 0)
            {
                return Ok(placeId);
            }
            else
            {
                return BadRequest();
            }

        }

        [HttpPost("{id}/image")]
        public async Task<IActionResult> AddPlaceImage(IFormFile file, int id)
        {
            await _placeService.AddPlaceImage(file, id);

            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult EditPlace(int id, [FromBody] PlaceNewDTO place)
        {
            var placeId = _placeService.EditPlace(id, _mapper.Map<Place>(place));
            if (placeId > 0)
            {
                return Ok(placeId);
            }
            else
            {
                return BadRequest();
            }

        }

        [HttpGet]
        public IActionResult GetAllPlaces()
        {
            var places = _mapper.Map<IEnumerable<PlaceDTO>>(_placeService.GetAllPlaces());
            return Ok(new {places = places});
        }

        [HttpGet("{id}")]
        public IActionResult GetPlace(int id)
        {
            var place = _mapper.Map<PlaceDTO>(_placeService.GetPlace(id));
            if(place != null)
            {
                return Ok(place);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet("search")]
        public IActionResult GetPlacesSearch([FromQuery] string term)
        {
            var places = _mapper.Map<IEnumerable<PlaceDTO>>(_placeService.GetPlacesSearch(term));
            return Ok(new { places = places });
        }


        //FLOOR
        [HttpPost("{id}/floors")]
        public IActionResult AddFloorToPlace([FromBody] FloorNewDTO floor, int id)
        {
            var floorId = _floorService.AddFloor(_mapper.Map<Floor>(floor), id);
            if (floorId > 0)
            {
                return Ok(floorId);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet("{id}/floors")]
        public IActionResult GetAllFloorsByPlace(int id)
        {
            var floors = _mapper.Map<IEnumerable<FloorDTO>>(_floorService.GetAllFloorsByPlace(id));
            return Ok(new { floors = floors });
        }

        [HttpGet("{id}/floors/search")]
        public IActionResult GetFloorsSearch([FromQuery] string term, int id)
        {
            var floors = _mapper.Map<IEnumerable<FloorDTO>>(_floorService.GetFloorsSearch(term,id));
            return Ok(new { floors = floors });
        }


        //CATEGORIES
        [HttpPost("{id}/categories")]
        public IActionResult AddCategoryToPlace([FromBody] CategoryNewDTO category, int id)
        {
            var categoryId = _categoryService.AddCategory(_mapper.Map<Category>(category), id);
            if (categoryId > 0)
            {
                return Ok(categoryId);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet("{id}/categories")]
        public IActionResult GetAllCategoriesByPlace(int id)
        {
            var categories = _mapper.Map<IEnumerable<CategoryDTO>>(_categoryService.GetAllCategoriesByPlace(id));
            return Ok( new { categories = categories });
        }


        [HttpGet("{id}/categories/search")]
        public IActionResult GetCategoriesSearch([FromQuery] string term, int id)
        {
            var categories = _mapper.Map<IEnumerable<CategoryDTO>>(_categoryService.GetCategoriesSearch(term, id));
            return Ok(new { categories = categories });
        }

        //TARGET

        [HttpGet("{id}/targets")]
        public IActionResult GetAllTargetsByPlace(int id)
        {
            var targets = _mapper.Map<IEnumerable<TargetDTO>>(_targetService.GetAllTargetsByPlace(id));
            return Ok(new { targets = targets });
        }

        [HttpGet("{id}/targets/search")]
        public IActionResult GetTargetsSearch([FromQuery] string term, int id)
        {
            var targets = _mapper.Map<IEnumerable<TargetDTO>>(_targetService.GetTargetsSearch(term, id));
            return Ok(new { targets = targets });
        }


        //MARKER

        [HttpGet("{id}/markers")]
        public IActionResult GetAllMarkersByPlace(int id)
        {
            var markers = _mapper.Map<IEnumerable<MarkerDTO>>(_markerService.GetAllMarkersByPlace(id));
            return Ok(new { markers = markers });
        }

        [HttpGet("{id}/markers/search")]
        public IActionResult GetMarkersSearch([FromQuery] string term, int id)
        {
            var markers = _mapper.Map<IEnumerable<MarkerDTO>>(_markerService.GetMarkersSearch(term, id));
            return Ok(new { markers = markers });
        }


        [HttpGet("{id}/targets/category-search")]
        public IActionResult GetTargetsSearchByCategory([FromQuery] string term, [FromQuery] string category, int id)
        {
            int categoryId = string.IsNullOrEmpty(category) ? 0 : int.Parse(category);

            var targets = _mapper.Map<IEnumerable<TargetDTO>>(_targetService.GetTargetsSearchByCategory(term, categoryId, id));
            return Ok(new { targets = targets });
        }

        [HttpPost("{id}/floor-switchers")]
        public IActionResult AddFloorSwitcherToPlace([FromBody] FloorSwitcherNewDTO floorSwitcher, int id)
        {
            var floorSwitcherId = _floorSwitcherService.AddFloorSwitcher(_mapper.Map<FloorSwitcher>(floorSwitcher), id);
            if (floorSwitcherId > 0)
            {
                return Ok(floorSwitcherId);
            }
            else
            {
                return BadRequest();
            }

        }

        //Get floor switchers
        [HttpGet("{id}/floor-switchers")]
        public IActionResult GetAllFloorSwitchersByPlace(int id)
        {
            var floorSwitchers = _mapper.Map<IEnumerable<FloorSwitcherDTO>>(_floorSwitcherService.GetAllFloorSwitchersByPlace(id));
            return Ok(new { floorswitchers = floorSwitchers });
        }

        //Get floor switchers search
        [HttpGet("{id}/floor-switchers/search")] 
        public IActionResult GetFloorSwitchersSearch([FromQuery] string term, int id)
        {
            var floorSwitchers = _mapper.Map<IEnumerable<FloorSwitcherDTO>>(_floorSwitcherService.GetFloorSwitchersSearch(term, id));
            return Ok(new { floorswitchers = floorSwitchers });
        }

        //Get floor switchers
        [HttpGet("{id}/floor-switcher-points")]
        public IActionResult GetAllFloorSwitcherPointsByPlace(int id)
        {
            var floorSwitcherPoints = _mapper.Map<IEnumerable<FloorSwitcherPointDTO>>(_floorSwitcherPointService.GetAllFloorSwitcherPointsByPlace(id));
            return Ok(new { floorswitcherpoints = floorSwitcherPoints });
        }

        //Get floor switchers search
        [HttpGet("{id}/floor-switcher-points/search")]
        public IActionResult GetFloorSwitcherPointsByPlaceSearch([FromQuery] string term, int id)
        {
            var floorSwitcherPoints = _mapper.Map<IEnumerable<FloorSwitcherPointDTO>>(_floorSwitcherPointService.GetFloorSwitcherPointsByPlaceSearch(term, id));
            return Ok(new { floorswitcherpoints = floorSwitcherPoints });
        }

        //Get floor switchers
        [HttpGet("{id}/free-floor-switcher-points")]
        public IActionResult GetFreeFloorSwitcherPointsByPlace(int id)
        {
            var floorSwitcherPoints = _mapper.Map<IEnumerable<FloorSwitcherPointDTO>>(_floorSwitcherPointService.GetFreeFloorSwitcherPointsByPlace(id));
            return Ok(new { floorswitcherpoints = floorSwitcherPoints });
        }

        [HttpPost("{id}/target-distances")]
        public IActionResult GetTargetDistancesByPlace(int id, [FromBody] PathStepListDTO pathDistances)
        {

            List<TargetPathStepsDTO> targetPathSteps = _targetService.GetPathToTargets(id, pathDistances.pathSteps);


            return Ok(new { targetPathSteps = targetPathSteps });

        }

        [HttpPost("{id}/target-distances/category-search")]
        public IActionResult GetTargetDistancesSearchByCategory([FromQuery] string term, [FromQuery] string category, int id, [FromBody] PathStepListDTO pathDistances)
        {
            int categoryId = string.IsNullOrEmpty(category) ? 0 : int.Parse(category);

            //var targets = _mapper.Map<IEnumerable<TargetDTO>>(_targetService.GetTargetsSearchByCategory(term, categoryId, id));

            List<TargetPathStepsDTO> targetPathSteps = _targetService.GetPathToTargetsSearchByCategory(id, pathDistances.pathSteps, term, categoryId);

            return Ok(new { targetPathSteps = targetPathSteps });
        }




        //Get floor switchers
        [HttpGet("{id}/qr-code")]
        public IActionResult GetQrCodeByPlace(int id, [FromQuery] string term)
        {

            QRCodePointDTO result = _mapper.Map<QRCodePointDTO>(_placeService.GetQRCodeByPlace(id, term));

            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest();
            }

        }

        [HttpGet("{id}/qr-codes")]
        public IActionResult GetQrCodeByPlace(int id)
        {
            var qrCodes = _mapper.Map<IEnumerable<QRCodePointDTO>>(_placeService.GetQRCodesByPlace(id));

            return Ok( new { targets = qrCodes });

        }

    }
}
