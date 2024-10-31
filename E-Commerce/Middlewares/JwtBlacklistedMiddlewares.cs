using E_Commerce.Interfaces;

namespace E_Commerce.Middlewares
{
    public class JwtBlacklistedMiddlewares
    {
        private readonly RequestDelegate _next;

        private readonly ITokenBlacklist _tokenBlacklist;

        public JwtBlacklistedMiddlewares(RequestDelegate next , ITokenBlacklist tokenBlacklist)
        {
            _next = next;
            _tokenBlacklist = tokenBlacklist;
        }
        public async Task InvokeAsync(HttpContext _context)
        {
            var token = _context.Request.Headers["Authorization"].ToString().Replace("Bearer", ""); 

            if(!string.IsNullOrEmpty(token) )
            {
                if ( _tokenBlacklist.IsTokenBlacklistedAsync(token))
                {
                    _context.Response.StatusCode = StatusCodes.Status401Unauthorized;

                    await _context.Response.WriteAsync("Login Please");

                    return;
                }
            }

            await _next(_context);
        }
    }
}
