using SER.Database.Models.AdditionalQualifications;
using SER.Database.Models.Students;
using SER.Domain.AdditionalQualifications;
using SER.Domain.Enterprises;
using SER.Domain.Groups;
using SER.Domain.Students;
using SER.Domain.Students.Enums;
using SER.Domain.Workplaces;
using SER.Services.AdditionalQualifications.Converters;
using SER.Services.Enterprises.Converters;
using SER.Services.Groups.Converters;
using SER.Services.Students.Extensions;
using SER.Services.WorkPlaces.Extensions;

namespace SER.Services.Students.Extensions;

internal static class StudetExtensions
{
	public static StudentEntity ToEntity(this Student student, List<AdditionalQualificationEntity> additionalQualifications)
	{
		
		return new StudentEntity()
		{
			Id = student.Id,
			Name = student.FullName.First,
			SecondName = student.FullName.Second,
			LastName = student.FullName.Last,
			Gender = student.Gender,
			BirthDate = student.BirthDate,
			PhoneNumber = student.PhoneNumber,
			RepresentativePhoneNumber = student.Representative.PhoneNumber,
			RepresentativeAlias = student.Representative.Alias,
			IsOnPaidStudy = student.IsOnPaidStudy,
			Snils = student.Snils,
			GroupId = student.Group.Id,
			PassportNumber = student.Passport.Number,
			PassportSeries = student.Passport.Series,
			PassportIssuedBy = student.Passport.IssuedBy,
			PassportIssuedDate = student.Passport.IssuedDate,
			PassportFiles = [.. student.Passport.Files],
			AdditionalQualifications = [.. additionalQualifications],
			IsTargetAgreement = student.TargetAgreement.Exist,
			TargetAgreementNumber = student.TargetAgreement.Number,
			TargetAgreementFiles = [.. student.TargetAgreement.Files],
			TargetAgreementDate = student.TargetAgreement.Date,
			TargetAgreementEnterpriseId = student.TargetAgreement.Enterprise?.Id,
			MustServeInArmy = student.Army.MustServe,
			ArmyCallDate = student.Army.CallDate,
			ArmySubpoenaFiles = [.. student.Army.SubpoenaFiles],
			SocialStatuses = [.. student.SocialStatuses.Select(s => (Int32)s)],
			Status = student.Status,
			Address = student.Address,
			IsForeignCitizen = student.IsForeignCitizen,
			Inn	= student.Inn,
			Mail = student.Mail,
			OtherFiles = [.. student.OtherFiles],
			CreatedDateTimeUtc = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Unspecified),
			ModifiedDateTimeUtc = null
		};
	}

	public static void ApplyChanges(this StudentEntity entity, Student student, List<AdditionalQualificationEntity> additionalQualifications)
	{
		entity.Name = student.FullName.First;
		entity.SecondName = student.FullName.Second;
		entity.LastName = student.FullName.Last;
		entity.Gender = student.Gender;
		entity.BirthDate = student.BirthDate;
		entity.PhoneNumber = student.PhoneNumber;
		entity.RepresentativePhoneNumber = student.Representative.PhoneNumber;
		entity.RepresentativeAlias = student.Representative.Alias;
		entity.IsOnPaidStudy = student.IsOnPaidStudy;
		entity.Snils = student.Snils;
		entity.GroupId = student.Group.Id;
		entity.PassportNumber = student.Passport.Number;
		entity.PassportSeries = student.Passport.Series;
		entity.PassportIssuedBy = student.Passport.IssuedBy;
		entity.PassportIssuedDate = student.Passport.IssuedDate;
		entity.PassportFiles = [.. student.Passport.Files];
		entity.IsTargetAgreement = student.TargetAgreement.Exist;
		entity.TargetAgreementNumber = student.TargetAgreement.Number;
		entity.TargetAgreementFiles = [.. student.TargetAgreement.Files];
		entity.TargetAgreementDate = student.TargetAgreement.Date;
		entity.TargetAgreementEnterpriseId = student.TargetAgreement.Enterprise?.Id;
		entity.MustServeInArmy = student.Army.MustServe;
		entity.ArmyCallDate = student.Army.CallDate;
		entity.AdditionalQualifications = additionalQualifications;
		entity.ArmySubpoenaFiles = [.. student.Army.SubpoenaFiles];
		entity.SocialStatuses = [.. student.SocialStatuses.Select(s => (Int32)s)];
		entity.Status = student.Status;
		entity.Address = student.Address;
		entity.IsForeignCitizen = student.IsForeignCitizen;
		entity.Inn = student.Inn;
		entity.Mail = student.Mail;
		entity.OtherFiles = [.. student.OtherFiles];
		entity.ModifiedDateTimeUtc = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Unspecified);
	}

	public static Student ToDomain(this StudentEntity entity)
	{
		Group group = entity.Group.ToDomain();
		WorkPlace[] workPlaces = [.. entity.WorkPlaces.Select(wp => wp.ToDomain())];
		AdditionalQualification[] additionalQualifications = [.. entity.AdditionalQualifications.Select(aq => aq.ToDomain())];
		Enterprise? targetAdreementEnterprise = entity.TargetAgreementEnterprise?.ToDomain();

		return Student.Create(entity.Id, entity.Name, entity.SecondName, entity.LastName, entity.Gender, entity.BirthDate, entity.PhoneNumber, entity.RepresentativePhoneNumber, entity.RepresentativeAlias, entity.IsOnPaidStudy, entity.Snils, group, entity.PassportNumber, entity.PassportSeries, entity.PassportIssuedBy, entity.PassportIssuedDate, [.. entity.PassportFiles], workPlaces, additionalQualifications, entity.IsTargetAgreement, entity.TargetAgreementNumber, targetAdreementEnterprise, entity.TargetAgreementDate, [.. entity.TargetAgreementFiles], entity.MustServeInArmy, entity.ArmyCallDate, [.. entity.ArmySubpoenaFiles], [.. entity.SocialStatuses.Select(s => (SocialStatus)s)], entity.Status, entity.Address, entity.IsForeignCitizen, entity.Inn, entity.Mail, [.. entity.OtherFiles]).Value;
	}
}
