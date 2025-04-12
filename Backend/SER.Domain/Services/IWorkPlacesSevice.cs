using SER.Domain.Workplaces;
using SER.Domain.WorkPlaces;
using SER.Tools.Types.IDs;
using SER.Tools.Types.Results;

namespace SER.Domain.Services;
public interface IWorkPlacesSevice
{
	public Task<DataResult<ID>> Save(WorkPlaceBlank blank, String groupAlias, String studentAlias);
	public Task<DataResult<ID[]>> Save(WorkPlaceBlank[] blanks, String groupAlias, String studentAlias);
	public Task<WorkPlaceDto?> Get(ID id);
	public Task<WorkPlaceDto[]> Get(ID[] ids);
}
