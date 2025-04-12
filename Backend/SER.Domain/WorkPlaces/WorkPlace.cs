using SER.Tools.Types.IDs;

namespace SER.Domain.Workplaces;
public class WorkPlace(
	ID id,
	ID enterpriseId,
	String? post,
	String? workBookExtractFile,
	DateTime? startDateTime,
	DateTime? finishDateTime,

	DateTime createdDateTimeUtc,
	DateTime? modifiedDateTimeUtc
)
{
	public ID Id { get; } = id;
	public ID EnterpriseId { get; } = enterpriseId;
	public String? Post {  get; } = post;
	public String? WorkBookExtractFile { get; } = workBookExtractFile;
	public DateTime? StartDateTime { get; } = startDateTime;
	public DateTime? FinishDateTime { get; } = finishDateTime;

	public DateTime CreatedDateTimeUtc { get; } = createdDateTimeUtc;
	public DateTime? ModifiedDateTimeUtc { get; } = modifiedDateTimeUtc;
}
