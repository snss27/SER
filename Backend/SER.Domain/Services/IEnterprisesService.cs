using SER.Domain.Enterprises;
using SER.Tools.Types;
using SER.Tools.Types.IDs;
using SER.Tools.Types.Results;

namespace SER.Domain.Services;
public interface IEnterprisesService
{
	public Task<OperationResult> Save(EnterpriseBlank blank);
	public Task<OperationResult> Remove(ID id);
	public Task<Enterprise?> Get(ID id);
	public Task<Enterprise[]> Get(ID[] ids);
	public Task<PagedResult<Enterprise>> GetPage(Int32 page, Int32 pageSize);
	public Task<Enterprise[]> GetBySearchText(String searchText);
}
