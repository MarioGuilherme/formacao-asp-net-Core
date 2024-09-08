using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace DevFreela.Infrastructure.Auth;

public class AuthService(IConfiguration configuration) : IAuthService {
    private readonly IConfiguration _configuration = configuration;

    public string GenerateJwtToken(string email, string role) {
        string issuer = this._configuration["Jwt:Issuer"];
        string audience = this._configuration["Jwt:Audience"];
        string key = this._configuration["Jwt:Key"];

        SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(key));
        SigningCredentials credentials = new(securityKey, SecurityAlgorithms.HmacSha256);

        List<Claim> claims = [
            new("userName", email),
            new(ClaimTypes.Role, role),
        ];

        JwtSecurityToken token = new(
            issuer: issuer,
            audience: audience,
            expires: DateTime.Now.AddHours(8),
            signingCredentials: credentials,
            claims: claims
        );

        JwtSecurityTokenHandler tokenHandler = new();

        string stringToken = tokenHandler.WriteToken(token);

        return stringToken;
    }

    public string ComputeSha256Hash(string rawPassword) {
        // ComputeHash - retorna byte array
        byte[] bytes = SHA256.HashData(Encoding.UTF8.GetBytes(rawPassword));

        // Converte byte array para string
        StringBuilder builder = new();

        for (int i = 0; i < bytes.Length; i++)
            builder.Append(bytes[i].ToString("x2")); // x2 faz com que seja convertido em representação hexadecimal

        return builder.ToString();
    }
}