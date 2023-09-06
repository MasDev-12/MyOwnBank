using MyOwnBank.Features.Users.Options;
using MyOwnBank.Features.Users.Services;

namespace MyOwnBank.Features.Users.Registrations;

public static class UserBuilderExtensions
{
    public static WebApplicationBuilder AddUsers(this WebApplicationBuilder webApplicationBuilder)
    {
        webApplicationBuilder.Services.Configure<UsersOptions>(webApplicationBuilder.Configuration.GetSection("Features:Users"));
        webApplicationBuilder.Services.Configure<Argon2Options>(webApplicationBuilder.Configuration.GetSection("Argon2Options"));
        webApplicationBuilder.Services.AddTransient<PasswordHashingService>();
        return webApplicationBuilder;
    }
}
