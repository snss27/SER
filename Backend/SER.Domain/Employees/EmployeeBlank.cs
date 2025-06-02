using SER.Tools.Types.IDs;

namespace SER.Domain.Employees;
public class EmployeeBlank
{
	public ID? Id { get; init; }
	public String? Name { get; init; }
	public String? SecondName { get; init; }
	public String? LastName { get; init; }
}
