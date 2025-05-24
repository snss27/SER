using Microsoft.Extensions.DependencyInjection;
using SER.Domain.Services;
using SER.Services.AdditionalQualifications;
using SER.Services.Clusters;
using SER.Services.EducationLevels;
using SER.Services.EducationLevels.Repositories;
using SER.Services.Employees;
using SER.Services.Employees.Repositories;
using SER.Services.Enterprises;
using SER.Services.Enterprises.Repositories;
using SER.Services.Files;
using SER.Services.Groups;
using SER.Services.Groups.Repositories;
using SER.Services.Students;
using SER.Services.Students.Repositories;
using SER.Services.WorkPlaces;
using SER.Services.WorkPlaces.Repositories;

namespace SER.Services.Configurator;
public static class ServicesConfigurator
{
    public static IServiceCollection Initialize(this IServiceCollection services)
    {
        #region Services

        services.AddSingleton<IStudentsService, StudentsService>();
		services.AddSingleton<IEducationLevelsService, EducationLevelsService>();
		services.AddSingleton<IGroupsService, GroupsService>();
		services.AddSingleton<IEmployeesService, EmployeesService>();
		services.AddSingleton<IAdditionalQualificationsService, AdditionalQualificationsService>();
		services.AddSingleton<IEnterprisesService, EnterprisesService>();
		services.AddSingleton<IClustersService, ClustersService>();
		services.AddSingleton<IFilesService, FilesService>();
		services.AddSingleton<IWorkPlacesSevice, WorkPlacesService>();

        #endregion

        #region Repositories

        services.AddSingleton<IStudentsRepository, StudentsRepository>();
		services.AddSingleton<IEducationLevelsRepository, EducationLevelsRepository>();
		services.AddSingleton<IGroupsRepository, GroupsRepository>();
		services.AddSingleton<IEmployeesRepository, EmployeesRepository>();
		services.AddSingleton<IEnterprisesRepository, EnterprisesRepository>();
		services.AddSingleton<IWorkPlacesRepository, WorkPlacesRepository>();

		#endregion


		return services;
    }
}
