using SER.Tools.DataBase.Query.GenericParameters;
using System.Data;
using System.Runtime.CompilerServices;

namespace SER.Tools.DataBase.Query;

public abstract partial class Query
{
	public virtual IParameter Add<T>(T value, [CallerArgumentExpression("value")] String name = "")
	{
		return Parameter<T>.Add(this, value, name);
	}

	public virtual IParameter AddJson(Object? value, [CallerArgumentExpression("value")] String name = "")
	{
		return Add(name, DbType.Object, Nullable, value ?? DBNull.Value);
	}
}