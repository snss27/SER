using SER.Database.Models.ConfigurationTools;

namespace SER.Database.Models.Employees;
public class EmployeeEntity : BaseEntity
{
	public String Name { get; set; } = default!;
	public String SecondName { get; set; } = default!;
	public String? LastName { get; set; }
}
