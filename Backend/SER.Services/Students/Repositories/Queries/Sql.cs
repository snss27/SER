using PMS.Services.Common;

namespace SER.Services.Students.Repositories.Queries;

internal class Sql
{
    #region Students

    public static String FlatStudents_Save => SqlFileProvider.GetQuery(folder: "Students");

    #endregion
}
