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
	public ID CuratorId { get; }
	public DateTime CreatedDateTimeUtc { get; }
	public DateTime? ModifiedDateTimeUtc { get; }

	public Group(ID id, String number, StructuralUnits structuralUnit, ID specialityId, Int32 enrollmentYear, ID curatorId, DateTime createdDateTimeUtc, DateTime? modifiedDateTimeUtc)
	{
		Id = id;
		Number = number;
		StructuralUnit = structuralUnit;
		SpecialityId = specialityId;
		EnrollmentYear = enrollmentYear;
		CuratorId = curatorId;
		CreatedDateTimeUtc = createdDateTimeUtc;
		ModifiedDateTimeUtc = modifiedDateTimeUtc;
	}
}
