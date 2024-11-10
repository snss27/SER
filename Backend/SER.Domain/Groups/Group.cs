using SER.Domain.Groups.Enums;
using SER.Tools.Types.IDs;

namespace SER.Domain.Groups;
public class Group
{
	public ID Id {  get;  }
	public String GroupNumber { get;  }
	public StructuralUnits StructuralUnit { get;  }
	public ID SpecialityId { get; }
	public Int32 EnrollmentYear { get; }
	public String CuratorName { get; }
	public DateTime CreatedDateTimeUtc { get; }
	public DateTime? ModifiedDateTimeUtc { get; }
}
