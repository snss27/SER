using CSharpFunctionalExtensions;
using SER.Tools.Types.IDs;
using SER.Tools.Types.Results;
using SER.Tools.Utils;

namespace SER.Domain.Enterprises;

public class Enterprise
{
	public ID Id { get; }
	public String Name { get; } = default!;
	public String? LegalAddress { get; }
	public String? ActualAddress { get; }
	public String? Address { get; }
	public String? INN { get; }
	public String? KPP { get; }
	public String? ORGN { get; }
	public String? Phone { get; }
	public String? Mail { get; }
	public Boolean IsOPK { get; }

	private Enterprise(ID id, String name, String? legalAddress, String? actualAddress, String? address, String? inn, String? kpp, String? orgn, String? phone, String? mail, Boolean isOPK)
	{
		Id = id;
		Name = name;
		LegalAddress = legalAddress;
		ActualAddress = actualAddress;
		Address = address;
		INN = inn;
		KPP = kpp;
		ORGN = orgn;
		Phone = phone;
		Mail = mail;
		IsOPK = isOPK;
	}

	public static Result<Enterprise, Error> Create(ID? id, String? name, String? legalAddress, String? actualAddress, String? address, String? inn, String? kpp, String? orgn, String? phone, String? mail, Boolean? isOPK)
	{
		if (String.IsNullOrWhiteSpace(name)) return new Error("Укажите наименование организации");

		if (!String.IsNullOrWhiteSpace(inn) && !Regexs.EnterpriseInnRegex.IsMatch(inn))
		{
			return new Error("Инн должен содержать 10 цифр");
		}

		if (!String.IsNullOrWhiteSpace(kpp) && !Regexs.KppRegex.IsMatch(kpp))
		{
			return new Error("КПП должен содержать 9 цифр");
		}

		if (!String.IsNullOrWhiteSpace(orgn) && !Regexs.OrgnRegex.IsMatch(orgn))
		{
			return new Error("ОРГН должен содержать 13 цифр");
		}

		if (!String.IsNullOrWhiteSpace(phone) && !Regexs.PhoneRegex.IsMatch(phone))
		{
			return new Error("Неверно указан номер телефона (скорее всего не полностью)");
		}

		if (!String.IsNullOrWhiteSpace(mail) && !Regexs.MailRegex.IsMatch(mail))
		{
			return new Error("Неверно указана электронная почта (скорее всего не полностью)");
		}

		if (isOPK is null) return new Error("Укажите, относится ли предприятие к ОПК");

		ID _id = id ?? ID.New();

		return new Enterprise(_id, name, legalAddress, actualAddress, address, inn, kpp, orgn, phone, mail, isOPK.Value);
	}
}
