using SER.Domain.Workplaces;
using SER.Services.WorkPlaces.Models;

namespace SER.Services.WorkPlaces.Converters;
public static class WorkPlacesConverter
{
	public static WorkPlace ToWorkPlace(this WorkPlaceDB db)
	{
		return new WorkPlace(
			db.Id,
			db.EnterpriseId,
			db.Post,
			db.WorkBookExtractFile,
			db.StartDate,
			db.FinishDate,
			db.CreatedDateTimeUtc,
			db.ModifiedDateTimeUtc
		);
	}

	public static WorkPlace[] ToWorkPlaces(this WorkPlaceDB[] dbs)
	{
		return dbs.Select(ToWorkPlace).ToArray();
	}
}
