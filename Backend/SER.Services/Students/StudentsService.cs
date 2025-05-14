using SER.Domain.AdditionalQualifications;
using SER.Domain.Enterprises;
using SER.Domain.Groups;
using SER.Domain.Services;
using SER.Domain.Students;
using SER.Domain.Students.Converters;
using SER.Domain.WorkPlaces;
using SER.Services.Students.Repositories;
using SER.Tools.Types.IDs;
using SER.Tools.Types.Results;
using SER.Tools.Utils;

namespace SER.Services.Students;

public class StudentsService(IStudentsRepository studentsRepository, IWorkPlacesSevice workPlacesSevice, IFilesService filesService, IGroupsService groupsService, IAdditionalQualificationsService additionalQualificationsService, IEnterprisesService enterprisesService) : IStudentsService
{

	public async Task<Result> Save(StudentBlank blank)
	{
		if (String.IsNullOrWhiteSpace(blank.Name))
		{
			return Result.Fail("Укажите имя студента");
		}

		if (String.IsNullOrWhiteSpace(blank.SecondName))
		{
			return Result.Fail("Укажите фамилию студента");
		}

		if (!String.IsNullOrWhiteSpace(blank.PhoneNumber) && !Regexs.PhoneRegex.IsMatch(blank.PhoneNumber))
		{
			return Result.Fail("Неверно указан номер телефона (скорее всего не полностью)");
		}

		if (!String.IsNullOrWhiteSpace(blank.RepresentativePhoneNumber) &&
			!Regexs.PhoneRegex.IsMatch(blank.RepresentativePhoneNumber))
		{
			return Result.Fail("Неверно указан номер телефона представителя (скорее всего не полностью)");
		}

		if (!String.IsNullOrWhiteSpace(blank.Snils) && !Regexs.SnilsRegex.IsMatch(blank.Snils))
		{
			return Result.Fail("Неверно указан снилс");
		}

		if (!String.IsNullOrWhiteSpace(blank.Mail) && !Regexs.MailRegex.IsMatch(blank.Mail))
		{
			return Result.Fail("Неверно указана электронная почта");
		}

		if (!String.IsNullOrWhiteSpace(blank.Inn) && !Regexs.HumanInnRegex.IsMatch(blank.Inn))
		{
			return Result.Fail("Неверный формат ИНН");
		}

		if (blank.Group is null)
		{
			return Result.Fail("Укажите группу студента");
		}

		if (!String.IsNullOrWhiteSpace(blank.PassportSeries) && !Regexs.PassportSeriesRegex.IsMatch(blank.PassportSeries))
		{
			return Result.Fail("Серия паспорта должна содержать 4 цифры");
		}

		if (!String.IsNullOrWhiteSpace(blank.PassportNumber) && !Regexs.PassportNumberRegex.IsMatch(blank.PassportNumber))
		{
			return Result.Fail("Номер паспорта должен содержать 6 цифр");
		}

		String[] passportFileUrls = filesService.SavePassportFiles(blank.PassportFiles, blank.Group.Number, blank.Name + blank.SecondName);

		ID? currentWorkpalceId = default;

		if (blank.CurrentWorkplace is not null)
		{
			DataResult<ID> currentWorkPlaceSaveResult = await workPlacesSevice.Save(blank.CurrentWorkplace, blank.Group.Number, blank.Name + blank.SecondName);

			if (!currentWorkPlaceSaveResult.IsSuccess) return Result.Fail(currentWorkPlaceSaveResult.Errors[0].Message);

			currentWorkpalceId = currentWorkPlaceSaveResult.Data;
		}

		ID[] prevWorkpalceIds = [];

		DataResult<ID[]> prevWorkPlacesSaveResult = await workPlacesSevice.Save(blank.PrevWorkplaces, blank.Group.Number, blank.Name + blank.SecondName);

		if (!prevWorkPlacesSaveResult.IsSuccess) return Result.Fail(prevWorkPlacesSaveResult.Errors[0].Message);

		prevWorkpalceIds = prevWorkPlacesSaveResult.Data ?? [];

		if (!blank.IsTargetAgreement)
		{
			blank.TargetAgreementNumber = null;
			blank.TargetAgreementDate = null;
			blank.TargetAgreementEnterprise = null;
		}

		String? targetAgreementFileUrl = default;

		if(blank.IsTargetAgreement)
		{
			String[] targetAgreementFiles = filesService.SaveTargetAfreementFiles(blank.TargetAgreementFile, blank.Group.Number, blank.Name + blank.SecondName);
			targetAgreementFileUrl = targetAgreementFiles.FirstOrDefault();
		}

		if (!blank.MustServeInArmy)
		{
			blank.ArmyCallDate = null;
		}

		String? armySubpoenaFileUrl = default;

		if (blank.MustServeInArmy)
		{
			String[] armySubpoenaFiles = filesService.SaveArmySubpoenaFiles(blank.ArmySubpoenaFile, blank.Group.Number, blank.Name + blank.SecondName);
			armySubpoenaFileUrl = armySubpoenaFiles.FirstOrDefault();
		}

		String[] otherFileUrls = filesService.SaveOtherFiles(blank.OtherFiles, blank.Group.Number, blank.Name + blank.SecondName);

		blank.Id ??= ID.New();
		return await studentsRepository.Save(blank, currentWorkpalceId, prevWorkpalceIds, passportFileUrls, targetAgreementFileUrl, armySubpoenaFileUrl, otherFileUrls);
	}

