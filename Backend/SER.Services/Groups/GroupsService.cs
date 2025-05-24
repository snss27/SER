using SER.Domain.Clusters;
using SER.Domain.EducationLevels;
using SER.Domain.Employees;
using SER.Domain.Groups;
using SER.Domain.Groups.Converters;
using SER.Domain.Services;
using SER.Domain.Students;
using SER.Services.Groups.Repositories;
using SER.Services.Students.Repositories;
using SER.Tools.Types.IDs;
using SER.Tools.Types.Results;
using SER.Tools.Utils;

namespace SER.Services.Groups;

public class GroupsService(
	IGroupsRepository groupsRepository,
	IEducationLevelsService educationLevelsService,
	IEmployeesService employeesService,
	IClustersService clustersService,
	IStudentsRepository studentsRepository
) : IGroupsService
{
	public async Task<OperationResult> Save(GroupBlank blank)
	{
		if (String.IsNullOrWhiteSpace(blank.Number))
		{
			return OperationResult.Fail("Укажите номер группы");
		}

		if (!Regexs.GroupNumberRegex.IsMatch(blank.Number))
		{
			return OperationResult.Fail("Номер группы должен быть целым пятизначным числом");
		}

		if (blank.StructuralUnit is null)
		{
			return OperationResult.Fail("Укажите струкрутное подразделение");
		}

		if (blank.EducationLevel is null)
		{
			return OperationResult.Fail("Укажите уровень образования у группы");
		}

		if (blank.EnrollmentYear is null)
		{
			return OperationResult.Fail("Укажите год поступления");
		}

		if (blank.HasCluster && blank.Cluster is null)
		{
			return OperationResult.Fail("Укажите кластер группы");
		}

		if (!blank.HasCluster)
		{
			blank.Cluster = null;
		}

		blank.Id ??= ID.New();

		return await groupsRepository.Save(blank);
	}

	public async Task<OperationResult> Remove(ID id)
	{
		Student[] students = await studentsRepository.GetByGroupId(id);
		if (students.Length > 0)
		{
			return OperationResult.Fail("Невозможно удалить, т.к. у этой группы есть привязанные студенты");
		}

		return await groupsRepository.Remove(id);
	}


	public async Task<GroupDto?> Get(ID id)
	{
		Group? group = await groupsRepository.Get(id);
		if (group is null)
		{
			return null;
		}

		Task<EducationLevel?> educationLevelTask = educationLevelsService.Get(group.EducationLevelId);
		Task<Employee?> curatorTask = group.CuratorId.HasValue
			? employeesService.Get(group.CuratorId.Value)
			: Task.FromResult<Employee?>(null);
		Task<Cluster?> clusterTask = group.ClusterId.HasValue
			? clustersService.Get(group.ClusterId.Value)
			: Task.FromResult<Cluster?>(null);

		await Task.WhenAll(educationLevelTask, curatorTask, clusterTask);

		EducationLevel? educationLevel = await educationLevelTask;
		Employee? curator = await curatorTask;
		Cluster? cluster = await clusterTask;

		return group.ToGroupDto(
			educationLevel ?? throw new InvalidOperationException("Education level not found"),
			curator,
			cluster
		);
	}

	public async Task<GroupDto[]> Get(ID[] ids)
	{
		Group[] groups = await groupsRepository.Get(ids);

		return await GroupDtos(groups);
	}

	public async Task<GroupDto[]> GetPage(Int32 page, Int32 pageSize)
	{
		Group[] groups = await groupsRepository.GetPage(page, pageSize);
		if(groups.Length == 0)
		{
			return [];
		}

		return await GroupDtos(groups);
	}

	public async Task<GroupDto[]> GetBySearchText(String searchText)
	{
		Group[] groups = await groupsRepository.GetBySearchText(searchText);
		if(groups.Length == 0)
		{
			return [];
		}

		return await GroupDtos(groups);
	}

	private async Task<GroupDto[]> GroupDtos(Group[] groups)
	{
		ID[] curatorIds = groups
	.Where(group => group.CuratorId is not null)
	.Select(group => group.CuratorId.Value)
	.ToArray();
		ID[] educationLevelIds = groups
			.Select(group => group.EducationLevelId)
			.ToArray();
		ID[] clusterIds = groups
			.Where(group => group.ClusterId is not null)
			.Select(group => group.ClusterId.Value)
			.ToArray();

		Task<EducationLevel[]> educationLevelsTask = educationLevelsService.Get(educationLevelIds);
		Task<Employee[]> curatorsTask = curatorIds.Length > 0 ? employeesService.Get(curatorIds) : Task.FromResult<Employee[]>([]);
		Task<Cluster[]> clustersTask = clusterIds.Length > 0 ? clustersService.Get(clusterIds) : Task.FromResult<Cluster[]>([]);

		await Task.WhenAll(curatorsTask, educationLevelsTask, clustersTask);

		Employee[] curators = await curatorsTask;
		EducationLevel[] educationLevels = await educationLevelsTask;
		Cluster[] clusters = await clustersTask;

		return groups.ToGroupDtos(educationLevels, curators, clusters);
	} 
}