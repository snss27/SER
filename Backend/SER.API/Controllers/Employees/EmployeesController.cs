using Microsoft.AspNetCore.Mvc;
using SER.Domain.Employees;
using SER.Domain.Services;
using SER.Tools.Types.IDs;
using SER.Tools.Types.Results;

namespace SER.API.Controllers.Employees;
public class EmployeesController : ControllerBase
{
	private readonly IEmployeesService _employeesService;

	public EmployeesController(IEmployeesService employeesService)
	{
		_employeesService = employeesService;
	}

	[HttpPost("api/employees/save")]
	public async Task<Result> Save([FromBody] EmployeeBlank blank)
	{
		return await _employeesService.Save(blank);
	}

	[HttpPost("api/employees/remove")]
	public async Task<Result> Remove([FromBody] ID id)
	{
		return await _employeesService.Remove(id);
	}

	[HttpGet("api/employees/get")]
	public async Task<Employee?> Get([FromQuery] ID id)
	{
		return await _employeesService.Get(id);
	}

	[HttpGet("api/employees/get_page")]
	public async Task<Employee[]> GetPage([FromQuery] Int32 page, [FromQuery] Int32 pageSize)
	{
		return await _employeesService.GetPage(page, pageSize);
	}

	[HttpGet("api/employees/get_by_search_text")]
	public async Task<Employee[]> GetBySearchText([FromQuery] String searchText)
	{
		return await _employeesService.Get(searchText);
	}
}
