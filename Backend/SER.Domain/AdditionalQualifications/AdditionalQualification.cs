using SER.Tools.Types.IDs;

namespace SER.Domain.AdditionalQualifications;

public class AdditionalQualification()
{
	public ID Id { get; set; }
	public String Name { get; set; }
	public String Code { get; set; }
	public String? StudyTime { get; set; }
}
