using PMS.Services.Common;

namespace SER.Services.Groups.Repositories.Queries;

internal class Sql
{
	#region Groups

	public static String Groups_Save => SqlFileProvider.GetQuery(folder: "Groups");
	public static String Groups_Remove => SqlFileProvider.GetQuery(folder: "Groups");
	public static String Groups_Get => SqlFileProvider.GetQuery(folder: "Groups");
	public static String Groups_GetAll => SqlFileProvider.GetQuery(folder: "Groups");

	#endregion
}