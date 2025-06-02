using SER.Domain.AdditionalQualifications;
using SER.Domain.Enterprises;
using SER.Domain.Groups;
using SER.Domain.Students.Enums;
using SER.Domain.Workplaces;
using SER.Tools.Types.IDs;

namespace SER.Domain.Students;

public class StudentBlank
{
	public ID? Id { get; init; }
	public String? Name { get; init; }
	public String? SecondName { get; init; }
	public String? LastName { get; init; }
	public StudentStatus? Status { get; init; }
	public Gender? Gender { get; init; }
	public String? PhoneNumber { get; init; }
	public String? RepresentativePhoneNumber { get; init; }
	public String? RepresentativeAlias { get; init; }
	public DateTime? BirthDate { get; init; }
	public String? Snils { get; init; }
	public SocialStatus[] SocialStatuses { get; init; } = [];
	public String? Address { get; init; }
	public String? Mail { get; init; }
	public String? Inn { get; init; }
	public GroupDto? Group { get; init; }
	public Boolean IsForeignCitizen { get; init; }
	public Boolean IsOnPaidStudy { get; init; }

	public String? PassportSeries { get; init; }
	public String? PassportNumber { get; init; }
	public String? PassportIssuedBy { get; init; }
	public DateTime? PassportIssuedDate { get; init; }
	public String[] PassportFiles { get; init; } = [];

	public WorkPlaceBlank[] WorkPlaces { get; init; } = [];

	public AdditionalQualificationDto[] AdditionalQualifications { get; init; } = [];

	public Boolean? IsTargetAgreement { get; init; }
	public String? TargetAgreementNumber { get; init; }
	public DateTime? TargetAgreementDate { get; init; }
	public EnterpriseDto? TargetAgreementEnterprise { get; init; }
	public String[] TargetAgreementFiles { get; init; } = [];

	public Boolean? MustServeInArmy { get; init; }
	public String[] ArmySubpoenaFiles { get; init; } = [];
	public DateTime? ArmyCallDate { get; init; }

	public String[] OtherFiles { get; init; } = [];
}
