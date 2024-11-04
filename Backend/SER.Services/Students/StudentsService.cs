using SER.Domain.Students;
using SER.Services.Students.Repositories;

namespace SER.Services.Students;

public class StudentsService : IStudentsService
{
    private readonly IStudentsRepository _studentsRepository;

    public StudentsService(IStudentsRepository studentsRepository)
    {
        _studentsRepository = studentsRepository;
    }

    public async Task<FlatStudent[]> GetFlatStudentsPage(Int32 page)
    {
        if (page < 0) throw new ArgumentOutOfRangeException(nameof(page));

        return await _studentsRepository.GetFlatStudentsPage(page);
    }
}
