using Microsoft.AspNetCore.Mvc;
using SER.Domain.Services;
using SER.Domain.Students;
using SER.Tools.Types.IDs;
using SER.Tools.Types.Results;

namespace SER.API.Controllers.Students;

public class StudentsController : ControllerBase
{
	private readonly IStudentsService _studentsService;

	public StudentsController(IStudentsService studentsService)
	{
		_studentsService = studentsService;
	}

	[HttpPost("api/students/save")]
	public async Task<Result> Save([FromBody] StudentBlank blank)
	{
		return await _studentsService.Save(blank);
	}

	[HttpPost("api/students/remove")]
	public async Task<Result> Remove([FromBody] ID id)
	{
		return await _studentsService.Remove(id);
	}

	[HttpGet("api/students/get")]
	public async Task<Student?> Get([FromQuery] ID id)
	{
		return await _studentsService.Get(id);
	}

	[HttpGet("api/students/get_page")]
	public async Task<Student[]> GetPage([FromQuery] Int32 page, [FromQuery] Int32 pageSize)
	{
		return await _studentsService.GetPage(page, pageSize);
	}
}
