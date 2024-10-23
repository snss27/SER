using SER.Tools.Types.IDs;
using System.Data;
using System.Runtime.CompilerServices;

namespace SER.Tools.DataBase.Query;

[System.Diagnostics.CodeAnalysis.SuppressMessage("Roslynator", "RCS1084:Use coalesce expression instead of conditional expression.", Justification = "<Pending>")]
public abstract partial class Query
{
    public virtual IParameter Add(String? value, [CallerArgumentExpression("value")] String name = "")
    {
        return Add(name, DbType.String, Nullable, value is null ? DBNull.Value : value);
    }

    public virtual IParameter Add(Boolean value, [CallerArgumentExpression("value")] String name = "")
    {
        return Add(name, DbType.Boolean, NotNullable, value);
    }

    public virtual IParameter Add(Boolean? value, [CallerArgumentExpression("value")] String name = "")
    {
        return Add(name, DbType.Boolean, Nullable, value is null ? DBNull.Value : value);
    }

    public virtual IParameter Add(DateTime value, [CallerArgumentExpression("value")] String name = "")
    {
        return Add(name, DbType.DateTime2, NotNullable, value);
    }

    public virtual IParameter Add(DateTime? value, [CallerArgumentExpression("value")] String name = "")
    {
        return Add(name, DbType.DateTime2, Nullable, value is null ? DBNull.Value : value);
    }

    public virtual IParameter Add(DateOnly value, [CallerArgumentExpression("value")] String name = "")
    {
        return Add(name, DbType.Date, NotNullable, value);
    }

    public virtual IParameter Add(DateOnly? value, [CallerArgumentExpression("value")] String name = "")
    {
        return Add(name, DbType.Date, Nullable, value is null ? DBNull.Value : value);
    }

    public virtual IParameter Add(TimeOnly value, [CallerArgumentExpression("value")] String name = "")
    {
        return Add(name, DbType.Time, NotNullable, value);
    }

    public virtual IParameter Add(TimeOnly? value, [CallerArgumentExpression("value")] String name = "")
    {
        return Add(name, DbType.Time, Nullable, value is null ? DBNull.Value : value);
    }

    public virtual IParameter Add(TimeSpan value, [CallerArgumentExpression("value")] String name = "")
    {
        return Add(name, DbType.Time, NotNullable, value);
    }

    public virtual IParameter Add(TimeSpan? value, [CallerArgumentExpression("value")] String name = "")
    {
        return Add(name, DbType.Time, Nullable, value is null ? DBNull.Value : value);
    }

    public virtual IParameter Add(Decimal value, [CallerArgumentExpression("value")] String name = "")
    {
        return Add(name, DbType.Decimal, NotNullable, value);
    }

    public virtual IParameter Add(Decimal? value, [CallerArgumentExpression("value")] String name = "")
    {
        return Add(name, DbType.Decimal, Nullable, value is null ? DBNull.Value : value);
    }

    public virtual IParameter Add(Single value, [CallerArgumentExpression("value")] String name = "")
    {
        return Add(name, DbType.Single, NotNullable, value);
    }

    public virtual IParameter Add(Single? value, [CallerArgumentExpression("value")] String name = "")
    {
        return Add(name, DbType.Single, Nullable, value is null ? DBNull.Value : value);
    }

    public virtual IParameter Add(Double value, [CallerArgumentExpression("value")] String name = "")
    {
        return Add(name, DbType.Double, NotNullable, value);
    }

    public virtual IParameter Add(Double? value, [CallerArgumentExpression("value")] String name = "")
    {
        return Add(name, DbType.Double, Nullable, value is null ? DBNull.Value : value);
    }

    public virtual IParameter Add(SByte value, [CallerArgumentExpression("value")] String name = "")
    {
        return Add(name, DbType.SByte, NotNullable, value);
    }

    public virtual IParameter Add(SByte? value, [CallerArgumentExpression("value")] String name = "")
    {
        return Add(name, DbType.SByte, Nullable, value is null ? DBNull.Value : value);
    }

    public virtual IParameter Add(Byte value, [CallerArgumentExpression("value")] String name = "")
    {
        return Add(name, DbType.Byte, NotNullable, value);
    }

    public virtual IParameter Add(Byte? value, [CallerArgumentExpression("value")] String name = "")
    {
        return Add(name, DbType.Byte, Nullable, value is null ? DBNull.Value : value);
    }

    public virtual IParameter Add(Int16 value, [CallerArgumentExpression("value")] String name = "")
    {
        return Add(name, DbType.Int16, NotNullable, value);
    }

    public virtual IParameter Add(Int16? value, [CallerArgumentExpression("value")] String name = "")
    {
        return Add(name, DbType.Int16, Nullable, value is null ? DBNull.Value : value);
    }

    public virtual IParameter Add(UInt16 value, [CallerArgumentExpression("value")] String name = "")
    {
        return Add(name, DbType.UInt16, NotNullable, value);
    }

    public virtual IParameter Add(UInt16? value, [CallerArgumentExpression("value")] String name = "")
    {
        return Add(name, DbType.UInt16, Nullable, value is null ? DBNull.Value : value);
    }

    public virtual IParameter Add(Int32 value, [CallerArgumentExpression("value")] String name = "")
    {
        return Add(name, DbType.Int32, NotNullable, value);
    }

    public virtual IParameter Add(Int32? value, [CallerArgumentExpression("value")] String name = "")
    {
        return Add(name, DbType.Int32, Nullable, value is null ? DBNull.Value : value);
    }

    public virtual IParameter Add(UInt32 value, [CallerArgumentExpression("value")] String name = "")
    {
        return Add(name, DbType.UInt32, NotNullable, value);
    }

    public virtual IParameter Add(UInt32? value, [CallerArgumentExpression("value")] String name = "")
    {
        return Add(name, DbType.UInt32, Nullable, value is null ? DBNull.Value : value);
    }

    public virtual IParameter Add(Int64 value, [CallerArgumentExpression("value")] String name = "")
    {
        return Add(name, DbType.Int64, NotNullable, value);
    }

    public virtual IParameter Add(Int64? value, [CallerArgumentExpression("value")] String name = "")
    {
        return Add(name, DbType.Int64, Nullable, value is null ? DBNull.Value : value);
    }

    public virtual IParameter Add(UInt64 value, [CallerArgumentExpression("value")] String name = "")
    {
        return Add(name, DbType.UInt64, NotNullable, value);
    }

    public virtual IParameter Add(UInt64? value, [CallerArgumentExpression("value")] String name = "")
    {
        return Add(name, DbType.UInt64, Nullable, value is null ? DBNull.Value : value);
    }

    public virtual IParameter Add(ID value, [CallerArgumentExpression("value")] String name = "")
    {
        return Add(name, DbType.Binary, NotNullable, value.ToByteArray());
    }

    public virtual IParameter Add(ID? value, [CallerArgumentExpression("value")] String name = "")
    {
        return Add(name, DbType.Binary, Nullable, value is null ? DBNull.Value : value.Value.ToByteArray());
    }
}