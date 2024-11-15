using SER.Domain.Specialities;
using SER.Tools.Types.IDs;
using SER.Tools.Types.Results;

namespace SER.Domain.Services;
public interface ISpecialitiesService
{
	public Task<Result> Save(SpecialityBlank blank);
	public Task<Result> Remove(ID id);
	public Task<Speciality?> Get(ID id);
	public Task<Speciality[]> Get(ID[] ids);
	public Task<Speciality[]> GetPage(Int32 page, Int32 pageSize);
	public Task<Speciality[]> Get(String searchText);
}
