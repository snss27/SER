using SER.Tools.Types.IDs;

namespace SER.Domain.Clusters;

public class Cluster(
	ID id,
	String name
)
{
	public ID Id { get; } = id;
	public String Name { get; } = name;
}
