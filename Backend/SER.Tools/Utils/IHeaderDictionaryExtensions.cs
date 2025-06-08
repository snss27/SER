using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;

namespace SER.Tools.Utils;
public static class IHeaderDictionaryExtensions
{
	public static Boolean TryGetValue(this IHeaderDictionary headers, String headerName, [NotNullWhen(true)] out String? value)
	{
		value = null;

		if (headers.TryGetValue(headerName, out StringValues stringValues))
		{
			value = stringValues.ToString();
			return true;
		}

		return false;
	}
}
