using SER.Configurator.Connectors;
using SER.Domain.Specialities;
using SER.Services._base;
using SER.Services.Specialities.Converters;
using SER.Services.Specialities.Models;
using SER.Services.Specialities.Repositories.Queries;
using SER.Tools.DataBase;
using SER.Tools.DataBase.Query;
using SER.Tools.Types.IDs;
using SER.Tools.Types.Results;

namespace SER.Services.Specialities.Repositories;
public class SpecialitiesRepository : BaseRepository, ISpecialitiesRepository
{
	public SpecialitiesRepository(MainConnector connector) : base(connector) { }

	public async Task<Result> Save(SpecialityBlank blank)
	{
		Query query = _connector.CreateQuery(Sql.Specialities_Save);
		{
			query.Add(blank.Id);
			query.Add(blank.Name);
			query.Add(blank.StudyYears);
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
			query.Add(DateTime.UtcNow, "p_currentdatetimeutc");
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
