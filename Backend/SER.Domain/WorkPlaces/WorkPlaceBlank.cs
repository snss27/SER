using SER.Domain.Enterprises;
using SER.Tools.Types.IDs;

namespace SER.Domain.Workplaces;
public class WorkPlaceBlank
{
	public ID? Id {  get; init; }
	public EnterpriseDto? Enterprise { get; init; }
	public String? Post {  get; init; }
	public String[] WorkBookExtractFiles { get; init; } = default!;
	public DateTime? StartDate { get; init; }
	public DateTime? FinishDate { get; init; }
	public Boolean? IsCurrent { get; init; }
}