	public async Task<Result> Remove(ID id)
	{
		return await studentsRepository.Remove(id);
	}

	public async Task<StudentDto?> Get(ID id)
	{
		Student? student = await studentsRepository.Get(id);

		if(student is null) return null;

		Task<GroupDto?> groupTask = groupsService.Get(student.GroupId);
		Task<WorkPlaceDto?> currentWorkPlaceTask = student.CurrentWorkpalceId.HasValue
			? workPlacesSevice.Get(student.CurrentWorkpalceId.Value)
			: Task.FromResult<WorkPlaceDto?>(null);
		Task<WorkPlaceDto[]> prevWorkPlacesTask = workPlacesSevice.Get(student.PrevWorkpalceIds);
		Task<AdditionalQualification[]> additionalQualificationsTask = additionalQualificationsService.Get(student.AdditionalQualifications);
		Task<Enterprise?> targetAgreementEnterpriseTask = student.TargetAgreementEnterpriseId.HasValue
			? enterprisesService.Get(student.TargetAgreementEnterpriseId.Value)
			: Task.FromResult<Enterprise?>(null);

		await Task.WhenAll(groupTask, currentWorkPlaceTask, prevWorkPlacesTask, additionalQualificationsTask, targetAgreementEnterpriseTask);

		GroupDto group = await groupTask ?? throw new NullReferenceException("Группа у студента не может отсутствовать");
		WorkPlaceDto? currentWorkPlace = await currentWorkPlaceTask;
		WorkPlaceDto[] prevWorkPlaces = await prevWorkPlacesTask;
		AdditionalQualification[] additionalQualifications = await additionalQualificationsTask;
		Enterprise? targetAgreementEnterprise = await targetAgreementEnterpriseTask;

		return student.ToStudentDto(group, currentWorkPlace, prevWorkPlaces, additionalQualifications, targetAgreementEnterprise);
	}

	public async Task<StudentDto[]> GetPage(int page, int pageSize)
	{
		Student[] students = await studentsRepository.GetPage(page, pageSize);

		return await StudentDtos(students);
	}

	public async Task<Student[]> GetByGroupId(ID groupId)
	{
		return await studentsRepository.GetByGroupId(groupId);
	}

	private async Task<StudentDto[]> StudentDtos(Student[] students)
	{
		ID[] groupIds = students.Select(student => student.GroupId).ToArray();
		Task<GroupDto[]> groupsTask = groupsService.Get(groupIds);

		ID[] currentWorkPlaceIds = students.Where(s => s.CurrentWorkpalceId.HasValue).Select(s => s.CurrentWorkpalceId.Value).Distinct().ToArray();
		Task<WorkPlaceDto[]> currentWorkPlacesTask = workPlacesSevice.Get(currentWorkPlaceIds);

		ID[] prevWorkPlaceIds = students.SelectMany(s => s.PrevWorkpalceIds).Distinct().ToArray();
		Task<WorkPlaceDto[]> prevWorkPlacesTask = workPlacesSevice.Get(prevWorkPlaceIds);

		ID[] additionalQualificationIds = students.SelectMany(s => s.AdditionalQualifications).Distinct().ToArray();
		Task<AdditionalQualification[]> additionalQualificationsTask = additionalQualificationsService.Get(additionalQualificationIds);

		ID[] targetAgreementEnterpriseIds = students.Where(s => s.TargetAgreementEnterpriseId.HasValue).Select(s => s.TargetAgreementEnterpriseId.Value).Distinct().ToArray();
		Task<Enterprise[]> targetArgreementEnterprisesTask = enterprisesService.Get(targetAgreementEnterpriseIds);

		await Task.WhenAll(groupsTask, currentWorkPlacesTask, prevWorkPlacesTask, additionalQualificationsTask, targetArgreementEnterprisesTask);

		GroupDto[] groups = await groupsTask;
		WorkPlaceDto[] currentWorkPlaces = await currentWorkPlacesTask;
		WorkPlaceDto[] prevWorkPlaces = await prevWorkPlacesTask;
		AdditionalQualification[] additionalQualifications = await additionalQualificationsTask;
		Enterprise[] targetAgreementEnterprises = await targetArgreementEnterprisesTask;

		return students.ToStudentDtos(groups, currentWorkPlaces, prevWorkPlaces, additionalQualifications, targetAgreementEnterprises);
	}
}
