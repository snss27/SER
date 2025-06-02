using SER.Tools.Types.IDs;

namespace SER.Domain.AdditionalQualifications;
public class AdditionalQualificationBlank
{
	public ID? Id { get; init; }
	public String? Name { get; init; }
	public String? Code { get; init; }
	public String? StudyTime { get; init; }
}
