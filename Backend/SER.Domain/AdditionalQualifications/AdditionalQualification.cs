using SER.Tools.Types.IDs;

namespace SER.Domain.AdditionalQualifications;
public class AdditionalQualification
{
	public ID Id { get; }
	public String Name { get; }
	public String Code { get; }
	public String? StudyTime { get; }

	public DateTime CreatedDateTimeUtc { get; }
	public DateTime? ModifiedDateTimeUtc { get; }

	public AdditionalQualification(ID id, String name, String code, String? studyTime, DateTime createdDateTimeUtc, DateTime? modifiedDateTimeUtc)
	{
		Id = id;
		Name = name;
		Code = code;
		StudyTime = studyTime;

		CreatedDateTimeUtc = createdDateTimeUtc;
		ModifiedDateTimeUtc = modifiedDateTimeUtc;
	}
}
