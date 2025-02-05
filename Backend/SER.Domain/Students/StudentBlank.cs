using SER.Domain.Students.Enums;
using SER.Tools.Types.IDs;

namespace SER.Domain.Students;
public class StudentBlank
{
	public ID? Id { get; set; }
	public String? Name { get; set; }
	public String? Surname { get; set; }
	public String? Patronymic { get; set; }
	public Genders? Gender { get; set; }
	public DateTime? BirthDate { get; set; }
	public String? PhoneNumber { get; set; }
	public String? RepresentativePhoneNumber { get; set; }
	public Boolean? IsOnPaidStudy { get; set; }
	public String? Snils { get; set; }
	public ID? GroupId { get; set; }
	public ID? PassportId { get; set; }
	public ID? WorkPlacesInfoId { get; set; }
	public ID[] AdditionalQualifications { get; set; }
	public Boolean? IsTargetAgreement { get; set; }
	public String? TargetAgreementFile { get; set; }
	public Boolean? IsSubjectToArmyDraft { get; set; }
	public String? ArmySubpoenaFile { get; set; }
	public DateTime? ArmyServeDate { get; set; }
	public Peculiarities[] Peculiarities { get; set; }
	public String? PassportSeries {  get; set; }
	public String? PassportNumber { get; set; }
	public String? Mail { get; set; }
	public String? Inn {get; set;}
	public Boolean IsForeignCitizen { get; set; }
	public String? Address { get; set; }
}
