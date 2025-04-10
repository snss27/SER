using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using SER.Domain.Services;
using SER.Tools.Types.IDs;
using SER.Tools.Types.Types;
using SER.Tools.Types.Types.Files;

namespace SER.Services.Files;
public class FilesService(IWebHostEnvironment hostingEnvironment) : IFilesService
{
	private const String PHOTO_DIRECTORY = "Img";
	private const String GROUPS_DIRECTORY = $"{PHOTO_DIRECTORY}\\Groups";
	private const String WORKBOOK_DIRECTORY = "WorkBook";
	private const String PASSPORT_DIRECTORY = "Passport";
	private const String TARGET_AGREEMENT_DIRECTORY = "TargetAgreement";
	private const String ARMY_DIRECTORY = "Army";
	private const String OTHER_DIRECTORY = "Other";

	private String SaveFile(IFormFile photo, String directory)
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
	
	private String[] SaveFiles(IFormFile[] photos, String directory)
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

	public String[] SaveWorkBookFile(BlankFiles blankFiles, String groupAlias, String studentAlias)
	{
		String directory = Path.Combine(GROUPS_DIRECTORY, groupAlias, studentAlias, WORKBOOK_DIRECTORY);
		return SaveFiles(blankFiles, directory);
	}

	public String[] SavePassportFiles(BlankFiles blankFiles, String groupAlias, String studentAlias)
	{
		String directory = Path.Combine(GROUPS_DIRECTORY, groupAlias, studentAlias, PASSPORT_DIRECTORY);
		return SaveFiles(blankFiles, directory);
	}

	public String[] SaveTargetAfreementFiles(BlankFiles blankFiles, String groupAlias, String studentAlias)
	{
		String directory = Path.Combine(GROUPS_DIRECTORY, groupAlias, studentAlias, TARGET_AGREEMENT_DIRECTORY);
		return SaveFiles(blankFiles, directory);
	}

	public String[] SaveArmySubpoenaFiles(BlankFiles blankFiles, String groupAlias, String studentAlias)
	{
		String directory = Path.Combine(GROUPS_DIRECTORY, groupAlias, studentAlias, ARMY_DIRECTORY);
		return SaveFiles(blankFiles, directory);
	}

	public String[] SaveOtherFiles(BlankFiles blankFiles, String groupAlias, String studentAlias)
	{
		String directory = Path.Combine(GROUPS_DIRECTORY, groupAlias, studentAlias, OTHER_DIRECTORY);
		return SaveFiles(blankFiles, directory);
	}

	private String[] SaveFiles(BlankFiles blankFiles, String directory)
	{
		List<String> urls = new(blankFiles.FileUrls);
		foreach (IFormFile file in blankFiles.Files)
		{
			String fileUrl = SaveFile(file, directory);
			urls.Add(fileUrl);
		}

		return urls.ToArray();
	}
}
