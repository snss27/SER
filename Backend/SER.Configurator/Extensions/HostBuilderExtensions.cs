using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SER.Configurator.Connectors;
using SER.Tools.Converters;
using SER.Tools.DataBase;
using SER.Tools.DataBase.Connectors.NpgSql;
using System.Reflection;

namespace SER.Configurator.Extensions;
public static class HostBuilderExtensions
{
	const String ENVIRONMENT_NAME = "ASPNETCORE_ENVIRONMENT";

	private static void DefaultAppConfiguration(HostBuilderContext context, IConfigurationBuilder builder)
	{
		builder
			.SetBasePath(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location))
			.AddJsonFile("configs/connectionStrings.json", optional: false, reloadOnChange: true)
			.AddJsonFile($"configs/connectionStrings.{context.HostingEnvironment.EnvironmentName}.json", optional: true, reloadOnChange: true)
			.AddEnvironmentVariables();
	}

	private static void DefaultConfigureServices(HostBuilderContext context, IServiceCollection serviceCollection)
	{
		IConfiguration configuration = context.Configuration;

		IJsonSerializer jsonSerializer = new TextJsonSerializer();

		MainConnector mainConnector = new MainConnector(NpgsqlConnectionString.Get(configuration, "Main"), jsonSerializer);

		serviceCollection
			.AddSingleton(mainConnector)
			.AddSingleton<IJsonSerializer>(jsonSerializer);
	}

	public static IHostBuilder ConfigureWeb(this IHostBuilder hostBuilder, Action<HostBuilderContext, IServiceCollection>? configureServiceCollection = null)
	{
		NpsqlConfigurator.Configure();

		hostBuilder.ConfigureAppConfiguration(DefaultAppConfiguration);
		hostBuilder.ConfigureServices((context, serviceCollection) =>
		{
			DefaultConfigureServices(context, serviceCollection); 
			configureServiceCollection?.Invoke(context, serviceCollection);
		});

		return hostBuilder;
	}
}
