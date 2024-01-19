using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SportBet.Application.Authentication.User;
using SportBet.Application.Behaviours;
using System.Reflection;
using System.Text;

namespace SportBet.Application;
public static class ApplicationExtension
{
    public static IServiceCollection AddApplication(this IServiceCollection services, ConfigurationManager configuration)
    {
        var assembly = Assembly.GetExecutingAssembly();

        services.AddValidatorsFromAssembly(assembly);
        services.AddMediatR(config => config.RegisterServicesFromAssemblies(assembly));

        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IAuthenticationService, AuthenticationService>();
        services.AddScoped<IUserContext, UserContext>();

        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(CachingBehavior<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(PreformanceBehavior<,>));

        services.AddJwtAuthentication(configuration);

        services.AddRoles();

        return services;
    }

    private static IServiceCollection AddJwtAuthentication(this IServiceCollection services, ConfigurationManager configuration)
    {
        var jwtSettings = configuration.GetSection(JwtSettings.SectionName).Get<JwtSettings>() ?? new JwtSettings();
        services.AddSingleton(Options.Create(jwtSettings));

        services.AddAuthorization();

        services.AddAuthentication(o =>
        {
            o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(o =>
        {
            o.RequireHttpsMetadata = false;
            o.SaveToken = true;
            o.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtSettings.Issuer,
                ValidAudience = jwtSettings.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret)),
                ClockSkew = TimeSpan.Zero,
                RequireExpirationTime = true
            };
        });

        return services;
    }

    private static IServiceCollection AddRoles(this IServiceCollection services)
    {

        services.AddAuthorization(options =>
        {
            options.AddPolicy("AdminPolicy", policy =>
                policy.RequireRole("Admin"));

            options.AddPolicy("RegularUserPolicy", policy =>
                policy.RequireRole("User"));

            options.AddPolicy("CanReadPolicy", policy =>
                policy.RequireClaim(CustomClaimTypes.Scope, Scopes.CanRead));

            options.AddPolicy("CanWritePolicy", policy =>
                policy.RequireClaim(CustomClaimTypes.Scope, Scopes.CanWrite));

            options.AddPolicy("CanDeletePolicy", policy =>
                policy.RequireClaim(CustomClaimTypes.Scope, Scopes.CanDelete));
        });

        return services;
    }
}