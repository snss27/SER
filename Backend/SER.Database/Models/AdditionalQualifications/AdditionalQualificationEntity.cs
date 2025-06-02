using SER.Database.Models.ConfigurationTools;

namespace SER.Database.Models.AdditionalQualifications;
public class AdditionalQualificationEntity : BaseEntity
{
	public String Name { get; set; } = default!;
	public String Code { get; set; } = default!;
	public String? StudyTime { get; set; }
}
