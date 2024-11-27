using SER.Domain.EducationLevels;
using SER.Domain.Services;
using SER.Services.EducationLevels.Repositories;
using SER.Tools.Types.IDs;
using SER.Tools.Types.Results;

namespace SER.Services.EducationLevels;
public class EducationLevelsService : IEducationLevelsService
{
	private readonly IEducationLevelsRepository _educationLevelsRepository;

	public EducationLevelsService(IEducationLevelsRepository educationLevelsRepository)
	{
		_educationLevelsRepository = educationLevelsRepository;
	}

	public async Task<Result> Save(EducationLevelBlank blank)
	{
		if (blank.Type is null) return Result.Fail("Укажите тип уровня образования");

		if (String.IsNullOrWhiteSpace(blank.Name)) return Result.Fail("Укажите наименование уровня образования");

		if (String.IsNullOrWhiteSpace(blank.Code)) return Result.Fail("Укажите код уровня образования");

		blank.Id ??= ID.New();

		return await _educationLevelsRepository.Save(blank);
	}

	public async Task<Result> Remove(ID id)
	{
		return await _educationLevelsRepository.Remove(id);
	}

	public async Task<EducationLevel?> Get(ID id)
	{
		return await _educationLevelsRepository.Get(id);
	}

	public async Task<EducationLevel[]> Get(ID[] ids)
	{
		return await _educationLevelsRepository.Get(ids);
	}

	public async Task<EducationLevel[]> GetPage(Int32 page, Int32 pageSize)
	{
		return await _educationLevelsRepository.GetPage(page, pageSize);
	}

	public async Task<EducationLevel[]> Get(String searchText)
	{
		return await _educationLevelsRepository.Get(searchText);
	}
}
