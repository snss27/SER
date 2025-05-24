using SER.Domain.Clusters;
using SER.Domain.Services;
using SER.Services.Clusters.Repositories;
using SER.Tools.Types.IDs;
using SER.Tools.Types.Results;

namespace SER.Services.Clusters;
public class ClustersService(IClustersRepository clustersRepository) : IClustersService
{
	public async Task<OperationResult> Save(ClusterBlank blank)
	{
		if (String.IsNullOrWhiteSpace(blank.Name)) return OperationResult.Fail("Укажите наименование кластера");

		blank.Id ??= ID.New();

		return await clustersRepository.Save(blank);
	}

	public async Task<OperationResult> Remove(ID id)
	{
		return await clustersRepository.Remove(id);
	}

	public async Task<Cluster?> Get(ID id)
	{
		return await clustersRepository.Get(id);
	}

	public async Task<Cluster[]> Get(ID[] ids)
	{
		return await clustersRepository.Get(ids);
	}

	public async Task<Cluster[]> GetPage(Int32 page, Int32 pageSize)
	{
		return await clustersRepository.GetPage(page, pageSize);
	}

	public async Task<Cluster[]> Get(String searchText)
	{
		return await clustersRepository.Get(searchText);
	}
}
