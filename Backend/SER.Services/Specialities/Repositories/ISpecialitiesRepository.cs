using SER.Domain.Specialities;
using SER.Tools.Types.Results;

namespace SER.Services.Specialities.Repositories;
public interface ISpecialitiesRepository
{
	public Task<Result> Save(SpecialityBlank db);
}
