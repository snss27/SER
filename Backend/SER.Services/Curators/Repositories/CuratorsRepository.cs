using SER.Configurator.Connectors;
using SER.Domain.Curators;
using SER.Domain.Specialities;
using SER.Services._base;
using SER.Services.Curators.Converters;
using SER.Services.Curators.Models;
using SER.Services.Curators.Repositories.Queries;
using SER.Services.Specialities.Models;
using SER.Tools.DataBase;
using SER.Tools.DataBase.Query;
using SER.Tools.Types.IDs;
using SER.Tools.Types.Results;
using static SER.Tools.Utils.NumberUtils;

namespace SER.Services.Curators.Repositories;
public class CuratorsRepository : BaseRepository, ICuratorsRepository
{
	public CuratorsRepository(MainConnector connector) : base(connector) { }

	public async Task<Result> Save(CuratorBlank blank)
	{
		Query query = _connector.CreateQuery(Sql.Curators_Save);
		{
			query.Add(blank.Id);
			query.Add(blank.Name);
			query.Add(blank.Surname);
			query.Add(blank.Patronymic);
			query.Add(DateTime.UtcNow, "currentdatetimeutc");
		}

		await using IAsyncSeparatelySession session = await _connector.CreateAsyncSession();

		await session.Execute(query);

		return Result.Success();
	}
	public async Task<Result> Remove(ID id)
	{
		Query query = _connector.CreateQuery(Sql.Curators_Remove);
		{
			query.Add(id);
			query.Add(DateTime.UtcNow, "currentdatetimeutc");
		}

		await using IAsyncSeparatelySession session = await _connector.CreateAsyncSession();

		await session.Execute(query);

		return Result.Success();
	}
	public async Task<Curator?> Get(ID id)
	{
		Query query = _connector.CreateQuery(Sql.Curators_Get);
		{
			query.Add(id);
		}

		await using IAsyncSeparatelySession session = await _connector.CreateAsyncSession();

		return (await session.Get<CuratorDB?>(query))?.ToCurator();
	}

	public async Task<Curator[]> Get(ID[] ids)
	{
		Query query = _connector.CreateQuery(Sql.Curators_GetByIds);
		{
			query.Add(ids);
		}

		await using IAsyncSeparatelySession session = await _connector.CreateAsyncSession();

		return (await session.GetArray<CuratorDB>(query)).ToCurators();
	}

	public async Task<Curator[]> GetPage(Int32 page, Int32 pageSize)
	{
		Query query = _connector.CreateQuery(Sql.Curators_GetPage);
		{
			(Int32 offset, Int32 limit) = NormalizeRange(page, pageSize);
			query.Add(offset);
			query.Add(limit);
		}

		await using IAsyncSeparatelySession session = await _connector.CreateAsyncSession();

		return (await session.GetArray<CuratorDB>(query)).ToCurators();
	}

	public async Task<Curator[]> Get(String searchText)
	{
		Query query = _connector.CreateQuery(Sql.Curators_GetBySearchText);
		{
			query.Add(searchText);
		}

		await using IAsyncSeparatelySession session = await _connector.CreateAsyncSession();

		return (await session.GetArray<CuratorDB>(query)).ToCurators();
	}
}
