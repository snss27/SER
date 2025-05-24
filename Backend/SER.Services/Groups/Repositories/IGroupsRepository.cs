using SER.Domain.Groups;
using SER.Tools.Types.IDs;
using SER.Tools.Types.Results;

namespace SER.Services.Groups.Repositories;
public interface IGroupsRepository
{
	public Task<OperationResult> Save(GroupBlank blank);
	public Task<OperationResult> Remove(ID id);
	public Task<Group?> Get(ID id);
	public Task<Group[]> Get(ID[] ids);
	public Task<Group[]> GetPage(Int32 page, Int32 pageSize);
	public Task<Group[]> GetBySearchText(String searchText);
	public Task<Group[]> GetByEducationLevelId(ID eductionLevelId);
}
