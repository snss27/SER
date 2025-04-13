using SER.Tools.Types.IDs;

namespace SER.Domain.Enterprises;

public class Enterprise()
{
	public ID Id { get; set; }
	public String Name { get; set; }
	public String? LegalAddress { get; set; }
	public String? ActualAddress { get; set; }
	public String? Address { get; set; }
	public String? INN { get; set; }
	public String? KPP { get; set; }
	public String? ORGN { get; set; }
	public String? Phone { get; set; }
	public String? Mail { get; set; }
	public Boolean IsOPK { get; set; }
}