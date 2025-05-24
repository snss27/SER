using Microsoft.AspNetCore.Mvc;
using SER.API.Models.Clusters;
using SER.API.Models.Clusters.Converters;
using SER.Domain.Clusters;
using SER.Domain.Services;
using SER.Tools.Types.IDs;
using SER.Tools.Types.Results;

namespace SER.API.Controllers.Clusters;

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
	public async Task<ClusterDto[]> GetPage([FromQuery] Int32 page, [FromQuery] Int32 pageSize)
	{
		Cluster[] clusters = await clustersService.GetPage(page, pageSize);
		return [.. clusters.Select(c => c.ToDto())];
	}

	[HttpGet("get_by_search_text")]
	public async Task<ClusterDto[]> GetBySearchText([FromQuery] String searchText)
	{
		Cluster[] clusters = await clustersService.Get(searchText);
		return [.. clusters.Select(c => c.ToDto())];
	}
}
