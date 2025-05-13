using SER.EfCore.Models.Types;
using SER.Tools.Types.IDs;

namespace SER.EfCore.Models;
public class EmployeeEntity
{
	public ID Id { get; set; }
	public Name Name { get; set; }

	public DateTime CreatedDateTimeUtc { get; set; }
	public DateTime ModifiedDateTimeUtc { get; set; }
	public Boolean IsRemoved { get; set; }
}
