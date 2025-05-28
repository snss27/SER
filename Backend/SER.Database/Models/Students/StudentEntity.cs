using SER.Database.Models.AdditionalQualifications;
using SER.Database.Models.ConfigurationTools;
using SER.Database.Models.Enterprises;
using SER.Database.Models.Groups;
using SER.Database.Models.WorkPlaces;
using SER.Domain.AdditionalQualifications;
using SER.Domain.Students.Enums;
using SER.Tools.Types.IDs;

namespace SER.Database.Models.Students;
public class StudentEntity : BaseEntity
{
	public String Name { get; set; } = default!;
	public String SecondName { get; set; } = default!;
	public String? LastName { get; set; }
	public Gender Gender { get; set; }
	public DateTime? BirthDate { get; set; }
	public String? PhoneNumber { get; set; }
	public String? RepresentativePhoneNumber { get; set; }
	public String? RepresentativeAlias { get; set; }
	public Boolean IsOnPaidStudy { get; set; }
	public String? Snils { get; set; }
	public ID GroupId { get; set; }
	public GroupEntity Group { get; set; } = default!;
	public String? PassportNumber { get; set; }
	public String? PassportSeries { get; set; }
	public String? PassportIssuedBy { get; set; }
	public DateTime? PassportIssuedDate { get; set; }
	public List<String> PassportFiles { get; set; } = default!;
	public List<WorkPlaceEntity> WorkPlaces { get; set; } = default!;
	public List<AdditionalQualificationEntity> AdditionalQualifications { get; set; } = default!;
	public Boolean IsTargetAgreement { get; set; }
	public String? TargetAgreementNumer { get; set; }
	public List<String> TargetAgreementFiles { get; set; } = default!;
	public DateTime? TargetAgreementDate { get; set; }
	public ID? TargetAgreementEnterpriseId { get; set; }
	public EnterpriseEntity? TargetAgreementEnterprise { get; set; }
	public Boolean MustServeInArmy { get; set; }
	public List<String> ArmySubpoenaFiles { get; set; } = default!;
	public DateTime? ArmyCallDate { get; set; }
	public List<SocialStatus> SocialStatuses { get; set; } = default!;
	public StudentStatus Status { get; set; }
	public String? Address { get; set; }
	public Boolean IsForeignCitizen { get; set; }
	public String? Inn { get; set; }
	public String? Mail { get; set; }
	public List<String> OtherFiles { get; set; } = default!;
}
