using SER.Tools.Types.IDs;

namespace SER.Domain.Employees;

public record EmployeeDto(
	ID Id,
	String Name,
	String SecondName,
	String? LastName
);
