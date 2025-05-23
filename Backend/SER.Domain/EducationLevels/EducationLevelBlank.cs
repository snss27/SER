using SER.Domain.EducationLevels.Enums;
using SER.Tools.Types.IDs;

namespace SER.Domain.EducationLevels;
public class EducationLevelBlank
{
	public ID? Id { get; set; }
	public EducationLevelType? Type { get; set; }
	public String? Name { get; set; }
	public String? Code { get; set; }
	public String? StudyTime { get; set; }
}
