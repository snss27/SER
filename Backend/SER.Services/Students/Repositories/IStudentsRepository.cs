using SER.Domain.Students;

namespace SER.Services.Students.Repositories;

public interface IStudentsRepository
{
    public Task<FlatStudent[]> GetFlatStudentsPage(Int32 page);
}
