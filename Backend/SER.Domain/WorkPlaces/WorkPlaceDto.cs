using SER.Domain.Enterprises;
using SER.Tools.Types.IDs;

namespace SER.Domain.WorkPlaces;
public record WorkPlaceDto(
	ID Id,
	EnterpriseDto Enterprise,
	String? Post,
	String[] WorkBookExtractFiles,
	DateTime? StartDate,
	DateTime? FinishDate
);
