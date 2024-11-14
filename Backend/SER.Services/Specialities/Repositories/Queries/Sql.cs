using Microsoft.AspNetCore.Http;
using PMS.Services.Common;

namespace SER.Services.Specialities.Repositories.Queries;

internal class Sql
{
	#region Specialities

	public static String Specialities_Save => SqlFileProvider.GetQuery(folder: "Specialities");
	public static String Specialities_Remove => SqlFileProvider.GetQuery(folder: "Specialities");
	public static String Specialities_Get => SqlFileProvider.GetQuery(folder: "Specialities");
	public static String Specialities_GetPage => SqlFileProvider.GetQuery(folder: "Specialities");

	#endregion
}
