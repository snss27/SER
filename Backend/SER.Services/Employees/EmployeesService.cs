using SER.Domain.Employees;
using SER.Domain.Services;
using SER.Services.Employees.Repositories;
using SER.Tools.Types.IDs;
using SER.Tools.Types.Results;

namespace SER.Services.Employees;
public class EmployeesService(IEmployeesRepository employeesRepository) : IEmployeesService
{
	public async Task<Result> Save(EmployeeBlank blank)
	{
		if (String.IsNullOrWhiteSpace(blank.Name)) return Result.Fail("Укажите имя работника");

		blank.Id ??= ID.New();

		return await employeesRepository.Save(blank);
	}
	public async Task<Result> Remove(ID id)
	{
		return await employeesRepository.Remove(id);
	}
	public async Task<Employee?> Get(ID id)
	{
		return await employeesRepository.Get(id);
	}

	public async Task<Employee[]> Get(ID[] ids)
	{
		return await employeesRepository.Get(ids);
	}

	public async Task<Employee[]> GetPage(Int32 page, Int32 pageSize)
	{
		return await employeesRepository.GetPage(page, pageSize);
	}

	public async Task<Employee[]> Get(String searchText)
	{
		return await employeesRepository.Get(searchText);
	}
}
