using SER.Domain.AdditionalQualifications;
using SER.Tools.Types;
using SER.Tools.Types.IDs;
using SER.Tools.Types.Results;

namespace SER.Domain.Services;
public interface IAdditionalQualificationsService
{
	public Task<OperationResult> Save(AdditionalQualificationBlank blank);
	public Task<OperationResult> Remove(ID id);
	public Task<AdditionalQualification?> Get(ID id);
	public Task<AdditionalQualification[]> Get(ID[] ids);
	public Task<PagedResult<AdditionalQualification>> GetPage(Int32 page, Int32 pageSize);
	public Task<AdditionalQualification[]> GetBySearchText(String searchText);
}
