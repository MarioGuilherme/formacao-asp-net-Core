using DevFreela.Core.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace DevFreela.Infrastructure.Auth;

public class AuthService : IAuthService {
    private readonly IConfiguration _configuration;

    public AuthService(IConfiguration configuration) {
        this._configuration = configuration;
    }

    public string GenerateJwtToken(string email, string role) {
        var issuer = this._configuration["Jwt:Issuer"];
        var audience = this._configuration["Jwt:Audience"];
        var key = this._configuration["Jwt:Key"];

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim> {
            new("userName", email),
            new(ClaimTypes.Role, role),
        };

        var token = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            expires: DateTime.Now.AddHours(8),
            signingCredentials: credentials,
            claims: claims
        );

        var tokenHandler = new JwtSecurityTokenHandler();

        var stringToken = tokenHandler.WriteToken(token);

        return stringToken;
    }

    public string ComputeSha256Hash(string rawPassword) {
        using (SHA256 sha256Hash = SHA256.Create()) {
            // ComputeHash - retorna byte array
            byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawPassword));

            // Converte byte array para string
            StringBuilder builder = new StringBuilder();

            for (int i = 0; i < bytes.Length; i++) {
                builder.Append(bytes[i].ToString("x2")); // x2 faz com que seja convertido em representação hexadecimal
            }

            return builder.ToString();
        }
    }
}