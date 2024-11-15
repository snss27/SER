using SER.Domain.Curators;
using SER.Domain.Services;
using SER.Services.Curators.Repositories;
using SER.Tools.Types.IDs;
using SER.Tools.Types.Results;

namespace SER.Services.Curators;
public class CuratorsService : ICuratorsService
{
	private readonly ICuratorsRepository _curatorsRepository;

	public CuratorsService(ICuratorsRepository curatorsRepository)
	{
		_curatorsRepository = curatorsRepository;
	}

	public async Task<Result> Save(CuratorBlank blank)
	{
		if (String.IsNullOrWhiteSpace(blank.Name)) return Result.Fail("Введите имя куратора");

		blank.Id ??= ID.New();

		return await _curatorsRepository.Save(blank);
	}
	public async Task<Result> Remove(ID id)
	{
		return await _curatorsRepository.Remove(id);
	}
	public async Task<Curator?> Get(ID id)
	{
		return await _curatorsRepository.Get(id);
	}

	public async Task<Curator[]> Get(ID[] ids)
	{
		return await _curatorsRepository.Get(ids);
	}

	public async Task<Curator[]> GetCuratorsPage(Int32 page, Int32 pageSize)
	{
		return await _curatorsRepository.GetPage(page, pageSize);
	}

	public async Task<Curator[]> Get(String searchText)
	{
		return await _curatorsRepository.Get(searchText);
	}
}
