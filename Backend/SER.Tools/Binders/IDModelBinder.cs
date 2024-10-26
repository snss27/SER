using Microsoft.AspNetCore.Mvc.ModelBinding;
using SER.Tools.Types.IDs;

namespace SER.Tools.Binders;

public class IDModelBinder : IModelBinder
{
	public Task BindModelAsync(ModelBindingContext bindingContext)
	{
		if (bindingContext == null) throw new ArgumentNullException(nameof(bindingContext));

		String? value;

		if (bindingContext.HttpContext.Request.Method == "POST" && bindingContext.BindingSource == BindingSource.Body)
		{
			using StreamReader sr = new(bindingContext.HttpContext.Request.Body);
			value = sr.ReadToEndAsync().Result.Trim('"');
			sr.Dispose();
		}
		else
			value = bindingContext.ValueProvider.GetValue(bindingContext.ModelName).FirstValue;

		if (String.IsNullOrWhiteSpace(value) || value == "null")
			bindingContext.Result = ModelBindingResult.Success(null);
		else
			bindingContext.Result = ModelBindingResult.Success(ID.Parse(value));


		return Task.CompletedTask;
	}
}
