using SER.Domain.Enterprises;
using SER.Domain.Services;
using SER.Services.Enterprises.Repositories;
using SER.Tools.Types.IDs;
using SER.Tools.Types.Results;
using SER.Tools.Utils;

namespace SER.Services.Enterprises;

public class EnterprisesService(IEnterprisesRepository enterprisesRepository) : IEnterprisesService
{
	//TODO Нужна ли регулярка на адрес, чтобы иметь одинаковый формат
	public async Task<Result> Save(EnterpriseBlank blank)
	{
		if (String.IsNullOrWhiteSpace(blank.Name))
		{
			return Result.Fail("Укажите наименование организации");
		}

		if (!String.IsNullOrWhiteSpace(blank.INN) && !Regexs.InnRegex.IsMatch(blank.INN))
		{
			return Result.Fail("Инн должен содержать 10 цифр");
		}

		if (!String.IsNullOrWhiteSpace(blank.KPP) && !Regexs.KppRegex.IsMatch(blank.KPP))
		{
			return Result.Fail("КПП должен содержать 9 цифр");
		}

		if (!String.IsNullOrWhiteSpace(blank.ORGN) && !Regexs.OrgnRegex.IsMatch(blank.ORGN))
		{
			return Result.Fail("ОРГН должен содержать 13 цифр");
		}

		if (!String.IsNullOrWhiteSpace(blank.Phone) && !Regexs.PhoneRegex.IsMatch(blank.Phone))
		{
			return Result.Fail("Неверно указан номер телефона (скорее всего не полностью)");
		}

		if (!String.IsNullOrWhiteSpace(blank.Mail) && !Regexs.MailRegex.IsMatch(blank.Mail))
		{
			return Result.Fail("Неверно указана электронная почта (скорее всего не полностью)");
		}

		if(blank.IsOPK is null) throw new ArgumentNullException(nameof(blank.IsOPK));

		blank.Id ??= ID.New();

		return await enterprisesRepository.Save(blank);
	}

	public async Task<Result> Remove(ID id)
	{
		return await enterprisesRepository.Remove(id);
	}

	public async Task<Enterprise?> Get(ID id)
	{
		return await enterprisesRepository.Get(id);
	}

	public async Task<Enterprise[]> GetPage(Int32 page, Int32 pageSize)
	{
		return await enterprisesRepository.GetPage(page, pageSize);
	}
}