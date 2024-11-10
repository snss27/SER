using PMS.Services.Common;

namespace SER.Services.Specialities.Repositories.Queries;

internal class Sql
{
	#region Specialities

	public static String Specialities_Save => SqlFileProvider.GetQuery(folder: "Specialities");

	#endregion
}
