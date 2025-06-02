using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PMS.Domain;
using SER.Tools.Json;

namespace SER.Startup;

public static class Startup
{
	public static IMvcBuilder AddJson(this IMvcBuilder builder, Action<MvcNewtonsoftJsonOptions>? action = null)
	{
		builder.AddJsonOptions(options =>
		{
			options.JsonSerializerOptions
				.AddJsonSettings()
				.ApplyToolsConverters()
				.ApplyAnyTypeConverters(DomainAssembly.Itself);
		});

		return builder;
	}

	public static IServiceCollection AddHttps(this IServiceCollection services)
	{
		services.AddHsts(options =>
		{
			options.Preload = true;
			options.IncludeSubDomains = true;
			options.MaxAge = TimeSpan.FromDays(60);
		});

		services.AddHttpsRedirection(options =>
		{
			options.HttpsPort = 443;
			options.RedirectStatusCode = StatusCodes.Status301MovedPermanently;
		});

		return services;
	}

	public static IServiceCollection AddResponseCompressionProviders(this IServiceCollection services, String[]? mimeTypes = null)
	{
		services.AddResponseCompression(options =>
		{
			options.EnableForHttps = true;
			options.Providers.Add<BrotliCompressionProvider>();
			options.Providers.Add<GzipCompressionProvider>();

			if (mimeTypes is not null)
				options.MimeTypes = mimeTypes;
		});

		return services;
	}

	public static void UseRequestBuffering(this WebApplication app)
	{
		app.Use((context, next) =>
		{
			context.Request.EnableBuffering();
			return next();
		});
	}

	public static void UseHttps(this WebApplication app)
	{
		if (app.Environment.IsDevelopment()) return;

		app.UseHttpsRedirection();
		app.UseHsts();
		app.UseMiddleware<SecurityHeadersMiddleware>();
		app.UseRewriter(new RewriteOptions()
			.AddRedirectToHttps()
			.AddRedirectToNonWww()
		);
	}

	public static void UseCors(this WebApplication app, String domain, Boolean isDebugMode = false)
	{
		if (isDebugMode || app.Environment.IsDevelopment())
		{
			app.UseCors(config => config
				.AllowAnyOrigin()
				.WithMethods("OPTIONS", "GET", "POST", "DELETE")
				.AllowAnyHeader()
			);
		}
		else
		{
			app.UseCors(config => config
				.SetIsOriginAllowed(origin =>
					origin == $"https://{domain}" ||
					origin == $"http://{domain}" ||
					origin.EndsWith($".{domain}") ||
					origin == "https://yandexwebcache.net"
				)
				.WithMethods("OPTIONS", "GET", "POST")
				.AllowAnyHeader()
			);
		}
	}

	public static void UseDefaultEndpoints(this WebApplication app)
	{
		app.UseEndpoints(endpoints =>
		endpoints.MapControllers());
	}
}
