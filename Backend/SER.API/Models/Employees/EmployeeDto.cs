using SER.Tools.Types.IDs;

namespace SER.API.Models.Employees;

public record EmployeeDto(
	ID Id,
	String Name,
	String SecondName,
	String? LastName
);
