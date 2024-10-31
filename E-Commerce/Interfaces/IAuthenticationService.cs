using E_Commerce.DTOs;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace E_Commerce.Interfaces
{
    public interface IAuthenticationService
    {
        Task<JwtSecurityToken> CreateJwtToken(CreateJwtTokenParametersDto dto);
        Task<string> GenerateRefreshToken();

        Task<ClaimsPrincipal?> GetPrincipleFromExpiredToken(string token);
    }
}
