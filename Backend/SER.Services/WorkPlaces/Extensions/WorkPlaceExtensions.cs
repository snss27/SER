using SER.Database.Models.WorkPlaces;
using SER.Domain.Enterprises;
using SER.Domain.Workplaces;
using SER.Services.Enterprises.Converters;
using SER.Tools.Types.IDs;

namespace SER.Services.WorkPlaces.Extensions;
public static class WorkPlaceExtensions
{
	public static WorkPlaceEntity ToEntity(this WorkPlace workPlace, ID studentId)
	{
		return new WorkPlaceEntity()
		{
			Id = workPlace.Id,
			EnterpriseId = workPlace.Enterprise.Id,
			Post = workPlace.Post,
			WorkBookExtractFiles = [.. workPlace.WorkBookExtractFiles],
			StartDate = workPlace.StartDateTime,
			FinishDate = workPlace.FinishDateTime,
			StudentId = studentId,
			IsCurrent = workPlace.IsCurrent,
			CreatedDateTimeUtc = DateTime.UtcNow,
			ModifiedDateTimeUtc = null
		};
	}

	public static void ApplyChanges(this WorkPlaceEntity entity, WorkPlace workPlace)
	{
		entity.EnterpriseId = workPlace.Enterprise.Id;
		entity.Post = workPlace.Post;
		entity.WorkBookExtractFiles = [.. workPlace.WorkBookExtractFiles];
		entity.StartDate = workPlace.StartDateTime;
		entity.FinishDate = workPlace.FinishDateTime;
		entity.IsCurrent = workPlace.IsCurrent;
		entity.ModifiedDateTimeUtc = DateTime.UtcNow;
	}

	public static WorkPlace ToDomain(this WorkPlaceEntity workPlace)
	{
		Enterprise enterprise = workPlace.Enterprise.ToDomain();

		return WorkPlace.Create(workPlace.Id, enterprise, workPlace.Post, [.. workPlace.WorkBookExtractFiles], workPlace.StartDate, workPlace.FinishDate, workPlace.IsCurrent).Value;
	}
}
