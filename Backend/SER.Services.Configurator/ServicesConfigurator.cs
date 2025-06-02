using Microsoft.Extensions.DependencyInjection;
using SER.Domain.Services;
using SER.Services.AdditionalQualifications;
using SER.Services.Clusters;
using SER.Services.EducationLevels;
using SER.Services.Employees;
using SER.Services.Enterprises;
using SER.Services.Groups;
using SER.Services.Students;

namespace SER.Services.Configurator;
public static class ServicesConfigurator
{
    public static IServiceCollection Initialize(this IServiceCollection services)
    {
        #region Services

        services.AddScoped<IStudentsService, StudentsService>();
		services.AddScoped<IEducationLevelsService, EducationLevelsService>();
		services.AddScoped<IGroupsService, GroupsService>();
		services.AddScoped<IEmployeesService, EmployeesService>();
		services.AddScoped<IAdditionalQualificationsService, AdditionalQualificationsService>();
		services.AddScoped<IEnterprisesService, EnterprisesService>();
		services.AddScoped<IClustersService, ClustersService>();

        #endregion

		return services;
    }
}
