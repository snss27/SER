using SER.Tools.Types.IDs;

namespace SER.Services.Curators.Models;
public class CuratorDB
{
	public ID Id { get; set; }
	public String Name { get; set; }
	public String? Surname { get; set; }
	public String? Patronymic { get; set; }

	public DateTime CreatedDateTimeUtc { get; set; }
	public DateTime? ModifiedDateTimeUtc { get; set; }
	public Boolean IsRemoved { get; set; }
}
