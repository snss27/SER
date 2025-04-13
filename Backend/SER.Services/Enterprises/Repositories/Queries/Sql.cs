using PMS.Services.Common;

namespace SER.Services.Enterprises.Repositories.Queries;
internal class Sql
{
	public static String Enterprises_Save => SqlFileProvider.GetQuery(folder: "Enterprises");
	public static String Enterprises_Remove => SqlFileProvider.GetQuery(folder: "Enterprises");
	public static String Enterprises_Get => SqlFileProvider.GetQuery(folder: "Enterprises");
	public static String Enterprises_GetByIds => SqlFileProvider.GetQuery(folder: "Enterprises");
	public static String Enterprises_GetPage => SqlFileProvider.GetQuery(folder: "Enterprises");
	public static String Enterprises_GetBySearchText => SqlFileProvider.GetQuery(folder: "Enterprises");

	public static String Students_RemoveTargetAgreementEnterprise => SqlFileProvider.GetQuery(folder: "Students");
}
