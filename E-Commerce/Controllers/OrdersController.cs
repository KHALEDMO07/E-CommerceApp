using E_Commerce.Interfaces;
using E_Commerce.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController(IUnitOfWork _unitOfWork) : ControllerBase
    {
        [HttpPost("Place Order")]
        [Authorize(Roles = "Customer")]
        ///The Gets The Id of the Cart and Create Order and convert all the CartProducts 
        ///into OrderItems
        public async Task<IActionResult> PlaceOrder(int cartId)
        {
            var cart = _unitOfWork._cartService.GetById(cartId);    

            if (cart == null) { return BadRequest(); }

            var order = new Order
            {
                Id = 0,
                PlacedOn = DateTime.Now,
                TotalPrice = cart.TotalPrice,
                UserId = cart.UserId,
                IsConfirmed = false
            };
            await _unitOfWork._orderService.AddAsync(order);
            _unitOfWork.Complete();
            var lst = cart.CartProducts; 
            foreach(var item in lst)
            {
                var orderItem = new OrderItem
                {
                    Id = 0,
                    ProductId = item.ProductId,
                    price = item.TotalPrice,
                    OrderId = order.Id
                    

                }; 
                await _unitOfWork._orderItemService.AddAsync(orderItem);
            }
            _unitOfWork.Complete();
            return Ok(order);
        }

        [HttpPut("Confirm Order")]
        [Authorize(Roles = "Customer")]

        public async Task<IActionResult> Confirm(int orderId)
        {
            var order = _unitOfWork._orderService.GetById(orderId);

            if (order == null) { return BadRequest(); }

            order.IsConfirmed = true;

            _unitOfWork._orderService.Update(order);
            _unitOfWork.Complete();

            return Ok(order);
        }

        [HttpGet("{orderId}")]
        [Authorize]

        public async Task<IActionResult> GetById(int orderId)
        {
            var order = _unitOfWork._orderService.GetById(orderId);

            if (order == null) { return BadRequest("There Is No Order By This Id"); } 

            return Ok(order);
        }
        [HttpGet("GetAll")]
        [Authorize]
        public async Task<IActionResult> GetAll()
        {
            var orders = _unitOfWork._orderService.GetAll(); 

            if(orders == null) { return BadRequest("There Is No Orders"); }

            return Ok(orders);
        }
        [HttpDelete("Delete Order")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult>Delete(int orderId)
        {
            var order = _unitOfWork._orderService.GetById(orderId); 

            if (order == null) { return BadRequest(); } 

            _unitOfWork._orderService.Delete(order);    

            _unitOfWork.Complete();

            return Ok(order);
        }

        
    }
}
