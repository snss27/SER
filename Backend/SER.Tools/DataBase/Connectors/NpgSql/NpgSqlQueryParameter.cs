using Npgsql;
using NpgsqlTypes;
using System.Data;

namespace SER.Tools.DataBase.Connectors.NpgSql;

public sealed class NpgSqlQueryParameter : NpgsqlParameter, IParameter
{
    internal NpgSqlQueryParameter()
    { }

    public string ColumnName => string.IsNullOrEmpty(SourceColumn) ? ParameterName : SourceColumn;

    internal NpgSqlQueryParameter(string parameterName, DbType type, bool isNullable, object value) : base
    (
        parameterName: Query.Query.MakeParameterName(parameterName),
        parameterType: type,
        size: 0,
        sourceColumn: string.Empty,
        direction: ParameterDirection.Input,
        isNullable: isNullable,
        precision: 0,
        scale: 0,
        sourceVersion: DataRowVersion.Current,
        value: value
    )
    { }

    internal NpgSqlQueryParameter(string parameterName, NpgsqlDbType type, bool isNullable, object value) : base
    (
        parameterName: Query.Query.MakeParameterName(parameterName),
        parameterType: type,
        size: 0,
        sourceColumn: string.Empty,
        direction: ParameterDirection.Input,
        isNullable: isNullable,
        precision: 0,
        scale: 0,
        sourceVersion: DataRowVersion.Current,
        value: value
    )
    { }
}