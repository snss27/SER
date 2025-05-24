using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using SER.Database;
using SER.Database.Models.Employees;
using SER.Domain.Employees;
using SER.Domain.Services;
using SER.Services.EducationLevels.Converters;
using SER.Services.Employees.Converters;
using SER.Tools.Types.IDs;
using SER.Tools.Types.Results;
using static SER.Tools.Utils.NumberUtils;

namespace SER.Services.Employees;

public class EmployeesService(SERDbContext dbContext) : IEmployeesService
{
	public async Task<OperationResult> Save(EmployeeBlank blank)
	{
		Result<Employee, Error> result = Employee.Create(blank.Id, blank.Name, blank.SecondName, blank.LastName);
		if(result.IsFailure) return OperationResult.Fail(result.Error);

		Employee employee = result.Value;

		Boolean isNew = blank.Id is null;

		if(isNew)
		{
			EmployeeEntity entity = employee.ToEntity();
			await dbContext.AddAsync(entity);
		}
		else
		{
			EmployeeEntity? entity = await dbContext.Employees.FirstOrDefaultAsync(e => e.Id == employee.Id);

			if (entity is null) return OperationResult.Fail("Работник не найден");

			entity.ApplyChanges(employee);
			dbContext.Update(entity);
		}

		await dbContext.SaveChangesAsync();
		return OperationResult.Success();
	}

	public async Task<OperationResult> Remove(ID id)
	{
		EmployeeEntity? entity = await dbContext.Employees.FirstOrDefaultAsync(e => e.Id == id);
		if (entity is null) return OperationResult.Fail("Работник не найден");

		dbContext.Remove(entity);
		await dbContext.SaveChangesAsync();

		return OperationResult.Success();
	}

	public async Task<Employee?> Get(ID id)
	{
		EmployeeEntity? entity = await dbContext.Employees.FirstOrDefaultAsync(e => e.Id == id);
		return entity?.ToDomain();
	}

	public async Task<Employee[]> Get(ID[] ids)
	{
		List<EmployeeEntity> entities = await dbContext.Employees
			.Where(el => ids.Contains(el.Id))
			.ToListAsync();

		return [.. entities.Select(e => e.ToDomain())];
	}

	//TASK Возвращать totalCount для клиента
	public async Task<Employee[]> GetPage(Int32 page, Int32 pageSize)
	{
		(Int32 offset, Int32 limit) = NormalizeRange(page, pageSize);

		List<EmployeeEntity> entities = await dbContext.Employees
			.OrderByDescending(e => e.CreatedDateTimeUtc)
			.ThenByDescending(e => e.ModifiedDateTimeUtc)
			.Skip(offset)
			.Take(limit)
			.ToListAsync();

		return [.. entities.Select(e => e.ToDomain())];
	}

	public async Task<Employee[]> Get(String searchText)
	{
		List<EmployeeEntity> entites = await dbContext.Employees
			.Where(e =>
			EF.Functions.ILike(e.Name, $"%{searchText}%")
			|| EF.Functions.ILike(e.SecondName, $"%{searchText}%")
			|| EF.Functions.ILike(e.LastName ?? "", $"%{searchText}%")
			)
			.OrderBy(e => e.Name)
			.ThenBy(e => e.SecondName)
			.ThenBy(e => e.LastName)
			.ToListAsync();

		return [.. entites.Select(el => el.ToDomain())];
	}
}