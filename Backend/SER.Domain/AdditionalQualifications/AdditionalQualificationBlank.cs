using SER.Tools.Types.IDs;

namespace SER.Domain.AdditionalQualifications;
public class AdditionalQualificationBlank
{
	public ID? Id { get; set; }
	public String? Name { get; set; }
	public String? Code { get; set; }
	public Int32? StudyYears { get; set; }
	public Int32? StudyMonths { get; set; }
}
