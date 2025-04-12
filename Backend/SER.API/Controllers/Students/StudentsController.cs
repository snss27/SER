using Microsoft.AspNetCore.Mvc;
using SER.Domain.Services;
using SER.Domain.Students;
using SER.Tools.Types.IDs;
using SER.Tools.Types.Results;

namespace SER.API.Controllers.Students;

[Route("api/students/")]
public class StudentsController(IStudentsService studentsService) : ControllerBase
{
	[HttpPost("save")]
	public async Task<Result> Save([FromForm] StudentBlank blank)
	{
		return await studentsService.Save(blank);
	}

	[HttpPost("remove")]
	public async Task<Result> Remove([FromBody] ID id)
	{
		return await studentsService.Remove(id);
	}

	[HttpGet("get")]
	public async Task<StudentDto?> Get([FromQuery] ID id)
	{
		return await studentsService.Get(id);
	}

	[HttpGet("get_page")]
	public async Task<StudentDto[]> GetPage([FromQuery] Int32 page, [FromQuery] Int32 pageSize)
	{
		return await studentsService.GetPage(page, pageSize);
	}
}
