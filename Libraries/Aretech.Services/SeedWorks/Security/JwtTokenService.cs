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
			var key = Encoding.UTF8.GetBytes(_configuration["JwtSecret"]);
			var issuer = _configuration["JwtIssuer"];
			var audience = _configuration["JwtAudience"];

			var claims = new List<Claim>
		{
			new Claim(ClaimTypes.NameIdentifier, account.Id.ToString()),
			new Claim(ClaimTypes.Name, account.Username)
		};

			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(claims),
				Expires = DateTime.UtcNow.AddHours(Convert.ToInt32(_configuration["ExpiresInMinutes"])),
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
			var key = Encoding.UTF8.GetBytes(_configuration["JwtSecret"]);
			var tokenHandler = new JwtSecurityTokenHandler();

			try
			{
				tokenHandler.ValidateToken(token, new TokenValidationParameters
				{
					ValidateIssuerSigningKey = true,
					IssuerSigningKey = new SymmetricSecurityKey(key),
					ValidateIssuer = true,
					ValidateAudience = true,
					ValidIssuer = _configuration["JwtIssuer"],
					ValidAudience = _configuration["JwtAudience"],
					ClockSkew = TimeSpan.Zero,
					LifetimeValidator = (notBefore, expires, securityToken, validationParameters) =>
					{
						if (expires.HasValue)
						{
							return DateTime.Now < expires.Value.AddMinutes(Convert.ToInt32(_configuration["ExpiresInMinutes"]));
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
	}
}