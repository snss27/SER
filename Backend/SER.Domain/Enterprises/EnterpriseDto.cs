using SER.Tools.Types.IDs;

namespace SER.Domain.Enterprises;

public record EnterpriseDto(
	ID Id,
	String Name,
	String? LegalAddress,
	String? ActualAddress,
	String? Address,
	String? INN,
	String? KPP,
	String? ORGN,
	String? Phone,
	String? Mail,
	Boolean IsOpk
);
