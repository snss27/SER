using Microsoft.AspNetCore.Mvc;
using SER.Domain.Services;
using SER.Domain.Students;
using SER.Tools.Types.IDs;
using SER.Tools.Types.Results;
using SER.Domain.Students.Converters;
using SER.Tools.Types;
using SER.Domain.Students.StudentsFilters;

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

	public record StudentsPageRequest(Int32 Page, Int32 PageSize, StudentsFilter StudentsFilter);

	[HttpPost("get_page")]
	public async Task<PagedResult<StudentDto>> GetPage([FromBody] StudentsPageRequest request)
	{
		PagedResult<Student> studentsPage = await studentsService.GetPage(request.Page, request.PageSize, request.StudentsFilter);
		return PagedResult.Create(
			studentsPage.Values.Select(s => s.ToDto()),
			studentsPage.TotalRows
		);
	}
}
