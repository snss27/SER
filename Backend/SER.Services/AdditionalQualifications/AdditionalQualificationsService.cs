using System.Numerics;
using SER.Domain.AdditionalQualifications;
using SER.Domain.Services;
using SER.Services.AdditionalQualifications.Repositories;
using SER.Services.Specialities.Repositories;
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
		if (String.IsNullOrWhiteSpace(blank.Name)) return Result.Fail("Укажите название");

		if (String.IsNullOrWhiteSpace(blank.Code)) return Result.Fail("Укажите код");

		if (blank.StudyYears is not null &&  blank.StudyYears > 10) return Result.Fail("Количество лет обучение не может быть больше 10 лет");

		if (blank.StudyMonths is null) return Result.Fail("Укажите количество месяцев обучения");
		if (blank.StudyMonths > 12) return Result.Fail("Количество месяцев обучения не может быть больше 12");

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
