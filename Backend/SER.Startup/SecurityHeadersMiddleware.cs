using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;

namespace SER.Startup;
internal sealed class SecurityHeadersMiddleware
{
	private readonly RequestDelegate _next;

	public SecurityHeadersMiddleware(RequestDelegate next)
	{
		_next = next;
	}

	public Task Invoke(HttpContext context)
	{
		// https://developer.mozilla.org/en-US/docs/Web/HTTP/Headers/Referrer-Policy
		context.Response.Headers.Add("referrer-policy", new StringValues("strict-origin-when-cross-origin"));

		// https://developer.mozilla.org/en-US/docs/Web/HTTP/Headers/X-Content-Type-Options
		context.Response.Headers.Add("x-content-type-options", new StringValues("nosniff"));

		// https://developer.mozilla.org/en-US/docs/Web/HTTP/Headers/X-Frame-Options
		context.Response.Headers.Add("x-frame-options", new StringValues("DENY"));

		// https://security.stackexchange.com/questions/166024/does-the-x-permitted-cross-domain-policies-header-have-any-benefit-for-my-websit
		context.Response.Headers.Add("X-Permitted-Cross-Domain-Policies", new StringValues("none"));

		// https://developer.mozilla.org/en-US/docs/Web/HTTP/Headers/X-XSS-Protection
		context.Response.Headers.Add("x-xss-protection", new StringValues("1; mode=block"));

		// TASK для Daemon'а нужно сделать доменное имя и тогда можно будет вернуть CSP
		//// https://developer.mozilla.org/en-US/docs/Web/HTTP/CSP
		//// https://developer.mozilla.org/en-US/docs/Web/HTTP/Headers/Content-Security-Policy
		//context.Response.Headers.Add("Content-Security-Policy", new StringValues(
		//    $"default-src 'self' 'unsafe-inline' 'unsafe-eval' {httpOrigin} {httpsOrigin} blob: data: " +
		//    $"https://widget.cloudpayments.ru " +
		//    $"https://www.google-analytics.com " +
		//    $"https://mc.yandex.ru " +
		//    $"http://localhost:8888;"
		//));

		return _next(context);
	}
}
