using SER.Domain.Students;

namespace SER.Domain.Services;
public interface IStudentsService
{
    public Task<FlatStudent[]> GetFlatStudentsPage(Int32 page);
}
