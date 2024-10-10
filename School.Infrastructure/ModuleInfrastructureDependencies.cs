using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using School.Data.Entities.Identity;
using School.Infrastructure.Abstracts;
using School.Infrastructure.Base;
using School.Infrastructure.Data;
using School.Infrastructure.Repository;

namespace School.Infrastructure;
public static class ModuleInfrastructureDependencies
{
    public static IServiceCollection AddInfrastructureDependencies(this IServiceCollection services)
    {
        // Configuration of Custome Repository
        services.AddTransient<IStudentRepository, StudentRepository>();
        services.AddTransient<IDepartmentRepository, DepartmentRepository>();

        // Configuration of Generic Repository
        services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));

        // Configruation of ApplicationUser
        services.AddIdentity<ApplicationUser, IdentityRole>(options =>
        {
            options.SignIn.RequireConfirmedEmail = true;
        }).AddEntityFrameworkStores<ApplicationDbContext>()
        .AddDefaultTokenProviders();

        return services;
    }
}
