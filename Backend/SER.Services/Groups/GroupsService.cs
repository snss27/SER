using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using SER.Database;
using SER.Database.Models.Groups;
using SER.Domain.Clusters;
using SER.Domain.Clusters.Converters;
using SER.Domain.EducationLevels;
using SER.Domain.EducationLevels.Converters;
using SER.Domain.Employees;
using SER.Domain.Employees.Converters;
using SER.Domain.Groups;
using SER.Domain.Services;
using SER.Services.Enterprises.Converters;
using SER.Services.Groups.Converters;
using SER.Tools.Types;
using SER.Tools.Types.IDs;
using SER.Tools.Types.Results;
using static SER.Tools.Utils.NumberUtils;

namespace SER.Services.Groups;

public class GroupsService(SERDbContext dbContext) : IGroupsService
{
	public async Task<OperationResult> Save(GroupBlank blank)
	{
		if (blank.EducationLevel is null) return OperationResult.Fail("Укажите уровень образования");
		EducationLevel educationLevel = blank.EducationLevel.ToDomain();
		Employee? curator = blank.Curator?.ToDomain();
		Cluster? cluster = blank.Cluster?.ToDomain();

		Result<Group, Error> result = Group.Create(blank.Id, blank.Number, blank.StructuralUnit, educationLevel, blank.EnrollmentYear, curator, blank.HasCluster, cluster);
		if (result.IsFailure) return OperationResult.Fail(result.Error);

		Group group = result.Value;

		Boolean isNew = blank.Id is null;

		if (isNew)
		{
			GroupEntity entity = group.ToEntity();
			await dbContext.AddAsync(entity);
		}
		else
		{
			GroupEntity? entity = await dbContext.Groups.FirstOrDefaultAsync(g => g.Id == group.Id);
			if (entity is null) return OperationResult.Fail("Группа не найдена");

			entity.ApplyChanges(group);
			dbContext.Update(entity);
		}

		await dbContext.SaveChangesAsync();
		return OperationResult.Success();
	}

	public async Task<OperationResult> Remove(ID id)
	{
		Boolean hasStudents = await dbContext.Students.AnyAsync(s => s.GroupId == id);
		if (hasStudents)
		{
			return OperationResult.Fail("Невозможно удалить, т.к. у этой группы есть привязанные студенты");
		}

		GroupEntity? entity = await dbContext.Groups.FirstOrDefaultAsync(g => g.Id == id);
		if (entity is null) return OperationResult.Fail("Группа не найдена");

		dbContext.Remove(entity);
		await dbContext.SaveChangesAsync();

		return OperationResult.Success();
	}


	public async Task<Group?> Get(ID id)
	{
		GroupEntity? entity = await dbContext.Groups
			.Include(g => g.EducationLevel)
			.Include(g => g.Curator)
			.Include(g => g.Cluster)
			.FirstOrDefaultAsync(g => g.Id == id);

		return entity?.ToDomain();
	}

	public async Task<Group[]> Get(ID[] ids)
	{
		List<GroupEntity> entities = await dbContext.Groups
			.Include(g => g.EducationLevel)
			.Include(g => g.Curator)
			.Include(g => g.Cluster)
			.Where(el => ids.Contains(el.Id))
			.ToListAsync();

		return [.. entities.Select(e => e.ToDomain())];
	}

	public async Task<PagedResult<Group>> GetPage(Int32 page, Int32 pageSize)
	{
		(Int32 offset, Int32 limit) = NormalizeRange(page, pageSize);

		IQueryable<GroupEntity> query = dbContext.Groups
			.AsNoTracking()
			.Include(g => g.EducationLevel)
			.Include(g => g.Curator)
			.Include(g => g.Cluster);

		Int32 totalRows = await query.CountAsync();

		List<GroupEntity> entities = await query
			.OrderByDescending(e => e.CreatedDateTimeUtc)
			.ThenByDescending(e => e.ModifiedDateTimeUtc)
			.Skip(offset)
			.Take(limit)
			.ToListAsync();

		return PagedResult.Create(entities.Select(e => e.ToDomain()), totalRows);
	}

	public async Task<Group[]> GetBySearchText(String searchText)
	{
		List<GroupEntity> entites = await dbContext.Groups
			.Include(g => g.EducationLevel)
			.Include(g => g.Curator)
			.Include(g => g.Cluster)
			.Where(e => EF.Functions.ILike(e.Number, $"%{searchText}%"))
			.OrderBy(e => e.Number)
			.ToListAsync();

		return [.. entites.Select(el => el.ToDomain())];
	}
}
