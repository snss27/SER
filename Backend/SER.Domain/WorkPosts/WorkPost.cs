using SER.Tools.Types.IDs;

namespace SER.Domain.WorkPosts;
public class WorkPost
{
	public ID Id { get;  }
	public String Name { get; }

	public DateTime CreatedDateTimeUtc { get; }
	public DateTime? ModifiedDateTimeUtc { get; }

	public WorkPost(ID id, String name, DateTime createdDateTimeUtc, DateTime? modifiedDateTimeUtc)
	{
		Id = id;
		Name = name;
		CreatedDateTimeUtc = createdDateTimeUtc;
		ModifiedDateTimeUtc = modifiedDateTimeUtc;
	}
}
