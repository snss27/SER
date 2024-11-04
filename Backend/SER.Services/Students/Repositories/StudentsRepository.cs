using SER.Configurator.Connectors;
using SER.Domain.Students;
using SER.Services.Students.Converters;
using SER.Services.Students.Models;
using SER.Services.Students.Repositories.Queries;
using SER.Tools.DataBase;
using SER.Tools.DataBase.Query;

namespace SER.Services.Students.Repositories;

public class StudentsRepository : IStudentsRepository
{
    private const Int32 _countInPage = 50;
    private readonly MainConnector _connector;

    public StudentsRepository(MainConnector connector)
    {
        _connector = connector;
    }

    public async Task<FlatStudent[]> GetFlatStudentsPage(Int32 page)
    {
        Query query = _connector.CreateQuery(Sql.FlatStudents_GetPage);
        {
            Int32 offset = page * _countInPage;

            query.Add(offset);
            query.Add(page, "limit");
        }

        await using IAsyncSeparatelySession session = await _connector.CreateAsyncSession();

        return (await session.GetArray<StudentDB>(query)).ToFlatStudents();
    }
}
