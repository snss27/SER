using PMS.Services.Common;

namespace SER.Services.Clusters.Repositories.Queries;
internal static class Sql
{
	public static String Clusters_Save => SqlFileProvider.GetQuery(folder: "Clusters");
	public static String Clusters_Remove => SqlFileProvider.GetQuery(folder: "Clusters");
	public static String Clusters_Get => SqlFileProvider.GetQuery(folder: "Clusters");
	public static String Clusters_GetPage => SqlFileProvider.GetQuery(folder: "Clusters");
}
