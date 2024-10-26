using SER.Tools.Converters;
using SER.Tools.DataBase;
using SER.Tools.DataBase.Connectors.NpgSql;

namespace SER.Configurator.Connectors;
public class MainConnector : NpgSqlConnector
{
	public MainConnector(IConnectionString connectionString, IJsonSerializer jsonSerializer) : base(connectionString, jsonSerializer)
	{
	}
}
