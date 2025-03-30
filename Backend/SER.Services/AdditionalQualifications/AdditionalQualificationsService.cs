using SER.Domain.AdditionalQualifications;
using SER.Domain.Services;
using SER.Services.AdditionalQualifications.Repositories;
using SER.Tools.Types.IDs;
using SER.Tools.Types.Results;

namespace SER.Services.AdditionalQualifications;

public class AdditionalQualificationsService(IAdditionalQualificationsRepository additionalQualificationsRepository)
	: IAdditionalQualificationsService
{
	public async Task<Result> Save(AdditionalQualificationBlank blank)
	{
		if (String.IsNullOrWhiteSpace(blank.Name))
		{
			return Result.Fail("Укажите наименование специальности");
		}

		if (String.IsNullOrWhiteSpace(blank.Code))
		{
			return Result.Fail("Укажите код");
		}

		blank.Id ??= ID.New();

		return await additionalQualificationsRepository.Save(blank);
	}

	public async Task<Result> Remove(ID id)
	{
		return await additionalQualificationsRepository.Remove(id);
	}

	public async Task<AdditionalQualification?> Get(ID id)
	{
		return await additionalQualificationsRepository.Get(id);
	}

	public async Task<AdditionalQualification[]> GetPage(Int32 page, Int32 pageSize)
	{
		return await additionalQualificationsRepository.GetPage(page, pageSize);
	}

	public async Task<AdditionalQualification[]> GetBySearchText(String searchText)
	{
		return await additionalQualificationsRepository.GetBySearchText(searchText);
	}
}