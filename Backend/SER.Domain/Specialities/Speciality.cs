using SER.Tools.Types.IDs;

namespace SER.Domain.Specialities;
public class Speciality
{
	public ID Id { get; }
	public String Name { get; }
	public Int32 StudyYears { get; }
	public Int32 StudyMonths { get; }

	public DateTime CreatedDateTimeUtc { get; }
	public DateTime? ModifiedDateTimeUtc { get; }

	public Speciality(ID id, String name, Int32 studyYears, Int32 studyMonths, DateTime createdDateTimeUtc, DateTime? modifiedDateTimeUtc)
	{
		Id = id;
		Name = name;
		StudyYears = studyYears;
		StudyMonths = studyMonths;
		CreatedDateTimeUtc = createdDateTimeUtc;
		ModifiedDateTimeUtc = modifiedDateTimeUtc;
	}
}
