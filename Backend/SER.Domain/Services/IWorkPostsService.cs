using SER.Domain.WorkPosts;
using SER.Tools.Types.IDs;
using SER.Tools.Types.Results;

namespace SER.Domain.Services;
public interface IWorkPostsService
{
	public Task<Result> Save(WorkPostBlank blank);
	public Task<Result> Remove(ID id);
	public Task<WorkPost?> Get(ID id);
	public Task<WorkPost[]> GetPage(Int32 page, Int32 pageSize);
}
