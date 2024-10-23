using SER.Tools.Types.Catalog;
using System.Text.Json.Serialization;

namespace SER.Tools.Types.Pages;

public sealed class Page
{
	public static readonly Page Empty = new();
	public static Page<T> Create<T>(Catalog<T> values, Int64 totalRows) => new(values, totalRows);

	private Page() { }
}

public readonly struct Page<T>
{
	public static readonly Page<T> Empty;

	public Catalog<T> Values { get; }
	public readonly Int64 TotalRows { get; }

	public Int32 Count => Values.Count;
	public Boolean IsEmpty => Values.IsEmpty;

	[JsonConstructor]
	public Page(Catalog<T> values, Int64 totalRows)
	{
		Values = values;
		TotalRows = totalRows;
	}

	public Page<R> Convert<R>(Func<T, R> converter)
	{
		if (IsEmpty) return Page<R>.Empty;

		return new(Values.Select(converter).ToList(), TotalRows);
	}

	public override String ToString() => Count + "/" + TotalRows;

	public static implicit operator Page<T>(Page _) => Empty;
}