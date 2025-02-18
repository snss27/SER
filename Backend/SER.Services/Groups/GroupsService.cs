using SER.Domain.Clusters;
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
	IEmployeesService employeesService,
	IClustersService clustersService
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

		if (blank.HasCluster && blank.ClusterId is null)
		{
			return Result.Fail("Выберите кластер группы");
		}

		if (!blank.HasCluster)
		{
			blank.ClusterId = null;
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

		Task<Employee?> curatorTask = employeesService.Get(group.CuratorId);
		Task<EducationLevel?> educationLevelTask = educationLevelsService.Get(group.EducationLevelId);
		Task<Cluster?> clusterTask = clustersService.Get(group.ClusterId);

		await Task.WhenAll(curatorTask, educationLevelTask, clusterTask);

		Employee? curator = await curatorTask;
		EducationLevel? educationLevel = await educationLevelTask;
		Cluster? cluster = await clusterTask;

		return group.ToGroupDto(educationLevel, curator, cluster);
	}

	public async Task<GroupDto[]> GetPage(Int32 page, Int32 pageSize)
	{
		Group[] groups = await groupsRepository.GetPage(page, pageSize);

		ID[] curatorIds = groups.Where(group => group.CuratorId is not null).Select(group => group.CuratorId.Value)
			.ToArray();
		ID[] educationLevelIds = groups.Where(group => group.EducationLevelId is not null)
			.Select(group => group.EducationLevelId.Value).ToArray();
		ID[] clusterIds = groups.Where(group => group.ClusterId is not null).Select(group => group.ClusterId.Value)
			.ToArray();

		//REFACTORING написать обёртку? Тут есть неплохой (вроде) вариант https://dev.to/serhii_korol_ab7776c50dba/the-elegant-way-to-await-multiple-tasks-in-net-11pl
		Task<Employee[]> curatorsTask = employeesService.Get(curatorIds);
		Task<EducationLevel[]> educationLevelsTask = educationLevelsService.Get(educationLevelIds);
		Task<Cluster[]> clustersTask = clustersService.Get(clusterIds);

		await Task.WhenAll(curatorsTask, educationLevelsTask, clustersTask);

		Employee[] curators = await curatorsTask;
		EducationLevel[] educationLevels = await educationLevelsTask;
		Cluster[] clusters = await clustersTask;

		return groups.ToGroupDtos(educationLevels, curators, clusters);
	}

	public async Task<GroupDto[]> GetAll()
	{
		Group[] groups = await groupsRepository.GetAll();

		ID[] curatorIds = groups
			.Where(group => group.CuratorId is not null)
			.Select(group => group.CuratorId.Value)
			.Distinct()
			.ToArray();

		ID[] educationLevelIds = groups
			.Where(group => group.EducationLevelId is not null)
			.Select(group => group.EducationLevelId.Value)
			.Distinct()
			.ToArray();

		ID[] clusterIds = groups
			.Where(group => group.ClusterId is not null)
			.Select(group => group.ClusterId.Value)
			.Distinct()
			.ToArray();

		//REFACTORING аналогично https://dev.to/serhii_korol_ab7776c50dba/the-elegant-way-to-await-multiple-tasks-in-net-11pl

		Task<Employee[]> curatorsTask = employeesService.Get(curatorIds);
		Task<EducationLevel[]> educationLevelsTask = educationLevelsService.Get(educationLevelIds);
		Task<Cluster[]> clustersTask = clustersService.Get(clusterIds);

		await Task.WhenAll(curatorsTask, educationLevelsTask, clustersTask);

		Employee[] curators = await curatorsTask;
		EducationLevel[] educationLevels = await educationLevelsTask;
		Cluster[] clusters = await clustersTask;

		return groups.ToGroupDtos(educationLevels, curators, clusters);
	}
}