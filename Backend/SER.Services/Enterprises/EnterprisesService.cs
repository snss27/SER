using SER.Domain.Enterprises;
using SER.Domain.Services;
using SER.Domain.Workplaces;
using SER.Services.Enterprises.Repositories;
using SER.Services.WorkPlaces.Repositories;
using SER.Tools.Types.IDs;
using SER.Tools.Types.Results;
using SER.Tools.Utils;

namespace SER.Services.Enterprises;

public class EnterprisesService(IEnterprisesRepository enterprisesRepository, IWorkPlacesRepository workPlacesRepository) : IEnterprisesService
{
	//TODO Нужна ли регулярка на адрес, чтобы иметь одинаковый формат
	public async Task<OperationResult> Save(EnterpriseBlank blank)
	{
		if (String.IsNullOrWhiteSpace(blank.Name))
		{
			return OperationResult.Fail("Укажите наименование организации");
		}

		if (!String.IsNullOrWhiteSpace(blank.INN) && !Regexs.EnterpriseInnRegex.IsMatch(blank.INN))
		{
			return OperationResult.Fail("Инн должен содержать 10 цифр");
		}

		if (!String.IsNullOrWhiteSpace(blank.KPP) && !Regexs.KppRegex.IsMatch(blank.KPP))
		{
			return OperationResult.Fail("КПП должен содержать 9 цифр");
		}

		if (!String.IsNullOrWhiteSpace(blank.ORGN) && !Regexs.OrgnRegex.IsMatch(blank.ORGN))
		{
			return OperationResult.Fail("ОРГН должен содержать 13 цифр");
		}

		if (!String.IsNullOrWhiteSpace(blank.Phone) && !Regexs.PhoneRegex.IsMatch(blank.Phone))
		{
			return OperationResult.Fail("Неверно указан номер телефона (скорее всего не полностью)");
		}

		if (!String.IsNullOrWhiteSpace(blank.Mail) && !Regexs.MailRegex.IsMatch(blank.Mail))
		{
			return OperationResult.Fail("Неверно указана электронная почта (скорее всего не полностью)");
		}

		if(blank.IsOPK is null) throw new ArgumentNullException(nameof(blank.IsOPK));

		blank.Id ??= ID.New();

		return await enterprisesRepository.Save(blank);
	}

	public async Task<OperationResult> Remove(ID id)
	{
		WorkPlace[] workPlaces = await workPlacesRepository.GetByEnterpriseId(id);
		if(workPlaces.Length > 0)
		{
			return OperationResult.Fail("Невозможно удалить, т.к. есть места работы с данной огранизацией");
		}

		return await enterprisesRepository.Remove(id);
	}

	public async Task<Enterprise?> Get(ID id)
	{
		return await enterprisesRepository.Get(id);
	}

	public async Task<Enterprise[]> Get(ID[] ids)
	{
		return await enterprisesRepository.Get(ids);
	}

	public async Task<Enterprise[]> GetPage(Int32 page, Int32 pageSize)
	{
		return await enterprisesRepository.GetPage(page, pageSize);
	}

	public async Task<Enterprise[]> GetBySearchText(String searchText)
	{
		return await enterprisesRepository.GetBySearchText(searchText);
	}
}