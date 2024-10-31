using E_Commerce.DTOs;
using E_Commerce.Interfaces;
using E_Commerce.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;
using System.Security.Claims;

namespace E_Commerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartsController(IUnitOfWork _unitOfWork) : ControllerBase
    {
        [HttpPost("Create")]
        [Authorize(Roles = "Customer")]

        public async Task<IActionResult> Create()
        {
            var userId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var cart = new Cart
            {
                Id = 0,
                UserId = userId,
            };
            await _unitOfWork._cartService.AddAsync(cart);
            _unitOfWork.Complete();
            return Ok(cart);
        }

        [HttpPost("CreateFromWishlist")]
        [Authorize(Roles = "Customer")]

        public async Task<IActionResult> Create(int id)
        {
            var wishlist = _unitOfWork._wishlistService.GetById(id);

            int userId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var cart = new Cart
            {
                Id = 0,
                UserId = userId,
            };

            await _unitOfWork._cartService.AddAsync(cart);
            _unitOfWork.Complete();
            
            foreach (var item in wishlist.WishlistProducts)
            {
                var cartProduct = new CartProduct
                {
                    Id = 0,
                    ProductId = item.ProductId,
                    CartId = cart.Id,
                    TotalPrice = item.TotalPrice
                };
                await _unitOfWork._cartProductService.AddAsync(cartProduct);
                cart.TotalPrice += cartProduct.TotalPrice; 
               
            }
            _unitOfWork._cartService.Update(cart);
            _unitOfWork.Complete();
            return Ok(cart);
        }
        [HttpPut("AddCartPrducts")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> AddProducts(AddCartProduct dto)
        {
            var product = await _unitOfWork._productService.GetById(dto.ProductId);
            var cart = _unitOfWork._cartService.GetById(dto.CartId); 

            if(product == null || cart == null) { return BadRequest(); }

            var cartProduct = new CartProduct
            {
                Id = 0,
                CartId = cart.Id,
                ProductId = product.Id,
                TotalPrice = dto.quantity * product.price
            };
            await _unitOfWork._cartProductService.AddAsync(cartProduct);
            cart.TotalPrice += cartProduct.TotalPrice;
            _unitOfWork._cartService.Update(cart); 
            _unitOfWork.Complete();
            return Ok(cartProduct);
        }
        [HttpPut("DeleteCartProduct")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult>DeleteProduct(int id)
        {
            var product = _unitOfWork._cartProductService.GetById(id);  
            if(product == null) { return BadRequest(); }
            product.cart.TotalPrice -= product.TotalPrice;

           _unitOfWork._cartProductService.Delete(product);
            
            _unitOfWork.Complete();
            return Ok(product);
        }
        [HttpGet("GetPrice")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> getPrice(int id)
        {
            var cart = _unitOfWork._cartService.GetById(id);

            if (cart == null) { return BadRequest(); }

            return Ok($"The Total Price Is {cart.TotalPrice}");
        }
        [HttpDelete("Delete")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult>Delete(int id)
        {
            var cart = _unitOfWork._cartService.GetById(id); 

            if(cart == null) { return BadRequest(); }

            _unitOfWork._cartService.Delete(cart);  

            _unitOfWork.Complete();

            return Ok(cart);
        }

       
    }
}

