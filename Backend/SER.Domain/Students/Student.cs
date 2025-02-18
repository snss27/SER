using SER.Domain.Students.Enums;
using SER.Tools.Types.IDs;

namespace SER.Domain.Students;

public class Student(
	ID id,
	String name,
	String? surname,
	String? patronymic,
	Genders? gender,
	DateTime? birthDate,
	String? phoneNumber,
	String? representativePhoneNumber,
	Boolean isOnPaidStudy,
	String? snils,
	ID? groupId,
	ID? passportId,
	String? passportIssued, // 
	ID? workPlacesInfoId,
	ID[] additionalQualifications,
	Boolean isTargetAgreement,
	String? targetAgreementFile,
	String? targetAgreementDate, // 
	ID? targetAgreementEnterpriseId, // 
	ArmyStatus? armyStatus, // 
	Boolean isSubjectToArmyDraft,
	String? armySubpoenaFile,
	DateTime? armyServeDate,
	Peculiarities[] peculiarities,
	String? passportSeries,
	String? passportNumber,
	String? mail,
	String? inn,
	Boolean isForeignCitizen,
	String? address,
	DateTime createdDateTimeUtc,
	DateTime? modifiedDateTimeUtc
)
{
	public ID Id { get; } = id;
	public String Name { get; } = name;
	public String? Surname { get; } = surname;
	public String? Patronymic { get; } = patronymic;
	public Genders? Gender { get; } = gender;
	public DateTime? BirthDate { get; } = birthDate;
	public String? PhoneNumber { get; } = phoneNumber;
	public String? RepresentativePhoneNumber { get; } = representativePhoneNumber;
	public Boolean IsOnPaidStudy { get; } = isOnPaidStudy;
	public String? Snils { get; } = snils;
	public ID? GroupId { get; } = groupId;
	public ID? PassportId { get; } = passportId;
	public String? PassportIssued { get; } = passportIssued; // 
	public ID? WorkPlacesInfoId { get; } = workPlacesInfoId;
	public ID[] AdditionalQualifications { get; } = additionalQualifications;
	public Boolean IsTargetAgreement { get; } = isTargetAgreement;
	public String? TargetAgreementFile { get; } = targetAgreementFile;
	public String? TargetAgreementDate { get; } = targetAgreementDate; // 
	public ID? TargetAgreementEnterpriseId { get; } = targetAgreementEnterpriseId; // 
	public ArmyStatus? ArmyStatus { get; } = armyStatus; // 
	public Boolean IsSubjectToArmyDraft { get; } = isSubjectToArmyDraft;
	public String? ArmySubpoenaFile { get; } = armySubpoenaFile;
	public DateTime? ArmyServeDate { get; } = armyServeDate;
	public Peculiarities[] Peculiarities { get; } = peculiarities;
	public String? PassportSeries { get; } = passportSeries;
	public String? PassportNumber { get; } = passportNumber;
	public String? Mail { get; } = mail;
	public String? Inn { get; } = inn;
	public Boolean IsForeignCitizen { get; } = isForeignCitizen;
	public String? Address { get; } = address;
	public DateTime CreatedDateTimeUtc { get; } = createdDateTimeUtc;
	public DateTime? ModifiedDateTimeUtc { get; } = modifiedDateTimeUtc;
}