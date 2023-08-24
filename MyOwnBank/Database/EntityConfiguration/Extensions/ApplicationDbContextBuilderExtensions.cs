using Microsoft.EntityFrameworkCore;

namespace MyOwnBank.Database.EntityConfiguration.Extensions;

public static class ApplicationDbContextBuilderExtensions
{
    public static WebApplicationBuilder AddDbContext(this WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<ApplicationDbContext>
        (options =>
         options.UseNpgsql(builder.Configuration.GetConnectionString("ConnectionStringsForWorking"))
         , contextLifetime: ServiceLifetime.Scoped
         , optionsLifetime: ServiceLifetime.Singleton
        );

        builder.Services.AddDbContextFactory<ApplicationDbContext>(
            (provider, options) => options.UseNpgsql(builder.Configuration.GetConnectionString("ConnectionStringsForWorking")));

        return builder;
    }
}
