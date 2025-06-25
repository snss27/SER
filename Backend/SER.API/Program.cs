using DotNetEnv;
using Microsoft.AspNetCore.CookiePolicy;
using OfficeOpenXml;
using SER.API.Infrastructure;
using SER.Database;
using SER.Services.Configurator;
using SER.Startup;
using SER.Tools.Binders;

Env.TraversePath().Load();
ExcelPackage.License.SetNonCommercialOrganization("College-Colomna");

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

IServiceCollection services = builder.Services;
services.AddSERDbContext(builder.Configuration);
services.Initialize();
services.AddHttps();
services.AddResponseCompressionProviders();
services.AddControllers(options =>
{
	options.ModelBinderProviders.Insert(0, new IDModelBinderProvider());
}).AddJson();
services.AddApiAuthentication();

WebApplication app = builder.Build();
app.UseRequestBuffering();
app.UseHttps();
app.UseResponseCompression();
app.UseRouting();
String domain = Environment.GetEnvironmentVariable("API_DOMAIN") ?? throw new ArgumentNullException(nameof(domain));
app.UseCors(domain);

app.UseCookiePolicy(new CookiePolicyOptions
{
	MinimumSameSitePolicy = SameSiteMode.Lax,
	HttpOnly = HttpOnlyPolicy.None,
	Secure = CookieSecurePolicy.None
});

app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<ApiMiddleware>();
app.UseDefaultEndpoints();

app.Run();
