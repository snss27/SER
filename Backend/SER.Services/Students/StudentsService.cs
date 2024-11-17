using SER.Domain.Services;
using SER.Domain.Students;
using SER.Services.Students.Repositories;
using SER.Tools.Types.Results;

namespace SER.Services.Students;

public class StudentsService : IStudentsService
{
    private readonly IStudentsRepository _studentsRepository;

    public StudentsService(IStudentsRepository studentsRepository)
    {
        _studentsRepository = studentsRepository;
    }

	//TODO не реализовал
    public async Task<Result> Save(StudentBlank blank)
    {
        return await _studentsRepository.Save(blank);
    }
}
