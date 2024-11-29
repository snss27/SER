using SER.Tools.Converters;
using SER.Tools.DataBase;
using SER.Tools.DataBase.Connectors.NpgSql;

namespace SER.Configurator.Connectors;

public class MainConnector(IConnectionString connectionString, IJsonSerializer jsonSerializer)
	: NpgSqlConnector(connectionString, jsonSerializer);