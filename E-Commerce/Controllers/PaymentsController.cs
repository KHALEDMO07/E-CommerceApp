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
    public class PaymentsController(IUnitOfWork _unitOfWork) : ControllerBase
    {
        [HttpPost("Create")]
        [Authorize(Roles = "Customer")]

        public async Task<IActionResult> Create(CreatePaymentDto dto)
        {
            var userId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var user = _unitOfWork._userService.GetById(userId);

            decimal paidAmount = 0;

            var lst = user.orders;


            foreach (var item in lst)
            {
                paidAmount += item.TotalPrice;
            }

            var payment = new Payment
            {
                Id = 0,
                UserId = user.Id,
                PaymentMethod = dto.PaymentMethod,
                PaidAmount = paidAmount,
                CreatedOn = DateTime.Now,
            };


            await _unitOfWork._paymentService.AddAsync(payment);
            _unitOfWork.Complete();

            foreach (var item in lst)
            {
                item.PaymentId = payment.Id;

                _unitOfWork._orderService.Update(item);
            }

            _unitOfWork.Complete();
            return Ok(payment);

        }
        [HttpPut("Update")]
        [Authorize(Roles = "Customer")]

        public async Task<IActionResult> Update(UpdatePaymentDto dto)
        {
            var userId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var user = _unitOfWork._userService.GetById(userId);

            var payment = _unitOfWork._paymentService.GetById(dto.PaymentId);

            payment.PaymentMethod = dto.PaymentMethod;

            decimal paidAmount = 0;

            var lst = user.orders;

            foreach (var item in lst)
            {
                item.PaymentId = payment.Id;
                _unitOfWork._orderService.Update(item);

                paidAmount += item.TotalPrice;
            }

            payment.PaidAmount = paidAmount;

            _unitOfWork._paymentService.Update(payment);
            _unitOfWork.Complete();

            return Ok();

        }

        [HttpGet("GetAll")]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> GetAllAsync()
        {
            var payments = _unitOfWork._paymentService.GetAll();    

            if( payments == null ) { return NotFound("There Is No Payments"); }

            return Ok(payments);    
        }

        [HttpGet("{id}")]
        [Authorize]

        public async Task<IActionResult> GetById(int id)
        {
            var payment = _unitOfWork._paymentService.GetById(id);   

            if( payment == null ) { return NotFound("There Is No Payment By This Id"); }

            return Ok(payment); 
        }

        [HttpDelete("Delete")]
        [Authorize(Roles  = "Customer")]

        public async Task<IActionResult> Delete(int paymentId)
        {
            var payment = _unitOfWork._paymentService.GetById(paymentId); 

            if( payment == null ) { return NotFound(); }

            _unitOfWork._paymentService.Delete(payment);
            _unitOfWork.Complete();

            return Ok(payment);
        }

    }
}
