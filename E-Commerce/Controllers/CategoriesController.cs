using E_Commerce.DTOs;
using E_Commerce.Interfaces;
using E_Commerce.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController(IUnitOfWork _unitOfWork) : ControllerBase
    {

        [HttpPost("Add")]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> Add(AddCategoryDto categoryDto)
        {
            var category = new Category { Id = 0, Name = categoryDto.CategoryName };

            if (!ModelState.IsValid) { return BadRequest(ModelState); }

            await _unitOfWork._categoryService.AddAsync(category);

            _unitOfWork.Complete();

            return Ok("Done");
        }

        [HttpGet("GetAll")]
        [Authorize]

        public async Task<IActionResult> GatAll()
        {
            var categories = _unitOfWork._categoryService.GetAll();

            if (categories == null) { return BadRequest("There Is No Categories"); }

            return Ok(categories);
        }
        [HttpGet("{categoryName}")]
        [Authorize]
        public async Task<IActionResult>Get(string categoryName)
        {
            var category = _unitOfWork._categoryService.GetByName(categoryName);

            if (category == null) { return BadRequest("There Is No Category By This Name"); }

            return Ok(category);    
        }
        [HttpDelete("Delete")]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> Delete(string name)
        {
            var category = _unitOfWork._categoryService.GetByName(name);

            if(category == null) { return BadRequest("There Is no such Category With That Name"); }

            _unitOfWork._categoryService.Delete(category);

            _unitOfWork.Complete();

            return Ok("Deleted!");
        }
    }
}
