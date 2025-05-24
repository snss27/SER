using SER.Database.Models.Clusters;
using SER.Domain.Clusters;

namespace SER.Services.Clusters.Converters;
public static class ClusterExtensions
{
	public static ClusterEntity ToEntity(this Cluster cluster)
	{
		return new ClusterEntity()
		{
			Id = cluster.Id,
			Name = cluster.Name,
			CreatedDateTimeUtc = DateTime.UtcNow,
			ModifiedDateTimeUtc = null
		};
	}

	public static void ApplyChanges(this ClusterEntity entity, Cluster cluster)
	{
		entity.Name = cluster.Name;
		entity.ModifiedDateTimeUtc = DateTime.UtcNow;
	}

	public static Cluster ToDomain(this ClusterEntity entity)
	{
		return Cluster.Create(entity.Id, entity.Name).Value;
	}
}
