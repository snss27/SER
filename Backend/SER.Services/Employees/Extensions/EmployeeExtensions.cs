using SER.Database.Models.Employees;
using SER.Domain.Employees;

namespace SER.Services.Employees.Converters;
internal static class EmployeeExtensions
{
	public static EmployeeEntity ToEntity(this Employee employee)
	{
		return new EmployeeEntity()
		{
			Id = employee.Id,
			Name = employee.FullName.First,
			SecondName = employee.FullName.Second,
			LastName = employee.FullName.Last,
			CreatedDateTimeUtc = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Unspecified),
			ModifiedDateTimeUtc = null,
		};
	}

	public static void ApplyChanges(this EmployeeEntity entity, Employee employee)
	{
		entity.Name = employee.FullName.First;
		entity.SecondName = employee.FullName.Second;
		entity.LastName = employee.FullName.Last;
		entity.ModifiedDateTimeUtc = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Unspecified);
	}

	public static Employee ToDomain(this EmployeeEntity entity)
	{
		return Employee.Create(entity.Id, entity.Name, entity.SecondName, entity.LastName).Value;
	}
}
