using SER.Domain.Clusters;
using SER.Tools.Types.IDs;
using SER.Tools.Types.Results;

namespace SER.Services.Clusters.Repositories;
public interface IClustersRepository
{
	public Task<Result> Save(ClusterBlank blank);
	public Task<Result> Remove(ID id);
	public Task<Cluster?> Get(ID id);
	public Task<Cluster[]> GetPage(Int32 page, Int32 pageSize);
}
