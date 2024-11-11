using SER.Tools.Types.IDs;

namespace SER.Domain.Curators;
public class CuratorBlank
{
	public ID? Id { get; set; }
	public String? Name { get; set; }
	public String? Surname { get; set; }
	public String? Patronymic { get; set; }
}
