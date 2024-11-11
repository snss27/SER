using PMS.Services.Common;

namespace SER.Services.Curators.Repositories.Queries;
internal class Sql
{
	#region Curators

	public static String Curators_Save => SqlFileProvider.GetQuery(folder: "Curators");
	public static String Curators_Remove => SqlFileProvider.GetQuery(folder: "Curators");
	public static String Curators_Get => SqlFileProvider.GetQuery(folder: "Curators");

	#endregion
}
