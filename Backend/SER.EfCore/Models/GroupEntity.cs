using SER.EfCore.Models.Enums;
using SER.Tools.Types.IDs;

namespace SER.EfCore.Models;
public class GroupEntity
{
	public ID Id { get; }
	public String Number { get; } = String.Empty;
	public StructuralUnit StructuralUnit { get; }
	public ID EducationLevelId { get; }
	public EducationLevelEntity EducationLevelEntity { get; }
	public Int32 EnrollmentYear { get; }
	public ID? CuratorId { get; }
	public EmployeeEntity? Curator {  get; }
	public Boolean HasCluster { get; }
	public ID? ClusterId { get; }
	public ClusterEntity? Cluster { get; }

	public DateTime CreatedDateTimeUtc { get; }
	public DateTime ModifiedDateTimeUtc { get; }
	public Boolean IsRemoved { get; }
}
