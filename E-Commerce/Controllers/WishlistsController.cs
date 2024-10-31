using E_Commerce.DTOs;
using E_Commerce.Interfaces;
using E_Commerce.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace E_Commerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WishlistsController(IUnitOfWork _unitOfWork) : ControllerBase
    {
        [HttpPost("Create")]
        [Authorize(Roles = "Customer")]

        public async Task<IActionResult> CraeteWishlist()
        {
            var userId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
           
            var wishlist = new Wishlist();
            wishlist.Id = 0;
            wishlist.UserId = userId;

            await _unitOfWork._wishlistService.AddAsync(wishlist);
            _unitOfWork.Complete();
            return Ok(wishlist);
        }
        [HttpDelete("Delete")]
        [Authorize(Roles = "Customer")]

        public async Task<IActionResult> Delete(int id)
        {
            var wishlist = _unitOfWork._wishlistService.GetById(id);

            _unitOfWork._wishlistService.Delete(wishlist);

            _unitOfWork.Complete();

            return Ok(wishlist);

        }
        [HttpPut("AddWishlistProduct")]
        [Authorize(Roles = "Customer")]

        public async Task<IActionResult> AddWishProduct(AddWishlistProductDto dto)
        {
            var wishlist = _unitOfWork._wishlistService.GetById(dto.WishlistId);

            var product = await _unitOfWork._productService.GetById(dto.ProductId);

            if (product == null || wishlist == null)
            {
                return BadRequest();
            }
           

            var WishlistProduct = new WishlistProduct
            {
                Id = 0 ,
                whishlist = wishlist, 
                product = product , 
                TotalPrice = dto.quantity * product.price
                
            };
            wishlist.TotalPrice += WishlistProduct.TotalPrice;
            await _unitOfWork._wishlistProductService.Add(WishlistProduct);
            _unitOfWork._wishlistService.Update(wishlist);  
            _unitOfWork.Complete();
            return Ok(WishlistProduct);
        }
        [HttpPut("DeleteWishlistProduct")]
        [Authorize(Roles = "Customer")]

        public async Task<IActionResult> DeleteWishlistProduct(int id)
        {
            var wishlistproduct = _unitOfWork._wishlistProductService.GetById(id);

            if (wishlistproduct == null) { return BadRequest("There Is No WishlistProduct By This Id"); }
            wishlistproduct.whishlist.TotalPrice -= wishlistproduct.TotalPrice;

            _unitOfWork._wishlistService.Update(wishlistproduct.whishlist);

            _unitOfWork._wishlistProductService.Delete(wishlistproduct);

            _unitOfWork.Complete();

            return Ok(wishlistproduct);
        }

        [HttpGet("Total_Price")]
        [Authorize(Roles  = "Customer")]

        public async Task<IActionResult>GetPrice(int id)
        {
            var wishlist = _unitOfWork._wishlistService.GetById(id);    

            if(wishlist == null)
            {
                return BadRequest();
            }
            

            return Ok($"The Total Price Of The Wishlist Is {wishlist.TotalPrice}");
        }

    }
}
