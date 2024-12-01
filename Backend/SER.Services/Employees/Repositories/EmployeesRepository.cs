using SER.Configurator.Connectors;
using SER.Domain.Employees;
using SER.Services._base;
using SER.Services.Employees.Converters;
using SER.Services.Employees.Models;
using SER.Services.Employees.Repositories.Queries;
using SER.Tools.DataBase;
using SER.Tools.DataBase.Query;
using SER.Tools.Types.IDs;
using SER.Tools.Types.Results;
using static SER.Tools.Utils.NumberUtils;

namespace SER.Services.Employees.Repositories;

public class EmployeesRepository(MainConnector connector) : BaseRepository(connector), IEmployeesRepository
{
	public async Task<Result> Save(EmployeeBlank blank)
	{
		Query query = _connector.CreateQuery(Sql.Employees_Save);
		{
			query.Add(blank.Id);
			query.Add(blank.Name);
			query.Add(blank.SecondName);
			query.Add(blank.LastName);
			query.Add(DateTime.UtcNow, "currentdatetimeutc");
		}

		await using IAsyncSeparatelySession session = await _connector.CreateAsyncSession();

		await session.Execute(query);

		return Result.Success();
	}

	public async Task<Employee?> Get(ID id)
	{
		Query query = _connector.CreateQuery(Sql.Employees_Get);
		{
			query.Add(id);
		}

		await using IAsyncSeparatelySession session = await _connector.CreateAsyncSession();

		return (await session.Get<EmployeeDB?>(query))?.ToEmployee();
	}

	public async Task<Employee[]> Get(ID[] ids)
	{
		Query query = _connector.CreateQuery(Sql.Employees_GetByIds);
		{
			query.Add(ids);
		}

		await using IAsyncSeparatelySession session = await _connector.CreateAsyncSession();

		return (await session.GetArray<EmployeeDB>(query)).ToEmployees();
	}

	public async Task<Employee[]> GetPage(Int32 page, Int32 pageSize)
	{
		Query query = _connector.CreateQuery(Sql.Employees_GetPage);
		{
			(Int32 offset, Int32 limit) = NormalizeRange(page, pageSize);
			query.Add(offset);
			query.Add(limit);
		}

		await using IAsyncSeparatelySession session = await _connector.CreateAsyncSession();

		return (await session.GetArray<EmployeeDB>(query)).ToEmployees();
	}

	public async Task<Employee[]> Get(String searchText)
	{
		Query query = _connector.CreateQuery(Sql.Employees_GetBySearchText);
		{
			query.Add(searchText);
		}

		await using IAsyncSeparatelySession session = await _connector.CreateAsyncSession();

		return (await session.GetArray<EmployeeDB>(query)).ToEmployees();
	}

	public async Task<Result> Remove(ID id)
	{
		Query query = _connector.CreateQuery(Sql.Employees_Remove);
		{
			query.Add(id);
			query.Add(DateTime.UtcNow, "currentdatetimeutc");
		}

		await using IAsyncTransactionSession transaction = await _connector.CreateAsyncTransaction();

		await Task.WhenAll(
			transaction.Execute(query),
			RemoveFromGroups(id, transaction)
		);

		return Result.Success();
	}

	private async Task RemoveFromGroups(ID curatorId, IAsyncTransactionSession transaction)
	{
		Query query = _connector.CreateQuery(Sql.Groups_RemoveCuratorById);
		{
			query.Add(curatorId);
			query.Add(DateTime.UtcNow, "currentdatetimeutc");
		}

		await transaction.Execute(query);
	}
}