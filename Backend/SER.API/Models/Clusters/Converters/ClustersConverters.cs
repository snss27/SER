using SER.Domain.Clusters;

namespace SER.API.Models.Clusters.Converters;

public static class ClustersConverters
{
	public static ClusterDto ToDto(this Cluster cluster)
	{
		return new ClusterDto(cluster.Id, cluster.Name);
	}
}
