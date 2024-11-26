using SER.Domain.Employees;
using SER.Domain.Groups.Enums;
using SER.Domain.Specialities;
using SER.Tools.Types.IDs;

namespace SER.Domain.Groups;
public class GroupBlank
{
	public ID? Id { get; set; }
	public String? Number { get; set; }
	public StructuralUnits? StructuralUnit { get; set; }
	public Speciality? Speciality {  get; set; }
	public Int32? EnrollmentYear { get; set; }
	public Employee? Curator { get; set; }
}
