using System.Text.Json.Serialization;

namespace SER.Tools.Types.FileStorage;

public abstract class FileDetails
{
	public String Name { get; }
	public String Extension { get; }
	public String? ContentType { get; }
	public String? Path { get; }

	[JsonIgnore]
	public String? FullPath => GetFullPath();

	[JsonConstructor]
	public FileDetails(String name, String extension, String? contentType = null, String? path = null)
	{
		Name = name;
		Extension = extension;
		ContentType = contentType;
		Path = path;
	}

	private String? GetFullPath()
	{
		if (String.IsNullOrWhiteSpace(Path)) return null;

		if (Path.EndsWith(Extension))
			return Path;

		return String.Concat(Path, Name, Extension);
	}
}

public class FileDetailsOfBytes : FileDetails
{
	public Byte[] Bytes { get; }

	[JsonConstructor]
	public FileDetailsOfBytes(String name, String extension, Byte[] bytes, String? contentType = null, String? path = null)
		: base(name, extension, contentType, path)
	{
		Bytes = bytes;
	}

	public FileDetailsOfBase64 AsBase64()
	{
		String base64String = Convert.ToBase64String(Bytes);

		return new FileDetailsOfBase64(Name, Extension, base64String, ContentType, Path);
	}
}

public class FileDetailsOfBase64 : FileDetails
{
	public String? Base64 { get; }

	[JsonConstructor]
	public FileDetailsOfBase64(String name, String extension, String? base64 = null, String? contentType = null, String? path = null)
		: base(name, extension, contentType, path)
	{
		Base64 = base64;
	}
}
