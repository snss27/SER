using SER.Tools.Types.IDs;

namespace SER.Domain.AdditionalQualifications;

public record AdditionalQualificationDto(
	ID Id,
	String Name,
	String Code,
	String? StudyTime
);
