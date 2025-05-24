using Microsoft.AspNetCore.Mvc;
using SER.Domain.Groups;
using SER.Domain.Groups.Converters;
using SER.Domain.Services;
using SER.Tools.Types.IDs;
using SER.Tools.Types.Results;

namespace SER.API.Controllers.Groups;

[Route("api/groups")]
public class GroupsController(IGroupsService groupsService) : ControllerBase
{
	[HttpPost("save")]
	public async Task<OperationResult> Save([FromBody] GroupBlank blank)
	{
		return await groupsService.Save(blank);
	}

	[HttpPost("remove")]
	public async Task<OperationResult> Remove([FromBody] ID id)
	{
		return await groupsService.Remove(id);
	}

	[HttpGet("get")]
	public async Task<GroupDto?> Get([FromQuery] ID id)
	{
		Group? group = await groupsService.Get(id);
		return group?.ToDto();
	}

	[HttpGet("get_page")]
	public async Task<GroupDto[]> GetPage(Int32 page, Int32 pageSize)
	{
		Group[] groups = await groupsService.GetPage(page, pageSize);
		return [.. groups.Select(group => group.ToDto())];
	}

	[HttpGet("get_by_search_text")]
	public async Task<GroupDto[]> GetBySearchText(String searchText)
	{
		Group[] groups = await groupsService.GetBySearchText(searchText);
		return [.. groups.Select(group => group.ToDto())];
	}
}