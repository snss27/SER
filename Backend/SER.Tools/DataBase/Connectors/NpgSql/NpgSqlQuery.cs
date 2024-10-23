using Npgsql;
using NpgsqlTypes;
using SER.Tools.Converters;
using SER.Tools.DataBase.Query;
using System.Collections;
using System.Data;
using System.Runtime.CompilerServices;

namespace SER.Tools.DataBase.Connectors.NpgSql;

public sealed partial class NpgSqlQuery : Query<NpgsqlCommand>
{
	private readonly HashSet<String> _parameterNames = new(StringComparer.OrdinalIgnoreCase);

	internal NpgSqlQuery(String template, CommandType commandType, Int32? timeout, Boolean preparable, IJsonSerializer jsonSerializer)
		: base(template, commandType, timeout, preparable, jsonSerializer) { }

	private NpgSqlQueryParameter AddParameter(NpgSqlQueryParameter parameter)
	{
		if (!_parameterNames.Add(parameter.ParameterName))
		{
			throw new ArgumentException($"Parameter '{parameter.ParameterName}' already exists", parameter.ParameterName);
		}

		_command.Parameters.Add(parameter);

		return parameter;
	}

	protected override IParameter Add(String name, DbType type, Boolean nullable, Object value)
	{
		String parameterName = MakeParameterName(name);

		return AddParameter(new(parameterName, type, nullable, value));
	}

	public override IParameter AddJson(Object? value, [CallerArgumentExpression("value")] String name = "")
	{
		String parameterName = MakeParameterName(name);
		Object serializedValue = value is null ? DBNull.Value : _jsonSerializer.Serialize(value, value.GetType());

		return AddParameter(new(parameterName, NpgsqlDbType.Jsonb, Nullable, serializedValue));
	}

	public override String ToString(Boolean reduce)
	{
		return ToString(_command.CommandText, _command.Parameters, reduce);
	}

	protected override String DisplayValue(Object? value, Boolean reduce) => value switch
	{
		Byte[] id when reduce && id.Length > 40 => $"decode('{BitConverter.ToString(id.Take(40).ToArray()).Replace("-", String.Empty, StringComparison.Ordinal)}', 'hex')",
		Byte[] id => $"decode('{BitConverter.ToString(id).Replace("-", String.Empty, StringComparison.Ordinal)}', 'hex')",

		String longStr when reduce && longStr.Length > 15 => $"'{longStr.Substring(0, 15)}…'",
		String str => $"'{str}'",

		ICollection collection when reduce && collection.Count > 5 => $"ARRAY[{String.Join(", ", collection.Cast<Object>().Take(5).Select(v => DisplayValue(v, reduce)))}, …]",
		IEnumerable array => $"ARRAY[{String.Join(", ", array.Cast<Object>().Select(v => DisplayValue(v, reduce)))}]",

		_ => base.DisplayValue(value, reduce),
	};
}