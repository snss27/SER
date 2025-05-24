namespace SER.Domain.Employees.Converters;

public static class EmployeesConverters
{
	public static EmployeeDto ToDto(this Employee employee)
	{
		return new EmployeeDto(employee.Id, employee.FullName.First, employee.FullName.Second, employee.FullName.Last);
	}

	public static Employee ToDomain(this EmployeeDto dto)
	{
		return Employee.Create(dto.Id, dto.Name, dto.SecondName, dto.LastName).Value;
	}
}
