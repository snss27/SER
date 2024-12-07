using SER.Domain.EducationLevels;
using SER.Domain.Services;
using SER.Services.EducationLevels.Repositories;
using SER.Tools.Types.IDs;
using SER.Tools.Types.Results;

namespace SER.Services.EducationLevels;

public class EducationLevelsService(IEducationLevelsRepository educationLevelsRepository) : IEducationLevelsService
{
	#region EducationLevels

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
		return await educationLevelsRepository.Remove(id);
	}

	public async Task<EducationLevel?> Get(ID id)
	{
		return await educationLevelsRepository.Get(id);
	}

	public async Task<EducationLevel[]> Get(ID[] ids)
	{
		if (ids.Length == 0)
		{
			return [];
		}

		return await educationLevelsRepository.Get(ids);
	}

	public async Task<EducationLevel[]> GetPage(Int32 page, Int32 pageSize)
	{
		return await educationLevelsRepository.GetPage(page, pageSize);
	}

	#endregion

	#region Specialities

	public async Task<EducationLevel?> GetSpeciality(ID? id)
	{
		if (id is null)
		{
			return null;
		}

		return await educationLevelsRepository.GetSpeciality(id.Value);
	}

	public async Task<EducationLevel[]> GetSpecialities(ID[] ids)
	{
		return await educationLevelsRepository.GetSpecialities(ids);
	}

	public async Task<EducationLevel[]> GetSpecialities(String searchText)
	{
		return await educationLevelsRepository.GetSpecialities(searchText);
	}

	#endregion
}