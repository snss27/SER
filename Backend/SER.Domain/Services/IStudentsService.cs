using SER.Domain.Students;
using SER.Tools.Types.Results;

namespace SER.Domain.Services;
public interface IStudentsService
{
    public Task<Result> Save(StudentBlank blank);
}
