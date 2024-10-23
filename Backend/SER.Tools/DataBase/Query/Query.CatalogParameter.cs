using SER.Tools.Types.Catalog;
using SER.Tools.Types.IDs;
using System.Data;
using System.Runtime.CompilerServices;

namespace SER.Tools.DataBase.Query;

public abstract partial class Query
{
	public virtual IParameter Add(Catalog<String?> value, [CallerArgumentExpression("value")] String name = "")
	{
		return Add(name, DbType.Object, NotNullable, value.ToArray());
	}

	public virtual IParameter Add(Catalog<Boolean> value, [CallerArgumentExpression("value")] String name = "")
	{
		return Add(name, DbType.Object, NotNullable, value.ToArray());
	}

	public virtual IParameter Add(Catalog<Boolean?> value, [CallerArgumentExpression("value")] String name = "")
	{
		return Add(name, DbType.Object, NotNullable, value.ToArray());
	}

	public virtual IParameter Add(Catalog<DateTime> value, [CallerArgumentExpression("value")] String name = "")
	{
		return Add(name, DbType.Object, NotNullable, value.ToArray());
	}

	public virtual IParameter Add(Catalog<DateTime?> value, [CallerArgumentExpression("value")] String name = "")
	{
		return Add(name, DbType.Object, NotNullable, value.ToArray());
	}

	public virtual IParameter Add(Catalog<DateOnly> value, [CallerArgumentExpression("value")] String name = "")
	{
		return Add(name, DbType.Object, NotNullable, value.ToArray());
	}

	public virtual IParameter Add(Catalog<DateOnly?> value, [CallerArgumentExpression("value")] String name = "")
	{
		return Add(name, DbType.Object, NotNullable, value.ToArray());
	}

	public virtual IParameter Add(Catalog<Decimal> value, [CallerArgumentExpression("value")] String name = "")
	{
		return Add(name, DbType.Object, NotNullable, value.ToArray());
	}

	public virtual IParameter Add(Catalog<Decimal?> value, [CallerArgumentExpression("value")] String name = "")
	{
		return Add(name, DbType.Object, NotNullable, value.ToArray());
	}

	public virtual IParameter Add(Catalog<Single> value, [CallerArgumentExpression("value")] String name = "")
	{
		return Add(name, DbType.Object, NotNullable, value.ToArray());
	}

	public virtual IParameter Add(Catalog<Single?> value, [CallerArgumentExpression("value")] String name = "")
	{
		return Add(name, DbType.Object, NotNullable, value.ToArray());
	}

	public virtual IParameter Add(Catalog<Double> value, [CallerArgumentExpression("value")] String name = "")
	{
		return Add(name, DbType.Object, NotNullable, value.ToArray());
	}

	public virtual IParameter Add(Catalog<Double?> value, [CallerArgumentExpression("value")] String name = "")
	{
		return Add(name, DbType.Object, NotNullable, value.ToArray());
	}

	public virtual IParameter Add(Catalog<SByte> value, [CallerArgumentExpression("value")] String name = "")
	{
		return Add(name, DbType.Object, NotNullable, value.ToArray());
	}

	public virtual IParameter Add(Catalog<SByte?> value, [CallerArgumentExpression("value")] String name = "")
	{
		return Add(name, DbType.Object, NotNullable, value.ToArray());
	}

	public virtual IParameter Add(Catalog<Byte> value, [CallerArgumentExpression("value")] String name = "")
	{
		return Add(name, DbType.Object, NotNullable, value.ToArray());
	}

	public virtual IParameter Add(Catalog<Byte?> value, [CallerArgumentExpression("value")] String name = "")
	{
		return Add(name, DbType.Object, NotNullable, value.ToArray());
	}

	public virtual IParameter Add(Catalog<Int16> value, [CallerArgumentExpression("value")] String name = "")
	{
		return Add(name, DbType.Object, NotNullable, value.ToArray());
	}

	public virtual IParameter Add(Catalog<Int16?> value, [CallerArgumentExpression("value")] String name = "")
	{
		return Add(name, DbType.Object, NotNullable, value.ToArray());
	}

	public virtual IParameter Add(Catalog<UInt16> value, [CallerArgumentExpression("value")] String name = "")
	{
		return Add(name, DbType.Object, NotNullable, value.ToArray());
	}

	public virtual IParameter Add(Catalog<UInt16?> value, [CallerArgumentExpression("value")] String name = "")
	{
		return Add(name, DbType.Object, NotNullable, value.ToArray());
	}

	public virtual IParameter Add(Catalog<Int32> value, [CallerArgumentExpression("value")] String name = "")
	{
		return Add(name, DbType.Object, NotNullable, value.ToArray());
	}

	public virtual IParameter Add(Catalog<Int32?> value, [CallerArgumentExpression("value")] String name = "")
	{
		return Add(name, DbType.Object, NotNullable, value.ToArray());
	}

	public virtual IParameter Add(Catalog<UInt32> value, [CallerArgumentExpression("value")] String name = "")
	{
		return Add(name, DbType.Object, NotNullable, value.ToArray());
	}

	public virtual IParameter Add(Catalog<UInt32?> value, [CallerArgumentExpression("value")] String name = "")
	{
		return Add(name, DbType.Object, NotNullable, value.ToArray());
	}

	public virtual IParameter Add(Catalog<Int64> value, [CallerArgumentExpression("value")] String name = "")
	{
		return Add(name, DbType.Object, NotNullable, value.ToArray());
	}

	public virtual IParameter Add(Catalog<Int64?> value, [CallerArgumentExpression("value")] String name = "")
	{
		return Add(name, DbType.Object, NotNullable, value.ToArray());
	}

	public virtual IParameter Add(Catalog<UInt64> value, [CallerArgumentExpression("value")] String name = "")
	{
		return Add(name, DbType.Object, NotNullable, value.ToArray());
	}

	public virtual IParameter Add(Catalog<UInt64?> value, [CallerArgumentExpression("value")] String name = "")
	{
		return Add(name, DbType.Object, NotNullable, value.ToArray());
	}
	
    public virtual IParameter Add(Catalog<ID> value, [CallerArgumentExpression("value")] String name = "")
	{
		return Add(name, DbType.Object, NotNullable, value.Select(v => v.ToByteArray()).ToArray());
	}

    public virtual IParameter Add(Catalog<ID?> value, [CallerArgumentExpression("value")] String name = "")
    {
        return Add(name, DbType.Object, NotNullable, value.Select(v => v?.ToByteArray()).ToArray());
    }
}