using Microsoft.AspNetCore.Mvc;
using SER.Domain.Employees;
using SER.Domain.Services;
using SER.Tools.Types.IDs;
using SER.Tools.Types.Results;

namespace SER.API.Controllers.Employees;

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
	public async Task<Employee?> Get([FromQuery] ID id)
	{
		return await employeesService.Get(id);
	}

	[HttpGet("get_page")]
	public async Task<Employee[]> GetPage([FromQuery] Int32 page, [FromQuery] Int32 pageSize)
	{
		return await employeesService.GetPage(page, pageSize);
	}

	[HttpGet("get_by_search_text")]
	public async Task<Employee[]> GetBySearchText([FromQuery] String searchText)
	{
		return await employeesService.Get(searchText);
	}
}