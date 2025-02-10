using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using SER.Domain.Services;
using SER.Tools.Types.IDs;
using SER.Tools.Types.Types.Files;

namespace SER.Services.Files;
public class FilesService(IWebHostEnvironment hostingEnvironment) : IFilesService
{
	private const String PHOTO_DIRECTORY = "Img";
	private const String GROUPS_DIRECTORY = $"{PHOTO_DIRECTORY}\\Groups";
	
	private String SavePhoto(IFormFile photo, String directory)
	{
		String uploadFolder = Path.Combine(hostingEnvironment.WebRootPath, directory);
		if (!Directory.Exists(uploadFolder)) Directory.CreateDirectory(uploadFolder);

		String extension = Path.GetExtension(photo.FileName);
		String photoName = $"{ID.New()}-{ID.New()}{extension}";

		String filePath = Path.Combine(uploadFolder, photoName);
		String shortFilePath = Path.Combine(directory, photoName);

		File.WriteAllBytes(filePath, photo.ToBytes());

		return shortFilePath;
	}
	
	private String[] SavePhotos(IFormFile[] photos, String directory)
	{
		String uploadFolder = Path.Combine(hostingEnvironment.WebRootPath, directory);
		if (!Directory.Exists(uploadFolder)) Directory.CreateDirectory(uploadFolder);

		String[] filePaths = photos.Select(photo =>
		{
			String extension = Path.GetExtension(photo.FileName);
			String photoName = $"{ID.New()}-{ID.New()}{extension}";

			String filePath = Path.Combine(uploadFolder, photoName);
			String shortFilePath = Path.Combine(directory, photoName);

			File.WriteAllBytes(filePath, photo.ToBytes());

			return shortFilePath;

		}).ToArray();

		return filePaths;
	}

	public String SaveStudentPhoto(IFormFile photo, String groupAlias, String studentAlias)
	{
		String directory = Path.Combine(GROUPS_DIRECTORY, groupAlias, studentAlias);
		return SavePhoto(photo, directory);
	}

	public String[] SaveStudentPhotos(IFormFile[] photos, String groupAlias, String studentAlias)
	{
		String directory = Path.Combine(GROUPS_DIRECTORY, groupAlias, studentAlias);
		return SavePhotos(photos, directory);
	}
}
