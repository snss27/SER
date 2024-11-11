using SER.Configurator.Connectors;

namespace SER.Services._base;
public abstract class BaseRepository
{
	private protected readonly MainConnector _connector;

	public BaseRepository(MainConnector connector)
	{
		_connector = connector;
	}
}
