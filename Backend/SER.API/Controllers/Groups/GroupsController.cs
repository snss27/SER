using Microsoft.AspNetCore.Mvc;
using SER.Domain.Groups;
using SER.Domain.Services;
using SER.Domain.Specialities;
using SER.Services.Specialities;
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
	public async Task<Group> Get([FromQuery] ID id)
	{
		return await _groupsService.Get(id);
	}

	[HttpGet("api/groups/get/all")]
	public async Task<Group[]> GetAll()
	{
		return await _groupsService.GetAll();
	}
}
