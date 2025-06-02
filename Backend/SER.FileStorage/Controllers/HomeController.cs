using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace SER.FileStorage.Controllers;

public class HomeController : ControllerBase
{
	private const String _successOperation = "Success";
	private const String _invalidOperation = "Error";
	private const String _fileStorageUploadFolder = "Files";

	private static readonly String Keyword = Environment.GetEnvironmentVariable("FILE_STORAGE_KEYWORD") ?? throw new InvalidOperationException("ENCRYPTION_KEY not set.");
	private static readonly String RootPath = Path.GetFullPath(_fileStorageUploadFolder).TrimEnd(Path.DirectorySeparatorChar) + Path.DirectorySeparatorChar;

	private static Boolean IsValidKeyword(String keyword) => !String.IsNullOrEmpty(keyword) && Keyword == keyword;
	private static Boolean IsPathInsideRoot(String path) => Path.GetFullPath(path).StartsWith(RootPath, StringComparison.OrdinalIgnoreCase);

	[HttpPost("/upload")]
	public async Task<IActionResult> Upload([FromQuery] String path, [FromQuery] String keyword, [FromForm] IFormFile file)
	{
		try
		{
			if (!IsValidKeyword(keyword)) return BadRequest(_invalidOperation);
			if (String.IsNullOrWhiteSpace(path)) return BadRequest(_invalidOperation);

			String fullPath = Path.GetFullPath(Path.Combine(RootPath, path));

			if (!IsPathInsideRoot(fullPath)) return BadRequest(_invalidOperation);

			String? directory = Path.GetDirectoryName(fullPath);
			if (directory is null) return BadRequest(_invalidOperation);

			if (!Directory.Exists(directory))
				Directory.CreateDirectory(directory);

			await using FileStream fileStream = new(fullPath, FileMode.Create);
			await file.CopyToAsync(fileStream);

			String domain = Environment.GetEnvironmentVariable("FILE_STORAGE_DOMAIN") ?? throw new InvalidOperationException("FILE_STORAGE_DOMAIN not set.");
			String urlPath = path.Replace('\\', '/');
			String publicUrl = $"https://{domain}//{WebUtility.UrlEncode(urlPath)}";

			return Ok(new { status = _successOperation, url = publicUrl });
		}
		catch (Exception)
		{
			return StatusCode(500, _invalidOperation);
		}
	}

	[HttpDelete("/delete")]
	public IActionResult Delete([FromQuery] String path, [FromQuery] String keyword)
	{
		try
		{
			if (!IsValidKeyword(keyword)) return BadRequest(_invalidOperation);
			if (String.IsNullOrWhiteSpace(path)) return BadRequest(_invalidOperation);

			String fullPath = Path.GetFullPath(Path.Combine(RootPath, path));

			if (!IsPathInsideRoot(fullPath)) return BadRequest(_invalidOperation);

			if (!System.IO.File.Exists(fullPath))
				return NotFound("File not found");

			System.IO.File.Delete(fullPath);
			return Ok(_successOperation);
		}
		catch (Exception)
		{
			return StatusCode(500, _invalidOperation);
		}
	}
}
