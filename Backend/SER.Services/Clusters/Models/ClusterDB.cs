using SER.Tools.Types.IDs;

namespace SER.Services.Clusters.Models;
public class ClusterDB
{
	public ID Id { get; set; }
	public String Name { get; set; }

	public DateTime CreatedDateTimeUtc { get; set; }
	public DateTime? ModifiedDateTimeUtc { get; set; }
	public Boolean IsRemoved { get; set; }
}
