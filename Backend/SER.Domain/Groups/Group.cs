using SER.Domain.Groups.Enums;
using SER.Tools.Types.IDs;

namespace SER.Domain.Groups;

public class Group(
	ID id,
	String number,
	StructuralUnit structuralUnit,
	ID educationLevelId,
	Int32 enrollmentYear,
	ID? curatorId,
	Boolean hasCluster,
	ID? clusterId,

	DateTime createdDateTimeUtc,
	DateTime? modifiedDateTimeUtc
)
{
	public ID Id { get; } = id;
	public String Number { get; } = number;
	public StructuralUnit StructuralUnit { get; } = structuralUnit;
	public ID EducationLevelId { get; } = educationLevelId;
	public Int32 EnrollmentYear { get; } = enrollmentYear;
	public ID? CuratorId { get; } = curatorId;
	public Boolean HasCluster { get; } = hasCluster;
	public ID? ClusterId { get; } = clusterId;

	public DateTime CreatedDateTimeUtc { get; } = createdDateTimeUtc;
	public DateTime? ModifiedDateTimeUtc { get; } = modifiedDateTimeUtc;
}