using System.Text.RegularExpressions;

namespace SER.Tools.Utils;

public static partial class Regexs
{
	public static Regex InnRegex => _innRegex();
	public static Regex KppRegex => _kppRegex();
	public static Regex OrgnRegex => _orgnRegex();
	public static Regex PhoneRegex => _phoneRegex();
	public static Regex MailRegex => _mailRegex();
	public static Regex GroupNumberRegex => _groupNumberRegex();

	#region Register

	[GeneratedRegex(@"^\d{10}$")]
	private static partial Regex _innRegex();

	[GeneratedRegex(@"^\d{9}$")]
	private static partial Regex _kppRegex();

	[GeneratedRegex(@"^\d{13}$")]
	private static partial Regex _orgnRegex();

	[GeneratedRegex(@"^\+7 \(\d{3}\) \d{3}-\d{2}-\d{2}$")]
	private static partial Regex _phoneRegex();

	[GeneratedRegex(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]+$")]
	private static partial Regex _mailRegex();

	[GeneratedRegex(@"^\d{9}$")]
	private static partial Regex _groupNumberRegex();

	#endregion
}