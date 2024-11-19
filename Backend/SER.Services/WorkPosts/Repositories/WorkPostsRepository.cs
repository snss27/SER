using SER.Configurator.Connectors;
using SER.Domain.WorkPosts;
using SER.Services._base;
using SER.Services.WorkPosts.Converters;
using SER.Services.WorkPosts.Models;
using SER.Services.WorkPosts.Repositories.Queries;
using SER.Tools.DataBase;
using SER.Tools.DataBase.Query;
using SER.Tools.Types.IDs;
using SER.Tools.Types.Results;
using static SER.Tools.Utils.NumberUtils;

namespace SER.Services.WorkPosts.Repositories;
public class WorkPostsRepository : BaseRepository, IWorkPostsRepository
{
	public WorkPostsRepository(MainConnector connector) : base(connector) { }

	public async Task<Result> Save(WorkPostBlank blank)
	{
		Query query = _connector.CreateQuery(Sql.WorkPosts_Save);
		{
			query.Add(blank.Id);
			query.Add(blank.Name);
			query.Add(DateTime.UtcNow, "p_currentdatetimeutc");
		}

		await using IAsyncSeparatelySession session = await _connector.CreateAsyncSession();

		await session.Execute(query);

		return Result.Success();
	}

	public async Task<Result> Remove(ID id)
	{
		Query query = _connector.CreateQuery(Sql.WorkPosts_Remove);
		{
			query.Add(id);
			query.Add(DateTime.UtcNow, "p_currentdatetimeutc");
		}

		await using IAsyncSeparatelySession session = await _connector.CreateAsyncSession();

		await session.Execute(query);

		return Result.Success();
	}

	public async Task<WorkPost?> Get(ID id)
	{
		Query query = _connector.CreateQuery(Sql.WorkPosts_Get);
		{
			query.Add(id);
		}

		await using IAsyncSeparatelySession session = await _connector.CreateAsyncSession();

		return (await session.Get<WorkPostDB?>(query))?.ToWorkPost();
	}

	public async Task<WorkPost[]> GetPage(Int32 page, Int32 pageSize)
	{
		Query query = _connector.CreateQuery(Sql.WorkPosts_GetPage);
		{
			(Int32 offset, Int32 limit) = NormalizeRange(page, pageSize);
			query.Add(offset);
			query.Add(limit);
		}

		await using IAsyncSeparatelySession session = await _connector.CreateAsyncSession();

		return (await session.GetArray<WorkPostDB>(query)).ToWorkPosts();
	}
}
