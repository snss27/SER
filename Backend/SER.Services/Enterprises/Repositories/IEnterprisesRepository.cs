using SER.Domain.Enterprises;
using SER.Tools.Types.IDs;
using SER.Tools.Types.Results;

namespace SER.Services.Enterprises.Repositories;
public interface IEnterprisesRepository
{
	public Task<Result> Save(EnterpriseBlank blank);
	public Task<Result> Remove(ID id);
	public Task<Enterprise?> Get(ID id);
	public Task<Enterprise[]> GetPage(Int32 page, Int32 pageSize);
}
