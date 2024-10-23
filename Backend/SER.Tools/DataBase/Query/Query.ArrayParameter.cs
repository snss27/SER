using SER.Tools.Types.IDs;
using System.Data;
using System.Runtime.CompilerServices;

namespace SER.Tools.DataBase.Query;
public abstract partial class Query
{
	public virtual IParameter Add(String[] value, [CallerArgumentExpression("value")] String name = "")
	{
		return Add(name, DbType.Object, NotNullable, value.ToArray());
	}

	public virtual IParameter Add(Boolean[] value, [CallerArgumentExpression("value")] String name = "")
	{
		return Add(name, DbType.Object, NotNullable, value.ToArray());
	}

	public virtual IParameter Add(DateTime[] value, [CallerArgumentExpression("value")] String name = "")
	{
		return Add(name, DbType.Object, NotNullable, value.ToArray());
	}

	public virtual IParameter Add(DateOnly[] value, [CallerArgumentExpression("value")] String name = "")
	{
		return Add(name, DbType.Object, NotNullable, value.ToArray());
	}

	public virtual IParameter Add(Decimal[] value, [CallerArgumentExpression("value")] String name = "")
	{
		return Add(name, DbType.Object, NotNullable, value.ToArray());
	}

	public virtual IParameter Add(Single[] value, [CallerArgumentExpression("value")] String name = "")
	{
		return Add(name, DbType.Object, NotNullable, value.ToArray());
	}

	public virtual IParameter Add(Double[] value, [CallerArgumentExpression("value")] String name = "")
	{
		return Add(name, DbType.Object, NotNullable, value.ToArray());
	}

	public virtual IParameter Add(SByte[] value, [CallerArgumentExpression("value")] String name = "")
	{
		return Add(name, DbType.Object, NotNullable, value.ToArray());
	}

	public virtual IParameter Add(Byte[] value, [CallerArgumentExpression("value")] String name = "")
	{
		return Add(name, DbType.Object, NotNullable, value.ToArray());
	}

	public virtual IParameter Add(Int16[] value, [CallerArgumentExpression("value")] String name = "")
	{
		return Add(name, DbType.Object, NotNullable, value.ToArray());
	}

	public virtual IParameter Add(UInt16[] value, [CallerArgumentExpression("value")] String name = "")
	{
		return Add(name, DbType.Object, NotNullable, value.ToArray());
	}

	public virtual IParameter Add(Int32[] value, [CallerArgumentExpression("value")] String name = "")
	{
		return Add(name, DbType.Object, NotNullable, value.ToArray());
	}

	public virtual IParameter Add(UInt32[] value, [CallerArgumentExpression("value")] String name = "")
	{
		return Add(name, DbType.Object, NotNullable, value.ToArray());
	}

	public virtual IParameter Add(Int64[] value, [CallerArgumentExpression("value")] String name = "")
	{
		return Add(name, DbType.Object, NotNullable, value.ToArray());
	}

	public virtual IParameter Add(UInt64[] value, [CallerArgumentExpression("value")] String name = "")
	{
		return Add(name, DbType.Object, NotNullable, value.ToArray());
	}

	public virtual IParameter Add(ID[]? value, [CallerArgumentExpression("value")] String name = "")
	{
		if (value is not null)
			return Add(name, DbType.Object, NotNullable, value.Select(v => v.ToByteArray()).ToArray());

		return Add(name, DbType.Object, Nullable, DBNull.Value);
	}
}
