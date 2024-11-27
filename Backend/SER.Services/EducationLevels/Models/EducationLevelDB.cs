using SER.Domain.EducationLevels.Enums;
using SER.Tools.Types.IDs;

namespace SER.Services.EducationLevels.Models;

public class EducationLevelDB
{
	public ID Id { get; set; }
	public EducationLevelTypes Type { get; set; }
	public String Name { get; set; }
	public String Code { get; set; }
	public String? StudyTime { get; set; }

	public DateTime CreatedDateTimeUtc { get; set; }
	public DateTime? ModifiedDateTimeUtc { get; set; }
	public Boolean IsRemoved { get; set; }
}

