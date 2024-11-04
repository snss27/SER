using Microsoft.Extensions.DependencyInjection;
using SER.Configurator.Connectors;
using SER.Services.Students;
using SER.Services.Students.Repositories;

namespace SER.Services.Configurator;
public static class ServicesConfigurator
{
    public static IServiceCollection Initialize(this IServiceCollection services)
    {
        #region Services

        services.AddSingleton<IStudentsService, StudentsService>();

        #endregion

        #region Repositories

        services.AddSingleton<IStudentsRepository, StudentsRepository>();

        #endregion


        return services;
    }
}
