using SER.Configurator.Connectors;
using SER.Domain.Groups;
using SER.Services._base;
using SER.Services.Groups.Converters;
using SER.Services.Groups.Models;
using SER.Services.Groups.Repositories.Queries;
using SER.Tools.DataBase;
using SER.Tools.DataBase.Query;
using SER.Tools.Types.IDs;
using SER.Tools.Types.Results;
using static SER.Tools.Utils.NumberUtils;

namespace SER.Services.Groups.Repositories;

public class GroupsRepository(MainConnector connector) : BaseRepository(connector), IGroupsRepository
{
	public async Task<OperationResult> Save(GroupBlank blank)
	{
		Query query = _connector.CreateQuery(Sql.Groups_Save);
		{
			query.Add(blank.Id);
			query.Add(blank.Number);
			query.Add(blank.StructuralUnit);
			query.Add(blank.EducationLevel?.Id, "p_educationlevelid");
			query.Add(blank.EnrollmentYear);
			query.Add(blank.Curator?.Id, "p_curatorid");
			query.Add(blank.HasCluster);
			query.Add(blank.Cluster?.Id, "p_clusterid");
			query.Add(DateTime.UtcNow, "p_currentdatetimeutc");
		}

		await using IAsyncSeparatelySession session = await _connector.CreateAsyncSession();

		await session.Execute(query);

		return OperationResult.Success();
	}

	public async Task<OperationResult> Remove(ID id)
	{
		Query query = _connector.CreateQuery(Sql.Groups_Remove);
		{
			query.Add(id);
			query.Add(DateTime.UtcNow, "p_currentdatetimeutc");
		}

		await using IAsyncSeparatelySession session = await _connector.CreateAsyncSession();

		await session.Execute(query);

		return OperationResult.Success();
	}

	public async Task<Group?> Get(ID id)
	{
		Query query = _connector.CreateQuery(Sql.Groups_Get);
		{
			query.Add(id);
		}

		await using IAsyncSeparatelySession session = await _connector.CreateAsyncSession();

		return (await session.Get<GroupDB?>(query))?.ToGroup();
	}

	public async Task<Group[]> Get(ID[] ids)
	{
		Query query = _connector.CreateQuery(Sql.Groups_GetByIds);
		{
			query.Add(ids);
		}

		await using IAsyncSeparatelySession session = await _connector.CreateAsyncSession();

		return (await session.GetArray<GroupDB>(query)).ToGroups();
	}

	public async Task<Group[]> GetPage(Int32 page, Int32 pageSize)
	{
		Query query = _connector.CreateQuery(Sql.Groups_GetPage);
		{
			(Int32 offset, Int32 limit) = NormalizeRange(page, pageSize);
			query.Add(offset);
			query.Add(limit);
		}

		await using IAsyncSeparatelySession session = await _connector.CreateAsyncSession();

		return (await session.GetArray<GroupDB>(query)).ToGroups();
	}

	public async Task<Group[]> GetBySearchText(String searchText)
	{
		Query query = _connector.CreateQuery(Sql.Groups_GetBySearchText);
		{
			query.Add(searchText);
		}

		await using IAsyncSeparatelySession session = await _connector.CreateAsyncSession();

		return (await session.GetArray<GroupDB>(query)).ToGroups();
	}

	public async Task<Group[]> GetByEducationLevelId(ID educationLevelId)
	{
		Query query = _connector.CreateQuery(Sql.Groups_GetByEducationLevelId);
		{
			query.Add(educationLevelId);
		}

		await using IAsyncSeparatelySession session = await _connector.CreateAsyncSession();

		return (await session.GetArray<GroupDB>(query)).ToGroups();
	}
}