using SER.Tools.Types.IDs;

namespace SER.Domain.Enterprises;
public class Enterprise
{
	public ID Id { get; }
	public String Name { get; }
	public String? LegalAddress { get; set; }
	public String? ActualAddress { get; set; }
	public String? Address { get; set; }
	public String? INN { get; set; }
	public String? KPP { get; set; }
	public String? ORGN { get; set; }
	public String? Phone { get; set; }
	public String? Mail { get; set; }

	public DateTime CreatedDateTimeUtc { get; }
	public DateTime? ModifiedDateTimeUtc { get; }

	public Enterprise(
		ID id,
		String name,
		String? legalAddress,
		String? actualAddress,
		String? address,
		String? inn,
		String? kpp,
		String? orgn,
		String? phone,
		String? mail,

		DateTime createdDateTimeUtc,
		DateTime? modifiedDateTimeUtc
	)
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

		CreatedDateTimeUtc = createdDateTimeUtc;
		ModifiedDateTimeUtc = modifiedDateTimeUtc;
	}
}
