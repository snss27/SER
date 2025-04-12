using SER.Domain.AdditionalQualifications;
using SER.Domain.Enterprises;
using SER.Domain.Groups;
using SER.Domain.Students.Enums;
using SER.Domain.Workplaces;
using SER.Domain.WorkPlaces;
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
	String? PassportIssuedBy,
	DateTime? PassportIssuedDate,
	String[] PassportFiles,
	WorkPlaceDto[] PrevWorkplaces,
	WorkPlaceDto? CurrentWorkplace,
	AdditionalQualification[] AdditionalQualifications,
	Boolean IsTargetAgreement,
	String? TargetAgreementFile,
	DateTime? TargetAgreementDate,
	Enterprise? TargetAgreementEnterprise,
	Boolean MustServeInArmy,
	String? ArmySubpoenaFile,
	DateTime? ArmyCallDate,
	SocialStatus[] SocialStatuses,
	StudentStatus Status,
	String? Address,
	Boolean IsForeignCitizen,
	String? Inn,
	String? Mail,
	String[] OtherFiles
);