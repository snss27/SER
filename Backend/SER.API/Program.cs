using DotNetEnv;
using SER.Database;
using SER.Services.Configurator;
using SER.Startup;
using SER.Tools.Binders;

Env.TraversePath().Load();

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

WebApplication app = builder.Build();
app.UseRequestBuffering();
app.UseHttps();
app.UseResponseCompression();
app.UseRouting();
String domain = Environment.GetEnvironmentVariable("API_DOMAIN") ?? throw new ArgumentNullException(nameof(domain));
app.UseCors(domain);
app.UseDefaultEndpoints();

app.Run();
