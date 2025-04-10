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
	Boolean isOnPaidStudy,
	String? snils,
	ID groupId,
	String? pasportNumber,
	String? pasportSeries,
	String? pasportIssued,
	String[] pasportFiles,
	ID[] prevWorkpalceIds,
	ID? currentWorkpalceId,
	ID[] additionalQualifications,
	Boolean isTargetAgreement,
	String? targetAgreementFile,
	DateTime? targetAgreementDate,
	ID? targetAgreementEnterpriseId,
	Boolean? mustServeInArmy,
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
	ID Id { get;  } = id;
	String Name { get;} = name;
	String SecondName { get; } = secondName;
	String? LastName { get; } = lastName;
	Genders Gender { get;  } = gender;
	DateTime? BirthDate { get; } = birthDate;
	String? PhoneNumber { get; } = phoneNumber;
	String? RepresentativePhoneNumber { get; } = representativePhoneNumber;
	Boolean IsOnPaidStudy { get; } = isOnPaidStudy;
	String? Snils { get; } = snils;
	ID GroupId { get; } = groupId;
	String? PasportNumber { get; } = pasportNumber;
	String? PasportSeries { get; } = pasportSeries;
	String? PassportIssued { get; } = pasportIssued;
	String[] PassportFiles { get; } = pasportFiles;
	ID[] PrevWorkpalceIds { get; } = prevWorkpalceIds;
	ID? CurrentWorkpalceId { get; } = currentWorkpalceId;
	ID[] AdditionalQualifications { get; } = additionalQualifications;
	Boolean IsTargetAgreement { get; } = isTargetAgreement;
	String? TargetAgreementFile { get; } = targetAgreementFile;
	DateTime? TargetAgreementDate { get; } = targetAgreementDate;
	ID? TargetAgreementEnterpriseId { get; } = targetAgreementEnterpriseId;
	Boolean? MustServeInArmy { get; } = mustServeInArmy;
	String? ArmySubpoenaFile { get; } = armySubpoenaFile;
	DateTime? ArmyCallDate { get; } = armyCallDate;
	SocialStatus[] SocialStatuses { get; } = socialStatuses;
	StudentStatus Status { get; } = status;
	String? Address { get; } = address;
	Boolean IsForeignCitizen { get; } = isForeignCitizen;
	String? Inn { get; } = inn;
	String? Mail { get; } = mail;
	String[] OtherFiles { get; } = otherFiles;

	DateTime CreatedDateTimeUtc { get; } = createdDateTimeUtc;
	DateTime? ModifiedDateTimeUtc { get; } = modifiedDateTimeUtc;
}
