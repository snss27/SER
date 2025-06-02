using SER.Domain.AdditionalQualifications;
using SER.Domain.AdditionalQualifications.Converters;
using SER.Domain.Enterprises;
using SER.Domain.Enterprises.Converters;
using SER.Domain.Groups;
using SER.Domain.Groups.Converters;
using SER.Domain.WorkPlaces;
using SER.Domain.WorkPlaces.Converters;

namespace SER.Domain.Students.Converters;
public static class StudentsConverters
{
	public static StudentDto ToDto(this Student student)
	{
		GroupDto group = student.Group.ToDto();
		WorkPlaceDto? currentWorkPlace = student.CurrentWorkPlace?.ToDto();
		WorkPlaceDto[] prevWorkPlaces = [.. student.PreviousWorkPlaces.Select(wp => wp.ToDto())];
		AdditionalQualificationDto[] additionalQualifications = [.. student.AdditionalQualifications.Select(aq => aq.ToDto())];
		EnterpriseDto? targetAgreementEnterise = student.TargetAgreement.Enterprise?.ToDto(); 

		return new StudentDto(student.Id, student.FullName.First, student.FullName.Second, student.FullName.Last, student.Gender, student.BirthDate, student.PhoneNumber, student.Representative.PhoneNumber, student.Representative.Alias, student.IsOnPaidStudy, student.Snils, group, student.Passport.Number, student.Passport.Series, student.Passport.IssuedBy, student.Passport.IssuedDate, student.Passport.Files, prevWorkPlaces, currentWorkPlace, additionalQualifications, student.TargetAgreement.Exist, student.TargetAgreement.Number, student.TargetAgreement.Files, student.TargetAgreement.Date, targetAgreementEnterise, student.Army.MustServe, student.Army.SubpoenaFiles, student.Army.CallDate, student.SocialStatuses, student.Status, student.Address, student.IsForeignCitizen, student.Inn, student.Mail, student.OtherFiles);
	}
}
