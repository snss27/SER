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
	public async Task<OperationResult> Save(ClusterBlank blank)
	{
		Query query = _connector.CreateQuery(Sql.Clusters_Save);
		{
			query.Add(blank.Id);
			query.Add(blank.Name);
			query.Add(DateTime.UtcNow, "p_currentdatetimeutc");
		}

		await using IAsyncSeparatelySession session = await _connector.CreateAsyncSession();

		await session.Execute(query);

		return OperationResult.Success();
	}

	public async Task<OperationResult> Remove(ID id)
	{
		Query query = _connector.CreateQuery(Sql.Clusters_Remove);
		{
			query.Add(id);
			query.Add(DateTime.UtcNow, "p_currentdatetimeutc");
		}

		await using IAsyncTransactionSession transaction = await _connector.CreateAsyncTransaction();

		await transaction.Execute(query);

		await RemoveFromGroups(id, transaction);

		return OperationResult.Success();
	}

	private async Task RemoveFromGroups(ID clusterId, IAsyncTransactionSession transaction)
	{
		Query query = _connector.CreateQuery(Sql.Groups_RemoveClusterById);
		{
			query.Add(clusterId);
			query.Add(DateTime.UtcNow, "currentdatetimeutc");
		}

		await transaction.Execute(query);
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

	public async Task<Cluster[]> Get(ID[] ids)
	{
		Query query = _connector.CreateQuery(Sql.Clusters_GetByIds);
		{
			query.Add(ids);
		}

		await using IAsyncSeparatelySession session = await _connector.CreateAsyncSession();

		return (await session.GetArray<ClusterDB>(query)).ToClusters();
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

	public async Task<Cluster[]> Get(String searchText)
	{
		Query query = _connector.CreateQuery(Sql.Clusters_GetBySearchText);
		{
			query.Add(searchText);
		}

		await using IAsyncSeparatelySession session = await _connector.CreateAsyncSession();

		return (await session.GetArray<ClusterDB>(query)).ToClusters();
	}
}
