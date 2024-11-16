using SER.Tools.Types.IDs;

namespace SER.Services.Specialities.Models;

public class SpecialityDB
{
	public ID Id { get; set; }
	public String Name { get; set; }
	public String Code { get; set; }
	public Int32 StudyYears { get; set; }
	public Int32 StudyMonths { get; set; }
	public DateTime CreatedDateTime { get; set; }
	public DateTime? ModifiedDateTime { get; set; }
	public Boolean IsRemoved { get; set; }
}

