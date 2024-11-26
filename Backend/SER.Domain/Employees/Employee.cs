using SER.Tools.Types.IDs;

namespace SER.Domain.Employees;
public class Employee
{
	public ID Id { get; }
	public String Name { get; }
	public String? SecondName { get; }
	public String? LastName { get; }

	public DateTime CreatedDateTimeUtc { get; }
	public DateTime? ModifiedDateTimeUtc { get; }


	public Employee(ID id, String name, String? secondName, String? lastName, DateTime createdDateTimeUtc, DateTime? modifiedDateTimeUtc)
	{
		Id = id;
		Name = name;
		SecondName = secondName;
		LastName = lastName;

		CreatedDateTimeUtc = createdDateTimeUtc;
		ModifiedDateTimeUtc = modifiedDateTimeUtc;
	}
}
