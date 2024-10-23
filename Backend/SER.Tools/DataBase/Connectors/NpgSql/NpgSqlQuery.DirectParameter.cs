using Npgsql;
using NpgsqlTypes;
using SER.Tools.DataBase.Query;

namespace SER.Tools.DataBase.Connectors.NpgSql;

public sealed partial class NpgSqlQuery : Query<NpgsqlCommand>
{
	public override IParameter Add(SByte value, String name) => AddParameter(new(name, NpgsqlDbType.Smallint, NotNullable, (Int16)value));
	public override IParameter Add(SByte? value, String name) => AddParameter(new(name, NpgsqlDbType.Smallint, Nullable, value is null ? DBNull.Value : (Int16)value));
	public override IParameter Add(UInt16 value, String name) => AddParameter(new(name, NpgsqlDbType.Integer, NotNullable, (Int32)value));
	public override IParameter Add(UInt16? value, String name) => AddParameter(new(name, NpgsqlDbType.Integer, Nullable, value is null ? DBNull.Value : (Int32)value));
	public override IParameter Add(UInt32 value, String name) => AddParameter(new(name, NpgsqlDbType.Bigint, NotNullable, (Int64)value));
	public override IParameter Add(UInt32? value, String name) => AddParameter(new(name, NpgsqlDbType.Bigint, Nullable, value is null ? DBNull.Value : (Int64)value));
	public override IParameter Add(UInt64 value, String name) => AddParameter(new(name, NpgsqlDbType.Numeric, NotNullable, unchecked((Decimal)value)));
	public override IParameter Add(UInt64? value, String name) => AddParameter(new(name, NpgsqlDbType.Numeric, Nullable, value is null ? DBNull.Value : unchecked((Decimal)value)));
}