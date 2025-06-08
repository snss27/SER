using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using PMS.Domain;
using SER.Domain.JwtTokens;
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
				.SetIsOriginAllowed(origin => true)
				.AllowAnyMethod()
				.AllowAnyHeader()
				.AllowCredentials()
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
				.WithMethods("OPTIONS", "GET", "POST", "DELETE")
				.AllowAnyHeader()
				.AllowCredentials()
			);
		}
	}

	public static void UseDefaultEndpoints(this WebApplication app)
	{
		app.UseEndpoints(endpoints =>
		endpoints.MapControllers());
	}

	public static void AddApiAuthentication(this IServiceCollection services)
	{
		String jwtKey = Environment.GetEnvironmentVariable("JWT_KEY") ?? throw new Exception("JWT_KEY does not set");
		String cookiesJwtTokenKey = Environment.GetEnvironmentVariable("COOCKIES_JWT_TOKEN_KEY") ?? throw new Exception("COOCKIES_JWT_TOKEN_KEY does not set");

		services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
			.AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
			{
				options.TokenValidationParameters = new()
				{
					ValidateIssuer = false,
					ValidateAudience = false,
					ValidateLifetime = true,
					ValidateIssuerSigningKey = true,
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
				};

				options.Events = new JwtBearerEvents
				{
					OnMessageReceived = context =>
					{
						context.Token = context.Request.Cookies[cookiesJwtTokenKey];

						return Task.CompletedTask;
					}
				};
			});
	}
}
