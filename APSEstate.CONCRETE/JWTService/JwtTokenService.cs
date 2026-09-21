using APSEstate.CORE.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace APSEstate.CONCRETE.JWTService
{
    public static class JwtTokenService
    {
        public static string GenerateToken(LoginResponseModel user,IConfiguration config)
        {
            try
            {
                int expireMinutes = int.Parse(config["Jwt:ExpireMinutes"]!);

                var key = Encoding.UTF8.GetBytes(config["Jwt:Key"]!);

                var tokenHandler = new JwtSecurityTokenHandler();

                var claims = new[]
                {
                    new Claim(
                        ClaimTypes.Name,
                        user.UserName ?? string.Empty),

                    new Claim(
                        ClaimTypes.Sid,
                        user.UserId.ToString()),

                    new Claim(
                        ClaimTypes.Email,
                        user.UserName ?? string.Empty),

                    new Claim(
                        ClaimTypes.Role,
                        user.UserTypeId.ToString()),

                    new Claim(
                        "UserId",
                        user.UserId.ToString()),

                    new Claim(
                        "UserTypeId",
                        user.UserTypeId.ToString()),

                    new Claim(
                        "UserTypeName",
                        user.UserTypeName ?? string.Empty),

                    new Claim(
                        "ReferenceId",
                        user.ReferenceId?.ToString() ?? string.Empty),

                    new Claim(
                        JwtRegisteredClaimNames.Jti,
                        Guid.NewGuid().ToString())
                };

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claims),

                    Expires = DateTime.UtcNow.AddMinutes(
                        expireMinutes),

                    SigningCredentials = new SigningCredentials(
                        new SymmetricSecurityKey(key),
                        SecurityAlgorithms.HmacSha256Signature),

                    Issuer = config["Jwt:Issuer"],
                    Audience = config["Jwt:Audience"]
                };

                var token =
                    tokenHandler.CreateToken(tokenDescriptor);

                return tokenHandler.WriteToken(token);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
