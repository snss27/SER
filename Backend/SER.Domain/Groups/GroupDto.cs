using SER.Domain.Curators;
using SER.Domain.Groups.Enums;
using SER.Domain.Specialities;
using SER.Tools.Types.IDs;

namespace SER.Domain.Groups;

public record GroupDto(
	ID Id,
	String Number,
	StructuralUnits StructuralUnit,
	Speciality? Speciality,
	Int32 EnrollmentYear,
	Curator? Curator
);
