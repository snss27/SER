using SER.Database.Models.Clusters;
using SER.Database.Models.ConfigurationTools;
using SER.Database.Models.EducationLevels;
using SER.Database.Models.Employees;
using SER.Domain.Groups.Enums;
using SER.Tools.Types.IDs;

namespace SER.Database.Models.Groups;
public class GroupEntity : BaseEntity
{
	public String Number { get; set; } = default!;
	public StructuralUnit StructuralUnit { get; set; }
	public ID EducationLevelId { get; set; }
	public EducationLevelEntity EducationLevel { get; set; } = default!;
	public Int32 EnrollmentYear { get; set; }
	public ID? CuratorId { get; set; }
	public EmployeeEntity? Curator {  get; set; }
	public Boolean HasCluster { get; set; }
	public ID? ClusterId { get; set; }
	public ClusterEntity? Cluster { get; set; }
}
