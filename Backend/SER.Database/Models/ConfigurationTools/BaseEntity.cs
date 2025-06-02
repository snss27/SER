using SER.Tools.Types.IDs;

namespace SER.Database.Models.ConfigurationTools;
public abstract class BaseEntity
{
	public ID Id { get; set; }
	public DateTime CreatedDateTimeUtc { get; set; }
	public DateTime? ModifiedDateTimeUtc { get; set; }
}
