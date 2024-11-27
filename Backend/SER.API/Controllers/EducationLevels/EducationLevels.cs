using Microsoft.AspNetCore.Mvc;
using SER.Domain.EducationLevels;
using SER.Domain.Services;
using SER.Tools.Types.IDs;
using SER.Tools.Types.Results;

namespace SER.API.Controllers.EducationLevels;

public class EducationLevels : ControllerBase
{
	private readonly IEducationLevelsService _educationLevelsService;

	public EducationLevels(IEducationLevelsService educationLevelsService)
	{
		_educationLevelsService = educationLevelsService;
	}

	[HttpPost("api/education_levels/save")]
	public async Task<Result> Save([FromBody] EducationLevelBlank blank)
	{
		return await _educationLevelsService.Save(blank);
	}

	[HttpPost("api/education_levels/remove")]
	public async Task<Result> Remove([FromBody] ID id)
	{
		return await _educationLevelsService.Remove(id);
	}

	[HttpGet("api/education_levels/get")]
	public async Task<EducationLevel?> Get([FromQuery] ID id)
	{
		return await _educationLevelsService.Get(id);
	}

	[HttpGet("api/education_levels/get_page")]
	public async Task<EducationLevel[]> GetPage([FromQuery] Int32 page, [FromQuery] Int32 pageSize)
	{
		return await _educationLevelsService.GetPage(page, pageSize);
	}

	[HttpGet("api/education_levels/get_by_search_text")]
	public async Task<EducationLevel[]> GetBySearchText([FromQuery] String searchText)
	{
		return await _educationLevelsService.Get(searchText);
	}
}
