using SER.Domain.Students;

namespace SER.Services.Students.Converters;

internal static class FlatStudentsConverter
{
	public static Student ToFlatStudent(this StudentDB studentDb)
	{
		return new Student(
			id: studentDb.Id,
			name: studentDb.Name,
			surname: studentDb.Surname,
			patronymic: studentDb.Patronymic,
			gender: studentDb.Gender,
			birthDate: studentDb.BirthDate,
			phoneNumber: studentDb.PhoneNumber,
			representativePhoneNumber: studentDb.RepresentativePhoneNumber,
			isOnPaidStudy: studentDb.IsOnPaidStudy,
			snils: studentDb.Snils,
			groupId: studentDb.GroupId,
			passportId: studentDb.PassportId,
			workPlacesInfoId: studentDb.WorkPlacesInfoId,
			additionalQualifications: studentDb.AdditionalQualifications,
			isTargetAgreement: studentDb.IsTargetAgreement,
			targetAgreementFile: studentDb.TargetAgreementFile,
			isSubjectToArmyDraft: studentDb.IsSubjectToArmyDraft,
			armySubpoenaFile: studentDb.ArmySubpoenaFile,
			armyServeDate: studentDb.ArmyServeDate,
			peculiarities: studentDb.Peculiarities,
			passportSeries: studentDb.PassportSeries,
			passportNumber: studentDb.PassportNumber,
			mail: studentDb.Mail,
			inn: studentDb.Inn,
			isForeignCitizen: studentDb.IsForeignCitizen,
			address: studentDb.Address,
			createdDateTimeUtc: studentDb.CreatedDateTimeUtc,
			modifiedDateTimeUtc: studentDb.ModifiedDateTimeUtc
		);
	}

	public static Student[] ToFlatStudents(this StudentDB[] studentDbs)
    {
        return studentDbs.Select(ToFlatStudent).ToArray();
    }
}
