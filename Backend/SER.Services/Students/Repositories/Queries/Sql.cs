using PMS.Services.Common;

namespace SER.Services.Students.Repositories.Queries;

internal class Sql
{
    #region Students

    public static String Students_Save => SqlFileProvider.GetQuery(folder: "Students");
	public static String Students_Remove => SqlFileProvider.GetQuery(folder: "Students");
	public static String Students_Get => SqlFileProvider.GetQuery(folder: "Students");
	public static String Students_GetPage => SqlFileProvider.GetQuery(folder: "Students");
	public static String Students_GetByGroupId => SqlFileProvider.GetQuery(folder: "Students");

	#endregion
}
