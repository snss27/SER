using SER.Domain.Groups;
using SER.Tools.Types;
using SER.Tools.Types.IDs;
using SER.Tools.Types.Results;

namespace SER.Domain.Services;
public interface IGroupsService
{
	public Task<OperationResult> Save(GroupBlank blank);
	public Task<OperationResult> Remove(ID id);
	public Task<Group?> Get(ID id);
	public Task<Group[]> Get(ID[] ids);
	public Task<PagedResult<Group>> GetPage(Int32 page, Int32 pageSize);
	public Task<Group[]> GetBySearchText(String searchText);
}
