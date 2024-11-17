using Microsoft.AspNetCore.Mvc;
using SER.Domain.Services;
using SER.Domain.Students;
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
}
