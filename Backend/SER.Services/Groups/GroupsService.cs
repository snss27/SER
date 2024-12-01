using SER.Domain.EducationLevels;
using SER.Domain.Employees;
using SER.Domain.Groups;
using SER.Domain.Groups.Converters;
using SER.Domain.Services;
using SER.Services.Groups.Repositories;
using SER.Tools.Types.IDs;
using SER.Tools.Types.Results;
using SER.Tools.Utils;

namespace SER.Services.Groups;

public class GroupsService(
	IGroupsRepository groupsRepository,
	IEducationLevelsService educationLevelsService,
	IEmployeesService employeesService
) : IGroupsService
{
	public async Task<Result> Save(GroupBlank blank)
	{
		if (String.IsNullOrWhiteSpace(blank.Number))
		{
			return Result.Fail("Введите номер группы");
		}

		if (!Regexs.GroupNumberRegex.IsMatch(blank.Number))
		{
			return Result.Fail("Номер группы должен быть целым пятизначным числом");
		}

		if (blank.StructuralUnit is null)
		{
			return Result.Fail("Выберите струкрутное подразделение");
		}

		if (blank.EnrollmentYear is null)
		{
			return Result.Fail("Выберите год поступления");
		}

		blank.Id ??= ID.New();

		return await groupsRepository.Save(blank);
	}

	public async Task<Result> Remove(ID id)
	{
		return await groupsRepository.Remove(id);
	}


	public async Task<GroupDto?> Get(ID id)
	{
		Group? group = await groupsRepository.Get(id);
		if (group is null)
		{
			return null;
		}

		Task<Employee?> curatorTask = employeesService.Get(group.CuratorId.Value);
		Task<EducationLevel?> educationLevelTask = educationLevelsService.Get(group.EducationLevelId.Value);

		await Task.WhenAll(curatorTask, educationLevelTask);

		Employee? curator = await curatorTask;
		EducationLevel? educationLevel = await educationLevelTask;

		return group.ToGroupDto(educationLevel, curator);
	}

	public async Task<GroupDto[]> GetPage(Int32 page, Int32 pageSize)
	{
		Group[] groups = await groupsRepository.GetPage(page, pageSize);

		ID[] curatorIds = groups.Where(group => group.CuratorId is not null).Select(group => group.CuratorId.Value)
			.ToArray();
		ID[] educationLevelIds = groups.Where(group => group.EducationLevelId is not null)
			.Select(group => group.EducationLevelId.Value).ToArray();

		//REFACTORING написать обёртку? Тут есть неплохой (вроде) вариант https://dev.to/serhii_korol_ab7776c50dba/the-elegant-way-to-await-multiple-tasks-in-net-11pl
		Task<Employee[]> curatorsTask = employeesService.Get(curatorIds);
		Task<EducationLevel[]> educationLevelsTask = educationLevelsService.Get(educationLevelIds);

		await Task.WhenAll(curatorsTask, educationLevelsTask);

		Employee[] curators = await curatorsTask;
		EducationLevel[] educationLevels = await educationLevelsTask;

		return groups.ToGroupDtos(educationLevels, curators);
	}
}