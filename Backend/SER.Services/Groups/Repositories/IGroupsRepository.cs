using SER.Domain.Groups;
using SER.Tools.Types.IDs;
using SER.Tools.Types.Results;

namespace SER.Services.Groups.Repositories;
public interface IGroupsRepository
{
	public Task<Result> Save(GroupBlank blank);
	public Task<Result> Remove(ID id);
	public Task<Group?> Get(ID id);
	public Task<Group[]> GetPage(Int32 page, Int32 pageSize);
}
