using SER.Tools.Types.IDs;

namespace SER.Services.Employees.Models;
public class EmployeeDB
{
	public ID Id { get; set; }
	public String Name { get; set; }
	public String? SecondName { get; set; }
	public String? LastName { get; set; }

	public DateTime CreatedDateTimeUtc { get; set; }
	public DateTime? ModifiedDateTimeUtc { get; set; }
	public Boolean IsRemoved { get; set; }
}
