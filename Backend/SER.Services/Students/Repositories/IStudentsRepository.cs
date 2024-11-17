using SER.Domain.Students;
using SER.Tools.Types.Results;

namespace SER.Services.Students.Repositories;

public interface IStudentsRepository
{
    public Task<Result> Save(StudentBlank blank);
}
