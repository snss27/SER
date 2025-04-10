using SER.Domain.AdditionalQualifications;
using SER.Domain.Enterprises;
using SER.Domain.Groups;
using SER.Domain.Students.Enums;
using SER.Domain.Workplaces;
using SER.Tools.Types.IDs;
using System;
using System.Reflection;

namespace SER.Domain.Students;

public record StudentDto(
	ID Id,
	String Name,
	String SecondName,
	String? LastName,
	Genders Gender,
	DateTime? BirthDate,
	String? PhoneNumber,
	String? RepresentativePhoneNumber,
	Boolean IsOnPaidStudy,
	String? Snils,
	GroupDto Group,
	String? PassportNumber,
	String? PassportSeries,
	String? PassportIssued,
	DateTime? PassportIssuedDate,
	String[] PassportFiles,
	WorkplaceBlank[] PrevWorkplaces,
	WorkplaceBlank? CurrentWorkplace,
	AdditionalQualification[] AdditionalQualifications,
	bool IsTargetAgreement,
	string? TargetAgreementFile,
	DateTime? TargetAgreementDate,
	Enterprise? TargetAgreementEnterprise,
	bool MustServeInArmy,
	string? ArmySubpoenaFile,
	DateTime? ArmyCallDate,
	SocialStatus[] SocialStatuses,
	StudentStatus Status,
	string? Address,
	bool IsForeignCitizen,
	string? Inn,
	string? Mail,
	string[] OtherFiles,
	DateTime CreatedDateTimeUtc,
	DateTime? ModifiedDateTimeUtc
);