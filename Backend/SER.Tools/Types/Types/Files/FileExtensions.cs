using Microsoft.AspNetCore.Http;

namespace SER.Tools.Types.Types.Files;
public static class FileExtensions
{
	public static Byte[] ToBytes(this IFormFile file)
	{
		using var memoryStream = new MemoryStream();
		file.CopyTo(memoryStream);

		return memoryStream.ToArray();
	}
}
