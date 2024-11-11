using SER.Tools.Types.IDs;

namespace SER.Domain.Curators;
public class Curator
{
	public ID Id { get; }
	public String Name { get; }
	public String? Surname { get; }
	public String? Patronymic { get; }

	public DateTime CreatedDateTimeUtc { get; }
	public DateTime? ModifiedDateTimeUtc { get; }


	public Curator(ID id, String name, String? surname, String? patronymic, DateTime createdDateTimeUtc, DateTime? modifiedDateTimeUtc)
	{
		Id = id;
		Name = name;
		Surname = surname;
		Patronymic = patronymic;
		CreatedDateTimeUtc = createdDateTimeUtc;
		ModifiedDateTimeUtc = modifiedDateTimeUtc;
	}
}
