using SER.Domain.Curators;
using SER.Tools.Types.IDs;
using SER.Tools.Types.Results;

namespace SER.Domain.Services;
public interface ICuratorsService
{
	public Task<Result> Save(CuratorBlank blank);
	public Task<Result> Remove(ID id);
	public Task<Curator> Get(ID id);
	public Task<Curator[]> GetCuratorsPage(Int32 page, Int32 pageSize);
}
