using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.OpenApi.Models;
using Serilog;
using SportBet.Api.Common;
using SportBet.Api.Mapping;
using SportBet.Application.Caching;
using Swashbuckle.AspNetCore.SwaggerGen;


namespace SportBet.Api;

public static class PresentationExtension
{
    public static IServiceCollection AddPresentation(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.AddDistributedMemoryCache();
        services.Configure<CacheSettings>(configuration.GetSection(CacheSettings.SectionName));

        services.AddControllers();

        services.AddSingleton<ProblemDetailsFactory, ApiProblemDetailsFactory>();
        services.AddHttpContextAccessor();

        services.AddSwagger();
        services.AddSerilog(configuration);
        services.AddMapping();

        return services;
    }

    public static IServiceCollection AddSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new() { Title = "Api", Version = "v1" });
            c.AddSecurityDefinition("bearer", new OpenApiSecurityScheme
            {
                Description = @"JWT Authorization using the Bearer scheme.",
                Type = SecuritySchemeType.Http,
                BearerFormat = "jwt",
                In = ParameterLocation.Header,
                Name = JwtBearerDefaults.AuthenticationScheme,
                Scheme = JwtBearerDefaults.AuthenticationScheme
            });
            c.OperationFilter<AuthorizeCheckOperationFilter>();
        });
        return services;
    }

    public static IServiceCollection AddSerilog(this IServiceCollection services, IConfiguration configuration)
    {
        var logger = new LoggerConfiguration().ReadFrom.Configuration(configuration).Enrich.FromLogContext().CreateLogger();
        services.AddLogging(loggingBuilder =>
        {
            loggingBuilder.ClearProviders();
            loggingBuilder.AddSerilog(logger);
        });
        return services;
    }
}

internal class AuthorizeCheckOperationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        var hasAuthorize = context.MethodInfo.DeclaringType != null && (context.MethodInfo.DeclaringType.GetCustomAttributes(true).OfType<AuthorizeAttribute>().Any() || context.MethodInfo.GetCustomAttributes(true).OfType<AuthorizeAttribute>().Any());

        if (hasAuthorize)
        {
            operation.Responses.Add("401", new OpenApiResponse { Description = "Unauthorized" });
            operation.Responses.Add("403", new OpenApiResponse { Description = "Forbidden" });

            operation.Security = new List<OpenApiSecurityRequirement>
            {
                new ()
                {
                    [
                        new OpenApiSecurityScheme { Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "bearer" } }
                    ] = new[] { "Bearer" }
                }
            };
        }
    }
}