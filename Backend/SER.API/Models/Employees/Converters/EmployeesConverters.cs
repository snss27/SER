using SER.Domain.Employees;

namespace SER.API.Models.Employees.Converters;

public static class EmployeesConverters
{
	public static EmployeeDto ToDto(this Employee employee)
	{
		return new EmployeeDto(employee.Id, employee.FullName.First, employee.FullName.Second, employee.FullName.Last);
	}
}
