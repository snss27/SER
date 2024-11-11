using Microsoft.AspNetCore.Mvc;
using SER.Domain.Curators;
using SER.Domain.Services;
using SER.Services.Curators.Repositories;
using SER.Tools.Types.IDs;
using SER.Tools.Types.Results;

namespace SER.API.Controllers.Curators;
public class CuratorsController : ControllerBase
{
	private readonly ICuratorsService _curatorsService;

	public CuratorsController(ICuratorsService curatorsService)
	{
		_curatorsService = curatorsService;
	}

	[HttpPost("api/curators/save")]
	public async Task<Result> Save([FromBody] CuratorBlank blank)
	{
		return await _curatorsService.Save(blank);
	}

	[HttpPost("/api/curators/remove")]
	public async Task<Result> Remove([FromBody] ID id)
	{
		return await _curatorsService.Remove(id);
	}

	[HttpGet("api/curators/get")]
	public async Task<Curator> Get([FromQuery] ID id)
	{
		return await _curatorsService.Get(id);
	}
}
