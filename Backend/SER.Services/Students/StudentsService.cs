using SER.Domain.Students;
using SER.Domain.Services;
using SER.Services.Students.Repositories;
using SER.Tools.Types.IDs;
using SER.Tools.Types.Results;
using SER.Tools.Utils;

namespace SER.Services.Students;

public class StudentsService : IStudentsService
{
	private readonly IStudentsRepository _studentsRepository;

	public StudentsService(IStudentsRepository studentsRepository)
	{
		_studentsRepository = studentsRepository;
	}

	public async Task<Result> Save(StudentBlank blank)
	{
		//if (string.IsNullOrWhiteSpace(blank.Name))
		//{
		//	return Result.Fail("Укажите имя студента");
		//}

		//if (!string.IsNullOrWhiteSpace(blank.PhoneNumber) && !Regexs.PhoneRegex.IsMatch(blank.PhoneNumber))
		//{
		//	return Result.Fail("Неверно указан номер телефона (скорее всего не полностью)");
		//}

		//if (!string.IsNullOrWhiteSpace(blank.RepresentativePhoneNumber) &&
		//	!Regexs.PhoneRegex.IsMatch(blank.RepresentativePhoneNumber))
		//{
		//	return Result.Fail("Неверно указан номер телефона представителя");
		//}

		////if (!string.IsNullOrWhiteSpace(blank.Snils) && !Regexs.SnilsRegex.IsMatch(blank.Snils))
		////{
		////	return Result.Fail("СНИЛС должен быть в формате 000-000-000 00");
		////}

		//if (!string.IsNullOrWhiteSpace(blank.Mail) && !Regexs.MailRegex.IsMatch(blank.Mail))
		//{
		//	return Result.Fail("Неверно указана электронная почта");
		//}

		//if (!string.IsNullOrWhiteSpace(blank.Inn) && !Regexs.InnRegex.IsMatch(blank.Inn))
		//{
		//	return Result.Fail("ИНН должен содержать 10 или 12 цифр");
		//}

		////if (!string.IsNullOrWhiteSpace(blank.PassportSeries) && !Regexs.PassportSeriesRegex.IsMatch(blank.PassportSeries))
		////{
		////	return Result.Fail("Серия паспорта должна содержать 4 цифры");
		////}

		////if (!string.IsNullOrWhiteSpace(blank.PassportNumber) && !Regexs.PassportNumberRegex.IsMatch(blank.PassportNumber))
		////{
		////	return Result.Fail("Номер паспорта должен содержать 6 цифр");
		////}

		blank.Id ??= ID.New();
		return await _studentsRepository.Save(blank);
	}

	public async Task<Result> Remove(ID id)
	{
		return await _studentsRepository.Remove(id);
	}

	public async Task<Student?> Get(ID id)
	{
		return await _studentsRepository.Get(id);
	}

	public async Task<Student[]> GetPage(int page, int pageSize)
	{
		return await _studentsRepository.GetPage(page, pageSize);
	}
}
