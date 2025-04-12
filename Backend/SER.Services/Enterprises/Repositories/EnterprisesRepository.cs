using SER.Configurator.Connectors;
using SER.Domain.Enterprises;
using SER.Services._base;
using SER.Services.Enterprises.Converters;
using SER.Services.Enterprises.Models;
using SER.Services.Enterprises.Repositories.Queries;
using SER.Tools.DataBase;
using SER.Tools.DataBase.Query;
using SER.Tools.Types.IDs;
using SER.Tools.Types.Results;
using static SER.Tools.Utils.NumberUtils;

namespace SER.Services.Enterprises.Repositories;
public class EnterprisesRepository(MainConnector connector) : BaseRepository(connector), IEnterprisesRepository
{
	public async Task<Result> Save(EnterpriseBlank blank)
	{
		Query query = _connector.CreateQuery(Sql.Enterprises_Save);
		{
			query.Add(blank.Id);
			query.Add(blank.Name);
			query.Add(blank.LegalAddress);
			query.Add(blank.ActualAddress);
			query.Add(blank.Address);
			query.Add(blank.INN);
			query.Add(blank.KPP);
			query.Add(blank.ORGN);
			query.Add(blank.Phone);
			query.Add(blank.Mail);
			query.Add(blank.IsOPK);
			query.Add(DateTime.UtcNow, "p_currentdatetimeutc");
		}

		await using IAsyncSeparatelySession session = await _connector.CreateAsyncSession();

		await session.Execute(query);

		return Result.Success();
	}

	public async Task<Result> Remove(ID id)
	{
		Query query = _connector.CreateQuery(Sql.Enterprises_Remove);
		{
			query.Add(id);
			query.Add(DateTime.UtcNow, "p_currentdatetimeutc");
		}

		await using IAsyncSeparatelySession session = await _connector.CreateAsyncSession();

		await session.Execute(query);

		return Result.Success();
	}

	public async Task<Enterprise?> Get(ID id)
	{
		Query query = _connector.CreateQuery(Sql.Enterprises_Get);
		{
			query.Add(id);
		}

		await using IAsyncSeparatelySession session = await _connector.CreateAsyncSession();

		return (await session.Get<EnterpriseDB?>(query))?.ToEnterprise();
	}

	public async Task<Enterprise[]> Get(ID[] ids)
	{
		Query query = _connector.CreateQuery(Sql.Enterprises_GetByIds);
		{
			query.Add(ids);
		}

		await using IAsyncSeparatelySession session = await _connector.CreateAsyncSession();

		return (await session.GetArray<EnterpriseDB>(query)).ToEnterprises();
	}

	public async Task<Enterprise[]> GetPage(Int32 page, Int32 pageSize)
	{
		Query query = _connector.CreateQuery(Sql.Enterprises_GetPage);
		{
			(Int32 offset, Int32 limit) = NormalizeRange(page, pageSize);
			query.Add(offset);
			query.Add(limit);
		}

		await using IAsyncSeparatelySession session = await _connector.CreateAsyncSession();

		return (await session.GetArray<EnterpriseDB>(query)).ToEnterprises();
	}

	public async Task<Enterprise[]> GetBySearchText(String searchText)
	{
		Query query = _connector.CreateQuery(Sql.Enterprises_GetBySearchText);
		{
			query.Add(searchText);
		}

		await using IAsyncSeparatelySession session = await _connector.CreateAsyncSession();

		return (await session.GetArray<EnterpriseDB>(query)).ToEnterprises();
	}
}
