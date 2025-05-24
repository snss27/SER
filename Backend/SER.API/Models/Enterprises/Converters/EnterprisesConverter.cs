using SER.Domain.Enterprises;

namespace SER.API.Models.Enterprises.Converters;

public static class EnterprisesConverter
{
	public static EnterpriseDto ToDto(this Enterprise enterprise)
	{
		return new EnterpriseDto(enterprise.Id, enterprise.Name, enterprise.LegalAddress, enterprise.ActualAddress, enterprise.Address, enterprise.INN, enterprise.KPP, enterprise.ORGN, enterprise.Phone, enterprise.Mail, enterprise.IsOPK);
	}
}
