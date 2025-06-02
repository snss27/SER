using SER.Domain.EducationLevels.Enums;
using SER.Tools.Types.IDs;

namespace SER.Domain.EducationLevels;
public class EducationLevelBlank
{
	public ID? Id { get; init; }
	public EducationLevelType? Type { get; init; }
	public String? Name { get; init; }
	public String? Code { get; init; }
	public String? StudyTime { get; init; }
}
