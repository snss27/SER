using SER.Domain.EducationLevels.Enums;
using SER.Tools.Types.IDs;

namespace SER.Domain.EducationLevels;

public record EducationLevelDto(
	ID Id,
	EducationLevelType Type,
	String Name,
	String Code,
	String? StudyTime
);
