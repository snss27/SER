using SER.Domain.Enterprises;
using SER.Services.Enterprises.Models;

namespace SER.Services.Enterprises.Converters;
internal static class EnterprisesConverter
{
	public static Enterprise ToEnterprise(this EnterpriseDB db)
	{
		return new Enterprise(
			db.Id,
			db.Name,
			db.LegalAddress,
			db.ActualAddress,
			db.Address,
			db.INN,
			db.KPP,
			db.ORGN,
			db.Phone,
			db.Mail,

			db.CreatedDateTimeUtc,
			db.ModifiedDateTimeUtc
		);
	}

	public static Enterprise[] ToEnterprises(this EnterpriseDB[] dbs)
	{
		return dbs.Select(ToEnterprise).ToArray();
	}
}
