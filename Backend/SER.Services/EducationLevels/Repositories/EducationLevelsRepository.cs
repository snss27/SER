using SER.Configurator.Connectors;
using SER.Domain.EducationLevels;
using SER.Services._base;
using SER.Services.EducationLevels.Converters;
using SER.Services.EducationLevels.Models;
using SER.Services.EducationLevels.Repositories.Queries;
using SER.Tools.DataBase;
using SER.Tools.DataBase.Query;
using SER.Tools.Types.IDs;
using SER.Tools.Types.Results;
using static SER.Tools.Utils.NumberUtils;

namespace SER.Services.EducationLevels.Repositories;
public class EducationLevelsRepository(MainConnector connector) : BaseRepository(connector), IEducationLevelsRepository
{
	public async Task<Result> Save(EducationLevelBlank blank)
	{
		Query query = _connector.CreateQuery(Sql.EducationLevels_Save);
		{
			query.Add(blank.Id);
			query.Add(blank.Type);
			query.Add(blank.Name);
			query.Add(blank.Code);
			query.Add(blank.StudyTime);
			query.Add(DateTime.UtcNow, "p_currentdatetimeutc");
		}

		await using IAsyncSeparatelySession session = await _connector.CreateAsyncSession();

		await session.Execute(query);

		return Result.Success();
	}

	public async Task<Result> Remove(ID id)
	{
		Query query = _connector.CreateQuery(Sql.EducationLevels_Remove);
		{
			query.Add(id);
			query.Add(DateTime.UtcNow, "p_currentdatetimeutc");
		}

		await using IAsyncSeparatelySession session = await _connector.CreateAsyncSession();

		await session.Execute(query);

		return Result.Success();
	}

	public async Task<EducationLevel?> Get(ID id)
	{
		Query query = _connector.CreateQuery(Sql.EducationLevels_Get);
		{
			query.Add(id);
		}

		await using IAsyncSeparatelySession session = await _connector.CreateAsyncSession();

		return (await session.Get<EducationLevelDB?>(query))?.ToEducationLevel();
	}

	public async Task<EducationLevel[]> Get(ID[] ids)
	{
		Query query = _connector.CreateQuery(Sql.EducationLevels_GetByIds);
		{
			query.Add(ids);
		}

		await using IAsyncSeparatelySession session = await _connector.CreateAsyncSession();

		return (await session.GetArray<EducationLevelDB>(query)).ToEducationLevels();
	}

	public async Task<EducationLevel[]> GetPage(Int32 page, Int32 pageSize)
	{
		Query query = _connector.CreateQuery(Sql.EducationLevels_GetPage);
		{
			(Int32 offset, Int32 limit) = NormalizeRange(page, pageSize);
			query.Add(offset);
			query.Add(limit);
		}

		await using IAsyncSeparatelySession session = await _connector.CreateAsyncSession();

		return (await session.GetArray<EducationLevelDB>(query)).ToEducationLevels();
	}

	public async Task<EducationLevel[]> Get(String searchText)
	{
		Query query = _connector.CreateQuery(Sql.EducationLevels_GetBySearchText);
		{
			query.Add(searchText);
		}

		await using IAsyncSeparatelySession session = await _connector.CreateAsyncSession();

		return (await session.GetArray<EducationLevelDB>(query)).ToEducationLevels();
	}
}
