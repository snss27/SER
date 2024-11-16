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
using static SER.Tools.Utils.NumberUtils;

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
			query.Add(blank.Code);
			query.Add(blank.StudyYears);
			query.Add(blank.StudyMonths);
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

	public async Task<Speciality?> Get(ID id)
	{
		Query query = _connector.CreateQuery(Sql.Specialities_Get);
		{
			query.Add(id);
		}

		await using IAsyncSeparatelySession session = await _connector.CreateAsyncSession();

		return (await session.Get<SpecialityDB?>(query))?.ToSpeciality();
	}

	public async Task<Speciality[]> Get(ID[] ids)
	{
		Query query = _connector.CreateQuery(Sql.Specialities_GetByIds);
		{
			query.Add(ids);
		}

		await using IAsyncSeparatelySession session = await _connector.CreateAsyncSession();

		return (await session.GetArray<SpecialityDB>(query)).ToSpecialities();
	}

	public async Task<Speciality[]> GetPage(Int32 page, Int32 pageSize)
	{
		Query query = _connector.CreateQuery(Sql.Specialities_GetPage);
		{
			(Int32 offset, Int32 limit) = NormalizeRange(page, pageSize);
			query.Add(offset);
			query.Add(limit);
		}

		await using IAsyncSeparatelySession session = await _connector.CreateAsyncSession();

		return (await session.GetArray<SpecialityDB>(query)).ToSpecialities();
	}

	public async Task<Speciality[]> Get(String searchText)
	{
		Query query = _connector.CreateQuery(Sql.Specialities_GetBySearchText);
		{
			query.Add(searchText);
		}

		await using IAsyncSeparatelySession session = await _connector.CreateAsyncSession();

		return (await session.GetArray<SpecialityDB>(query)).ToSpecialities();
	}
}
