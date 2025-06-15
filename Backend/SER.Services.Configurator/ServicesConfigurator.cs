using Microsoft.Extensions.DependencyInjection;
using SER.Database.Models.ConfigurationTools;
using SER.Domain.JwtTokens;
using SER.Domain.Services;
using SER.Services.AdditionalQualifications;
using SER.Services.Clusters;
using SER.Services.EducationLevels;
using SER.Services.Employees;
using SER.Services.Enterprises;
using SER.Services.Groups;
using SER.Services.Reports;
using SER.Services.Students;
using SER.Services.Users;

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
		services.AddScoped<IUsersService, UsersService>();
		services.AddScoped<IReportsService, ReportsService>();
		services.AddScoped<JwtProvider>();

        #endregion

		return services;
    }
}
