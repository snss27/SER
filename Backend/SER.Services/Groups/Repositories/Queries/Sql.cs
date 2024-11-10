using PMS.Services.Common;

namespace SER.Services.Groups.Repositories.Queries;

internal class Sql
{
	#region Groups

	public static String Groups_Save => SqlFileProvider.GetQuery(folder: "Groups");
 
	#endregion
}