using PMS.Services.Common;

namespace SER.Services.Clusters.Repositories.Queries;
internal static class Sql
{
	public static String Clusters_Save => SqlFileProvider.GetQuery(folder: "Clusters");
	public static String Clusters_Remove => SqlFileProvider.GetQuery(folder: "Clusters");
	public static String Clusters_Get => SqlFileProvider.GetQuery(folder: "Clusters");
	public static String Clusters_GetPage => SqlFileProvider.GetQuery(folder: "Clusters");
	public static String Clusters_GetBySearchText => SqlFileProvider.GetQuery(folder: "Clusters");
	public static String Clusters_GetByIds => SqlFileProvider.GetQuery(folder: "Clusters");

	public static String Groups_RemoveClusterById => SqlFileProvider.GetQuery(folder: "Groups");
}
