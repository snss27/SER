using SER.Tools.Types.IDs;

namespace SER.Domain.Employees;
public class EmployeeBlank
{
	public ID? Id { get; set; }
	public String? Name { get; set; }
	public String? SecondName { get; set; }
	public String? LastName { get; set; }
}
