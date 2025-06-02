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
			CreatedDateTimeUtc = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Unspecified),
			ModifiedDateTimeUtc = null
		};
	}

	public static void ApplyChanges(this ClusterEntity entity, Cluster cluster)
	{
		entity.Name = cluster.Name;
		entity.ModifiedDateTimeUtc = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Unspecified);
	}

	public static Cluster ToDomain(this ClusterEntity entity)
	{
		return Cluster.Create(entity.Id, entity.Name).Value;
	}
}
