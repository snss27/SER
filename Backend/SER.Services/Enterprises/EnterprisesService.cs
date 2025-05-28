using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using SER.Database;
using SER.Database.Models.Enterprises;
using SER.Domain.Enterprises;
using SER.Domain.Services;
using SER.Services.Employees.Converters;
using SER.Services.Enterprises.Converters;
using SER.Tools.Types.IDs;
using SER.Tools.Types.Results;
using static SER.Tools.Utils.NumberUtils;

namespace SER.Services.Enterprises;

public class EnterprisesService(SERDbContext dbContext) : IEnterprisesService
{
	public async Task<OperationResult> Save(EnterpriseBlank blank)
	{
		Result<Enterprise, Error> result = Enterprise.Create(blank.Id, blank.Name, blank.LegalAddress, blank.ActualAddress, blank.Address, blank.INN, blank.KPP, blank.ORGN, blank.Phone, blank.Mail, blank.IsOPK);
		if (result.IsFailure) return OperationResult.Fail(result.Error);

		Enterprise enterprise = result.Value;

		Boolean isNew = blank.Id is null;

		if (isNew)
		{
			EnterpriseEntity entity = enterprise.ToEntity();
			await dbContext.AddAsync(entity);
		}
		else
		{
			EnterpriseEntity? entity = await dbContext.Enterprises.FirstOrDefaultAsync(e => e.Id == enterprise.Id);
			if (entity is null) return OperationResult.Fail("Предприятие не найдено");

			entity.ApplyChanges(enterprise);
			dbContext.Update(entity);
		}

		await dbContext.SaveChangesAsync();
		return OperationResult.Success();
	}

	public async Task<OperationResult> Remove(ID id)
	{
		Boolean hasWokPlaces = await dbContext.WorkPlaces.AnyAsync(wp => wp.EnterpriseId == id);
		if(hasWokPlaces)
		{
			return OperationResult.Fail("Невозможно удалить, т.к. есть места работы с данной огранизацией");
		}

		EnterpriseEntity? entity = await dbContext.Enterprises.FirstOrDefaultAsync(e => e.Id == id);
		if (entity is null) return OperationResult.Fail("Организация не найдена");

		dbContext.Remove(entity);
		await dbContext.SaveChangesAsync();

		return OperationResult.Success();
	}

	public async Task<Enterprise?> Get(ID id)
	{
		EnterpriseEntity? entity = await dbContext.Enterprises.FirstOrDefaultAsync(e => e.Id == id);
		return entity?.ToDomain();
	}

	public async Task<Enterprise[]> Get(ID[] ids)
	{
		List<EnterpriseEntity> entities = await dbContext.Enterprises
			.Where(el => ids.Contains(el.Id))
			.ToListAsync();

		return [.. entities.Select(e => e.ToDomain())];
	}

	//TASK Возвращать totalCount для клиента
	public async Task<Enterprise[]> GetPage(Int32 page, Int32 pageSize)
	{
		(Int32 offset, Int32 limit) = NormalizeRange(page, pageSize);

		List<EnterpriseEntity> entities = await dbContext.Enterprises
			.OrderByDescending(e => e.CreatedDateTimeUtc)
			.ThenByDescending(e => e.ModifiedDateTimeUtc)
			.Skip(offset)
			.Take(limit)
			.ToListAsync();

		return [.. entities.Select(e => e.ToDomain())];
	}

	public async Task<Enterprise[]> GetBySearchText(String searchText)
	{
		List<EnterpriseEntity> entites = await dbContext.Enterprises
			.Where(e => EF.Functions.ILike(e.Name, $"%{searchText}%"))
			.OrderBy(e => e.Name)
			.ToListAsync();

		return [.. entites.Select(el => el.ToDomain())];
	}
}
