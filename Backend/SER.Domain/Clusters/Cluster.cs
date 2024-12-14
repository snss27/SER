using SER.Tools.Types.IDs;

namespace SER.Domain.Clusters;

public class Cluster(
	ID id,
	String name,

	DateTime createdDateTimeUtc,
	DateTime? modifiedDateTimeUtc
)
{
	public ID Id { get; } = id;
	public String Name { get; } = name;

	public DateTime CreatedDateTimeUtc { get; } = createdDateTimeUtc;
	public DateTime? ModifiedDateTimeUtc { get; } = modifiedDateTimeUtc;
}
