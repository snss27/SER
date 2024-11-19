using SER.Tools.Types.IDs;

namespace SER.Services.WorkPosts.Models;
internal class WorkPostDB
{
	public ID Id { get; set; }
	public String Name { get; set; }
	public DateTime CreatedDateTimeUtc { get; set; }
	public DateTime? ModifiedDateTimeUtc { get; set; }
	public Boolean IsRemoved { get; set; }
}
