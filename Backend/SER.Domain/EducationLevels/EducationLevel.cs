using SER.Domain.EducationLevels.Enums;
using SER.Tools.Types.IDs;

namespace SER.Domain.EducationLevels;
public class EducationLevel
{
	public ID Id { get; }
	public EducationLevelTypes Type { get; }
	public String Name { get; }
	public String Code { get; }
	public String? StudyTime { get; }

	public DateTime CreatedDateTimeUtc { get; }
	public DateTime? ModifiedDateTimeUtc { get; }

	public EducationLevel(ID id, EducationLevelTypes type, String name, String code, String? studyTime, DateTime createdDateTimeUtc, DateTime? modifiedDateTimeUtc)
	{
		Id = id;
		Type = type;
		Name = name;
		Code = code;
		StudyTime = studyTime;

		CreatedDateTimeUtc = createdDateTimeUtc;
		ModifiedDateTimeUtc = modifiedDateTimeUtc;
	}
}
