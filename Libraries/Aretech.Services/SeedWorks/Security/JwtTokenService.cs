using Aretech.Domain.Accounts;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Aretech.Services.SeedWorks.Security
{
	public class JwtTokenService : ITokenService
	{
		private readonly IConfiguration _configuration;
		public JwtTokenService(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		public string GenerateToken(Account account)
		{
			var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);
			var issuer = _configuration["Jwt:Issuer"];
			var audience = _configuration["Jwt:Audience"];

			var claims = new List<Claim>
		{
			new Claim(ClaimTypes.NameIdentifier, account.Id.ToString()),
			new Claim(ClaimTypes.Name, account.Username)
		};

			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(claims),
				Expires = DateTime.UtcNow.AddHours(Convert.ToInt32(_configuration["Jwt:ExpiresInMinutes"])),
				SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
				Issuer = issuer,
				Audience = audience
			};

			var tokenHandler = new JwtSecurityTokenHandler();
			var token = tokenHandler.CreateToken(tokenDescriptor);
			return tokenHandler.WriteToken(token);
		}

		public bool ValidateToken(string token)
		{
			var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);
			var tokenHandler = new JwtSecurityTokenHandler();

			try
			{
				tokenHandler.ValidateToken(token, new TokenValidationParameters
				{
					ValidateIssuerSigningKey = true,
					IssuerSigningKey = new SymmetricSecurityKey(key),
					ValidateIssuer = true,
					ValidateAudience = true,
					ValidIssuer = _configuration["Jwt:Issuer"],
					ValidAudience = _configuration["Jwt:Audience"],
					ClockSkew = TimeSpan.Zero,
					LifetimeValidator = (notBefore, expires, securityToken, validationParameters) =>
					{
						if (expires.HasValue)
						{
							return DateTime.Now < expires.Value.AddMinutes(Convert.ToInt32(_configuration["Jwt:ExpiresInMinutes"]));
						}
						return false;
					}
				}, out SecurityToken validatedToken);

				return validatedToken != null;
			}
			catch
			{
				return false;
			}
		}

		public string GenerateRefreshToken()
		{
			return Guid.NewGuid().ToString();
		}

		public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
		{
			var tokenValidationParameters = new TokenValidationParameters
			{
				ValidateAudience = false,
				ValidateIssuer = false,
				ValidateIssuerSigningKey = true,
				IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"])),
				ValidateLifetime = false // Süre dolmuş token'ları da kabul et
			};

			var tokenHandler = new JwtSecurityTokenHandler();
			var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var securityToken);

			if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
			{
				throw new SecurityTokenException("Geçersiz token.");
			}

			return principal;
		}
	}
}