using Microsoft.AspNetCore.Mvc;
using SER.Domain.Clusters;
using SER.Domain.Employees;
using SER.Domain.Services;
using SER.Services.Employees;
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
	public async Task<Cluster?> Get([FromQuery] ID id)
	{
		return await clustersService.Get(id);
	}

	[HttpGet("get_page")]
	public async Task<Cluster[]> GetPage([FromQuery] Int32 page, [FromQuery] Int32 pageSize)
	{
		return await clustersService.GetPage(page, pageSize);
	}

	[HttpGet("get_by_search_text")]
	public async Task<Cluster[]> GetBySearchText([FromQuery] String searchText)
	{
		return await clustersService.Get(searchText);
	}
}
