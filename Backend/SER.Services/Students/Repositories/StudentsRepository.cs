using SER.Configurator.Connectors;
using SER.Domain.Students;
using SER.Services._base;
using SER.Services.Students.Converters;
using SER.Services.Students.Models;
using SER.Services.Students.Repositories.Queries;
using SER.Tools.DataBase;
using SER.Tools.DataBase.Query;
using SER.Tools.Types.Results;

namespace SER.Services.Students.Repositories;

public class StudentsRepository : BaseRepository, IStudentsRepository
{
	public StudentsRepository(MainConnector connector) : base(connector) { }
	public async Task<Result> Save(StudentBlank blank)
	{
		return Result.Success();
	}
}
