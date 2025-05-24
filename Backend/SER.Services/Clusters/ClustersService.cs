using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using SER.Database;
using SER.Database.Models.Clusters;
using SER.Domain.Clusters;
using SER.Domain.Services;
using SER.Services.Clusters.Converters;
using SER.Tools.Types.IDs;
using SER.Tools.Types.Results;
using static SER.Tools.Utils.NumberUtils;

namespace SER.Services.Clusters;
public class ClustersService(SERDbContext dbContext) : IClustersService
{
	public async Task<OperationResult> Save(ClusterBlank blank)
	{
		Result<Cluster, Error> result = Cluster.Create(blank.Id, blank.Name);
		if(result.IsFailure) return OperationResult.Fail(result.Error);

		Cluster cluster = result.Value;

		Boolean isNew = blank.Id is null;

		if (isNew)
		{
			ClusterEntity entity = cluster.ToEntity();
			dbContext.Add(entity);
		}
		else
		{
			ClusterEntity? entity = await dbContext.Clusters.FirstOrDefaultAsync(c => c.Id == cluster.Id);

			if(entity is null) return OperationResult.Fail("Кластер не найден");

			entity.ApplyChanges(cluster);
			dbContext.Update(entity);
		}

		await dbContext.SaveChangesAsync();
		return OperationResult.Success();
	}

	public async Task<OperationResult> Remove(ID id)
	{
		ClusterEntity? entity =  await dbContext.Clusters.FirstOrDefaultAsync(c => c.Id == id);

		if (entity is null) return OperationResult.Fail("Кластер не найден");

		dbContext.Remove(entity);
		await dbContext.SaveChangesAsync();

		return OperationResult.Success();
	}

	public async Task<Cluster?> Get(ID id)
	{
		ClusterEntity? entity = await dbContext.Clusters.FirstOrDefaultAsync(c => c.Id == id);
		return entity?.ToDomain();
	}

	public async Task<Cluster[]> Get(ID[] ids)
	{
		List<ClusterEntity> entities = await dbContext.Clusters
			.Where(c => ids.Contains(c.Id))
			.ToListAsync();

		return [.. entities.Select(c => c.ToDomain())];
	}

	//TASK Возвращать totalCount для клиента
	public async Task<Cluster[]> GetPage(Int32 page, Int32 pageSize)
	{
		(Int32 offset, Int32 limit) = NormalizeRange(page, pageSize);

		List<ClusterEntity> entiteies = await dbContext.Clusters
			.OrderByDescending(c => c.CreatedDateTimeUtc)
			.ThenByDescending(c => c.ModifiedDateTimeUtc)
			.Skip(offset)
			.Take(limit)
			.ToListAsync();

		return [.. entiteies.Select(c => c.ToDomain())];
	}

	public async Task<Cluster[]> Get(String searchText)
	{
		List<ClusterEntity> entities = await dbContext.Clusters
			.Where(c => EF.Functions.ILike(c.Name, $"%{searchText}%"))
			.OrderBy(c => c.Name)
			.ToListAsync();

		return [.. entities.Select(e => e.ToDomain())];
	}
}
