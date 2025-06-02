using SER.Domain.Students;
using SER.Tools.Types.IDs;
using SER.Tools.Types.Results;

namespace SER.Domain.Services;
public interface IStudentsService
{
    public Task<OperationResult> Save(StudentBlank blank);
	public Task<OperationResult> Remove(ID id);
	public Task<Student?> Get(ID id);
	public Task<Student[]> GetPage(Int32 page, Int32 pageSize);
}
