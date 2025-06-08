using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace SER.Domain.JwtTokens;
public class JwtProvider()
{
	public String GenerateToken()
	{
		var signingCredentials = new SigningCredentials(
			new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtOptions.Key)),
			SecurityAlgorithms.HmacSha256
		);

		var token = new JwtSecurityToken(
			signingCredentials: signingCredentials,
			expires: DateTime.UtcNow.AddHours(JwtOptions.ExpiryHours)
		);

		var tokenValue = new JwtSecurityTokenHandler().WriteToken(token);

		return tokenValue;
	}
}
