using SER.Domain.Specialities;
using SER.Tools.Types.IDs;
using SER.Tools.Types.Results;

namespace SER.Services.Specialities.Repositories;
public interface ISpecialitiesRepository
{
	public Task<Result> Save(SpecialityBlank db);
	public Task<Result> Remove(ID id);
	public Task<Speciality> Get(ID id);
	public Task<Speciality[]> GetAll();
}
