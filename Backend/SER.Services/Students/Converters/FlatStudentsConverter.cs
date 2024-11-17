using SER.Domain.Students;
using SER.Services.Students.Models;

namespace SER.Services.Students.Converters;

internal static class FlatStudentsConverter
{
    public static Student ToFlatStudent(this StudentDB studentDb)
    {
        return new Student();
    }

    public static Student[] ToFlatStudents(this StudentDB[] studentDbs)
    {
        return studentDbs.Select(ToFlatStudent).ToArray();
    }
}
