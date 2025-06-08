using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SER.Tools.Converters;

namespace SER.Database.Models.ConfigurationTools;

public class EncryptedStringConverter : ValueConverter<string?, string?>
{
	public EncryptedStringConverter()
		: base(
			v => v == null ? null : Encrypt(v),
			v => v == null ? null : Decrypt(v))
	{ }

	public static string Encrypt(string value)
	{
		var key = Environment.GetEnvironmentVariable("ENCRYPTION_KEY")
				  ?? throw new InvalidOperationException("ENCRYPTION_KEY is not set");
		return EncryptionHelper.Encrypt(value, key);
	}

	public static string Decrypt(string value)
	{
		var key = Environment.GetEnvironmentVariable("ENCRYPTION_KEY")
				  ?? throw new InvalidOperationException("ENCRYPTION_KEY is not set");
		return EncryptionHelper.Decrypt(value, key);
	}
}
