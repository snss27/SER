using DotNetEnv;
using Microsoft.Extensions.FileProviders;
using SER.Startup;

Env.TraversePath().Load();

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

IServiceCollection services = builder.Services;

services
	.AddControllers()
	.AddJson();

services
	.AddHttps()
	.AddResponseCompressionProviders();

WebApplication app = builder.Build();

app.UseHttps();
app.UseRequestBuffering();
app.UseRouting();

String domain = Environment.GetEnvironmentVariable("FILE_STORAGE_DOMAIN") ?? throw new ArgumentNullException(nameof(domain));
app.UseCors(domain);
app.UseResponseCompression();

PhysicalFileProvider fileProvider = new (Path.Combine(builder.Environment.ContentRootPath, "Files"));
app.UseStaticFiles(new StaticFileOptions { FileProvider = fileProvider });

app.UseDefaultEndpoints();

app.Run();
