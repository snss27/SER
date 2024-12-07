using SER.Domain.EducationLevels;
using SER.Tools.Types.IDs;
using SER.Tools.Types.Results;

namespace SER.Domain.Services;

public interface IEducationLevelsService
{
	#region EducationLevels

	public Task<Result> Save(EducationLevelBlank blank);
	public Task<Result> Remove(ID id);
	public Task<EducationLevel?> Get(ID id);
	public Task<EducationLevel[]> GetPage(Int32 page, Int32 pageSize);

	#endregion

	#region Specialities

	public Task<EducationLevel?> GetSpeciality(ID? id);
	public Task<EducationLevel[]> GetSpecialities(ID[] ids);
	public Task<EducationLevel[]> GetSpecialities(String searchText);

	#endregion
}