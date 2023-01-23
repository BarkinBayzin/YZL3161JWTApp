using JWTAppBackOffice.Core.DTOs;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JWTAppBackOffice.Infrastructure.Tools
{
    public class JwtGenerator
    {
        public static JwtResponse GenerateToken(CheckUserResponseDto dto)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtSettings.Key));

            SigningCredentials credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            List<Claim> claims = new List<Claim>();

            // foreach (var item in new string[] { "", "" }) //birden fazla rol olsaydı bu şekilde geçerli olanları alıp, döngü ile ekleyebilirdik
            // claims.Add(new Claim(ClaimTypes.Role, dto.Role));

            claims.Add(new Claim(ClaimTypes.Role, dto.Role));
            claims.Add(new Claim(ClaimTypes.Name, dto.Username));
            claims.Add(new Claim(ClaimTypes.NameIdentifier, dto.Id.ToString()));

            var expireDate = DateTime.UtcNow.AddMinutes(JwtSettings.Expire);

            JwtSecurityToken token = new JwtSecurityToken
                (issuer:JwtSettings.Issuer,
                audience:JwtSettings.Audience,
                claims:claims,
                notBefore:DateTime.UtcNow,
                expires:expireDate,
                signingCredentials:credentials);

            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();

            return new JwtResponse(handler.WriteToken(token), expireDate);
        }   
    }
}
