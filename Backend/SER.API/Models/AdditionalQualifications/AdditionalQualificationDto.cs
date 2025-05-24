using SER.Tools.Types.IDs;

namespace SER.API.Models.AdditionalQualifications;

public record AdditionalQualificationDto(
	ID Id,
	String Name,
	String Code,
	String? StudyTime
);
