using SER.Domain.AdditionalQualifications;
using SER.Domain.Enterprises;
using SER.Domain.Groups;
using SER.Domain.Students.Enums;
using SER.Domain.WorkPlaces;
using SER.Tools.Types.IDs;

namespace SER.Domain.Students;

public record StudentDto(
	ID Id,
	String Name,
	String SecondName,
	String? LastName,
	Gender Gender,
	DateTime? BirthDate,
	String? PhoneNumber,
	String? RepresentativePhoneNumber,
	String? RepresentativeAlias,
	Boolean IsOnPaidStudy,
	String? Snils,
	GroupDto Group,
	String? PassportNumber,
	String? PassportSeries,
	String? PassportIssuedBy,
	DateTime? PassportIssuedDate,
	String[] PassportFiles,
	WorkPlaceDto[] PrevWorkplaces,
	WorkPlaceDto? CurrentWorkplace,
	AdditionalQualificationDto[] AdditionalQualifications,
	Boolean IsTargetAgreement,
	String? TargetAgreementNumber,
	String[] TargetAgreementFiles,
	DateTime? TargetAgreementDate,
	EnterpriseDto? TargetAgreementEnterprise,
	Boolean MustServeInArmy,
	String[] ArmySubpoenaFiles,
	DateTime? ArmyCallDate,
	SocialStatus[] SocialStatuses,
	StudentStatus Status,
	String? Address,
	Boolean IsForeignCitizen,
	String? Inn,
	String? Mail,
	String[] OtherFiles
);
