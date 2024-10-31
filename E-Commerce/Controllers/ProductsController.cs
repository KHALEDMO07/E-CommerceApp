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
    public class ProductsController(IUnitOfWork _unitOfWork) : ControllerBase
    {
        [HttpPost("AddProduct")]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> AddProduct([FromForm] AddProductDto dto)
        {
            if (!ModelState.IsValid) { return BadRequest(ModelState); }

            var category = _unitOfWork._categoryService.GetByName(dto.categoryName);

            var DataStream = new MemoryStream();

            await dto.ImageFile.CopyToAsync(DataStream);

            var product = new Product
            {
                Id = 0,
                Name = dto.Name,
                Description = dto.Description,
                price = dto.price,
                CategoryId = category.Id,
                category = category,
                image = DataStream.ToArray()

            };
            await _unitOfWork._productService.AddAsync(product);
            _unitOfWork.Complete();
            return Ok(product);
        }

        [HttpPut("Update")]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> Update(int id, [FromForm] AddProductDto dto)
        {
            var product = await _unitOfWork._productService.GetById(id);

            if (!ModelState.IsValid) { return BadRequest(ModelState); }

            if(product == null) { return BadRequest("There Is No Product by this Id!!!"); }

            var DataStream = new MemoryStream();
            await dto.ImageFile.CopyToAsync(DataStream);

            product.Name = dto.Name;
            product.Description = dto.Description;
            product.price = dto.price;
            if(dto.categoryName != product.category.Name)
            {
                var c = _unitOfWork._categoryService.GetByName(dto.categoryName);   
                if(c == null)
                {
                    return BadRequest("There Is No Category By This Name");
                }
                product.category = c;
            } 
            product.image = DataStream.ToArray();

            _unitOfWork._productService.Update(product);
            _unitOfWork.Complete();

            return Ok(product);
        }

        [HttpGet()]

        public async Task<IActionResult> GetProductsByCategoryOrderedByPrice(string categoryName)
        {
            var category = _unitOfWork._categoryService.GetByName(categoryName);

            if(category == null) { return BadRequest("Choose Correct Category!"); }

            var products = _unitOfWork._productService.GetByCategory(categoryName);

            var res = new List<ProductDto>();

            foreach(var item in products)
            {
                var dto = new ProductDto
                {
                    Name = item.Name,
                    Description = item.Description,
                    price = item.price,
                    categoryName = item.category.Name,
                    imageFile = item.image
                };
                res.Add(dto);
            }
            return Ok(res);
        }

        [HttpGet("GetAll")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAll()
        {
            var products = _unitOfWork._productService.GetAll(); 

            if(products == null) { return NotFound(); }
            return Ok(products);
        }
        [HttpDelete("Delete")]
        [Authorize(Roles  = "Admin")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _unitOfWork._productService.GetById(id);

            if(product == null)
            {
                return BadRequest("There Is No Product By This Id!");
            }

            _unitOfWork._productService.Delete(product);
            _unitOfWork.Complete();
            return Ok(product);
        }
        
    }
}
