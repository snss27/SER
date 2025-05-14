using System.Collections.Generic;
using SER.Domain.AdditionalQualifications;
using SER.Domain.Enterprises;
using SER.Domain.Groups;
using SER.Domain.Workplaces;
using SER.Domain.WorkPlaces;
using SER.Tools.Types.Results;

namespace SER.Domain.Students.Converters;
public static class StudentDtoConverters
{
	public static StudentDto ToStudentDto(
		this Student student,
		GroupDto group,
		WorkPlaceDto? currentWorkPlace,
		WorkPlaceDto[] prevWorkPlaces,
		AdditionalQualification[] additionalQualifications,
		Enterprise? targetAgreementEnterprise
	)
	{
		return new StudentDto(
			student.Id,
			student.Name,
			student.SecondName,
			student.LastName,
			student.Gender,
			student.BirthDate,
			student.PhoneNumber,
			student.RepresentativePhoneNumber,
			student.RepresentativeAlias,
			student.IsOnPaidStudy,
			student.Snils,
			group,
			student.PasportNumber,
			student.PasportSeries,
			student.PassportIssuedBy,
			student.PassportIssuedDate,
			student.PassportFiles,
			prevWorkPlaces,
			currentWorkPlace,
			additionalQualifications,
			student.IsTargetAgreement,
			student.TargetAgreementNumber,
			student.TargetAgreementFile,
			student.TargetAgreementDate,
			targetAgreementEnterprise,
			student.MustServeInArmy,
			student.ArmySubpoenaFile,
			student.ArmyCallDate,
			student.SocialStatuses,
			student.Status,
			student.Address,
			student.IsForeignCitizen,
			student.Inn,
			student.Mail,
			student.OtherFiles
		);
	}

	public static StudentDto[] ToStudentDtos(
		this Student[] students,
		GroupDto[] groups,
		WorkPlaceDto[] currentWorkPlaces,
		WorkPlaceDto[] prevWorkplaces,
		AdditionalQualification[] additionalQualifications,
		Enterprise[] targetAgreementEnterprises
	)
	{
		List<StudentDto> result = new();
		result.AddRange(
		from @student in students
		let studentGroup = groups.FirstOrDefault(g => g.Id == @student.GroupId)
		let studentPrevWorkPlaces = prevWorkplaces.Where(pv => student.PrevWorkpalceIds.Contains(pv.Id)).ToArray()
		let studentCurrentWorkPlace = currentWorkPlaces.FirstOrDefault(cv => cv.Id == @student.CurrentWorkpalceId)
		let studentAdditionalQualifications = additionalQualifications.Where(aq => student.AdditionalQualifications.Contains(aq.Id)).ToArray()
		let targetAgreementEnterprise = targetAgreementEnterprises.FirstOrDefault(tae => tae.Id == @student.TargetAgreementEnterpriseId)
		select new StudentDto(
			@student.Id,
			@student.Name,
			@student.SecondName,
			@student.LastName,
			@student.Gender,
			@student.BirthDate,
			@student.PhoneNumber,
			@student.RepresentativePhoneNumber,
			@student.RepresentativeAlias,
			@student.IsOnPaidStudy,
			@student.Snils,
			studentGroup,
			@student.PasportNumber,
			@student.PasportSeries,
			@student.PassportIssuedBy,
			@student.PassportIssuedDate,
			@student.PassportFiles,
			studentPrevWorkPlaces,
			studentCurrentWorkPlace,
			studentAdditionalQualifications,
			@student.IsTargetAgreement,
			@student.TargetAgreementNumber,
			@student.TargetAgreementFile,
			@student.TargetAgreementDate,
			targetAgreementEnterprise,
			@student.MustServeInArmy,
			@student.ArmySubpoenaFile,
			@student.ArmyCallDate,
			@student.SocialStatuses,
			@student.Status,
			@student.Address,
			@student.IsForeignCitizen,
			@student.Inn,
			@student.Mail,
			@student.OtherFiles
		)
		);

		return result.ToArray();
	}
}
