using Microsoft.AspNetCore.Mvc;
using SER.Domain.Enterprises;
using SER.Domain.Services;
using SER.Tools.Types.IDs;
using SER.Tools.Types.Results;

namespace SER.API.Controllers.Enterprises;
public class EnterprisesController : ControllerBase
{
	private readonly IEnterprisesService _enterprisesService;

	public EnterprisesController(IEnterprisesService enterprisesService)
	{
		_enterprisesService = enterprisesService;
	}

	[HttpPost("api/enterprises/save")]
	public async Task<Result> Save([FromBody] EnterpriseBlank blank)
	{
		return await _enterprisesService.Save(blank);
	}

	[HttpPost("api/enterprises/remove")]
	public async Task<Result> Remove([FromBody] ID id)
	{
		return await _enterprisesService.Remove(id);
	}

	[HttpGet("api/enterprises/get")]
	public async Task<Enterprise?> Get([FromQuery] ID id)
	{
		return await _enterprisesService.Get(id);
	}

	[HttpGet("api/enterprises/get_page")]
	public async Task<Enterprise[]> GetPage([FromQuery] Int32 page, [FromQuery] Int32 pageSize)
	{
		return await _enterprisesService.GetPage(page, pageSize);
	}
}
