using SER.Domain.Groups.Enums;
using SER.Tools.Types.IDs;

namespace SER.Domain.Groups;
public class Group
{
	public ID Id {  get;  }
	public String Number { get;  }
	public StructuralUnits StructuralUnit { get;  }
	public ID SpecialityId { get; }
	public Int32 EnrollmentYear { get; }
	public String CuratorName { get; }
	public DateTime CreatedDateTimeUtc { get; }
	public DateTime? ModifiedDateTimeUtc { get; }

	public Group(ID id, String number, StructuralUnits structuralUnit, ID specialityId, Int32 enrollmentYear, String curatorName, DateTime createdDateTimeUtc, DateTime? modifiedDateTimeUtc)
	{
		Id = id;
		Number = number;
		StructuralUnit = structuralUnit;
		SpecialityId = specialityId;
		EnrollmentYear = enrollmentYear;
		CuratorName = curatorName;
		CreatedDateTimeUtc = createdDateTimeUtc;
		ModifiedDateTimeUtc = modifiedDateTimeUtc;
	}
}
