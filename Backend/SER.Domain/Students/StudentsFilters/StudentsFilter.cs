using SER.Domain.AdditionalQualifications;
using SER.Domain.Enterprises;
using SER.Domain.Groups;
using SER.Domain.Students.Enums;
using SER.Domain.Students.StudentsFilters.Enums;
using SER.Tools.Types;

namespace SER.Domain.Students.StudentsFilters;
public record StudentsFilter(
	String SearchText,
	Gender? Gender,
	StudentStatus[] Statuses,
	DateTimePeriod BirthDatePeriod,
	SocialStatus[] SocialStatuses,
	GroupDto[] Groups,
	OnPaidStudyVariant OnPaidStudyVariant,
	ForeignCitizenVariant ForeignCitizenVariant,
	AdditionalQualificationDto[] AdditionalQualifications,
	TargetAgreementVariant TargetAgreementVariant,
	EnterpriseDto[] TargetAgreementEnterprises,
	MustServeInArmyVariant MustServeInArmyVariant,
	DateTimePeriod ArmyCallDatePeriod
);
