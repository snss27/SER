using PMS.Services.Common;

namespace SER.Services.Employees.Repositories.Queries;
internal class Sql
{
	public static String Employees_Save => SqlFileProvider.GetQuery(folder: "Employees");
	public static String Employees_Remove => SqlFileProvider.GetQuery(folder: "Employees");
	public static String Employees_Get => SqlFileProvider.GetQuery(folder: "Employees");
	public static String Employees_GetByIds => SqlFileProvider.GetQuery(folder: "Employees");
	public static String Employees_GetPage => SqlFileProvider.GetQuery(folder: "Employees");
	public static String Employees_GetBySearchText => SqlFileProvider.GetQuery(folder: "Employees");
}
