using Microsoft.AspNetCore.Mvc;
using SER.Domain.Services;
using SER.Domain.Students;
using SER.Tools.Types.IDs;
using SER.Tools.Types.Results;
using SER.Domain.Students.Converters;
using SER.Tools.Types;

namespace SER.API.Controllers.Students;

[Route("api/students/")]
public class StudentsController(IStudentsService studentsService) : ControllerBase
{
	[HttpPost("save")]
	public async Task<OperationResult> Save([FromBody] StudentBlank blank)
	{
		return await studentsService.Save(blank);
	}

	[HttpPost("remove")]
	public async Task<OperationResult> Remove([FromBody] ID id)
	{
		return await studentsService.Remove(id);
	}

	[HttpGet("get")]
	public async Task<StudentDto?> Get([FromQuery] ID id)
	{
		Student? student = await studentsService.Get(id);
		return student?.ToDto();
	}

	[HttpGet("get_page")]
	public async Task<PagedResult<StudentDto>> GetPage([FromQuery] Int32 page, [FromQuery] Int32 pageSize)
	{
		PagedResult<Student> studentsPage = await studentsService.GetPage(page, pageSize);
		return PagedResult.Create(
			studentsPage.Values.Select(s => s.ToDto()),
			studentsPage.TotalRows
		);
	}
}
