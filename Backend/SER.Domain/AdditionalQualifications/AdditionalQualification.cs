using SER.Tools.Types.IDs;

namespace SER.Domain.AdditionalQualifications;

public class AdditionalQualification(
	ID id,
	String name,
	String code,
	String? studyTime,
	DateTime createdDateTimeUtc,
	DateTime? modifiedDateTimeUtc
)
{
	public ID Id { get; } = id;
	public String Name { get; } = name;
	public String Code { get; } = code;
	public String? StudyTime { get; } = studyTime;

	public DateTime CreatedDateTimeUtc { get; } = createdDateTimeUtc;
	public DateTime? ModifiedDateTimeUtc { get; } = modifiedDateTimeUtc;
}