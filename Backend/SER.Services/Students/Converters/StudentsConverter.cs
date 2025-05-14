using SER.Domain.Students;

namespace SER.Services.Students.Converters;

public static class StudentsConverter
{
	public static Student ToStudent(this StudentDB studentDb)
	{
		return new Student(
			id: studentDb.Id,
			name: studentDb.Name,
			secondName: studentDb.SecondName,
			lastName: studentDb.LastName,
			gender: studentDb.Gender,
			birthDate: studentDb.BirthDate,
			phoneNumber: studentDb.PhoneNumber,
			representativePhoneNumber: studentDb.RepresentativePhoneNumber,
			representativeAlias: studentDb.RepresentativeAlias,
			isOnPaidStudy: studentDb.IsOnPaidStudy,
			snils: studentDb.Snils,
			groupId: studentDb.GroupId,
			pasportNumber: studentDb.PassportNumber,
			pasportSeries: studentDb.PassportSeries,
			pasportIssuedBy: studentDb.PassportIssuedBy,
			pasportIssuedDate: studentDb.PassportIssuedDate,
			pasportFiles: studentDb.PassportFiles,
			prevWorkpalceIds: studentDb.PrevWorkplaceIds,
			currentWorkpalceId: studentDb.CurrentWorkplaceId,
			additionalQualifications: studentDb.AdditionalQualifications,
			isTargetAgreement: studentDb.IsTargetAgreement,
			targetAgreementNumber: studentDb.TargetAgreementNumber,
			targetAgreementFile: studentDb.TargetAgreementFile,
			targetAgreementDate: studentDb.TargetAgreementDate,
			targetAgreementEnterpriseId: studentDb.TargetAgreementEnterpriseId,
			mustServeInArmy: studentDb.MustServeInArmy,
			armySubpoenaFile: studentDb.ArmySubpoenaFile,
			armyCallDate: studentDb.ArmyCallDate,
			socialStatuses: studentDb.SocialStatuses,
			status: studentDb.Status,
			address: studentDb.Address,
			isForeignCitizen: studentDb.IsForeignCitizen,
			inn: studentDb.Inn,
			mail: studentDb.Mail,
			otherFiles: studentDb.OtherFiles,
			createdDateTimeUtc: studentDb.CreatedDateTimeUtc,
			modifiedDateTimeUtc: studentDb.ModifiedDateTimeUtc
		);
	}

	public static Student[] ToStudents(this StudentDB[] studentDbs)
    {
        return studentDbs.Select(ToStudent).ToArray();
    }
}
