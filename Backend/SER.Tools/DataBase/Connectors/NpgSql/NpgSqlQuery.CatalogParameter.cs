using Npgsql;
using NpgsqlTypes;
using SER.Tools.DataBase.Query;
using SER.Tools.Types.Catalog;
using SER.Tools.Types.IDs;

namespace SER.Tools.DataBase.Connectors.NpgSql;

public sealed partial class NpgSqlQuery : Query<NpgsqlCommand>
{
	public override IParameter Add(Catalog<String?> value, String name) => AddParameter(new(name, NpgsqlDbType.Array | NpgsqlDbType.Text, NotNullable, value.ToArray()));
	public override IParameter Add(Catalog<Boolean> value, String name) => AddParameter(new(name, NpgsqlDbType.Array | NpgsqlDbType.Boolean, NotNullable, value.ToArray()));
	public override IParameter Add(Catalog<Boolean?> value, String name) => AddParameter(new(name, NpgsqlDbType.Array | NpgsqlDbType.Boolean, NotNullable, value.ToArray()));
	public override IParameter Add(Catalog<DateTime> value, String name) => AddParameter(new(name, NpgsqlDbType.Array | NpgsqlDbType.Timestamp, NotNullable, value.ToArray()));
	public override IParameter Add(Catalog<DateTime?> value, String name) => AddParameter(new(name, NpgsqlDbType.Array | NpgsqlDbType.Timestamp, NotNullable, value.ToArray()));
	public override IParameter Add(Catalog<Decimal> value, String name) => AddParameter(new(name, NpgsqlDbType.Array | NpgsqlDbType.Numeric, NotNullable, value.ToArray()));
	public override IParameter Add(Catalog<Decimal?> value, String name) => AddParameter(new(name, NpgsqlDbType.Array | NpgsqlDbType.Numeric, NotNullable, value.ToArray()));
	public override IParameter Add(Catalog<Single> value, String name) => AddParameter(new(name, NpgsqlDbType.Array | NpgsqlDbType.Real, NotNullable, value.ToArray()));
	public override IParameter Add(Catalog<Single?> value, String name) => AddParameter(new(name, NpgsqlDbType.Array | NpgsqlDbType.Real, NotNullable, value.ToArray()));
	public override IParameter Add(Catalog<Double> value, String name) => AddParameter(new(name, NpgsqlDbType.Array | NpgsqlDbType.Double, NotNullable, value.ToArray()));
	public override IParameter Add(Catalog<Double?> value, String name) => AddParameter(new(name, NpgsqlDbType.Array | NpgsqlDbType.Double, NotNullable, value.ToArray()));
	public override IParameter Add(Catalog<SByte> value, String name) => AddParameter(new(name, NpgsqlDbType.Array | NpgsqlDbType.Smallint, NotNullable, value.ToArray()));
	public override IParameter Add(Catalog<SByte?> value, String name) => AddParameter(new(name, NpgsqlDbType.Array | NpgsqlDbType.Smallint, NotNullable, value.ToArray()));
	public override IParameter Add(Catalog<Byte> value, String name) => AddParameter(new(name, NpgsqlDbType.Array | NpgsqlDbType.Smallint, NotNullable, value.ToArray()));
	public override IParameter Add(Catalog<Byte?> value, String name) => AddParameter(new(name, NpgsqlDbType.Array | NpgsqlDbType.Smallint, NotNullable, value.ToArray()));
	public override IParameter Add(Catalog<Int16> value, String name) => AddParameter(new(name, NpgsqlDbType.Array | NpgsqlDbType.Smallint, NotNullable, value.ToArray()));
	public override IParameter Add(Catalog<Int16?> value, String name) => AddParameter(new(name, NpgsqlDbType.Array | NpgsqlDbType.Smallint, NotNullable, value.ToArray()));
	public override IParameter Add(Catalog<UInt16> value, String name) => AddParameter(new(name, NpgsqlDbType.Array | NpgsqlDbType.Smallint, NotNullable, value.ToArray()));
	public override IParameter Add(Catalog<UInt16?> value, String name) => AddParameter(new(name, NpgsqlDbType.Array | NpgsqlDbType.Smallint, NotNullable, value.ToArray()));
	public override IParameter Add(Catalog<Int32> value, String name) => AddParameter(new(name, NpgsqlDbType.Array | NpgsqlDbType.Integer, NotNullable, value.ToArray()));
	public override IParameter Add(Catalog<Int32?> value, String name) => AddParameter(new(name, NpgsqlDbType.Array | NpgsqlDbType.Integer, NotNullable, value.ToArray()));
	public override IParameter Add(Catalog<UInt32> value, String name) => AddParameter(new(name, NpgsqlDbType.Array | NpgsqlDbType.Integer, NotNullable, value.ToArray()));
	public override IParameter Add(Catalog<UInt32?> value, String name) => AddParameter(new(name, NpgsqlDbType.Array | NpgsqlDbType.Integer, NotNullable, value.ToArray()));
	public override IParameter Add(Catalog<Int64> value, String name) => AddParameter(new(name, NpgsqlDbType.Array | NpgsqlDbType.Bigint, NotNullable, value.ToArray()));
	public override IParameter Add(Catalog<Int64?> value, String name) => AddParameter(new(name, NpgsqlDbType.Array | NpgsqlDbType.Bigint, NotNullable, value.ToArray()));
	public override IParameter Add(Catalog<UInt64> value, String name) => AddParameter(new(name, NpgsqlDbType.Array | NpgsqlDbType.Bigint, NotNullable, value.ToArray()));
	public override IParameter Add(Catalog<UInt64?> value, String name) => AddParameter(new(name, NpgsqlDbType.Array | NpgsqlDbType.Bigint, NotNullable, value.ToArray()));
	public override IParameter Add(Catalog<ID> value, String name) => AddParameter(new(name, NpgsqlDbType.Array | NpgsqlDbType.Bytea, NotNullable, value.Select(v => v.ToByteArray()).ToArray()));
	public override IParameter Add(Catalog<ID?> value, String name) => AddParameter(new(name, NpgsqlDbType.Array | NpgsqlDbType.Bytea, NotNullable, value.Select(v => v?.ToByteArray()).ToArray()));
}