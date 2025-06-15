using SER.Domain.AdditionalQualifications;
using SER.Domain.Enterprises;
using SER.Domain.Reports.Grouping.Enums;
using SER.Domain.Students.Enums;
using SER.Domain.Students.StudentsFilters.Enums;
using SER.Tools.Types;

namespace SER.Domain.Reports.Grouping;
public class ReportGroupingOptionsDto
{
	public Gender? Gender { get; set; }
	public DateTimePeriod BirthDatePeriod { get; set; }
	public OnPaidStudyVariant OnPaidStudyVariant { get; set; }
	public GroupGroupingOptionsDto GroupGroupingOptions { get; set; }
	public EnterpriseDto[] WorkPlaceEnterprises { get; set; }
	public WorkPlaceGroupingType WorkPlaceGroupingType { get; set; }
	public Boolean UseStrictMatchForAdditionalQualifications { get; set; }
	public AdditionalQualificationDto[] AdditionalQualifications { get; set; }
	public EnterpriseDto[] TargetAgreementEnterprises { get; set; }
	public ArmyGroupingOptionsDto? ArmyGroupingOptions { get; set; }
	public Boolean UseStrictMatchForSocialStatuses { get; set; }
	public SocialStatus[] SocialStatuses { get; set; }
	public StudentStatus[] Statuses { get; set; }
	public ForeignCitizenVariant ForeignCitizenVariant { get; set; }
}
