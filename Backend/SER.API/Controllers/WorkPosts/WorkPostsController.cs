using Microsoft.AspNetCore.Mvc;
using SER.Domain.Services;
using SER.Domain.WorkPosts;
using SER.Tools.Types.IDs;
using SER.Tools.Types.Results;

namespace SER.API.Controllers.WorkPosts;
public class WorkPostsController : ControllerBase
{
	private readonly IWorkPostsService _workPostsService;

	public WorkPostsController(IWorkPostsService workPostsService)
	{
		_workPostsService = workPostsService;
	}

	[HttpPost("api/work_posts/save")]
	public async Task<Result> Save([FromBody] WorkPostBlank blank)
	{
		return await _workPostsService.Save(blank);
	}

	[HttpPost("api/work_posts/remove")]
	public async Task<Result> Remove([FromBody] ID id)
	{
		return await _workPostsService.Remove(id);
	}

	[HttpGet("api/work_posts/get")]
	public async Task<WorkPost?> Get([FromQuery] ID id)
	{
		return await _workPostsService.Get(id);
	}

	[HttpGet("api/work_posts/get_page")]
	public async Task<WorkPost[]> GetPage([FromQuery] Int32 page, [FromQuery] Int32 pageSize)
	{
		return await _workPostsService.GetPage(page, pageSize);
	}
}
