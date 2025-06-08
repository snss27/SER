using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Controllers;

namespace SER.Tools.Utils;
public static class HttpContextExtensions
{
	public static Boolean TryGetHeader(this HttpContext context, String headerName, [NotNullWhen(true)] out String? value)
	{
		return context.Request.Headers.TryGetValue(headerName, out value);
	}

	public static Boolean EndpointHasAttribute<T>(this HttpContext context) where T : Attribute
	{
		Boolean? isEndpointHasAttribute = context.GetEndpoint()
			?.Metadata
			.GetMetadata<ControllerActionDescriptor>()
			?.MethodInfo
			.GetCustomAttributes(inherit: true)
			.OfType<T>()
			.Any();

		return isEndpointHasAttribute ?? false;
	}
}
