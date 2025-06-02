using Microsoft.AspNetCore.Mvc;
using SER.Domain.AdditionalQualifications;
using SER.Domain.AdditionalQualifications.Converters;
using SER.Domain.Services;
using SER.Tools.Types.IDs;
using SER.Tools.Types.Results;

namespace SER.API.Controllers.AdditionalQualifications;

[Route("/api/additional_qualifications")]
public class AdditionalQualificationsController(IAdditionalQualificationsService additionalQualificationsService): ControllerBase
{
	[HttpPost("save")]
	public async Task<OperationResult> Save([FromBody] AdditionalQualificationBlank blank)
	{
		return await additionalQualificationsService.Save(blank);
	}

	[HttpPost("remove")]
	public async Task<OperationResult> Remove([FromBody] ID id)
	{
		return await additionalQualificationsService.Remove(id);
	}

	[HttpGet("get")]
	public async Task<AdditionalQualificationDto?> Get([FromQuery] ID id)
	{
		AdditionalQualification? additionalQualification = await additionalQualificationsService.Get(id);
		return additionalQualification?.ToDto();
	}

	[HttpGet("get_page")]
	public async Task<AdditionalQualificationDto[]> GetPage([FromQuery] Int32 page, [FromQuery] Int32 pageSize)
	{
		AdditionalQualification[] additionalQualifications = await additionalQualificationsService.GetPage(page, pageSize);
		return [..additionalQualifications.Select(aq => aq.ToDto())];
	}

	[HttpGet("get_by_search_text")]
	public async Task<AdditionalQualificationDto[]> GetBySearchText([FromQuery] String searchText)
	{
		AdditionalQualification[] additionalQualifications =  await additionalQualificationsService.GetBySearchText(searchText);
		return [.. additionalQualifications.Select(aq => aq.ToDto())];
	}
}