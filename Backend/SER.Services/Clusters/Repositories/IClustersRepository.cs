using SER.Domain.Clusters;
using SER.Tools.Types.IDs;
using SER.Tools.Types.Results;

namespace SER.Services.Clusters.Repositories;
public interface IClustersRepository
{
	public Task<OperationResult> Save(ClusterBlank blank);
	public Task<OperationResult> Remove(ID id);
	public Task<Cluster?> Get(ID id);
	public Task<Cluster[]> Get(ID[] ids);
	public Task<Cluster[]> GetPage(Int32 page, Int32 pageSize);
	public Task<Cluster[]> Get(String searchText);
}
