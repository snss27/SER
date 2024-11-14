using Microsoft.AspNetCore.Mvc;
using SER.Domain.Services;
using SER.Domain.Specialities;
using SER.Tools.Types.IDs;
using SER.Tools.Types.Results;

namespace SER.API.Controllers.Specialities;

public class SpecialitiesController : ControllerBase
{
	private readonly ISpecialitiesService _specialitiesService;

	public SpecialitiesController(ISpecialitiesService specialitiesService)
	{
		_specialitiesService = specialitiesService;
	}

	[HttpPost("api/specialities/save")]
	public async Task<Result> Save([FromBody] SpecialityBlank blank)
	{
		return await _specialitiesService.Save(blank);
	}

	[HttpPost("api/specialities/remove")]
	public async Task<Result> Remove([FromBody] ID id)
	{
		return await _specialitiesService.Remove(id);
	}

	[HttpGet("api/specialities/get")]
	public async Task<Speciality> Get([FromQuery] ID id)
	{
		return await _specialitiesService.Get(id);
	}

	[HttpGet("api/specialities/get_page")]
	public async Task<Speciality[]> GetPage([FromQuery] Int32 page, [FromQuery] Int32 pageSize)
	{
		return await _specialitiesService.GetPage(page, pageSize);
	}
}
