using SER.Domain.Groups.Enums;
using SER.Tools.Types.IDs;

namespace SER.Domain.Groups;

public class Group(
	ID id,
	String number,
	StructuralUnits structuralUnit,
	ID? educationLevelId,
	Int32 enrollmentYear,
	ID? curatorId,
	DateTime createdDateTimeUtc,
	DateTime? modifiedDateTimeUtc
)
{
	public ID Id { get; } = id;
	public String Number { get; } = number;
	public StructuralUnits StructuralUnit { get; } = structuralUnit;
	public ID? EducationLevelId { get; } = educationLevelId;
	public Int32 EnrollmentYear { get; } = enrollmentYear;
	public ID? CuratorId { get; } = curatorId;

	public DateTime CreatedDateTimeUtc { get; } = createdDateTimeUtc;
	public DateTime? ModifiedDateTimeUtc { get; } = modifiedDateTimeUtc;
}