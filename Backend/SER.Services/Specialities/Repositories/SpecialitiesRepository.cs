using SER.Configurator.Connectors;
using SER.Domain.Specialities;
using SER.Services.Specialities.Converters;
using SER.Services.Specialities.Models;
using SER.Services.Specialities.Repositories.Queries;
using SER.Tools.DataBase;
using SER.Tools.DataBase.Query;
using SER.Tools.Types.IDs;
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
			query.Add(db.StudyYears);
			query.Add(DateTime.UtcNow, "p_currentdatetimeutc");
		}

		await using IAsyncSeparatelySession session = await _connector.CreateAsyncSession();

		await session.Execute(query);

		return Result.Success();
	}

	public async Task<Result> Remove(ID id)
	{
		Query query = _connector.CreateQuery(Sql.Specialities_Remove);
		{
			query.Add(id);
		}

		await using IAsyncSeparatelySession session = await _connector.CreateAsyncSession();

		await session.Execute(query);

		return Result.Success();
	}

	public async Task<Speciality> Get(ID id)
	{
		Query query = _connector.CreateQuery(Sql.Specialities_Get);
		{
			query.Add(id);
		}

		await using IAsyncSeparatelySession session = await _connector.CreateAsyncSession();

		return (await session.Get<SpecialityDB>(query)).ToSpeciality();
	}

	public async Task<Speciality[]> GetAll()
	{
		Query query = _connector.CreateQuery(Sql.Specialities_GetAll);

		await using IAsyncSeparatelySession session = await _connector.CreateAsyncSession();

		return (await session.GetArray<SpecialityDB>(query)).ToSpecialities();
	}
}
