using SER.Tools.Json.Converters;
using System.Text.Json.Serialization;

namespace SER.Tools.Types.Times;

[Serializable]
[JsonConverter(typeof(TimeJsonConverter))]
public readonly struct Time : IComparable<Time>
{
	private readonly Int32 _totalSeconds;

	private const Int32 SecondsInDay = 86400;
	private const Int32 SecondsInMinute = 60;
	private const Int32 SecondsInHour = 3600;

	[JsonIgnore] public Int32 Hour => _totalSeconds / SecondsInHour;
	[JsonIgnore] public Int32 Minute => _totalSeconds / SecondsInMinute % SecondsInMinute;
	[JsonIgnore] public Int32 Second => _totalSeconds % SecondsInMinute;

	public Int32 TotalSeconds => _totalSeconds;

	[JsonIgnore] public static Time StartOfDay => new(0);
	[JsonIgnore] public static Time EndOfDay => new(SecondsInDay - 1);

	[JsonIgnore]
	public static Time NowUtc
	{
		get
		{
			DateTime utcNow = DateTime.UtcNow;
			return new Time(utcNow.Hour, utcNow.Minute, utcNow.Second);
		}
	}

	public Time(Int32 totalSeconds)
	{
		_totalSeconds = totalSeconds >= 0
			? totalSeconds % SecondsInDay
			: SecondsInDay + totalSeconds % SecondsInDay;
	}

	public Time(Int32 hours, Int32 minutes, Int32 seconds = 0) : this(hours * SecondsInHour + minutes * SecondsInMinute + seconds)
	{
		if (hours is > 23 or < 0) throw new ArgumentException("hours must be between 0 and 23 inclusive", nameof(hours));
		if (minutes is > 59 or < 0) throw new ArgumentException("minutes must be between 0 and 59 inclusive", nameof(minutes));
		if (seconds is > 59 or < 0) throw new ArgumentException("seconds must be between 0 and 59 inclusive", nameof(seconds));
	}

	#region Equals

	public override Boolean Equals(Object obj)
	{
		if (ReferenceEquals(null, obj)) return false;
		if (obj.GetType() != GetType()) return false;

		return Equals((Time)obj);
	}

	private Boolean Equals(Time time)
	{
		return TotalSeconds == time.TotalSeconds;
	}

	public override Int32 GetHashCode()
	{
		return _totalSeconds.GetHashCode();
	}

	public Int32 CompareTo(Time other)
	{
		return TotalSeconds - other.TotalSeconds;
	}

	#endregion Equals

	#region Operators

	public static Boolean operator ==(Time firstTime, Time secondTime)
	{
		if ((Object)firstTime == (Object)secondTime) return true;
		if ((Object)firstTime == null) return false;
		if ((Object)secondTime == null) return false;

		return firstTime.Equals(secondTime);
	}

	public static Boolean operator !=(Time firstTime, Time secondTime)
	{
		return !(firstTime == secondTime);
	}

	public static Boolean operator <(Time firstTime, Time secondTime)
	{
		return firstTime.TotalSeconds < secondTime.TotalSeconds;
	}

	public static Boolean operator >(Time firstTime, Time secondTime)
	{
		return firstTime.TotalSeconds > secondTime.TotalSeconds;
	}

	public static Boolean operator >=(Time firstTime, Time secondTime)
	{
		return firstTime.TotalSeconds >= secondTime.TotalSeconds;
	}

	public static Boolean operator <=(Time firstTime, Time secondTime)
	{
		return firstTime.TotalSeconds <= secondTime.TotalSeconds;
	}

	public static TimeSpan operator +(Time firstTime, Time secondTime)
	{
		const Int32 ticksInSecond = 10000000;

		return new TimeSpan((firstTime.TotalSeconds + secondTime.TotalSeconds) * ticksInSecond);
	}

	public static TimeSpan operator -(Time firstTime, Time secondTime)
	{
		const Int32 ticksInSecond = 10000000;

		return new TimeSpan((firstTime.TotalSeconds - secondTime.TotalSeconds) * ticksInSecond);
	}

	public static implicit operator Time(TimeSpan timeSpan) => new Time((Int32)timeSpan.TotalSeconds);
	public static implicit operator TimeSpan(Time time) => TimeSpan.FromSeconds(time.TotalSeconds);

	#endregion operator

	public override String ToString()
	{
		String[] parts =
		{
			Hour.ToString("00"),
			Minute.ToString("00"),
			Second.ToString("00")
		};

		return String.Join(":", parts);
	}

	public static Time Parse(String time)
	{
		String[] parts = time.Split(":");

		Int32 hour = Int32.Parse(parts[0]);
		Int32 minute = Int32.Parse(parts[1]);
		Int32 second = parts.Length == 3 ? Int32.Parse(parts[2]) : 0;

		return new Time(hour, minute, second);
	}
}
