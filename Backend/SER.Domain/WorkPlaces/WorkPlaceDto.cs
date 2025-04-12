using SER.Domain.Enterprises;
using SER.Tools.Types.IDs;

namespace SER.Domain.WorkPlaces;
public record WorkPlaceDto(
	ID Id,
	Enterprise Enterprise,
	String? Post,
	String? WorkBookExtractFile,
	DateTime? StartDate,
	DateTime? FinishDate
);
