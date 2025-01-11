using SER.Domain.Groups.Enums;
using SER.Tools.Types.IDs;

namespace SER.Domain.Groups;

public class GroupBlank
{
	public ID? Id { get; set; }
	public String? Number { get; set; }
	public StructuralUnits? StructuralUnit { get; set; }
	public ID? EducationLevelId { get; set; }
	public Int32? EnrollmentYear { get; set; }
	public ID? CuratorId { get; set; }
	public Boolean HasCluster { get; set; }
	public ID? ClusterId { get; set; }
}