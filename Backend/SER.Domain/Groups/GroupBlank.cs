using SER.Domain.Curators;
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
	public Curator? Curator { get; set; }
}
