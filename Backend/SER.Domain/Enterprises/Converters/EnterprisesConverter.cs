namespace SER.Domain.Enterprises.Converters;

public static class EnterprisesConverter
{
	public static EnterpriseDto ToDto(this Enterprise enterprise)
	{
		return new EnterpriseDto(enterprise.Id, enterprise.Name, enterprise.LegalAddress, enterprise.ActualAddress, enterprise.Address, enterprise.INN, enterprise.KPP, enterprise.ORGN, enterprise.Phone, enterprise.Mail, enterprise.IsOPK);
	}

	public static Enterprise ToDomain(this EnterpriseDto dto)
	{
		return Enterprise.Create(dto.Id, dto.Name, dto.LegalAddress, dto.ActualAddress, dto.Address, dto.INN, dto.KPP, dto.ORGN, dto.Phone, dto.Mail, dto.IsOpk).Value;
	}
}
