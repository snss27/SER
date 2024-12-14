using SER.Configurator.Connectors;
using SER.Domain.Clusters;
using SER.Services._base;
using SER.Services.Clusters.Converters;
using SER.Services.Clusters.Models;
using SER.Services.Clusters.Repositories.Queries;
using SER.Tools.DataBase;
using SER.Tools.DataBase.Query;
using SER.Tools.Types.IDs;
using SER.Tools.Types.Results;
using static SER.Tools.Utils.NumberUtils;

namespace SER.Services.Clusters.Repositories;
public class ClustersRepository(MainConnector connector) : BaseRepository(connector), IClustersRepository
{
	public async Task<Result> Save(ClusterBlank blank)
	{
		Query query = _connector.CreateQuery(Sql.Clusters_Save);
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
		Query query = _connector.CreateQuery(Sql.Clusters_Remove);
		{
			query.Add(id);
			query.Add(DateTime.UtcNow, "p_currentdatetimeutc");
		}

		await using IAsyncSeparatelySession session = await _connector.CreateAsyncSession();

		await session.Execute(query);
		return Result.Success();
	}

	public async Task<Cluster?> Get(ID id)
	{
		Query query = _connector.CreateQuery(Sql.Clusters_Get);
		{
			query.Add(id);
		}

		await using IAsyncSeparatelySession session = await _connector.CreateAsyncSession();

		return (await session.Get<ClusterDB?>(query))?.ToCluster();
	}

	public async Task<Cluster[]> GetPage(Int32 page, Int32 pageSize)
	{
		Query query = _connector.CreateQuery(Sql.Clusters_GetPage);
		{
			(Int32 offset, Int32 limit) = NormalizeRange(page, pageSize);
			query.Add(offset);
			query.Add(limit);
		}

		await using IAsyncSeparatelySession session = await _connector.CreateAsyncSession();

		return (await session.GetArray<ClusterDB>(query)).ToClusters();
	}
}
