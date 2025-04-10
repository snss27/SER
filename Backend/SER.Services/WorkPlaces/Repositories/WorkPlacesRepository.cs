using SER.Configurator.Connectors;
using SER.Domain.Workplaces;
using SER.Services._base;
using SER.Tools.DataBase.Query;
using SER.Tools.DataBase;
using SER.Tools.Types.IDs;
using SER.Tools.Types.Results;
using SER.Services.WorkPlaces.Repositories.Queries;

namespace SER.Services.WorkPlaces.Repositories;
public class WorkPlacesRepository(MainConnector connector) : BaseRepository(connector), IWorkPlacesRepository
{
	public async Task<ID> Save(WorkPlaceBlank blank, String? workBookExtractFile)
	{
		Query query = _connector.CreateQuery(Sql.WorkPlaces_Save);
		{
			query.Add(blank.Id);
			query.Add(blank.Enterprise!.Id, "p_enterpriseid");
			query.Add(blank.Post);
			query.Add(workBookExtractFile);
			query.Add(blank.StartDate);
			query.Add(blank.FinishDate);
			query.Add(DateTime.UtcNow, "p_currentdatetimeutc");
		}

		await using IAsyncSeparatelySession session = await _connector.CreateAsyncSession();

		return await session.Get<ID>(query);
	}
}
