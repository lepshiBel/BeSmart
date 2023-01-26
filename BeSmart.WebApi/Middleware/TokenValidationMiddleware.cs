using BeSmart.Application;
using BeSmart.Application.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace BeSmart.WebApi.Middleware
{
    public class TokenValidationMiddleware
    {
        private readonly RequestDelegate next;
        public TokenValidationMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context, IUserService userService)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (token != null)
            {
                await ValidateAndAttach(context, token, userService);
            }

            await next(context);
        }

        private async Task ValidateAndAttach(HttpContext context, string token, IUserService userService)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = SomeOptions.GenerateBytes();
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidIssuer = SomeOptions.Issuer,
                    ValidAudience = SomeOptions.Issuer,
                    ClockSkew = TimeSpan.Zero,
                    ValidateIssuerSigningKey = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                }, out SecurityToken validated);

                var jwtToken = (JwtSecurityToken)validated;

                var id = int.Parse(jwtToken.Claims.First(x => x.Type == "id").Value);

                context.Items["User"] = await userService.FindUserByIdAsync(id);
            }
            catch { }
        }
    }
}
