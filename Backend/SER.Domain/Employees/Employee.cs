using SER.Tools.Types.IDs;

namespace SER.Domain.Employees;

public class Employee(
	ID id,
	String name,
	String secondName,
	String? lastName,
	DateTime createdDateTimeUtc,
	DateTime? modifiedDateTimeUtc
)
{
	public ID Id { get; } = id;
	public String Name { get; } = name;
	public String SecondName { get; } = secondName;
	public String? LastName { get; } = lastName;

	public DateTime CreatedDateTimeUtc { get; } = createdDateTimeUtc;
	public DateTime? ModifiedDateTimeUtc { get; } = modifiedDateTimeUtc;
}