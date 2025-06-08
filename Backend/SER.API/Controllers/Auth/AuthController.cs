using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Mvc;
using SER.Domain.Services;
using SER.Tools.Types.Results;

namespace SER.API.Controllers.Auth;

[Route("api/auth")]
public class AuthController(IUsersService usersService) : ControllerBase
{
	private String JwtTokenCookieName => Environment.GetEnvironmentVariable("COOCKIES_JWT_TOKEN_KEY") ?? throw new Exception("COOCKIES_JWT_TOKEN_KEY does not set");
	public record AuthRequest(String login, String password);

	[HttpPost("auth")]
	public async Task<OperationResult> Auth([FromBody] AuthRequest authRequest)
	{
		Result<String, Error> authResult = await usersService.Login(authRequest.login, authRequest.password);

		if (authResult.IsFailure) return OperationResult.Fail(authResult.Error);

		String jwtToken = authResult.Value;

		HttpContext.Response.Cookies.Append(JwtTokenCookieName, jwtToken);

		return OperationResult.Success();
	}
}
