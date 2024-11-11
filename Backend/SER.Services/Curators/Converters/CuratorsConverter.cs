using SER.Domain.Curators;
using SER.Services.Curators.Models;

namespace SER.Services.Curators.Converters;
public static class CuratorsConverter
{
	public static Curator ToCurator(this CuratorDB db)
	{
		return new Curator(db.Id, db.Name, db.Surname, db.Patronymic, db.CreatedDateTimeUtc, db.ModifiedDateTimeUtc);
	}
}
