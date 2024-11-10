using SER.Domain.Groups;
using SER.Tools.Types.Results;

namespace SER.Domain.Services;
public interface IGroupsService
{
	public Task<Result> Save(GroupBlank blank); 
}
