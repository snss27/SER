using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using SER.Database;
using SER.Database.Models.EducationLevels;
using SER.Domain.EducationLevels;
using SER.Domain.Services;
using SER.Services.EducationLevels.Converters;
using SER.Tools.Types.IDs;
using SER.Tools.Types.Results;
using static SER.Tools.Utils.NumberUtils;

namespace SER.Services.EducationLevels;

public class EducationLevelsService(SERDbContext dbContext) : IEducationLevelsService
{
	public async Task<OperationResult> Save(EducationLevelBlank blank)
	{
		Result<EducationLevel, Error> result = EducationLevel.Create(blank.Id, blank.Type, blank.Name, blank.Code, blank.StudyTime);
		if (result.IsFailure) return OperationResult.Fail(result.Error);

		EducationLevel educationLevel = result.Value;

		Boolean isNew = blank.Id is null;

		if (isNew)
		{
			EducationLevelEntity entity = educationLevel.ToEntity();
			await dbContext.AddAsync(entity);
		}
		else
		{
			EducationLevelEntity? entity = await dbContext.EducationLevels.FirstOrDefaultAsync(el => el.Id == educationLevel.Id);

			if (entity is null) return OperationResult.Fail("Уровень образования не найден");

			entity.ApplyChanges(educationLevel);
			dbContext.Update(entity);
		}

		await dbContext.SaveChangesAsync();
		return OperationResult.Success();
	}

	public async Task<OperationResult> Remove(ID id)
	{
		Boolean hasGroups = await dbContext.Groups
			.AnyAsync(g => g.EducationLevelId == id);
		if (hasGroups) return OperationResult.Fail("Невозможно удалить, т.к. существуют группы с данным уровнем образования");

		EducationLevelEntity? entity = await dbContext.EducationLevels.FirstOrDefaultAsync(el => el.Id == id);
		if (entity is null) return OperationResult.Fail("Уровень образования не найден");

		dbContext.Remove(entity);
		await dbContext.SaveChangesAsync();

		return OperationResult.Success();
	}

	public async Task<EducationLevel?> Get(ID id)
	{
		EducationLevelEntity? entity = await dbContext.EducationLevels.FirstOrDefaultAsync(el => el.Id == id);
		return entity?.ToDomain();
	}

	public async Task<EducationLevel[]> Get(ID[] ids)
	{
		List<EducationLevelEntity> entities = await dbContext.EducationLevels
			.Where(el => ids.Contains(el.Id))
			.ToListAsync();

		return [.. entities.Select(el => el.ToDomain())];
	}

	//TASK Возвращать totalCount для клиента
	public async Task<EducationLevel[]> GetPage(Int32 page, Int32 pageSize)
	{
		(Int32 offset, Int32 limit) = NormalizeRange(page, pageSize);

		List<EducationLevelEntity> entities = await dbContext.EducationLevels
			.OrderByDescending(el => el.CreatedDateTimeUtc)
			.ThenByDescending(el => el.ModifiedDateTimeUtc)
			.Skip(offset)
			.Take(limit)
			.ToListAsync();

		return [.. entities.Select(el => el.ToDomain())];
	}

	public async Task<EducationLevel[]> Get(String searchText)
	{
		List<EducationLevelEntity> entites = await dbContext.EducationLevels
			.Where(el =>
			EF.Functions.ILike(el.Name, $"%{searchText}%")
			|| EF.Functions.ILike(el.Code, $"%{searchText}%")
			)
			.OrderBy(el => el.Name)
			.ToListAsync();

		return [.. entites.Select(el => el.ToDomain())];
	}
}