using Microsoft.Extensions.DependencyInjection;
using SER.Domain.Services;
using SER.Services.AdditionalQualifications;
using SER.Services.AdditionalQualifications.Repositories;
using SER.Services.Curators;
using SER.Services.Curators.Repositories;
using SER.Services.Groups;
using SER.Services.Groups.Repositories;
using SER.Services.Specialities;
using SER.Services.Specialities.Repositories;
using SER.Services.Students;
using SER.Services.Students.Repositories;
using SER.Services.WorkPosts;
using SER.Services.WorkPosts.Repositories;

namespace SER.Services.Configurator;
public static class ServicesConfigurator
{
    public static IServiceCollection Initialize(this IServiceCollection services)
    {
        #region Services

        services.AddSingleton<IStudentsService, StudentsService>();
		services.AddSingleton<ISpecialitiesService, SpecialitiesService>();
		services.AddSingleton<IGroupsService, GroupsService>();
		services.AddSingleton<ICuratorsService, CuratorsService>();
		services.AddSingleton<IAdditionalQualificationsService, AdditionalQualificationsService>();
		services.AddSingleton<IWorkPostsService, WorkPostsService>();

        #endregion

        #region Repositories

        services.AddSingleton<IStudentsRepository, StudentsRepository>();
		services.AddSingleton<ISpecialitiesRepository, SpecialitiesRepository>();
		services.AddSingleton<IGroupsRepository, GroupsRepository>();
		services.AddSingleton<ICuratorsRepository, CuratorsRepository>();
		services.AddSingleton<IAdditionalQualificationsRepository, AdditionalQualificationsRepository>();
		services.AddSingleton<IWorkPostsRepository, WorkPostsRepository>();

		#endregion


		return services;
    }
}
