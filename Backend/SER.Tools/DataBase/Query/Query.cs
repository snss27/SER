using SER.Tools.Converters;
using System.Collections;
using System.Data;
using System.Data.Common;
using System.Text.RegularExpressions;

namespace SER.Tools.DataBase.Query;

public abstract class Query<C> : Query where C : DbCommand, new()
{
	protected readonly C _command = new();

	public Boolean Preparable { get; private set; }

	protected Query(String template, CommandType commandType, Int32? timeout, Boolean preparable, IJsonSerializer jsonSerializer) : base(jsonSerializer)
	{
		if (String.IsNullOrWhiteSpace(template)) throw new ArgumentNullException(nameof(template));

		_command.CommandText = template;
		_command.CommandType = commandType;
		Preparable = preparable;

		if (timeout is not null)
		{
			_command.CommandTimeout = timeout.Value;
		}
	}

	public override Query Replace(String argument, String value)
	{
		_command.CommandText = _command.CommandText.Replace(argument, value, StringComparison.Ordinal);
		Preparable = false;

		return this;
	}

	public C Build() => _command;
}

public abstract partial class Query
{
	protected const Boolean Nullable = true;
	protected const Boolean NotNullable = false;
	protected static readonly Regex SpaceRemover = new("[\t\r\n ]+", RegexOptions.Compiled, TimeSpan.FromSeconds(3));

	protected readonly IJsonSerializer _jsonSerializer;

	protected Query(IJsonSerializer jsonSerializer)
	{
		_jsonSerializer = jsonSerializer;
	}

	protected virtual String DisplayValue(Object? value, Boolean reduce) => value switch
	{
		null => "NULL",
		DBNull _ => "NULL",
		true => "true",
		false => "false",

		Byte[] id when reduce && id.Length > 40 => $"'{BitConverter.ToString(id.Take(40).ToArray()).Replace("-", String.Empty, StringComparison.Ordinal)}'",
		Byte[] id => $"'{BitConverter.ToString(id).Replace("-", String.Empty, StringComparison.Ordinal)}'",

		String longStr when reduce && longStr.Length > 15 => $"'{longStr.Substring(0, 15)}…'",
		String str => $"'{str}'",

		TimeSpan time => $"'{time:HH:mm:ss.ffff}'",
		TimeOnly time => $"'{time:HH:mm:ss.ffff}'",
		DateOnly date => $"'{date:yyyy-MM-dd}'",
		DateTime date when date.TimeOfDay == TimeSpan.Zero => $"'{date:yyyy-MM-dd}'",
		DateTime dateTime when dateTime.Millisecond == 0 => $"'{dateTime:yyyy-MM-dd HH:mm:ss}'",
		DateTime fullDateTime => $"'{fullDateTime:yyyy-MM-dd HH:mm:ss.ffff}'",

		ICollection collection when reduce && collection.Count > 5 => $"[{String.Join(", ", collection.Cast<Object>().Take(5).Select(v => DisplayValue(v, reduce)))}, …]",
		IEnumerable array => $"[{String.Join(", ", array.Cast<Object>().Select(v => DisplayValue(v, reduce)))}]",

		SByte or UInt16 or UInt32 or UInt64 or Byte or Int16 or Int32 or Int64 or Single or Double or Decimal => value.ToString()!,

		_ => DisplayValue(_jsonSerializer.Serialize(value, value.GetType()), reduce),
	};

	public override String ToString() => ToString(true);
	public abstract String ToString(Boolean reduce);
	public abstract Query Replace(String argument, String value);
	protected abstract IParameter Add(String name, DbType type, Boolean nullable, Object value);

	protected String ToString<P>(String template, IEnumerable<P> parameters, Boolean reduce = true) where P : DbParameter
	{
		String query = reduce ? SpaceRemover.Replace(template, " ") : template;

		foreach (IParameter parameter in parameters.OrderByDescending(p => p.ParameterName.Length))
		{
			query = query.Replace("@" + parameter.ParameterName, DisplayValue(parameter.Value, reduce), StringComparison.OrdinalIgnoreCase);
		}

		return query;
	}

    public static String MakeParameterName(String expression)
    {
        if (expression is null or "") throw new ArgumentNullException(nameof(expression));

        Int32 dotIndex = expression.IndexOf('.') + 1;
        
        String columnName = expression[dotIndex..].Replace(".", "");

        if (columnName.StartsWith("p_")) return columnName;

        return "p_" + FirstCharToLowerCase(columnName);
    }

    private static String FirstCharToLowerCase(String str)
    {
        return Char.ToLower(str[0]) + str[1..];
    }
}