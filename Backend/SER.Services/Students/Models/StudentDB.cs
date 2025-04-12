using SER.Domain.Students.Enums;
using SER.Tools.Types.Catalog;
using SER.Tools.Types.IDs;

namespace SER.Domain.Students;

public class StudentDB
{
	public ID Id { get; set; }
	public String Name { get; set; }
	public String SecondName { get; set; }
	public String? LastName { get; set; }
	public Genders Gender { get; set; }
	public DateTime? BirthDate { get; set; }
	public String? PhoneNumber { get; set; }
	public String? RepresentativePhoneNumber { get; set; }
	public Boolean IsOnPaidStudy { get; set; }
	public String? Snils { get; set; }
	public ID GroupId { get; set; }
	public String? PasportNumber { get; set; }
	public String? PasportSeries { get; set; }
	public String? PassportIssuedBy { get; set; }
	public DateTime? PassportIssuedDate { get; set; }
	public Catalog<String> PassportFiles { get; set; }
	public Catalog<ID> PrevWorkplaceIds { get; set; }
	public ID? CurrentWorkplaceId { get; set; }
	public Catalog<ID> AdditionalQualifications { get; set; }
	public Boolean IsTargetAgreement { get; set; }
	public String? TargetAgreementFile { get; set; }
	public DateTime? TargetAgreementDate { get; set; }
	public ID? TargetAgreementEnterpriseId { get; set; }
	public Boolean MustServeInArmy { get; set; }
	public String? ArmySubpoenaFile { get; set; }
	public DateTime? ArmyCallDate { get; set; }
	public Catalog<SocialStatus> SocialStatuses { get; set; }
	public StudentStatus Status { get; set; }
	public String? Address { get; set; }
	public Boolean IsForeignCitizen { get; set; }
	public String? Inn { get; set; }
	public String? Mail { get; set; }
	public Catalog<String> OtherFiles { get; set; }

	public DateTime CreatedDateTimeUtc { get; set; }
	public DateTime? ModifiedDateTimeUtc { get; set; }
	public Boolean IsRemoved { get; set; }
}
