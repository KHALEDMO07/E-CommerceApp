using E_Commerce.DTOs;
using E_Commerce.Enum;
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
    public class ShipmentsController(IUnitOfWork _unitOfWork) : ControllerBase
    {
        [HttpPost("Create")]
        [Authorize(Roles = "Customer")]

        public async Task<IActionResult> Create()
        {
            var user = _unitOfWork._userService.
                GetById(Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier)));

            if (user == null) { return BadRequest("The User Is not in The System"); }

            var address = new AddressDto
            {
                country = user.address.Country,
                city = user.address.City,
                region = user.address.Region
            };
            var shipmentPrice = _unitOfWork._shipmentPriceService.GetOne(address);

            var price = shipmentPrice == null ? 0 : shipmentPrice.price;

            var orders = user.orders;

            decimal total = price;

            foreach (var item in orders)
            {
                total += item.TotalPrice;
            }
            var shipment = new Shipment
            {
                Id = 0,
                ShipmentTime = DateTime.Now,
                Status = 0,
                UserId = user.Id,
                shipmentPrice = price,
                TotalPrice = total
            };
            await _unitOfWork._shipmentService.AddAsync(shipment);
            _unitOfWork.Complete();
            foreach (var item in orders)
            {
                item.ShipmentId = shipment.Id;
                _unitOfWork._orderService.Update(item);
            }
            _unitOfWork.Complete();
            return Ok(shipment);
        }
        [HttpPut("Update")]

        [Authorize(Roles = "Customer")]

        public async Task<IActionResult> Update(int shipmentId)
        {
            var user = _unitOfWork._userService.
               GetById(Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier)));

            if (user == null) { return BadRequest("The User Is not in The System"); }

            var shipment = _unitOfWork._shipmentService.GetShipmentById(shipmentId);

            if (shipment == null) { return BadRequest("There Is No shipment"); }

            var address = new AddressDto
            {
                country = user.address.Country,
                city = user.address.City,
                region = user.address.Region
            };
            var shipmentPrice = _unitOfWork._shipmentPriceService.GetOne(address);

            var price = shipmentPrice == null ? 0 : shipmentPrice.price;

            var orders = user.orders;

            decimal total = price;

            foreach (var item in orders)
            {
                total += item.TotalPrice;
                item.ShipmentId = shipmentId;
                _unitOfWork._orderService.Update(item);
            }
            shipment.TotalPrice = total;
            shipment.shipmentPrice = price;
            _unitOfWork._shipmentService.Update(shipment);
            _unitOfWork.Complete();
            return Ok(shipment);
        }
        [HttpPut("UpdateByAdmin")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateByAdmin(UpdateShipmentByAdmin dto)
        {
            var shipment = _unitOfWork._shipmentService.GetShipmentById(dto.shipmentId);

            if(shipment == null) { return BadRequest($"There Is No Shipment by Id #{dto.shipmentId}"); }

            shipment.Status = dto.status;

            if(dto.status == ShipmentStatus.Delivered)
            {
                shipment.DeliveredOn = DateTime.Now;
            }
            _unitOfWork._shipmentService.Update(shipment);
            _unitOfWork.Complete();
            return Ok(shipment);    
        }

        [HttpGet("GetAll")]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> GetAll()
        {
            var shipments = _unitOfWork._shipmentService.GetAll();

            if(shipments == null)
            {
                return BadRequest("There Is No Shipments");
            }
            return Ok(shipments);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> Get(int id)
        {
            var shipment = _unitOfWork._shipmentService.GetShipmentById(id);

            if(shipment == null)
            {
                return BadRequest("There Is No Shipment With This Id"); 
            }
            return Ok(shipment);
        }

        [HttpDelete("Delete")]
        [Authorize]
        public async Task<IActionResult>Delete(int id)
        {
            var shipment = _unitOfWork._shipmentService.GetShipmentById(id); 

            if(shipment == null)
            {
                return BadRequest("There Is No Shipments With This Id");
            }
            _unitOfWork._shipmentService.Delete(shipment);
            _unitOfWork.Complete(); 
            return Ok(shipment);
        }
    }
}
