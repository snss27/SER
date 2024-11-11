using SER.Configurator.Connectors;
using SER.Domain.Curators;
using SER.Services._base;
using SER.Services.Curators.Converters;
using SER.Services.Curators.Models;
using SER.Services.Curators.Repositories.Queries;
using SER.Tools.DataBase;
using SER.Tools.DataBase.Query;
using SER.Tools.Types.IDs;
using SER.Tools.Types.Results;

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
		}

		await using IAsyncSeparatelySession session = await _connector.CreateAsyncSession();

		await session.Execute(query);

		return Result.Success();
	}
	public async Task<Curator> Get(ID id)
	{
		Query query = _connector.CreateQuery(Sql.Curators_Remove);
		{
			query.Add(id);
		}

		await using IAsyncSeparatelySession session = await _connector.CreateAsyncSession();

		return (await session.Get<CuratorDB>(query)).ToCurator();
	}
}
