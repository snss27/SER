using Microsoft.Extensions.DependencyInjection;
using SER.Domain.Services;
using SER.Services.Specialities;
using SER.Services.Specialities.Repositories;
using SER.Services.Students;
using SER.Services.Students.Repositories;

namespace SER.Services.Configurator;
public static class ServicesConfigurator
{
    public static IServiceCollection Initialize(this IServiceCollection services)
    {
        #region Services

        services.AddSingleton<IStudentsService, StudentsService>();
		services.AddSingleton<ISpecialitiesService, SpecialitiesService>();

        #endregion

        #region Repositories

        services.AddSingleton<IStudentsRepository, StudentsRepository>();
		services.AddSingleton<ISpecialitiesRepository, SpecialitiesRepository>();

        #endregion


        return services;
    }
}
