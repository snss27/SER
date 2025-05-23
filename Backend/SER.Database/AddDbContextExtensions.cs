using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace SER.Database;
public static class AddDbContextExtensions
{
	public static IServiceCollection AddSERDbContext(this IServiceCollection services, ConfigurationManager configuration)
	{
		return services.AddDbContext<SERDbContext>(options =>
		{
			options.UseNpgsql(configuration.GetConnectionString(nameof(SERDbContext)));
		});
	}
}
