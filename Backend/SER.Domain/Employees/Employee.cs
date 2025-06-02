using CSharpFunctionalExtensions;
using SER.EfCore.Models.Types;
using SER.Tools.Types.IDs;
using SER.Tools.Types.Results;

namespace SER.Domain.Employees;

public class Employee
{
	public ID Id { get; }
	public FullName FullName { get; } = default!;

	private Employee(ID id, FullName fullName)
	{
		Id = id;
		FullName = fullName;
	}

	public static Result<Employee, Error> Create(ID? id, String? firstName, String? secondName, String? lastName)
	{
		Result<FullName, Error> result = FullName.Create(firstName, secondName, lastName);
		if (result.IsFailure) return result.Error;

		FullName fullName = result.Value;

		ID _id = id ?? ID.New();
		return new Employee(_id, fullName);
	}
}
