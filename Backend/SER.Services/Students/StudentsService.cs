using SER.Domain.Students;
using SER.Domain.Services;
using SER.Services.Students.Repositories;
using SER.Tools.Types.IDs;
using SER.Tools.Types.Results;
using SER.Tools.Utils;
using SER.Domain.Workplaces;

namespace SER.Services.Students;

public class StudentsService(IStudentsRepository studentsRepository, IWorkPlacesSevice workPlacesSevice, IFilesService filesService) : IStudentsService
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
			blank.TargetAgreementDate = null;
			blank.TargetAgreementEnterprise = null;
		}

		String? targetAgreementFileUrl = default;

		if(blank.IsTargetAgreement)
		{
			String[] targetAgreementFiles = filesService.SaveTargetAfreementFiles(blank.TargetAgreementFile, blank.Group.Number, blank.Name + blank.SecondName);
			targetAgreementFileUrl = targetAgreementFiles[0];
		}

		if (!blank.MustServeInArmy)
		{
			blank.ArmyCallDate = null;
		}

		String? armySubpoenaFileUrl = default;

		if (blank.MustServeInArmy)
		{
			String[] armySubpoenaFiles = filesService.SaveArmySubpoenaFiles(blank.ArmySubpoenaFile, blank.Group.Number, blank.Name + blank.SecondName);
			armySubpoenaFileUrl = armySubpoenaFiles[0];
		}

		String[] otherFileUrls = filesService.SaveOtherFiles(blank.OtherFiles, blank.Group.Number, blank.Name + blank.SecondName);

		blank.Id ??= ID.New();
		return await studentsRepository.Save(blank, currentWorkpalceId, prevWorkpalceIds, passportFileUrls, targetAgreementFileUrl, armySubpoenaFileUrl, otherFileUrls);
	}

	public async Task<Result> Remove(ID id)
	{
		return await studentsRepository.Remove(id);
	}

	public async Task<Student?> Get(ID id)
	{
		return await studentsRepository.Get(id);
	}

	public async Task<Student[]> GetPage(int page, int pageSize)
	{
		return await studentsRepository.GetPage(page, pageSize);
	}

	public async Task<Student[]> GetByGroupId(ID groupId)
	{
		return await studentsRepository.GetByGroupId(groupId);
	}
}
