using SER.Domain.Groups;
using SER.Tools.Types.IDs;
using SER.Tools.Types.Results;

namespace SER.Domain.Services;
public interface IGroupsService
{
	public Task<Result> Save(GroupBlank blank);
	public Task<Result> Remove(ID id);
	public Task<GroupDto?> Get(ID id);
	public Task<GroupDto[]> GetPage(Int32 page, Int32 pageSize);
	public Task<GroupDto[]> GetBySearchText(String searchText);
}
