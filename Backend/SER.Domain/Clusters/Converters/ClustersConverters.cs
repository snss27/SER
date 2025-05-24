namespace SER.Domain.Clusters.Converters;

public static class ClustersConverters
{
	public static ClusterDto ToDto(this Cluster cluster)
	{
		return new ClusterDto(cluster.Id, cluster.Name);
	}

	public static Cluster ToDomain(this ClusterDto dto)
	{
		return Cluster.Create(dto.Id, dto.Name).Value;
	}
}
