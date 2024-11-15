using SER.Domain.Curators;
using SER.Domain.Groups;
using SER.Domain.Groups.Converters;
using SER.Domain.Services;
using SER.Domain.Specialities;
using SER.Services.Curators.Models;
using SER.Services.Groups.Repositories;
using SER.Services.Specialities.Models;
using SER.Tools.Types.IDs;
using SER.Tools.Types.Results;

namespace SER.Services.Groups;
public class GroupsService : IGroupsService
{
	private readonly IGroupsRepository _groupsRepository;
	private readonly ISpecialitiesService _specialitiesService;
	private readonly ICuratorsService _curatorsService;

	public GroupsService(IGroupsRepository groupsRepository, ISpecialitiesService specialitiesService, ICuratorsService curatorsService)
	{
		_groupsRepository = groupsRepository;
		_specialitiesService = specialitiesService;
		_curatorsService = curatorsService;
	}

	public async Task<Result> Save(GroupBlank blank)
	{
		if (String.IsNullOrWhiteSpace(blank.Number)) return Result.Fail("Введите номер группы");

		blank.Number = blank.Number.Trim();

		if (!Int32.TryParse(blank.Number, out Int32 id)) return Result.Fail("Номер группы должен быть целым числом");

		if (blank.Number.Length != 5) return Result.Fail("Номер группы должен состоять из 5 цифр");

		if (blank.StructuralUnit is null) return Result.Fail("Выберите струкрутное подразделение");

		if (blank.EnrollmentYear is null) return Result.Fail("Выберите год поступления");

		blank.Id ??= ID.New();

		return await _groupsRepository.Save(blank);
	}

	public async Task<Result> Remove(ID id)
	{
		return await _groupsRepository.Remove(id);
	}

	public async Task<GroupDto?> Get(ID id)
	{
		Group? group = await _groupsRepository.Get(id);
		if (group is null) return null;

		Curator? curator = group.CuratorId is null ? null : await _curatorsService.Get(group.CuratorId.Value);
		Speciality? speciality = group.SpecialityId is null ? null : await _specialitiesService.Get(group.SpecialityId.Value);

		return group.ToGroupDto(speciality, curator);
	}

	public async Task<GroupDto[]> GetPage(Int32 page, Int32 pageSize)
	{
		Group[] groups = await _groupsRepository.GetPage(page, pageSize);

		ID[] curatorIds = groups.Where(group => group.CuratorId is not null).Select(group => group.CuratorId!.Value).ToArray();
		ID[] specialityIds = groups.Where(group => group.SpecialityId is not null).Select(group => group.SpecialityId!.Value).ToArray();

		//REFACTORING написать обёртку? Тут есть неплохой (вроде) вариант https://dev.to/serhii_korol_ab7776c50dba/the-elegant-way-to-await-multiple-tasks-in-net-11pl
		var curatorsTask = _curatorsService.Get(curatorIds);
		var specialitiesTask = _specialitiesService.Get(specialityIds);

		await Task.WhenAll(curatorsTask, specialitiesTask);

		Curator[] curators = await curatorsTask;
		Speciality[] specialities = await specialitiesTask;

		return groups.ToGroupDtos(specialities, curators);
	}
}

