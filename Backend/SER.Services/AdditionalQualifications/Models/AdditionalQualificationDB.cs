using SER.Tools.Types.IDs;

namespace SER.Services.AdditionalQualifications.Models;
internal class AdditionalQualificationDB
{
	public ID Id { get; set; }
	public String Name { get; set; }
	public String Code { get; set; }
	public String? StudyTime { get; set; }

	public DateTime CreatedDateTimeUtc { get; set; }
	public DateTime? ModifiedDateTimeUtc { get; set; }
	public Boolean IsRemoved { get; set; }
}
