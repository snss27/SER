using SER.Database.Models.ConfigurationTools;
using SER.Database.Models.Enterprises;
using SER.Database.Models.Students;
using SER.Tools.Types.IDs;

namespace SER.Database.Models.WorkPlaces;
public class WorkPlaceEntity : BaseEntity
{
	public ID EnterpriseId { get; set; }
	public EnterpriseEntity Enterprise { get; set; } = default!;
	public String? Post { get; set; }
	public List<String> WorkBookExtractFiles { get; set; } = [];
	public DateTime? StartDate { get; set; }
	public DateTime? FinishDate { get; set; }
	public ID StudentId { get; set; }
	public StudentEntity Student { get; set; } = default!;
	public Boolean IsCurrent { get; set; }
}
