using PMS.Services.Common;

namespace SER.Services.Groups.Repositories.Queries;

internal class Sql
{
	public static String Groups_Save => SqlFileProvider.GetQuery(folder: "Groups");
	public static String Groups_Remove => SqlFileProvider.GetQuery(folder: "Groups");
	public static String Groups_Get => SqlFileProvider.GetQuery(folder: "Groups");
	public static String Groups_GetPage => SqlFileProvider.GetQuery(folder: "Groups");
	public static String Groups_GetBySearchText => SqlFileProvider.GetQuery(folder: "Groups");
	public static String Groups_GetByEducationLevelId => SqlFileProvider.GetQuery(folder: "Groups");
}