using SER.Domain.Employees;
using SER.Services.Employees.Models;

namespace SER.Services.Employees.Converters;
public static class EmployeesConverter
{
	public static Employee ToEmployee(this EmployeeDB db)
	{
		return new Employee(db.Id, db.Name, db.SecondName, db.LastName, db.CreatedDateTimeUtc, db.ModifiedDateTimeUtc);
	}

	public static Employee[] ToEmployees(this EmployeeDB[] dbs)
	{
		return dbs.Select(ToEmployee).ToArray();
	}
}
