using SER.Domain.EducationLevels;
using SER.Domain.Groups;
using SER.Domain.Services;
using SER.Services.EducationLevels.Repositories;
using SER.Services.Groups.Repositories;
using SER.Tools.Types.IDs;
using SER.Tools.Types.Results;

namespace SER.Services.EducationLevels;

public class EducationLevelsService(
	IEducationLevelsRepository educationLevelsRepository,
	IGroupsRepository groupsRepository
	) : IEducationLevelsService
{
	public async Task<Result> Save(EducationLevelBlank blank)
	{
		if (blank.Type is null)
		{
			return Result.Fail("Укажите тип уровня образования");
		}

		if (String.IsNullOrWhiteSpace(blank.Name))
		{
			return Result.Fail("Укажите наименование уровня образования");
		}

		if (String.IsNullOrWhiteSpace(blank.Code))
		{
			return Result.Fail("Укажите код уровня образования");
		}

		blank.Id ??= ID.New();

		return await educationLevelsRepository.Save(blank);
	}

	public async Task<Result> Remove(ID id)
	{
		Group[] groups = await groupsRepository.GetByEducationLevelId(id);
		if (groups.Length > 0) return Result.Fail("Невозможно удалить, т.к. существуют группы с данным уровнем образования");

		return await educationLevelsRepository.Remove(id);
	}

	public async Task<EducationLevel?> Get(ID id)
	{
		return await educationLevelsRepository.Get(id);
	}

	public async Task<EducationLevel[]> Get(ID[] ids)
	{
		return await educationLevelsRepository.Get(ids);
	}

	public async Task<EducationLevel[]> GetPage(Int32 page, Int32 pageSize)
	{
		return await educationLevelsRepository.GetPage(page, pageSize);
	}

	public async Task<EducationLevel[]> Get(String searchText)
	{
		return await educationLevelsRepository.Get(searchText);
	}
}