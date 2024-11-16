using SER.Domain.Services;
using SER.Domain.Specialities;
using SER.Services.Specialities.Repositories;
using SER.Tools.Types.IDs;
using SER.Tools.Types.Results;

namespace SER.Services.Specialities;
public class SpecialitiesService : ISpecialitiesService
{
	private readonly ISpecialitiesRepository _specialitiesRepository;

	public SpecialitiesService(ISpecialitiesRepository specialitiesRepository)
	{
		_specialitiesRepository = specialitiesRepository;
	}

	public async Task<Result> Save(SpecialityBlank blank)
	{
		if (String.IsNullOrWhiteSpace(blank.Name)) return Result.Fail("Укажите название");

		if (String.IsNullOrWhiteSpace(blank.Code)) return Result.Fail("Укажите код");

		if (blank.StudyYears is null) return Result.Fail("Укажите количество лет обучения");
		if (blank.StudyYears > 10) return Result.Fail("Количество лет обучение не может быть больше 10 лет");

		if (blank.StudyMonths is null) return Result.Fail("Укажите количество месяцев обучения");
		if (blank.StudyMonths > 12) return Result.Fail("Количество месяцев обучения не может быть больше 12");

		blank.Id ??= ID.New();

		return await _specialitiesRepository.Save(blank);
	}

	public async Task<Result> Remove(ID id)
	{
		return await _specialitiesRepository.Remove(id);
	}

	public async Task<Speciality?> Get(ID id)
	{
		return await _specialitiesRepository.Get(id);
	}

	public async Task<Speciality[]> Get(ID[] ids)
	{
		return await _specialitiesRepository.Get(ids);
	}

	public async Task<Speciality[]> GetPage(Int32 page, Int32 pageSize)
	{
		return await _specialitiesRepository.GetPage(page, pageSize);
	}

	public async Task<Speciality[]> Get(String searchText)
	{
		return await _specialitiesRepository.Get(searchText);
	}
}
