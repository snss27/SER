using Microsoft.AspNetCore.Mvc;
using SER.Domain.Groups;
using SER.Domain.Services;
using SER.Tools.Types.IDs;
using SER.Tools.Types.Results;

namespace SER.API.Controllers.Groups;

[Route("api/groups")]
public class GroupsController(IGroupsService groupsService) : ControllerBase
{
	[HttpPost("save")]
	public async Task<Result> Save([FromBody] GroupBlank blank)
	{
		return await groupsService.Save(blank);
	}

	[HttpPost("remove")]
	public async Task<Result> Remove([FromBody] ID id)
	{
		return await groupsService.Remove(id);
	}

	[HttpGet("get")]
	public async Task<GroupDto?> Get([FromQuery] ID id)
	{
		return await groupsService.Get(id);
	}

	[HttpGet("get_page")]
	public async Task<GroupDto[]> GetPage(Int32 page, Int32 pageSize)
	{
		return await groupsService.GetPage(page, pageSize);
	}

	[HttpGet("get_by_search_text")]
	public async Task<GroupDto[]> GetBySearchText(String searchText)
	{
		return await groupsService.GetBySearchText(searchText);
	}
}