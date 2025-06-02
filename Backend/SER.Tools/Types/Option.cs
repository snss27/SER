namespace SER.Tools.Types;

public struct Option<T> where T : notnull
{
	private readonly bool isSome;
	private readonly T value;

	private Option(T value)
	{
		this.value = value;
		isSome = true;
	}

	public static Option<T> Some(T value) => new Option<T>(value);

	public static Option<T> None => default;

	public bool IsSome(out T value)
	{
		value = this.value;
		return isSome;
	}
}

public static class OptionExtensions
{
	public static U Match<T, U>(this Option<T> option, Func<T, U> onIsSome, Func<U> onIsNone) where T : notnull =>
		option.IsSome(out var value) ? onIsSome(value) : onIsNone();

	public static Option<U> Map<T, U>(this Option<T> option, Func<T, U> mapper) where T : notnull
	{
		if (option.IsSome(out var value))
			return Option<U>.Some(mapper(value));
		return Option<U>.None;
	}

	public static Option<U> Bind<T, U>(this Option<T> option, Func<T, Option<U>> binder) where T : notnull
	{
		if (option.IsSome(out var value))
			return binder(value);
		return Option<U>.None;
	}
}
