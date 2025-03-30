using SER.Domain.AdditionalQualifications;
using SER.Tools.Types.IDs;
using SER.Tools.Types.Results;

namespace SER.Domain.Services;
public interface IAdditionalQualificationsService
{
	public Task<Result> Save(AdditionalQualificationBlank blank);
	public Task<Result> Remove(ID id);
	public Task<AdditionalQualification?> Get(ID id);
	public Task<AdditionalQualification[]> GetPage(Int32 page, Int32 pageSize);
	public Task<AdditionalQualification[]> GetBySearchText(String searchText);
}
