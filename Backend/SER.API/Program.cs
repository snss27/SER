using Microsoft.AspNetCore.ResponseCompression;
using PMS.Domain;
using SER.Configurator.Extensions;
using SER.Tools.Binders;
using SER.Tools.Json;
using SER.Services.Configurator;
using Microsoft.AspNetCore.Http.Features;
using static System.Int32;
using DotNetEnv;
using SER.Database;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

Env.Load();

builder.Host.ConfigureWeb((context, serviceCollection) => 
{
    serviceCollection.Initialize();
});

builder.Services.Configure<FormOptions>(options =>
{
	options.ValueLengthLimit = MaxValue;
	options.BufferBodyLengthLimit = MaxValue;
	options.KeyLengthLimit = MaxValue;
	options.MultipartBodyLengthLimit = Int64.MaxValue;
});

builder.Services.AddSERDbContext(builder.Configuration);

builder.Services.AddResponseCompression(options =>
{
    options.EnableForHttps = true;
    options.Providers.Add<GzipCompressionProvider>();
    options.Providers.Add<BrotliCompressionProvider>();
});

builder.Services.AddControllers(mvcOptions =>
    {
        mvcOptions.ModelBinderProviders.Insert(0, new IDModelBinderProvider());
    })
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions
            .AddJsonSettings()
            .ApplyToolsConverters()
            .ApplyAnyTypeConverters(DomainAssembly.Itself);
    });

builder.Services.AddCors(options =>
{
	options.AddDefaultPolicy(builder =>
	{
		builder
			.AllowAnyOrigin()
			.AllowAnyMethod()
			.AllowAnyHeader();
	});
});

WebApplication application = builder.Build();

if (application.Environment.IsDevelopment())
    application.UseDeveloperExceptionPage();

application
    .UseResponseCompression()
    .UseHttpsRedirection()
	.UseCors()
	.UseRouting()
	.UseStaticFiles()
    .UseEndpoints(endpoints =>
    {
        endpoints.MapControllers();
    });

application.Run();
