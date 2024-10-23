using Npgsql;
using NpgsqlTypes;
using SER.Tools.DataBase.Query;
using SER.Tools.Types.IDs;

namespace SER.Tools.DataBase.Connectors.NpgSql;
public sealed partial class NpgSqlQuery : Query<NpgsqlCommand>
{
	public override IParameter Add(String?[] value, String name) => AddParameter(new(name, NpgsqlDbType.Array | NpgsqlDbType.Text, NotNullable, value.ToArray()));
	public override IParameter Add(Boolean[] value, String name) => AddParameter(new(name, NpgsqlDbType.Array | NpgsqlDbType.Boolean, NotNullable, value.ToArray()));
	public override IParameter Add(DateTime[] value, String name) => AddParameter(new(name, NpgsqlDbType.Array | NpgsqlDbType.Timestamp, NotNullable, value.ToArray()));
	public override IParameter Add(Decimal[] value, String name) => AddParameter(new(name, NpgsqlDbType.Array | NpgsqlDbType.Numeric, NotNullable, value.ToArray()));
	public override IParameter Add(Single[] value, String name) => AddParameter(new(name, NpgsqlDbType.Array | NpgsqlDbType.Real, NotNullable, value.ToArray()));
	public override IParameter Add(Double[] value, String name) => AddParameter(new(name, NpgsqlDbType.Array | NpgsqlDbType.Double, NotNullable, value.ToArray()));
	public override IParameter Add(SByte[] value, String name) => AddParameter(new(name, NpgsqlDbType.Array | NpgsqlDbType.Smallint, NotNullable, value.ToArray()));
	public override IParameter Add(Byte[] value, String name) => AddParameter(new(name, NpgsqlDbType.Array | NpgsqlDbType.Smallint, NotNullable, value.ToArray()));
	public override IParameter Add(Int16[] value, String name) => AddParameter(new(name, NpgsqlDbType.Array | NpgsqlDbType.Smallint, NotNullable, value.ToArray()));
	public override IParameter Add(UInt16[] value, String name) => AddParameter(new(name, NpgsqlDbType.Array | NpgsqlDbType.Smallint, NotNullable, value.ToArray()));
	public override IParameter Add(Int32[] value, String name) => AddParameter(new(name, NpgsqlDbType.Array | NpgsqlDbType.Integer, NotNullable, value.ToArray()));
	public override IParameter Add(UInt32[] value, String name) => AddParameter(new(name, NpgsqlDbType.Array | NpgsqlDbType.Integer, NotNullable, value.ToArray()));
	public override IParameter Add(Int64[] value, String name) => AddParameter(new(name, NpgsqlDbType.Array | NpgsqlDbType.Bigint, NotNullable, value.ToArray()));
	public override IParameter Add(UInt64[] value, String name) => AddParameter(new(name, NpgsqlDbType.Array | NpgsqlDbType.Bigint, NotNullable, value.ToArray()));
	public override IParameter Add(ID[]? value, String name)
	{
		if (value is not null)
			return AddParameter(new(name, NpgsqlDbType.Array | NpgsqlDbType.Bytea, NotNullable, value.Select(v => v.ToByteArray()).ToArray()));

		return AddParameter(new(name, NpgsqlDbType.Array | NpgsqlDbType.Bytea, Nullable, DBNull.Value));
	}
}
