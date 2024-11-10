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

		if (blank.StudyYears is null) return Result.Fail("Укажите количество лет для обучения");
		if (blank.StudyYears > 10) return Result.Fail("Количество лет для обучение не может быть больше 10 лет");

		blank.Id ??= ID.New();

		return await _specialitiesRepository.Save(blank);
	}

	public async Task<Result> Remove(ID id)
	{
		return await _specialitiesRepository.Remove(id);
	}

	public async Task<Speciality> Get(ID id)
	{
		return await _specialitiesRepository.Get(id);
	}

	public async Task<Speciality[]> GetAll()
	{
		return await _specialitiesRepository.GetAll();
	}
}
