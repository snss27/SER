using SER.Domain.Services;
using SER.Domain.WorkPosts;
using SER.Services.WorkPosts.Repositories;
using SER.Tools.Types.IDs;
using SER.Tools.Types.Results;

namespace SER.Services.WorkPosts;
public class WorkPostsService : IWorkPostsService
{
	private readonly IWorkPostsRepository _workPostsRepository;

	public WorkPostsService(IWorkPostsRepository workPostsRepository)
	{
		_workPostsRepository = workPostsRepository;
	}

	public async Task<Result> Save(WorkPostBlank blank)
	{
		if (String.IsNullOrWhiteSpace(blank.Name)) return Result.Fail("Укажите название");

		blank.Id ??= ID.New();

		return await _workPostsRepository.Save(blank);
	}

	public async Task<Result> Remove(ID id)
	{
		return await _workPostsRepository.Remove(id);
	}

	public async Task<WorkPost?> Get(ID id)
	{
		return await _workPostsRepository.Get(id);
	}

	public async Task<WorkPost[]> GetPage(Int32 page, Int32 pageSize)
	{
		return await _workPostsRepository.GetPage(page, pageSize);
	}
}
