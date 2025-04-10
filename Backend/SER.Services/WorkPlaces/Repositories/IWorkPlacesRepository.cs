using SER.Domain.Workplaces;
using SER.Tools.Types.IDs;

namespace SER.Services.WorkPlaces.Repositories;
public interface IWorkPlacesRepository
{
	public Task<ID> Save(WorkPlaceBlank blank, String? workBookExtractFile); 
}

