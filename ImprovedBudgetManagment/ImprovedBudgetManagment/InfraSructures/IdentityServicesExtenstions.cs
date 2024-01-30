using ImprovedBudgetManagment.Data;
using ImprovedBudgetManagment.Models.Entitites;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ImprovedBudgetManagment.InfraSructures;

public static class IdentityServicesExtenstions
{
    public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("Database")));

        services.AddIdentity<User, IdentityRole>()
            .AddDefaultTokenProviders()
            .AddEntityFrameworkStores<AppDbContext>();
        return services;
    }
}