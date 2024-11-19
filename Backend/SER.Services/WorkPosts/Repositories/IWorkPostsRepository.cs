using SER.Domain.WorkPosts;
using SER.Tools.Types.IDs;
using SER.Tools.Types.Results;

namespace SER.Services.WorkPosts.Repositories;
public interface IWorkPostsRepository
{
	public Task<Result> Save(WorkPostBlank blank);
	public Task<Result> Remove(ID id);
	public Task<WorkPost?> Get(ID id);
	public Task<WorkPost[]> GetPage(Int32 page, Int32 pageSize);
}
