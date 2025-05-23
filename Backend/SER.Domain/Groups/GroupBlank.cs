using SER.Domain.Clusters;
using SER.Domain.EducationLevels;
using SER.Domain.Employees;
using SER.Domain.Groups.Enums;
using SER.Tools.Types.IDs;

namespace SER.Domain.Groups;

public class GroupBlank
{
	public ID? Id { get; set; }
	public String? Number { get; set; }
	public StructuralUnit? StructuralUnit { get; set; }
	public EducationLevel? EducationLevel { get; set; }
	public Int32? EnrollmentYear { get; set; }
	public Employee? Curator { get; set; }
	public Boolean HasCluster { get; set; }
	public Cluster? Cluster { get; set; }
}