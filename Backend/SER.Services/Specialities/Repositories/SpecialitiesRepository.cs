using SER.Configurator.Connectors;
using SER.Domain.Specialities;
using SER.Services.Specialities.Repositories.Queries;
using SER.Tools.DataBase;
using SER.Tools.DataBase.Query;
using SER.Tools.Types.Results;

namespace SER.Services.Specialities.Repositories;
public class SpecialitiesRepository : ISpecialitiesRepository
{
	private readonly MainConnector _connector;

	public SpecialitiesRepository(MainConnector connector)
	{
		_connector = connector;
	}
	public async Task<Result> Save(SpecialityBlank db)
	{
		Query query = _connector.CreateQuery(Sql.Specialities_Save);
		{
			query.Add(db.Id);
			query.Add(db.Name);
			query.Add(db.StudyYears, "p_study_years");
			query.Add(DateTime.UtcNow, "p_current_date_time_utc");
		}

		await using IAsyncSeparatelySession session = await _connector.CreateAsyncSession();

		await session.Execute(query);

		return Result.Success();
	}
}
