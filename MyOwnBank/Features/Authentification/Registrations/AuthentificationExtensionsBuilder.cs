using MyOwnBank.Features.Authentification.Options;

namespace MyOwnBank.Features.Authentification.Registrations;

public static class AuthentificationExtensionsBuilder
{
    public static WebApplicationBuilder AddAuthentification(this WebApplicationBuilder webApplicationBuilder)
    {
        webApplicationBuilder.Services.Configure<RefreshTokenOptions>(webApplicationBuilder.Configuration.GetSection("Features:Authentification:RefreshTokenOptions"));
        webApplicationBuilder.Services.Configure<AuthTokenOptions>(webApplicationBuilder.Configuration.GetSection("Features:Authentification:Jwt"));
        return webApplicationBuilder;
    }
}
