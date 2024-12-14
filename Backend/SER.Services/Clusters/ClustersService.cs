using SER.Domain.Clusters;
using SER.Domain.Services;
using SER.Services.Clusters.Repositories;
using SER.Tools.Types.IDs;
using SER.Tools.Types.Results;

namespace SER.Services.Clusters;
public class ClustersService(IClustersRepository clustersRepository) : IClustersService
{
	public async Task<Result> Save(ClusterBlank blank)
	{
		if (String.IsNullOrWhiteSpace(blank.Name)) return Result.Fail("Укажите наименование кластера");

		blank.Id ??= ID.New();

		return await clustersRepository.Save(blank);
	}

	public async Task<Result> Remove(ID id)
	{
		return await clustersRepository.Remove(id);
	}

	public async Task<Cluster?> Get(ID id)
	{
		return await clustersRepository.Get(id);
	}

	public async Task<Cluster[]> GetPage(Int32 page, Int32 pageSize)
	{
		return await clustersRepository.GetPage(page, pageSize);
	}
}
