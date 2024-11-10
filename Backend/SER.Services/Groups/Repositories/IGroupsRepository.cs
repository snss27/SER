using SER.Domain.Groups;
using SER.Tools.Types.Results;

namespace SER.Services.Groups.Repositories;
public interface IGroupsRepository
{
	public Task<Result> Save(GroupBlank blank);
}
