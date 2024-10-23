using SER.Tools.Types.Pages;
using System.Collections;
using System.Collections.Immutable;

namespace SER.Tools.Types.Catalog;

public sealed class Catalog
{
	public static readonly Catalog Empty = new();
	public static Catalog<T> Create<T>(params T[] values) => new(values);
	public static Catalog<T> From<T>(IReadOnlyCollection<T>? values) => new(values);

	private Catalog()
	{ }
}

public readonly struct Catalog<T> : IReadOnlyCollection<T>
{
	public static readonly Catalog<T> Empty;

	private readonly IReadOnlyCollection<T>? _values;

	public Boolean IsEmpty => _values is null || _values.Count == 0;
	public IReadOnlyCollection<T> Values => _values ?? Array.Empty<T>();
	public Int32 Count => _values?.Count ?? 0;

	public Catalog(IReadOnlyCollection<T>? values)
	{
		if (values is Catalog<T> catalog)
		{
			_values = catalog._values;
		}
		else
		{
			_values = values;
		}
	}

	public Int32 IndexOf(T item)
	{
		const Int32 NotFound = -1;
		if (_values is null) return NotFound;

		using IEnumerator<T> enumerator = _values.GetEnumerator();

		for (Int32 i = 0; enumerator.MoveNext(); i++)
		{
			if (EqualityComparer<T>.Default.Equals(enumerator.Current, item)) return i;
		}

		return NotFound;
	}

	public T FindBy<R>(Func<T, R?> tryGet, R? item)
	{
		if (_values is null || item is null) return default!;

		return _values.FirstOrDefault(value => EqualityComparer<R>.Default.Equals(tryGet(value), item))!;
	}

	public Catalog<R> SelectDistinct<R>(Func<T, R?> tryGet)
	{
		if (_values is null) return Catalog.Empty;

		HashSet<R> items = new HashSet<R>();

		foreach (T value in _values)
		{
			if (tryGet(value) is R item) items.Add(item);
		}

		return items;
	}

	public Catalog<R> SelectManyDistinct<R>(Func<T, Catalog<R>> tryGet)
	{
		if (_values is null) return Catalog.Empty;

		HashSet<R> items = new();

		foreach (T value in _values)
			foreach (R item in tryGet(value))
			{
				if (item is not null) items.Add(item);
			}

		return items;
	}

	public Catalog<(T item, Int32 index)> WithIndex()
	{
		return Values.Select((item, index) => (item, index)).ToList();
	}

	public Catalog<R> Convert<R>(Func<T, R> converter)
	{
		if (_values is null || _values.Count == 0) return Catalog<R>.Empty;

		return _values.Select(converter).ToList();
	}

	public override String ToString() => _values?.Count switch
	{
		null => "Empty (null)",
		0 => "Empty",
		1 => "1 item",
		_ => $"{_values.Count} items",
	};

	public IEnumerator<T> GetEnumerator() => (_values ?? Enumerable.Empty<T>()).GetEnumerator();
	IEnumerator IEnumerable.GetEnumerator() => (_values ?? Enumerable.Empty<T>()).GetEnumerator();

	public static implicit operator List<T>(Catalog<T> catalog) => catalog.Values.ToList();
	public static implicit operator Catalog<T>(List<T> values) => new(values);

	public static implicit operator ImmutableArray<T>(Catalog<T> catalog) => catalog.Values.ToImmutableArray();
	public static implicit operator Catalog<T>(ImmutableArray<T> values) => new(values);

	public static implicit operator HashSet<T>(Catalog<T> catalog) => catalog.Values.ToHashSet();
	public static implicit operator Catalog<T>(HashSet<T> values) => new(values);

	public static implicit operator ImmutableHashSet<T>(Catalog<T> catalog) => catalog.Values.ToImmutableHashSet();
	public static implicit operator Catalog<T>(ImmutableHashSet<T> values) => new(values);

	public static implicit operator SortedSet<T>(Catalog<T> catalog) => new(catalog.Values);
	public static implicit operator Catalog<T>(SortedSet<T> values) => new(values);

	public static implicit operator T[](Catalog<T> catalog) => catalog.Values.ToArray();
	public static implicit operator Catalog<T>(T[] values) => new(values);

	public static implicit operator Catalog<T>(Page<T> result) => new(result.Values);
	public static implicit operator Catalog<T>(Catalog _) => Empty;
}