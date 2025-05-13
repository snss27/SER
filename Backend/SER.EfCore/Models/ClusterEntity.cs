using SER.Tools.Types.IDs;

namespace SER.EfCore.Models;

public class ClusterEntity
{
	public ID Id { get; set; }
	public String Name { get; set; } = String.Empty;

	public DateTime CreatedDateTimeUtc { get; set; }
	public DateTime ModifiedDateTimeUtc { get; set; }
	public Boolean IsRemoved { get; set; }
}
