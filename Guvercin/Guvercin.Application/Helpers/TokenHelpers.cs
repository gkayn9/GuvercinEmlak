using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Guvercin.Application.Dtos.AuthDtos;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace Guvercin.Application.Helpers;

public class TokenHelpers
{
    private readonly IConfiguration _configuration;

    public TokenHelpers(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public  string GenerateToken(TokenDto dto)
    {
        //Appsettings.json'dan JWT ayarlarını alıyoruz
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        
        //Token İçine eklenecek claim'ler
        var claims = new List<Claim>
        {
            new Claim("email", dto.Email),
            new Claim("user",dto.Id),
            new Claim("role", dto.Role),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        };
        //Token oluşturma işlemi
        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(30),
            signingCredentials: credentials
        );
        
        var resultToken= new JwtSecurityTokenHandler().WriteToken(token);
        return resultToken;
    }
}