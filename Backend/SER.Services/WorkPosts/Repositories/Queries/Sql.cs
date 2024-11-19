using PMS.Services.Common;

namespace SER.Services.WorkPosts.Repositories.Queries;
internal class Sql
{
	#region WorkPosts

	public static String WorkPosts_Save => SqlFileProvider.GetQuery(folder: "WorkPosts");
	public static String WorkPosts_Remove => SqlFileProvider.GetQuery(folder: "WorkPosts");
	public static String WorkPosts_Get => SqlFileProvider.GetQuery(folder: "WorkPosts");
	public static String WorkPosts_GetPage => SqlFileProvider.GetQuery(folder: "WorkPosts");

	#endregion
}
