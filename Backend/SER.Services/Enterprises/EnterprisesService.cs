using SER.Domain.Enterprises;
using SER.Domain.Services;
using SER.Services.Enterprises.Repositories;
using SER.Tools.Types.IDs;
using SER.Tools.Types.Results;

namespace SER.Services.Enterprises;
public class EnterprisesService : IEnterprisesService
{
	private readonly IEnterprisesRepository _enterprisesRepository;

	public EnterprisesService(IEnterprisesRepository enterprisesRepository)
	{
		_enterprisesRepository = enterprisesRepository;
	}

	public async Task<Result> Save(EnterpriseBlank blank)
	{
		if (String.IsNullOrWhiteSpace(blank.Name)) return Result.Fail("Укажите наименование организации");

		blank.Id ??= ID.New();

		return await _enterprisesRepository.Save(blank);
	}

	public async Task<Result> Remove(ID id)
	{
		return await _enterprisesRepository.Remove(id);
	}

	public async Task<Enterprise?> Get(ID id)
	{
		return await _enterprisesRepository.Get(id);
	}

	public async Task<Enterprise[]> GetPage(Int32 page, Int32 pageSize)
	{
		return await _enterprisesRepository.GetPage(page, pageSize);
	}
}
