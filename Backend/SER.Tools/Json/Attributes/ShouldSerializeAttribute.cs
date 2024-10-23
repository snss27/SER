using System.Runtime.CompilerServices;

namespace SER.Tools.Json.Attributes;

public class ShouldSerializeAttribute : Attribute
{
	public String ShouldSerializeMethodName { get; }

	public ShouldSerializeAttribute(String shouldSerializeMethodName, [CallerArgumentExpression("shouldSerializeMethodName")] String? shouldSerializeMethodNameExpression = null)
	{
		if (shouldSerializeMethodNameExpression is null || !shouldSerializeMethodNameExpression.Contains("nameof"))
			throw new Exception("Некорректный формат записи ifTrue в ShouldSerializeAttribute. Ожидалось \"ifTrue: nameof(КлассСвойства.ЕгоЧлен)\"");

		ShouldSerializeMethodName = shouldSerializeMethodNameExpression.Replace("nameof(", "").Replace(")", "");
	}
}