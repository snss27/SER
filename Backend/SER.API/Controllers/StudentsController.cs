using Microsoft.AspNetCore.Mvc;
using SER.Domain.Students;

namespace SER.API.Controllers;

public class StudentsController : ControllerBase
{
    private readonly IStudentsService _studentsService;

    public StudentsController(IStudentsService studentsService)
    {
        _studentsService = studentsService;
    }

    [HttpPost("api/students/get_page")]
    public async Task<IActionResult> AddErrorOld([FromQuery] Int32 page)
    {
        FlatStudent[] students = await _studentsService.GetFlatStudentsPage(page);

        return new JsonResult(students);
    }
}
