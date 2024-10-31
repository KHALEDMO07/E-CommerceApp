using E_Commerce.Action_Filters;
using E_Commerce.DTOs;
using E_Commerce.Interfaces;
using E_Commerce.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace E_Commerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShipmentPricesController(IUnitOfWork _unitOfWork) : ControllerBase
    {
        [HttpPost("Create")]
        [Authorize(Roles = "Admin")]
        [ValidationActionFilter]
        public async Task<IActionResult> Create(ShipmentPrice shipmentPrice)
        {
            await _unitOfWork._shipmentPriceService.AddAsync(shipmentPrice);

            _unitOfWork.Complete(); 

            return Ok(shipmentPrice);   
        }

        [HttpPut("Update")]
        [Authorize(Roles = "Admin")]
        [ValidationActionFilter]
        public async Task<IActionResult>Update(ShipmentPrice shipmentPrice)
        {
            _unitOfWork._shipmentPriceService.Update(shipmentPrice);
            _unitOfWork.Complete();

            return Ok(shipmentPrice);
        }
        [HttpGet("GetAll")]
        [Authorize(Roles = "Admin")]
        [ValidationActionFilter]    
        public async Task<IActionResult> Get()
        {
            var res = _unitOfWork._shipmentPriceService.GetAll();

            if(res == null) { return BadRequest("There is no prices"); }

            return Ok(res);
        }

        [HttpGet("GetOne")]
        [Authorize(Roles = "Admin")]
        [ValidationActionFilter]

        public async Task<IActionResult> GetOne(AddressDto dto)
        {
            var shipment = _unitOfWork._shipmentPriceService.GetOne(dto); 

            if(shipment == null) { return BadRequest("There Is No Pricing For this Address"); }

            return Ok(shipment);
        }

        [HttpDelete("Delete")]
        [Authorize(Roles = "Admin")]
        [ValidationActionFilter]
        public async Task<IActionResult> Delete(ShipmentPrice shipmentPrice)
        {
            _unitOfWork._shipmentPriceService.Delete(shipmentPrice);
            _unitOfWork.Complete();

            return Ok(shipmentPrice);
        }
    }
}
