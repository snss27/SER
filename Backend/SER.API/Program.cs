using DotNetEnv;
using SER.API.Infrastructure;
using SER.Database;
using SER.Services.Configurator;
using SER.Startup;
using SER.Tools.Binders;
using Microsoft.AspNetCore.CookiePolicy;
using OfficeOpenXml;

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
	MinimumSameSitePolicy = SameSiteMode.None,
	HttpOnly = HttpOnlyPolicy.Always,
	Secure = CookieSecurePolicy.Always
});

app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<ApiMiddleware>();
app.UseDefaultEndpoints();

app.Run();
