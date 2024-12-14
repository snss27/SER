using SER.Tools.Types.IDs;

namespace SER.Domain.Enterprises;

public class Enterprise(
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
	Boolean isOPK,
	DateTime createdDateTimeUtc,
	DateTime? modifiedDateTimeUtc
)
{
	public ID Id { get; } = id;
	public String Name { get; } = name;
	public String? LegalAddress { get; set; } = legalAddress;
	public String? ActualAddress { get; set; } = actualAddress;
	public String? Address { get; set; } = address;
	public String? INN { get; set; } = inn;
	public String? KPP { get; set; } = kpp;
	public String? ORGN { get; set; } = orgn;
	public String? Phone { get; set; } = phone;
	public String? Mail { get; set; } = mail;
	public Boolean IsOPK { get; set; } = isOPK;

	public DateTime CreatedDateTimeUtc { get; } = createdDateTimeUtc;
	public DateTime? ModifiedDateTimeUtc { get; } = modifiedDateTimeUtc;
}