using Microsoft.AspNetCore.Mvc;
using SER.Domain.Clusters;
using SER.Domain.Services;
using SER.Tools.Types.IDs;
using SER.Tools.Types.Results;
using SER.Domain.Clusters.Converters;
using SER.Tools.Types;
using Microsoft.AspNetCore.Authorization;

namespace SER.API.Controllers.Clusters;

[Authorize]
[Route("api/clusters")]
public class ClustersController(IClustersService clustersService) : ControllerBase
{
	[HttpPost("save")]
	public async Task<OperationResult> Save([FromBody] ClusterBlank blank)
	{
		return await clustersService.Save(blank);
	}

	[HttpPost("remove")]
	public async Task<OperationResult> Remove([FromBody] ID id)
	{
		return await clustersService.Remove(id);
	}

	[HttpGet("get")]
	public async Task<ClusterDto?> Get([FromQuery] ID id)
	{
		Cluster? cluster = await clustersService.Get(id);
		return cluster?.ToDto();
	}

	[HttpGet("get_page")]
	public async Task<PagedResult<ClusterDto>> GetPage([FromQuery] Int32 page, [FromQuery] Int32 pageSize)
	{
		PagedResult<Cluster> clustersPage = await clustersService.GetPage(page, pageSize);
		return PagedResult.Create(
			clustersPage.Values.Select(c => c.ToDto()),
			clustersPage.TotalRows
		);
	}

	[HttpGet("get_by_search_text")]
	public async Task<ClusterDto[]> GetBySearchText([FromQuery] String searchText)
	{
		Cluster[] clusters = await clustersService.Get(searchText);
		return [.. clusters.Select(c => c.ToDto())];
	}
}
