using SER.Domain.Clusters;
using SER.Services.Clusters.Models;

namespace SER.Services.Clusters.Converters;
public static class ClustersConverter
{
	public static Cluster ToCluster(this ClusterDB db)
	{
		return new Cluster(db.Id, db.Name);
	}

	public static Cluster[] ToClusters(this ClusterDB[] dbs)
	{
		return dbs.Select(ToCluster).ToArray();
	}
}
