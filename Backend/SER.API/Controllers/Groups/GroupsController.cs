using Microsoft.AspNetCore.Mvc;
using SER.Domain.Groups;
using SER.Domain.Services;
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
}
