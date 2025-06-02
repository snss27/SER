using SER.Domain.Employees;
using SER.Tools.Types;
using SER.Tools.Types.IDs;
using SER.Tools.Types.Results;

namespace SER.Domain.Services;

public interface IEmployeesService
{
	public Task<OperationResult> Save(EmployeeBlank blank);
	public Task<OperationResult> Remove(ID id);
	public Task<Employee?> Get(ID id);
	public Task<Employee[]> Get(ID[] ids);
	public Task<PagedResult<Employee>> GetPage(Int32 page, Int32 pageSize);
	public Task<Employee[]> Get(String searchText);
}