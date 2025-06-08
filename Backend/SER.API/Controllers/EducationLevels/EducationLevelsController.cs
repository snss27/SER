using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SER.Domain.EducationLevels;
using SER.Domain.EducationLevels.Converters;
using SER.Domain.Services;
using SER.Tools.Types;
using SER.Tools.Types.IDs;
using SER.Tools.Types.Results;

namespace SER.API.Controllers.EducationLevels;

[Authorize]
[Route("api/education_levels")]
public class EducationLevelsController(IEducationLevelsService educationLevelsService) : ControllerBase
{
	[HttpPost("save")]
	public async Task<OperationResult> Save([FromBody] EducationLevelBlank blank)
	{
		return await educationLevelsService.Save(blank);
	}

	[HttpPost("remove")]
	public async Task<OperationResult> Remove([FromBody] ID id)
	{
		return await educationLevelsService.Remove(id);
	}

	[HttpGet("get")]
	public async Task<EducationLevelDto?> Get([FromQuery] ID id)
	{
		EducationLevel? educationLevel = await educationLevelsService.Get(id);
		return educationLevel?.ToDto();
	}

	[HttpGet("get_page")]
	public async Task<PagedResult<EducationLevelDto>> GetPage([FromQuery] Int32 page, [FromQuery] Int32 pageSize)
	{
		PagedResult<EducationLevel> educationsLevelsPage = await educationLevelsService.GetPage(page, pageSize);

		return PagedResult.Create(
			educationsLevelsPage.Values.Select(el => el.ToDto()),
			educationsLevelsPage.TotalRows
		);
	}

	[HttpGet("get_by_search_text")]
	public async Task<EducationLevelDto[]> GetBySearchText([FromQuery] String searchText)
	{
		EducationLevel[] educationLevels = await educationLevelsService.Get(searchText);
		return [.. educationLevels.Select(el => el.ToDto())];
	}
}
