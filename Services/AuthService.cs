using factoring1.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

public class AuthService
{
    private readonly IConfiguration _configuration;

    public AuthService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string GenerateJwtToken(Individu adherent)
    {
        if (adherent == null)
            throw new ArgumentNullException(nameof(adherent));

        var jwtSettings = _configuration.GetSection("JwtSettings");
        var key = jwtSettings["SecretKey"];
        var issuer = jwtSettings["Issuer"];
        var audience = jwtSettings["Audience"];
        var expiryInMinutes = jwtSettings["ExpiryInMinutes"];

        if (string.IsNullOrEmpty(key) || key.Length < 32 || string.IsNullOrEmpty(issuer) || string.IsNullOrEmpty(audience) || string.IsNullOrEmpty(expiryInMinutes))
            throw new InvalidOperationException("JWT settings are not properly configured.");

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, adherent.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()), // Unique identifier for the token
            new Claim("id", adherent.IndividuId.ToString()),
            new Claim("role", "Adherent")
        };

        var symmetricKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
        var creds = new SigningCredentials(symmetricKey, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            expires: DateTime.Now.AddMinutes(Convert.ToDouble(expiryInMinutes)),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}