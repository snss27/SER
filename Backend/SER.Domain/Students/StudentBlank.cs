using SER.Domain.AdditionalQualifications;
using SER.Domain.Enterprises;
using SER.Domain.Groups;
using SER.Domain.Students.Enums;
using SER.Domain.Workplaces;
using SER.Tools.Types.IDs;
using SER.Tools.Types.Types;

namespace SER.Domain.Students;

public class StudentBlank
{
	public ID? Id { get; set; }
	public String? Name { get; set; }
	public String? SecondName { get; set; }
	public String? LastName { get; set; }
	public StudentStatus Status { get; set; }
	public Genders Gender { get; set; }
	public String? PhoneNumber { get; set; }
	public String? RepresentativePhoneNumber { get; set; }
	public DateTime? BirthDate { get; set; }
	public String? Snils { get; set; }
	public SocialStatus[] SocialStatuses { get; set; }
	public String? Address { get; set; }
	public String? Mail { get; set; }
	public String? Inn { get; set; }
	public GroupDto? Group { get; set; }
	public Boolean IsForeignCitizen { get; set; }
	public Boolean IsOnPaidStudy { get; set; }

	public String? PassportSeries { get; set; }
	public String? PassportNumber { get; set; }
	public String? PassportIssuedBy { get; set; }
	public DateTime? PassportIssuedDate { get; set; }
	public BlankFiles PassportFiles { get; set; }

	public WorkPlaceBlank? CurrentWorkplace { get; set; }
	public WorkPlaceBlank[] PrevWorkplaces { get; set; }

	public AdditionalQualification[] AdditionalQualifications { get; set; }

	public Boolean IsTargetAgreement { get; set; }
	public DateTime? TargetAgreementDate { get; set; }
	public Enterprise? TargetAgreementEnterprise { get; set; }
	public BlankFiles TargetAgreementFile { get; set; }

	public Boolean MustServeInArmy { get; set; }
	public	BlankFiles ArmySubpoenaFile { get; set; }
	public DateTime? ArmyCallDate { get; set; }

	public BlankFiles OtherFiles { get; set; }
}
