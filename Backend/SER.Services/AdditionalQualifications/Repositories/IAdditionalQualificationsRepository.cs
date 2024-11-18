using SER.Domain.AdditionalQualifications;
using SER.Tools.Types.IDs;
using SER.Tools.Types.Results;

namespace SER.Services.AdditionalQualifications.Repositories;
public interface IAdditionalQualificationsRepository
{
	public Task<Result> Save(AdditionalQualificationBlank blank);
	public Task<Result> Remove(ID id);
	public Task<AdditionalQualification?> Get(ID id);
	public Task<AdditionalQualification[]> GetPage(Int32 page, Int32 pageSize);
}
