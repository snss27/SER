using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SER.Domain.Employees;
using SER.Domain.Employees.Converters;
using SER.Domain.Services;
using SER.Tools.Types;
using SER.Tools.Types.IDs;
using SER.Tools.Types.Results;

namespace SER.API.Controllers.Employees;

[Authorize]
[Route("api/employees")]
public class EmployeesController(IEmployeesService employeesService) : ControllerBase
{
	[HttpPost("save")]
	public async Task<OperationResult> Save([FromBody] EmployeeBlank blank)
	{
		return await employeesService.Save(blank);
	}

	[HttpPost("remove")]
	public async Task<OperationResult> Remove([FromBody] ID id)
	{
		return await employeesService.Remove(id);
	}

	[HttpGet("get")]
	public async Task<EmployeeDto?> Get([FromQuery] ID id)
	{
		Employee? employee = await employeesService.Get(id);
		return employee?.ToDto();
	}

	[HttpGet("get_page")]
	public async Task<PagedResult<EmployeeDto>> GetPage([FromQuery] Int32 page, [FromQuery] Int32 pageSize)
	{
		PagedResult<Employee> employeesPage = await employeesService.GetPage(page, pageSize);
		return PagedResult.Create(
			employeesPage.Values.Select(e => e.ToDto()),
			employeesPage.TotalRows
		);
	}

	[HttpGet("get_by_search_text")]
	public async Task<EmployeeDto[]> GetBySearchText([FromQuery] String searchText)
	{
		Employee[] employees =  await employeesService.Get(searchText);
		return [.. employees.Select(e => e.ToDto())];
	}
}