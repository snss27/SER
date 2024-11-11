using SER.Domain.Curators;
using SER.Tools.Types.IDs;
using SER.Tools.Types.Results;

namespace SER.Services.Curators.Repositories;
public interface ICuratorsRepository
{
	public Task<Result> Save(CuratorBlank blank);
	public Task<Result> Remove(ID id);
	public Task<Curator> Get(ID id);
}
