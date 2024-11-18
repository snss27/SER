using PMS.Services.Common;

namespace SER.Services.AdditionalQualifications.Repositories.Queries;
internal class Sql
{
	#region AdditionalQualifications

	public static String AdditionalQualifications_Save => SqlFileProvider.GetQuery(folder: "AdditionalQualifications");
	public static String AdditionalQualifications_Remove => SqlFileProvider.GetQuery(folder: "AdditionalQualifications");
	public static String AdditionalQualifications_Get => SqlFileProvider.GetQuery(folder: "AdditionalQualifications");
	public static String AdditionalQualifications_GetPage => SqlFileProvider.GetQuery(folder: "AdditionalQualifications");

	#endregion
}
