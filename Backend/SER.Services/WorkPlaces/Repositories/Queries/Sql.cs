using PMS.Services.Common;

namespace SER.Services.WorkPlaces.Repositories.Queries;
internal static class Sql
{
	public static String WorkPlaces_Save => SqlFileProvider.GetQuery(folder: "WorkPlaces");
}
