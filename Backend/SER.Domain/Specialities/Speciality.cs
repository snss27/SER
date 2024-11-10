using SER.Tools.Types.IDs;

namespace SER.Domain.Specialities;
public class Speciality
{
	public ID Id { get; }
	public String Name { get; }
	public Int32 StudyYears { get; }

	public DateTime CreatedDateTimeUtc { get; }
	public DateTime? ModifiedDateTimeUtc { get; }

	public Speciality(ID id, String name, Int32 studyYears, DateTime createdDateTimeUtc, DateTime? modifiedDateTimeUtc)
	{
		Id = id;
		Name = name;
		StudyYears = studyYears;
		CreatedDateTimeUtc = createdDateTimeUtc;
		ModifiedDateTimeUtc = modifiedDateTimeUtc;
	}
}
