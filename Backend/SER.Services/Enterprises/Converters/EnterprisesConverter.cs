using SER.Domain.Enterprises;
using SER.Services.Enterprises.Models;

namespace SER.Services.Enterprises.Converters;
internal static class EnterprisesConverter
{
	public static Enterprise ToEnterprise(this EnterpriseDB db)
	{
		return new Enterprise()
		{
			Id = db.Id,
			Name = db.Name,
			ActualAddress = db.ActualAddress,
			Address = db.Address,
			INN = db.INN,
			IsOPK = db.IsOPK,
			KPP = db.KPP,
			LegalAddress = db.LegalAddress,
			Mail = db.Mail,
			ORGN = db.ORGN,
			Phone = db.Phone
		};
	}

	public static Enterprise[] ToEnterprises(this EnterpriseDB[] dbs)
	{
		return dbs.Select(ToEnterprise).ToArray();
	}
}
