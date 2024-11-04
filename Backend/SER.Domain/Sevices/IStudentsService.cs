using SER.Domain.Students;

public interface IStudentsService
{
    public Task<FlatStudent[]> GetFlatStudentsPage(Int32 page);
}
