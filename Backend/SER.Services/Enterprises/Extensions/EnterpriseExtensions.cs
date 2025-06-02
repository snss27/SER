using SER.Database.Models.Enterprises;
using SER.Domain.Employees;
using SER.Domain.Enterprises;

namespace SER.Services.Enterprises.Converters;
internal static class EnterpriseExtensions
{
	public static EnterpriseEntity ToEntity(this Enterprise enterprise)
	{
		return new EnterpriseEntity()
		{
			Id = enterprise.Id,
			Name = enterprise.Name,
			LegalAddress = enterprise.LegalAddress,
			ActualAddress = enterprise.ActualAddress,
			Address = enterprise.Address,
			INN = enterprise.INN,
			KPP = enterprise.KPP,
			ORGN = enterprise.ORGN,
			Phone = enterprise.Phone,
			Mail = enterprise.Mail,
			IsOPK = enterprise.IsOPK,
			CreatedDateTimeUtc = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Unspecified),
			ModifiedDateTimeUtc = null
		};
	}

	public static void ApplyChanges(this EnterpriseEntity entity, Enterprise enterprise)
	{
		entity.Name = enterprise.Name;
		entity.LegalAddress = enterprise.LegalAddress;
		entity.ActualAddress = enterprise.ActualAddress;
		entity.Address = enterprise.Address;
		entity.INN = enterprise.INN;
		entity.KPP = enterprise.KPP;
		entity.ORGN = enterprise.ORGN;
		entity.Phone = enterprise.Phone;
		entity.Mail = enterprise.Mail;
		entity.IsOPK = enterprise.IsOPK;
		entity.ModifiedDateTimeUtc = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Unspecified);
	}

	public static Enterprise ToDomain(this EnterpriseEntity entity)
	{
		return Enterprise.Create(entity.Id, entity.Name, entity.LegalAddress, entity.ActualAddress, entity.Address, entity.INN, entity.KPP, entity.ORGN, entity.Phone, entity.Mail, entity.IsOPK).Value;
	}
}
