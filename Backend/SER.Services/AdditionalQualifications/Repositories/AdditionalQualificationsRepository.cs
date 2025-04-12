using SER.Configurator.Connectors;
using SER.Domain.AdditionalQualifications;
using SER.Services._base;
using SER.Services.AdditionalQualifications.Converters;
using SER.Services.AdditionalQualifications.Models;
using SER.Services.AdditionalQualifications.Repositories.Queries;
using SER.Tools.DataBase;
using SER.Tools.DataBase.Query;
using SER.Tools.Types.IDs;
using SER.Tools.Types.Results;
using static SER.Tools.Utils.NumberUtils;

namespace SER.Services.AdditionalQualifications.Repositories;

public class AdditionalQualificationsRepository(MainConnector connector)
	: BaseRepository(connector), IAdditionalQualificationsRepository
{
	public async Task<Result> Save(AdditionalQualificationBlank blank)
	{
		Query query = _connector.CreateQuery(Sql.AdditionalQualifications_Save);
		{
			query.Add(blank.Id);
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
		Query query = _connector.CreateQuery(Sql.AdditionalQualifications_Remove);
		{
			query.Add(id);
			query.Add(DateTime.UtcNow, "p_currentdatetimeutc");
		}

		await using IAsyncSeparatelySession session = await _connector.CreateAsyncSession();

		await session.Execute(query);

		return Result.Success();
	}

	public async Task<AdditionalQualification?> Get(ID id)
	{
		Query query = _connector.CreateQuery(Sql.AdditionalQualifications_Get);
		{
			query.Add(id);
		}

		await using IAsyncSeparatelySession session = await _connector.CreateAsyncSession();

		return (await session.Get<AdditionalQualificationDB?>(query))?.ToAdditionalQualification();
	}

	public async Task<AdditionalQualification[]> Get(ID[] ids)
	{
		Query query = _connector.CreateQuery(Sql.AdditionalQualifications_GetByIds);
		{
			query.Add(ids);
		}

		await using IAsyncSeparatelySession session = await _connector.CreateAsyncSession();

		return (await session.GetArray<AdditionalQualificationDB>(query)).ToAdditionalQualifications();
	}

	public async Task<AdditionalQualification[]> GetPage(Int32 page, Int32 pageSize)
	{
		Query query = _connector.CreateQuery(Sql.AdditionalQualifications_GetPage);
		{
			(Int32 offset, Int32 limit) = NormalizeRange(page, pageSize);
			query.Add(offset);
			query.Add(limit);
		}

		await using IAsyncSeparatelySession session = await _connector.CreateAsyncSession();

		return (await session.GetArray<AdditionalQualificationDB>(query)).ToAdditionalQualifications();
	}

	public async Task<AdditionalQualification[]> GetBySearchText(String searchText)
	{
		Query query = _connector.CreateQuery(Sql.AdditionalQualifications_GetBySearchText);
		{
			query.Add(searchText);
		}

		await using IAsyncSeparatelySession session = await _connector.CreateAsyncSession();

		return (await session.GetArray<AdditionalQualificationDB>(query)).ToAdditionalQualifications();
	}
}