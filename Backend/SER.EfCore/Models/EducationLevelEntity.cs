using SER.EfCore.Models.Enums;
using SER.Tools.Types.IDs;

namespace SER.EfCore.Models;
public class EducationLevelEntity
{
	public ID Id { get; set; }
	public EducationLevelType Type { get; set; }
	public String Name { get; set; } = String.Empty;
	public String Code { get; set; } = String.Empty;
	public String StudyTime { get; set; } = String.Empty;

	public DateTime CreatedDateTimeUtc { get; set; }
	public DateTime ModifiedDateTimeUtc { get; set; }
	public Boolean IsRemoved { get; set; }
}
