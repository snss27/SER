namespace SER.Tools.Types;
public class DateTimePeriod
{
	public DateTime? From { get; set; }
	public DateTime? To { get; set; }
}

public static class DateTimePeriodExtesions
{
	public static Boolean Contains(this DateTimePeriod period, DateTime date)
	{
		return (!period.From.HasValue || date >= period.From.Value)
			&& (!period.To.HasValue || date <= period.To.Value);
	}
}
