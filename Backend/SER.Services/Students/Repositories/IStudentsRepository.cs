using SER.Domain.Students;
using SER.Tools.Types.IDs;
using SER.Tools.Types.Results;

namespace SER.Services.Students.Repositories;

public interface IStudentsRepository
{
	Task<Result> Save(StudentBlank blank, ID? currentWorkplaceId, ID[] prevWorkplaceIds, String[] passportFileUrls, String? targetAgreementFileUrl, String? armySunpoenaFileUrl, String[] otherFileUrls);
	Task<Result> Remove(ID id);
	Task<Student?> Get(ID id);
	Task<Student[]> GetPage(int page, int pageSize);
	Task<Student[]> GetByGroupId(ID groupId);
}