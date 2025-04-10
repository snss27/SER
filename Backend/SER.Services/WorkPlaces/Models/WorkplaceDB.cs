using SER.Tools.Types.IDs;

namespace SER.Services.WorkPlaces.Models;
public class WorkplaceDB
{
	public ID Id { get; set; }
	public ID EnterpriseId { get; set; }
	public String? Post {  get; set; }
	public String? WorkBookExtractFile { get; set; }
	public DateTime? StartDate { get; set; }
	public DateTime? FinishDate { get; set; }

	public DateTime CreatedDateTimeUtc { get; set; }
	public DateTime? ModifiedDateTimeUtc { get; set; }
	public Boolean IsRemoved { get; set; }
}
