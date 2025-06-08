using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace SER.Tools.Types.BadRequestExceptions;
public class BadRequestException : Exception
{
	public BadRequestException(String message) : base(message) { }
}

public static class BadRequestExceptionExtensions
{
	public static T Required<T>([NotNull] this T? value, [CallerArgumentExpression("value")] String name = "")
		where T : class =>
		value ?? throw new BadRequestException(name.LastName() + " is required");

	public static T Required<T>([NotNull] this T? value, [CallerArgumentExpression("value")] String name = "")
		where T : struct =>
		value ?? throw new BadRequestException(name.LastName() + " is required");

	private static String LastName(this String name) => name.Split(".").Last();
}
