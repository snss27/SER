using SER.Tools.Types.IDs;

namespace SER.Domain.Enterprises;
public class EnterpriseBlank
{
	public ID? Id { get; init; }
	public String? Name { get; init; }
	public String? LegalAddress { get; init; }
	public String? ActualAddress { get; init; }
	public String? Address { get; init; }
	public String? INN {  get; init; }
	public String? KPP { get; init; }
	public String? ORGN { get; init; }
	public String? Phone {  get; init; }
	public String? Mail { get; init; }
	public Boolean? IsOPK { get; init; }
}
