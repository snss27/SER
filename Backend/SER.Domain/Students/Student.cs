using CSharpFunctionalExtensions;
using SER.Domain.AdditionalQualifications;
using SER.Domain.Enterprises;
using SER.Domain.Groups;
using SER.Domain.Students.Enums;
using SER.Domain.ValueObjects;
using SER.Domain.Workplaces;
using SER.EfCore.Models.Types;
using SER.Tools.Types.IDs;
using SER.Tools.Types.Results;
using SER.Tools.Utils;

namespace SER.Domain.Students;

public class Student
{
	public ID Id { get; }
	public FullName FullName { get; } = default!;
	public Gender Gender { get; }
	public DateTime? BirthDate { get; }
	public String? PhoneNumber { get; }
	public Representative Representative { get; } = default!;
	public Boolean IsOnPaidStudy { get; }
	public String? Snils { get; }
	public Group Group { get; } = default!;
	public Passport Passport { get; } = default!;
	public WorkPlace[] WorkPlaces { get; } = default!;
	public AdditionalQualification[] AdditionalQualifications { get; } = default!;
	public TargetAgreement TargetAgreement { get; } = default!;
	public Army Army { get; } = default!;
	public SocialStatus[] SocialStatuses { get; } = default!;
	public StudentStatus Status { get; }
	public String? Address { get; }
	public Boolean IsForeignCitizen { get; }
	public String? Inn { get; }
	public String? Mail { get; }
	public String[] OtherFiles { get; } = default!;

	public WorkPlace? CurrentWorkPlace => WorkPlaces.FirstOrDefault(wp => wp.IsCurrent);
	public WorkPlace[] PreviousWorkPlaces => [.. WorkPlaces.Where(wp => !wp.IsCurrent)];

	public Student(ID id, FullName fullName, Gender gender, DateTime? birthDate, String? phoneNumber, Representative representative, Boolean isOnPaidStudy, String? snils, Group group, Passport passport, WorkPlace[] workPlaces, AdditionalQualification[] additionalQualifications, TargetAgreement targetAgreement, Army army, SocialStatus[] socialStatuses, StudentStatus status, String? address, Boolean isForeignCitizen, String? inn, String? mail, String[] otherFiles)
	{
		Id = id;
		FullName = fullName;
		Gender = gender;
		BirthDate = birthDate;
		PhoneNumber = phoneNumber;
		Representative = representative;
		IsOnPaidStudy = isOnPaidStudy;
		Snils = snils;
		Group = group;
		Passport = passport;
		WorkPlaces = workPlaces;
		AdditionalQualifications = additionalQualifications;
		TargetAgreement = targetAgreement;
		Army = army;
		SocialStatuses = socialStatuses;
		Status = status;
		Address = address;
		IsForeignCitizen = isForeignCitizen;
		Inn = inn;
		Mail = mail;
		OtherFiles = otherFiles;
	}

	public static Result<Student, Error> Create(ID? id, String? name, String? secondName, String? lastName, Gender? gender, DateTime? birthDate, String? phoneNumber, String? representativePhoneNumber, String? representativeAlias, Boolean? isOnPaidStudy, String? snils, Group? group, String? passportNumber, String? passportSeries, String? passportIssuedBy, DateTime? passportIssuedDate, String[] passportFiles, WorkPlace[] workPlaces, AdditionalQualification[] additionalQualifications, Boolean? isTargetAgreement, String? targetAgreementNumber, Enterprise? targetAgreementEnterprise, DateTime? targetAgreementDate, String[] targetAgreementFiles, Boolean? mustServeInArmy, DateTime? armyCallDate, String[] armySubpoenaFiles, SocialStatus[] socialStatuses, StudentStatus? status, String? address, Boolean? isForeignCitizen, String? inn, String? mail, String[] otherFiles)
	{
		Result<FullName, Error> createFullNameResult = FullName.Create(name, secondName, lastName);
		if(createFullNameResult.IsFailure) return createFullNameResult.Error;
		FullName fullName = createFullNameResult.Value;

		if (gender is null) return new Error("Укажите пол студента");

		if(!String.IsNullOrWhiteSpace(phoneNumber) && !Regexs.PhoneRegex.IsMatch(phoneNumber))
		{
			return new Error("Неверный формат номера телефона");
		}

		Result<Representative, Error> createRepresentativeResult = Representative.Create(representativePhoneNumber, representativeAlias);
		if(createRepresentativeResult.IsFailure) return createRepresentativeResult.Error;
		Representative representative = createRepresentativeResult.Value;

		if (isOnPaidStudy is null) return new Error("Укажите, обучается ли студент на платной основе");

		if (!String.IsNullOrWhiteSpace(snils) && !Regexs.SnilsRegex.IsMatch(snils))
		{
			return new Error("Неверно указан снилс");
		}

		if(group is null)
		{
			return new Error("Укажите группу студента");
		}

		Result<Passport, Error> createPassportResult = Passport.Create(passportNumber, passportSeries, passportIssuedBy, passportIssuedDate, passportFiles);
		if(createPassportResult.IsFailure) return createPassportResult.Error;
		Passport passport = createPassportResult.Value;

		Result<TargetAgreement, Error> createTargetAgreementResult = TargetAgreement.Create(isTargetAgreement, targetAgreementNumber, targetAgreementEnterprise, targetAgreementDate, targetAgreementFiles);
		if(createTargetAgreementResult.IsFailure) return createTargetAgreementResult.Error;
		TargetAgreement targetAgreement = createTargetAgreementResult.Value;

		Result<Army, Error> createArmyResult = Army.Create(mustServeInArmy, armyCallDate, armySubpoenaFiles);
		if(createArmyResult.IsFailure) return createArmyResult.Error;
		Army army = createArmyResult.Value;

		if (status is null) return new Error("Укажите статус студента");

		if (isForeignCitizen is null) return new Error("Укажите, является ли студент иностранным гражданином");

		if (!String.IsNullOrWhiteSpace(mail) && !Regexs.MailRegex.IsMatch(mail))
		{
			return new Error("Неверно указана электронная почта");
		}

		if (!String.IsNullOrWhiteSpace(inn) && !Regexs.HumanInnRegex.IsMatch(inn))
		{
			return new Error("Неверный формат ИНН");
		}

		ID _id = id ?? ID.New();

		return new Student(_id, fullName, gender.Value, birthDate, phoneNumber, representative, isOnPaidStudy.Value, snils, group, passport, workPlaces, additionalQualifications, targetAgreement, army, socialStatuses, status.Value, address, isForeignCitizen.Value, inn, mail, otherFiles);
	}
}
