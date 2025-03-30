using Microsoft.AspNetCore.Mvc;
using SER.Domain.AdditionalQualifications;
using SER.Domain.Services;
using SER.Tools.Types.IDs;
using SER.Tools.Types.Results;

namespace SER.API.Controllers.AdditionalQualifications;

[Route("/api/additional_qualifications")]
public class AdditionalQualificationsController(IAdditionalQualificationsService additionalQualificationsService)
	: ControllerBase
{
	[HttpPost("save")]
	public async Task<Result> Save([FromBody] AdditionalQualificationBlank blank)
	{
		return await additionalQualificationsService.Save(blank);
	}

	[HttpPost("remove")]
	public async Task<Result> Remove([FromBody] ID id)
	{
		return await additionalQualificationsService.Remove(id);
	}

	[HttpGet("get")]
	public async Task<AdditionalQualification?> Get([FromQuery] ID id)
	{
		return await additionalQualificationsService.Get(id);
	}

	[HttpGet("get_page")]
	public async Task<AdditionalQualification[]> GetPage([FromQuery] Int32 page, [FromQuery] Int32 pageSize)
	{
		return await additionalQualificationsService.GetPage(page, pageSize);
	}

	[HttpGet("get_by_search_text")]
	public async Task<AdditionalQualification[]> GetBySearchText([FromQuery] String searchText)
	{
		return await additionalQualificationsService.GetBySearchText(searchText);
	}
}