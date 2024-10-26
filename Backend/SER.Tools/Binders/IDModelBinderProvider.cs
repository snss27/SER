using Microsoft.AspNetCore.Mvc.ModelBinding;
using SER.Tools.Types.IDs;

namespace SER.Tools.Binders;

public class IDModelBinderProvider : IModelBinderProvider
{
	private readonly IDModelBinder _binder = new();

	public IModelBinder? GetBinder(ModelBinderProviderContext context)
	{
		return (context.Metadata.ModelType == typeof(ID) || context.Metadata.ModelType == typeof(ID?)) ? _binder : null;
	}
}
