using Microsoft.AspNetCore.Mvc;
using SER.Domain.Enterprises;
using SER.Domain.Enterprises.Converters;
using SER.Domain.Services;
using SER.Tools.Types.IDs;
using SER.Tools.Types.Results;

namespace SER.API.Controllers.Enterprises;
[Route("/api/enterprises")]
public class EnterprisesController(IEnterprisesService enterprisesService) : ControllerBase
{
	[HttpPost("save")]
	public async Task<OperationResult> Save([FromBody] EnterpriseBlank blank)
	{
		return await enterprisesService.Save(blank);
	}

	[HttpPost("remove")]
	public async Task<OperationResult> Remove([FromBody] ID id)
	{
		return await enterprisesService.Remove(id);
	}

	[HttpGet("get")]
	public async Task<EnterpriseDto?> Get([FromQuery] ID id)
	{
		Enterprise? enterprise = await enterprisesService.Get(id);
		return enterprise?.ToDto();
	}

	[HttpGet("get_page")]
	public async Task<EnterpriseDto[]> GetPage([FromQuery] Int32 page, [FromQuery] Int32 pageSize)
	{
		Enterprise[] enterprises = await enterprisesService.GetPage(page, pageSize);
		return [.. enterprises.Select(e => e.ToDto())];
	}

	[HttpGet("get_by_search_text")]
	public async Task<EnterpriseDto[]> GetBySearchText([FromQuery] String searchText)
	{
		Enterprise[] enterprises = await enterprisesService.GetBySearchText(searchText);
		return [.. enterprises.Select(e => e.ToDto())];
	}
}
