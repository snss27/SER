using SER.Domain.Employees;
using SER.Tools.Types.IDs;
using SER.Tools.Types.Results;

namespace SER.Domain.Services;

public interface IEmployeesService
{
	public Task<Result> Save(EmployeeBlank blank);
	public Task<Result> Remove(ID id);
	public Task<Employee?> Get(ID id);
	public Task<Employee[]> Get(ID[] ids);
	public Task<Employee[]> GetPage(Int32 page, Int32 pageSize);
	public Task<Employee[]> Get(String searchText);
}