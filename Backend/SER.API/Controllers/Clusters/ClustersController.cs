using Microsoft.AspNetCore.Mvc;
using SER.Domain.Clusters;
using SER.Domain.Services;
using SER.Tools.Types.IDs;
using SER.Tools.Types.Results;

namespace SER.API.Controllers.Clusters;

[Route("api/clusters")]
public class ClustersController(IClustersService clustersService) : ControllerBase
{
	[HttpPost("save")]
	public async Task<Result> Save([FromBody] ClusterBlank blank)
	{
		return await clustersService.Save(blank);
	}

	[HttpPost("remove")]
	public async Task<Result> Remove([FromBody] ID id)
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
}
