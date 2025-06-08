using SER.Tools.Types.BadRequestExceptions;
using SER.Tools.Utils;

namespace SER.API.Infrastructure;

internal class ApiMiddleware
{
	private readonly RequestDelegate _next;

	public const String KeywordApiHeaderName = "KeywordApi";

	public ApiMiddleware(RequestDelegate next)
	{
		_next = next;
	}

	public async Task Invoke(HttpContext context)
	{
		ValidateKeyword(context);

		await _next.Invoke(context);
	}

	private static void ValidateKeyword(HttpContext context)
	{
		String keyword = Environment.GetEnvironmentVariable("API_KEYWORD") ?? throw new InvalidOperationException("API_KEYWORD not set.");

		Boolean containsApiKeyword = context.TryGetHeader(KeywordApiHeaderName, out String? requestKeyword);

		if (!containsApiKeyword || requestKeyword != keyword)
			BreakValidation(reason: "API key is invalid");
	}

	private static void BreakValidation(String reason)
	{
		throw new BadRequestException(message: reason);
	}
}
