using SER.Database.Models.ConfigurationTools;
using SER.Domain.EducationLevels.Enums;

namespace SER.Database.Models.EducationLevels;
public class EducationLevelEntity : BaseEntity
{
	public EducationLevelType Type { get; set; }
	public String Name { get; set; } = default!;
	public String Code { get; set; } = default!;
	public String? StudyTime { get; set; }
}
