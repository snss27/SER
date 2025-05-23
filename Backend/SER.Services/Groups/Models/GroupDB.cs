using SER.Domain.Groups.Enums;
using SER.Tools.Types.IDs;

namespace SER.Services.Groups.Models;

public class GroupDB
{
	public ID Id { get; set; }
	public String Number { get; set; }
	public StructuralUnit StructuralUnit { get; set; }
	public ID EducationLevelId { get; set; }
	public Int32 EnrollmentYear { get; set; }
	public ID? CuratorId { get; set; }
	public Boolean HasCluster { get; set; }
	public ID? ClusterId { get; set; }

	public DateTime CreatedDateTimeUtc { get; set; }
	public DateTime? ModifiedDateTimeUtc { get; set; }
	public Boolean IsRemoved { get; set; }
}