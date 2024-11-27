using SER.Domain.AdditionalQualifications;
using SER.Domain.Services;
using SER.Services.AdditionalQualifications.Repositories;
using SER.Tools.Types.IDs;
using SER.Tools.Types.Results;

namespace SER.Services.AdditionalQualifications;
public class AdditionalQualificationsService : IAdditionalQualificationsService
{
	private readonly IAdditionalQualificationsRepository _additionalQualificationsRepository;

	public AdditionalQualificationsService(IAdditionalQualificationsRepository additionalQualificationsRepository)
	{
		_additionalQualificationsRepository = additionalQualificationsRepository;
	}

	public async Task<Result> Save(AdditionalQualificationBlank blank)
	{
		if (String.IsNullOrWhiteSpace(blank.Name)) return Result.Fail("Укажите наименование специальности");

		if (String.IsNullOrWhiteSpace(blank.Code)) return Result.Fail("Укажите код");

		blank.Id ??= ID.New();

		return await _additionalQualificationsRepository.Save(blank);
	}

	public async Task<Result> Remove(ID id)
	{
		return await _additionalQualificationsRepository.Remove(id);
	}

	public async Task<AdditionalQualification?> Get(ID id)
	{
		return await _additionalQualificationsRepository.Get(id);
	}

	public async Task<AdditionalQualification[]> GetPage(Int32 page, Int32 pageSize)
	{
		return await _additionalQualificationsRepository.GetPage(page, pageSize);
	}
}
