using SER.Domain.EducationLevels.Enums;
using SER.Tools.Types.IDs;

namespace SER.Domain.EducationLevels;

public class EducationLevel(
	ID id,
	EducationLevelTypes type,
	String name,
	String code,
	String? studyTime,
	DateTime createdDateTimeUtc,
	DateTime? modifiedDateTimeUtc)
{
	public ID Id { get; } = id;
	public EducationLevelTypes Type { get; } = type;
	public String Name { get; } = name;
	public String Code { get; } = code;
	public String? StudyTime { get; } = studyTime;

	public DateTime CreatedDateTimeUtc { get; } = createdDateTimeUtc;
	public DateTime? ModifiedDateTimeUtc { get; } = modifiedDateTimeUtc;
}