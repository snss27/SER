using System.Security.Cryptography;
using System.Text;

namespace SER.Tools.Converters;
public static class EncryptionHelper
{
	public static string Encrypt(string plainText, string key)
	{
		using var aes = Aes.Create();
		aes.Key = Encoding.UTF8.GetBytes(key.PadRight(32).Substring(0, 32));
		aes.GenerateIV();

		using var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
		using var ms = new MemoryStream();
		ms.Write(aes.IV, 0, aes.IV.Length);
		using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
		using (var sw = new StreamWriter(cs))
			sw.Write(plainText);
		return Convert.ToBase64String(ms.ToArray());
	}

	public static string Decrypt(string cipherText, string key)
	{
		var fullCipher = Convert.FromBase64String(cipherText);

		using var aes = Aes.Create();
		aes.Key = Encoding.UTF8.GetBytes(key.PadRight(32).Substring(0, 32));
		aes.IV = fullCipher[..16];

		using var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
		using var ms = new MemoryStream(fullCipher[16..]);
		using var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read);
		using var sr = new StreamReader(cs);
		return sr.ReadToEnd();
	}
}
