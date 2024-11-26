using SER.Domain.Employees;
using SER.Domain.Services;
using SER.Services.Employees.Repositories;
using SER.Tools.Types.IDs;
using SER.Tools.Types.Results;

namespace SER.Services.Employees;
public class EmployeesService : IEmployeesService
{
	private readonly IEmployeesRepository _employeesRepository;

	public EmployeesService(IEmployeesRepository employeesRepository)
	{
		_employeesRepository = employeesRepository;
	}

	public async Task<Result> Save(EmployeeBlank blank)
	{
		if (String.IsNullOrWhiteSpace(blank.Name)) return Result.Fail("Укажите имя работника");

		blank.Id ??= ID.New();

		return await _employeesRepository.Save(blank);
	}
	public async Task<Result> Remove(ID id)
	{
		return await _employeesRepository.Remove(id);
	}
	public async Task<Employee?> Get(ID id)
	{
		return await _employeesRepository.Get(id);
	}

	public async Task<Employee[]> Get(ID[] ids)
	{
		return await _employeesRepository.Get(ids);
	}

	public async Task<Employee[]> GetPage(Int32 page, Int32 pageSize)
	{
		return await _employeesRepository.GetPage(page, pageSize);
	}

	public async Task<Employee[]> Get(String searchText)
	{
		return await _employeesRepository.Get(searchText);
	}
}
