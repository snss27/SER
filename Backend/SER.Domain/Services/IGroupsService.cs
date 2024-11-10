using SER.Domain.Groups;
using SER.Domain.Specialities;
using SER.Tools.Types.IDs;
using SER.Tools.Types.Results;

namespace SER.Domain.Services;
public interface IGroupsService
{
	public Task<Result> Save(GroupBlank blank);
	public Task<Result> Remove(ID id);
	public Task<Group> Get(ID id);
	public Task<Group[]> GetAll();
}
