using PMS.Services.Common;

namespace SER.Services.Students.Repositories.Queries;

internal class Sql
{
    #region Students

    public static String FlatStudents_GetPage => SqlFileProvider.GetQuery(folder: "FlatStudents");

    #endregion
}
