using Microsoft.AspNetCore.Mvc;
using SER.Domain.Services;
using SER.Domain.Specialities;
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
}
