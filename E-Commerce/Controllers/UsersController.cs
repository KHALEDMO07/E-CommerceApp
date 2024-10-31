using E_Commerce.Action_Filters;
using E_Commerce.DTOs;
using E_Commerce.Email_Seneding;
using E_Commerce.Interfaces;
using E_Commerce.Models;
using E_Commerce.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace E_Commerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController( IUnitOfWork _unitOfWork , Jwt JwtOptions  , 
        ITokenBlacklist _tokenblacklist ,IEmailSender _emailSender
        , E_Commerce.Interfaces.IAuthenticationService _authenticationService) : ControllerBase
    {
        [HttpPost("RegisterUser")]
        [ValidationActionFilter]
        public async Task<IActionResult> Register(RegisterUserDto user)
        {
            Random random = new Random();
            var verificationCode = random.Next(100000, 999999).ToString();
            var User = new User { 
                Id = 0,
                UserName = user.UserName ,
                Email = user.Email ,    
                Password = user.Password ,  
                FullName = user.FullName ,
                Role = user.Role ,
                IsActive = false , 
                verifyCode = verificationCode,

            };
            await _unitOfWork._userService.Add(User);
            _unitOfWork.Complete();
            var address = new UserAddress
            {
                Id = 0,
                UserId = User.Id,
                Country = user.Country,
                City = user.City,
                Region = user.Region,
                user = User
            };

            User.address = address;
            _unitOfWork.Complete();

            var toEmail = user.Email;
            var subject = "Account Confirmation!";
            var message = "Hello! , Dear " + user.FullName +"\n" +
                "We Recived Your Request To Join Our E-Commerce App and now Your Turn To Active Your Account" + "\n"
                +"Your Verfication Code is " + verificationCode;

            await _emailSender.SendEmailAsync(subject, toEmail, message);

            return Ok(user);
        }

        [HttpPut("Active Account")]
        [ValidationActionFilter]
        public async Task<IActionResult> Active(ActiveAccountDto activeAccount)
        {
            var user = _unitOfWork._userService.
                GetById(activeAccount.userId);

            if(user == null) { return NotFound();  }

            if(user.verifyCode != activeAccount.VerifyCode)
            {
                return BadRequest("Wrong Verfication Code");
            }
            user.IsActive = true; 
            _unitOfWork._userService.Update(user);
            _unitOfWork.Complete();
            return Ok();
        }

        [HttpPut("Resend Code")]
        [ValidationActionFilter]
        public async Task<IActionResult> Resend(ResendCodeDto dto)
        {
            var user = _unitOfWork._userService.
              GetById(dto.userId);

            if (user == null) { return NotFound(); }

            var random = new Random();

            var verificationCode = random.Next(100000, 999999).ToString();

            var toEmail = user.Email;
            var subject = "Account Confirmation!";
            var message = "Hello! , Dear " + user.FullName + "\n" +
                "We Recived Your Request To Join Our E-Commerce App and now Your Turn To Active Your Account" + "\n"
                + "Your Verfication Code is " + verificationCode;

            user.verifyCode = verificationCode;
            _unitOfWork._userService.Update(user);
            _unitOfWork.Complete();

            await _emailSender.SendEmailAsync(subject, toEmail, message);

            return Ok();

        }

        [HttpPut("Update")]
        [Authorize]
        [ValidationActionFilter]
        public async Task<IActionResult> Update(UpdateUserDataDto dto)
        {
            var user = _unitOfWork._userService.
                GetById(Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier)));

            if (user == null) { return NotFound(); }

            user.FullName = dto.FullName;
            user.Role = dto.Role;
            user.address.City = dto.City;
            user.address.Country = dto.Country;
            user.address.Region = dto.Region;
            user.Email = dto.Email;

            _unitOfWork._userService.Update(user);
            _unitOfWork.Complete();
            return Ok(user);
        }
        [HttpPut("Update Username")]
        [Authorize]
        [ValidationActionFilter]

        public async Task<IActionResult> UpdateUsername(UpdateUserNameDto dto)
        {
            var user = _unitOfWork._userService.
               GetById(Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier)));

            if (user == null) { return NotFound(); }

            if (_unitOfWork._userService.isExisted(dto.UserName))
            {
                return BadRequest("Someone uses this username");
            }
            user.UserName = dto.UserName;
            _unitOfWork.Complete();
            return Ok(user);
        }

        [HttpPut("Update Password")]
        [Authorize]
        [ValidationActionFilter]    
        public async Task<IActionResult>UpdatePassword(UpdatePasswordDto dto)
        {
            var user = _unitOfWork._userService.
              GetById(Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier)));

            if (user == null) { return NotFound(); } 

            if(dto.newPassword != dto.ConfirmedPassword)
            {
                return BadRequest("Confirm password again!");
            }
            user.Password = dto.newPassword;
            _unitOfWork._userService.Update(user);
            _unitOfWork.Complete();
            return Ok(user);    
        }
        //Login//

        [HttpGet("Login")]
        [ValidationActionFilter]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var User = _unitOfWork._userService.GetByCredentials(dto); 
            

            if(User == null)
            {
                return Unauthorized("UserName Or Password Is Wrong");
            }

            var parameters = new CreateJwtTokenParametersDto
            {
                Id = User.Id,
                FullName = User.UserName,
                Role = User.Role
            };

            var accessToken = await _authenticationService.CreateJwtToken(parameters);
            var refreshToken =await  _authenticationService.GenerateRefreshToken();

            User.RefreshToken = refreshToken;
            User.RefreshTokenExpiry = DateTime.UtcNow.AddDays(7);
            _unitOfWork._userService.Update(User);
            _unitOfWork.Complete();

            var response = new LoginResponseDto
            {
                accessToken = new JwtSecurityTokenHandler().WriteToken(accessToken),
                AccessTokenExpiration = accessToken.ValidTo, 
                RefreshToken = refreshToken
            };
            

            return Ok(response);
        }

        [HttpGet("Refresh")]
        public async Task<IActionResult> Refresh(RefreshModelDto dto)
        {
            if (!ModelState.IsValid) return BadRequest();

            var principal = await _authenticationService.GetPrincipleFromExpiredToken(dto.accessToken);

            if (principal?.Identity?.Name is null)
                return Unauthorized();

            var userId = Convert.ToInt32(principal.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            var user = _unitOfWork._userService.GetById(userId);

            if (user is null || dto.refreshToken != user.RefreshToken || user.RefreshTokenExpiry < DateTime.UtcNow)
            {
                return Unauthorized();
            }

            var parameters = new CreateJwtTokenParametersDto
            {
                Id = userId,
                FullName = user.FullName,
                Role = user.Role
            };
            var token = await _authenticationService.CreateJwtToken(parameters);

            var response = new LoginResponseDto
            {
                accessToken = new JwtSecurityTokenHandler().WriteToken(token),
                AccessTokenExpiration = token.ValidTo,
                RefreshToken = dto.refreshToken
            };
            return Ok(response);
        }
        [HttpDelete("Revoke")]
        [Authorize]
        public async Task<IActionResult> Revoke()
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId is null) return Unauthorized();

            var user = _unitOfWork._userService.GetById(Convert.ToInt32(userId));

            if (user is null) return Unauthorized();

            user.RefreshToken = null;

            _unitOfWork._userService.Update(user);
            _unitOfWork.Complete();

            return Ok();
        }

        [HttpPost("Logout")]
        [Authorize]

        public async Task<IActionResult> Logout()
        {
            var token = Request.Headers["Authorization"].ToString().Replace("Bearer", "");

            if (string.IsNullOrEmpty(token))
            {
                return BadRequest("Token Missed");
            }

             _tokenblacklist.AddTokenToBlacklistAsync(token);

            return Ok("Logged Out Successfully");
        }

        [HttpGet("GetAll")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAll()
        {
            var users = _unitOfWork._userService.GetAll();

            if (users == null) { return NotFound("There is no users"); }

            return Ok(users);
        }
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> GetBydId(int id)
        {
            var user = _unitOfWork._userService.GetById(id);

            if (user == null) { return NotFound(); }

            return Ok(user);
        }
        [HttpDelete("DeleteAccount")]
        [Authorize]
        [ValidationActionFilter]
        public async Task<IActionResult> Delete(LoginDto dto)
        {
            var token = Request.Headers["Authorization"].ToString().Replace("Bearer", "");
            if (_tokenblacklist.IsTokenBlacklistedAsync(token))
            {
                return BadRequest("Login");
            }
            var user = _unitOfWork._userService.GetByCredentials(dto); 

            _unitOfWork._userService.Delete(user);

            _unitOfWork.Complete();

            return Ok("Deleted!");
        }
       
    }
}
