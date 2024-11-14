namespace SER.Tools.Utils;
public class NumberUtils
{
	public static (Int32 offset, Int32 limit) NormalizeRange(Int32 page, Int32 pageSize)
	{
		Int32 offset = Math.Max((page - 1) * pageSize, 0);
		Int32 limit = Math.Max(pageSize, 0);

		return (offset, limit);
	}

	/// <param name="value">Число</param>
	/// <param name="nominative">Именительный падеж. Например "День"</param>
	/// <param name="genitive">Родительный падеж. Например "Дня"</param>
	/// <param name="plural">Множественное число. Например "Дней"</param>
	/// <returns>Возвращает слово в нужном падеже в зависимости от числа</returns>
	public static String Conjugate(Int32 value, String nominative, String genitive, String plural)
	{
		value %= 100;

		if (value is >= 11 and <= 19)
			return plural;

		value %= 10;

		return value switch
		{
			1 => nominative,
			2 or 3 or 4 => genitive,
			_ => plural,
		};
	}
}
