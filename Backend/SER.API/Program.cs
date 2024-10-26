using Microsoft.AspNetCore.ResponseCompression;
using PMS.Domain;
using SER.Configurator.Extensions;
using SER.Tools.Binders;
using SER.Tools.Json;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Host.ConfigureWeb((context, serviceCollection) => { });

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

WebApplication application = builder.Build();

if (application.Environment.IsDevelopment())
    application.UseDeveloperExceptionPage();

application
    .UseResponseCompression()
    .UseHttpsRedirection()
    .UseRouting()
    .UseEndpoints(endpoints =>
    {
        endpoints.MapControllers();
    });

application.Run();
