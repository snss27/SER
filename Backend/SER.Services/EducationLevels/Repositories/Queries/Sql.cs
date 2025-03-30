using PMS.Services.Common;

namespace SER.Services.EducationLevels.Repositories.Queries;

internal class Sql
{

	#region EducationLevels

	public static String EducationLevels_Save => SqlFileProvider.GetQuery(folder: "EducationLevels");
	public static String EducationLevels_Remove => SqlFileProvider.GetQuery(folder: "EducationLevels");
	public static String EducationLevels_Get => SqlFileProvider.GetQuery(folder: "EducationLevels");
	public static String EducationLevels_GetByIds => SqlFileProvider.GetQuery(folder: "EducationLevels");
	public static String EducationLevels_GetPage => SqlFileProvider.GetQuery(folder: "EducationLevels");
	public static String EducationLevels_GetBySearchText => SqlFileProvider.GetQuery(folder: "EducationLevels");

	#endregion
}