using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using SER.Database;
using SER.Database.Models.AdditionalQualifications;
using SER.Domain.AdditionalQualifications;
using SER.Domain.Services;
using SER.Services.AdditionalQualifications.Converters;
using SER.Tools.Types;
using SER.Tools.Types.IDs;
using SER.Tools.Types.Results;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;
using static SER.Tools.Utils.NumberUtils;
namespace SER.Services.AdditionalQualifications;

public class AdditionalQualificationsService(SERDbContext dbContext) : IAdditionalQualificationsService
{
	public async Task<OperationResult> Save(AdditionalQualificationBlank blank)
	{
		Result<AdditionalQualification, Error> result = AdditionalQualification.Create(blank.Id, blank.Name, blank.Code, blank.StudyTime);
		if (result.IsFailure) return OperationResult.Fail(result.Error);

		AdditionalQualification additionalQualification = result.Value;

		Boolean isNew = blank.Id is null;

		if (isNew)
		{
			AdditionalQualificationEntity entity = additionalQualification.ToEntity();
			await dbContext.AddAsync(entity);
		}
		else
		{
			AdditionalQualificationEntity? entity = await dbContext.AdditionalQualifications.FirstOrDefaultAsync(aq => aq.Id == additionalQualification.Id);

			if (entity is null) return OperationResult.Fail("Квалификация не найдена");

			entity.ApplyChanges(additionalQualification);
			dbContext.Update(entity);
		}

		await dbContext.SaveChangesAsync();
		return OperationResult.Success();
	}

	public async Task<OperationResult> Remove(ID id)
	{
		AdditionalQualificationEntity? entity = await dbContext.AdditionalQualifications.FirstOrDefaultAsync(aq => aq.Id == id);

		if (entity is null) return OperationResult.Fail("Квалификация не найдена");

		dbContext.Remove(entity);
		await dbContext.SaveChangesAsync();

		return OperationResult.Success();
	}

	public async Task<AdditionalQualification?> Get(ID id)
	{
		AdditionalQualificationEntity? entity = await dbContext.AdditionalQualifications
			.FirstOrDefaultAsync(aq => aq.Id == id);
		return entity?.ToDomain();
	}

	public async Task<AdditionalQualification[]> Get(ID[] ids)
	{
		List<AdditionalQualificationEntity> entities = await dbContext.AdditionalQualifications
			.Where(aq => ids.Contains(aq.Id))
			.ToListAsync();

		return [.. entities.Select(e => e.ToDomain())];
	}

	public async Task<PagedResult<AdditionalQualification>> GetPage(Int32 page, Int32 pageSize)
	{
		(Int32 offset, Int32 limit) = NormalizeRange(page, pageSize);

		IQueryable<AdditionalQualificationEntity> query = dbContext.AdditionalQualifications.AsNoTracking();

		Int32 totalRows = await query.CountAsync();

		List<AdditionalQualificationEntity> entities = await query
			.OrderByDescending(aq => aq.CreatedDateTimeUtc)
			.ThenByDescending(aq => aq.ModifiedDateTimeUtc)
			.Skip(offset)
			.Take(limit)
			.ToListAsync();

		return PagedResult.Create(entities.Select(e => e.ToDomain()), totalRows);
	}

	public async Task<AdditionalQualification[]> GetBySearchText(String searchText)
	{
		List<AdditionalQualificationEntity> entities = await dbContext.AdditionalQualifications
			.Where(aq => EF.Functions.ILike(aq.Name, $"%{searchText}%") || EF.Functions.ILike(aq.Code, $"%{searchText}%"))
			.OrderBy(aq => aq.Name)
			.ToListAsync();

		return [.. entities.Select(e => e.ToDomain())];
	}
}