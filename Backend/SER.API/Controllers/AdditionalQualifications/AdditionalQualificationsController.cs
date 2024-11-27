using Microsoft.AspNetCore.Mvc;
using SER.Domain.AdditionalQualifications;
using SER.Domain.Services;
using SER.Tools.Types.IDs;
using SER.Tools.Types.Results;

namespace SER.API.Controllers.AdditionalQualifications;
public class AdditionalQualificationsController : ControllerBase
{
	private readonly IAdditionalQualificationsService _additionalQualificationsService;

	public AdditionalQualificationsController(IAdditionalQualificationsService additionalQualificationsService)
	{
		_additionalQualificationsService = additionalQualificationsService;
	}

	[HttpPost("api/additional_qualifications/save")]
	public async Task<Result> Save([FromBody] AdditionalQualificationBlank blank)
	{
		return await _additionalQualificationsService.Save(blank);
	}

	[HttpPost("api/additional_qualifications/remove")]
	public async Task<Result> Remove([FromBody] ID id)
	{
		return await _additionalQualificationsService.Remove(id);
	}

	[HttpGet("api/additional_qualifications/get")]
	public async Task<AdditionalQualification?> Get([FromQuery] ID id)
	{
		return await _additionalQualificationsService.Get(id);
	}

	[HttpGet("api/additional_qualifications/get_page")]
	public async Task<AdditionalQualification[]> GetPage([FromQuery] Int32 page, [FromQuery] Int32 pageSize)
	{
		return await _additionalQualificationsService.GetPage(page, pageSize);
	}
}
