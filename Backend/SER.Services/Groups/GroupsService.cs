using SER.Domain.Clusters;
using SER.Domain.EducationLevels;
using SER.Domain.Employees;
using SER.Domain.Groups;
using SER.Domain.Groups.Converters;
using SER.Domain.Services;
using SER.Domain.Students;
using SER.Services.Groups.Repositories;
using SER.Tools.Types.IDs;
using SER.Tools.Types.Results;
using SER.Tools.Utils;

namespace SER.Services.Groups;

public class GroupsService(
	IGroupsRepository groupsRepository,
	IEducationLevelsService educationLevelsService,
	IEmployeesService employeesService,
	IClustersService clustersService,
	IStudentsService studentsSevice
) : IGroupsService
{
	public async Task<Result> Save(GroupBlank blank)
	{
		if (String.IsNullOrWhiteSpace(blank.Number))
		{
			return Result.Fail("Укажите номер группы");
		}

		if(blank.EducationLevelId is null)
		{
			return Result.Fail("Укажите уровень образования у группы");
		}

		if (!Regexs.GroupNumberRegex.IsMatch(blank.Number))
		{
			return Result.Fail("Номер группы должен быть целым пятизначным числом");
		}

		if (blank.StructuralUnit is null)
		{
			return Result.Fail("Укажите струкрутное подразделение");
		}

		if (blank.EnrollmentYear is null)
		{
			return Result.Fail("Укажите год поступления");
		}

		if (blank.HasCluster && blank.ClusterId is null)
		{
			return Result.Fail("Укажите кластер группы");
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
		Student[] students = await studentsSevice.GetByGroupId(id);
		if (students.Length > 0)
		{
			return Result.Fail("У этой группе есть привязанные студенты");
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
		Group[] groups = await groupsRepository.GetBySeacrhText(searchText);
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

	public async Task<Group[]> GetByEducationLevelId(ID educationLevelId)
	{
		return await groupsRepository.GetByEducationLevelId(educationLevelId);
	}
}