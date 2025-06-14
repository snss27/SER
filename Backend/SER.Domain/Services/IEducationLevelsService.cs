using SER.Domain.EducationLevels;
using SER.Tools.Types;
using SER.Tools.Types.IDs;
using SER.Tools.Types.Results;

namespace SER.Domain.Services;

public interface IEducationLevelsService
{
	public Task<OperationResult> Save(EducationLevelBlank blank);
	public Task<OperationResult> Remove(ID id);
	public Task<EducationLevel?> Get(ID id);
	public Task<EducationLevel[]> Get(ID[] ids);
	public Task<PagedResult<EducationLevel>> GetPage(Int32 page, Int32 pageSize);
	public Task<EducationLevel[]> Get(String searchText);
}