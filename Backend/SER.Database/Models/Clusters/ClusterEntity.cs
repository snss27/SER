using SER.Database.Models.ConfigurationTools;

namespace SER.Database.Models.Clusters;

public class ClusterEntity : BaseEntity
{
	public String Name { get; set; } = default!;
}
