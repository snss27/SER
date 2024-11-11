using SER.Configurator.Connectors;
using SER.Domain.Groups;
using SER.Services._base;
using SER.Services.Groups.Converters;
using SER.Services.Groups.Models;
using SER.Services.Groups.Repositories.Queries;
using SER.Services.Specialities.Models;
using SER.Tools.DataBase;
using SER.Tools.DataBase.Query;
using SER.Tools.Types.IDs;
using SER.Tools.Types.Results;

namespace SER.Services.Groups.Repositories;
public class GroupsRepository : BaseRepository, IGroupsRepository
{
	public GroupsRepository(MainConnector connector) : base(connector) { }

	public async Task<Result> Save(GroupBlank blank)
	{
		Query query = _connector.CreateQuery(Sql.Groups_Save);
		{
			query.Add(blank.Id);
			query.Add(blank.Number);
			query.Add(blank.StructuralUnit);
			query.Add(blank.SpecialityId);
			query.Add(blank.EnrollmentYear);
			query.Add(blank.CuratorId);
			query.Add(DateTime.UtcNow, "p_currentdatetimeutc");
		}

		await using IAsyncSeparatelySession session = await _connector.CreateAsyncSession();

		await session.Execute(query);

		return Result.Success();
	}

	public async Task<Result> Remove(ID id)
	{
		Query query = _connector.CreateQuery(Sql.Groups_Remove);
		{
			query.Add(id);
			query.Add(DateTime.UtcNow, "p_currentdatetimeutc");
		}

		await using IAsyncSeparatelySession session = await _connector.CreateAsyncSession();

		await session.Execute(query);

		return Result.Success();
	}

	public async Task<Group> Get(ID id)
	{
		Query query = _connector.CreateQuery(Sql.Groups_Get);
		{
			query.Add(id);
		}

		await using IAsyncSeparatelySession session = await _connector.CreateAsyncSession();

		return (await session.Get<GroupDB>(query)).ToGroup();
	}

	public async Task<Group[]> GetAll()
	{
		Query query = _connector.CreateQuery(Sql.Groups_GetAll);

		await using IAsyncSeparatelySession session = await _connector.CreateAsyncSession();

		return (await session.GetArray<GroupDB>(query)).ToGroups();
	}
}
