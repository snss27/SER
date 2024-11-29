using Microsoft.AspNetCore.Mvc;
using SER.Domain.Enterprises;
using SER.Domain.Services;
using SER.Tools.Types.IDs;
using SER.Tools.Types.Results;

namespace SER.API.Controllers.Enterprises;
[Route("/api/enterprises")]
public class EnterprisesController(IEnterprisesService enterprisesService) : ControllerBase
{
	[HttpPost("save")]
	public async Task<Result> Save([FromBody] EnterpriseBlank blank)
	{
		return await enterprisesService.Save(blank);
	}

	[HttpPost("remove")]
	public async Task<Result> Remove([FromBody] ID id)
	{
		return await enterprisesService.Remove(id);
	}

	[HttpGet("get")]
	public async Task<Enterprise?> Get([FromQuery] ID id)
	{
		return await enterprisesService.Get(id);
	}

	[HttpGet("get_page")]
	public async Task<Enterprise[]> GetPage([FromQuery] Int32 page, [FromQuery] Int32 pageSize)
	{
		return await enterprisesService.GetPage(page, pageSize);
	}
}
