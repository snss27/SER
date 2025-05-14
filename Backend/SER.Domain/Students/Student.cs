using SER.Domain.Students.Enums;
using SER.Tools.Types.IDs;

namespace SER.Domain.Students;

public class Student(
	ID id,
	String name,
	String secondName,
	String? lastName,
	Genders gender,
	DateTime? birthDate,
	String? phoneNumber,
	String? representativePhoneNumber,
	String? representativeAlias,
	Boolean isOnPaidStudy,
	String? snils,
	ID groupId,
	String? pasportNumber,
	String? pasportSeries,
	String? pasportIssuedBy,
	DateTime? pasportIssuedDate,
	String[] pasportFiles,
	ID[] prevWorkpalceIds,
	ID? currentWorkpalceId,
	ID[] additionalQualifications,
	Boolean isTargetAgreement,
	String? targetAgreementNumber,
	String? targetAgreementFile,
	DateTime? targetAgreementDate,
	ID? targetAgreementEnterpriseId,
	Boolean mustServeInArmy,
	String? armySubpoenaFile,
	DateTime? armyCallDate,
	SocialStatus[] socialStatuses,
	StudentStatus status,
	String? address,
	Boolean isForeignCitizen,
	String? inn,
	String? mail,
	String[] otherFiles,

	DateTime createdDateTimeUtc,
	DateTime? modifiedDateTimeUtc
)
{
	public ID Id { get;  } = id;
	public String Name { get;} = name;
	public String SecondName { get; } = secondName;
	public String? LastName { get; } = lastName;
	public Genders Gender { get;  } = gender;
	public DateTime? BirthDate { get; } = birthDate;
	public String? PhoneNumber { get; } = phoneNumber;
	public String? RepresentativePhoneNumber { get; } = representativePhoneNumber;
	public String? RepresentativeAlias { get; } = representativeAlias;
	public Boolean IsOnPaidStudy { get; } = isOnPaidStudy;
	public String? Snils { get; } = snils;
	public ID GroupId { get; } = groupId;
	public String? PasportNumber { get; } = pasportNumber;
	public String? PasportSeries { get; } = pasportSeries;
	public String? PassportIssuedBy { get; } = pasportIssuedBy;
	public DateTime? PassportIssuedDate { get; } = pasportIssuedDate;
	public String[] PassportFiles { get; } = pasportFiles;
	public ID[] PrevWorkpalceIds { get; } = prevWorkpalceIds;
	public ID? CurrentWorkpalceId { get; } = currentWorkpalceId;
	public ID[] AdditionalQualifications { get; } = additionalQualifications;
	public Boolean IsTargetAgreement { get; } = isTargetAgreement;
	public String? TargetAgreementNumber { get; } = targetAgreementNumber;
	public String? TargetAgreementFile { get; } = targetAgreementFile;
	public DateTime? TargetAgreementDate { get; } = targetAgreementDate;
	public ID? TargetAgreementEnterpriseId { get; } = targetAgreementEnterpriseId;
	public Boolean MustServeInArmy { get; } = mustServeInArmy;
	public String? ArmySubpoenaFile { get; } = armySubpoenaFile;
	public DateTime? ArmyCallDate { get; } = armyCallDate;
	public SocialStatus[] SocialStatuses { get; } = socialStatuses;
	public StudentStatus Status { get; } = status;
	public String? Address { get; } = address;
	public Boolean IsForeignCitizen { get; } = isForeignCitizen;
	public String? Inn { get; } = inn;
	public String? Mail { get; } = mail;
	public String[] OtherFiles { get; } = otherFiles;

	public DateTime CreatedDateTimeUtc { get; } = createdDateTimeUtc;
	public DateTime? ModifiedDateTimeUtc { get; } = modifiedDateTimeUtc;
}
