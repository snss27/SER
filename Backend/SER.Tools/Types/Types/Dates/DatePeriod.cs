using System.Text.Json.Serialization;

namespace SER.Tools.Types.Dates;
public readonly struct DatePeriod
{
	public DateOnly Begin { get; }
	public DateOnly End { get; }

	[JsonConstructor]
	public DatePeriod(DateOnly begin, DateOnly end)
	{
		Begin = begin;
		End = end;
	}

	public static DatePeriod New(DateOnly beginDate, DateOnly endDate)
	{
		return new DatePeriod(beginDate, endDate);
	}

	public static DatePeriod New(DateTime beginDateTime, DateTime endDateTime)
	{
		DateOnly beginDate = DateOnly.FromDateTime(beginDateTime);
		DateOnly endDate = DateOnly.FromDateTime(endDateTime);

		return new DatePeriod(beginDate, endDate);
	}

	public Boolean IncludesDate(DateOnly dateToCheck) => dateToCheck >= Begin && dateToCheck <= End;

	public DateOnly[] GetDays()
	{
		List<DateOnly> days = new List<DateOnly>();

		for (DateOnly day = Begin; day <= End; day = day.AddDays(1))
			days.Add(day);

		return days.ToArray();
	}

	public DateOnly[] GetDays(Int32 year)
	{
		return GetDays().Where(day => day.Year == year).ToArray();
	}
}
