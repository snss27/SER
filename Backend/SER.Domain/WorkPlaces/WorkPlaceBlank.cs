using SER.Domain.Enterprises;
using SER.Tools.Types.IDs;
using SER.Tools.Types.Types;

namespace SER.Domain.Workplaces;
public class WorkPlaceBlank
{
	public ID? Id {  get; set; }
	public EnterpriseBlank? Enterprise { get; set; }
	public String? Post {  get; set; }
	public BlankFiles WorkBookExtractFile { get; set; }
	public DateTime? StartDate { get; set; }
	public DateTime? FinishDate { get; set; }
}
