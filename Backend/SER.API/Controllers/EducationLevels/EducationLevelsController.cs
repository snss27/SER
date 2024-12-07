using Microsoft.AspNetCore.Mvc;
using SER.Domain.EducationLevels;
using SER.Domain.Services;
using SER.Tools.Types.IDs;
using SER.Tools.Types.Results;

namespace SER.API.Controllers.EducationLevels;

[Route("api/education_levels")]
public class EducationLevelsController(IEducationLevelsService educationLevelsService) : ControllerBase
{
	#region Specialities

	[HttpGet("specialities/get_by_search_text")]
	public async Task<EducationLevel[]> GetSpecialitiesBySearchText([FromQuery] String searchText)
	{
		return await educationLevelsService.GetSpecialities(searchText);
	}

	#endregion

	#region EducationLevels

	[HttpPost("save")]
	public async Task<Result> Save([FromBody] EducationLevelBlank blank)
	{
		return await educationLevelsService.Save(blank);
	}

	[HttpPost("remove")]
	public async Task<Result> Remove([FromBody] ID id)
	{
		return await educationLevelsService.Remove(id);
	}

	[HttpGet("get")]
	public async Task<EducationLevel?> Get([FromQuery] ID id)
	{
		return await educationLevelsService.Get(id);
	}

	[HttpGet("get_page")]
	public async Task<EducationLevel[]> GetPage([FromQuery] Int32 page, [FromQuery] Int32 pageSize)
	{
		return await educationLevelsService.GetPage(page, pageSize);
	}

	#endregion
}