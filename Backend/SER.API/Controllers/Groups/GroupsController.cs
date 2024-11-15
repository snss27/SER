using Microsoft.AspNetCore.Mvc;
using SER.Domain.Groups;
using SER.Domain.Services;
using SER.Tools.Types.IDs;
using SER.Tools.Types.Results;

namespace SER.API.Controllers.Groups;
public class GroupsController : ControllerBase
{
	private readonly IGroupsService _groupsService;

	public GroupsController(IGroupsService groupsService)
	{
		_groupsService = groupsService;
	}

	[HttpPost("api/groups/save")]
	public async Task<Result> Save([FromBody] GroupBlank blank)
	{
		return await _groupsService.Save(blank);
	}

	[HttpPost("api/groups/remove")]
	public async Task<Result> Remove([FromBody] ID id)
	{
		return await _groupsService.Remove(id);
	}

	[HttpGet("api/groups/get")]
	public async Task<GroupDto?> Get([FromQuery] ID id)
	{
		return await _groupsService.Get(id);
	}

	[HttpGet("api/groups/get_page")]
	public async Task<GroupDto[]> GetAll(Int32 page, Int32 pageSize)
	{
		return await _groupsService.GetPage(page, pageSize);
	}
}
