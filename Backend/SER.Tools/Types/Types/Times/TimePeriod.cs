namespace SER.Tools.Types.Times;

public class TimePeriod
{
	public Time Start { get; }
	public Time End { get; }

	public Boolean OverMidnight => End < Start;

	public static TimePeriod FullDay => new(Time.StartOfDay, Time.EndOfDay);

	public TimePeriod(Time begin, Time end)
	{
		Start = begin;
		End = end;
	}

	public Boolean Includes(Time currentTime)
	{
		return OverMidnight
			? currentTime >= Start && currentTime <= Time.EndOfDay || currentTime >= Time.StartOfDay && currentTime <= End
			: currentTime >= Start && currentTime <= End;
	}

	public override String ToString() => $"{Start} - {End}";

	public override Boolean Equals(Object obj)
	{
		if (ReferenceEquals(null, obj)) return false;
		if (ReferenceEquals(this, obj)) return true;
		if (obj.GetType() != GetType()) return false;

		return Equals((TimePeriod)obj);
	}

	protected Boolean Equals(TimePeriod other)
	{
		return Start.Equals(other.Start) && End.Equals(other.End);
	}

	public override Int32 GetHashCode()
	{
		unchecked
		{
			return Start.GetHashCode() * 397 ^ End.GetHashCode();
		}
	}
}
