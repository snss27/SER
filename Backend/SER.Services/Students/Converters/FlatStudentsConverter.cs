using SER.Domain.Students;
using SER.Services.Students.Models;

namespace SER.Services.Students.Converters;

internal static class FlatStudentsConverter
{
    public static FlatStudent ToFlatStudent(this StudentDB studentDb)
    {
        return new FlatStudent();
    }

    public static FlatStudent[] ToFlatStudents(this StudentDB[] studentDbs)
    {
        return studentDbs.Select(ToFlatStudent).ToArray();
    }
}
