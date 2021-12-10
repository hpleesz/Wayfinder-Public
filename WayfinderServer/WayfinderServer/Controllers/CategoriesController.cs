using AutoMapper;
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
    public class CategoriesController : ControllerBase
    {

        private ICategoryService _categoryService;

        private IMapper _mapper;
        public CategoriesController(ICategoryService categoryService, IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }

        [HttpPut("{id}")]
        public IActionResult EditCategory(int id, [FromBody] CategoryNewDTO category)
        {
            var categoryId = _categoryService.EditCategory(id, _mapper.Map<Category>(category));
            if (categoryId > 0)
            {
                return Ok(categoryId);
            }
            else
            {
                return BadRequest();
            }


        }

        [HttpGet("{id}")]
        public IActionResult GetCategory(int id)
        {
            var category = _mapper.Map<CategoryDTO>(_categoryService.GetCategory(id));
            if (category != null)
            {
                return Ok(category);
            }
            else
            {
                return BadRequest();
            }
        }

    }
}
