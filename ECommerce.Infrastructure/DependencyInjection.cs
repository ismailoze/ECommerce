using ECommerce.Domain.Interfaces;
using ECommerce.Infrastructure.Data;
using ECommerce.Infrastructure.Repositories;
using ECommerce.Infrastructure.Services;
using ECommerce.Infrastructure.Services.Search;
using ECommerce.Infrastructure.Configuration;
using ECommerce.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerce.Infrastructure;

/// <summary>
/// Infrastructure katmanı için dependency injection yapılandırması
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Infrastructure servislerini ekle
    /// </summary>
    /// <param name="services">Service collection</param>
    /// <param name="configuration">Configuration</param>
    /// <returns>Service collection</returns>
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        // Entity Framework Core ekle
        services.AddDbContext<ECommerceDbContext>(options =>
        {
            options.UseSqlServer(
                configuration.GetConnectionString("DefaultConnection"),
                b => b.MigrationsAssembly(typeof(ECommerceDbContext).Assembly.FullName));
        });

        // Repository'leri ekle
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IPermissionRepository, PermissionRepository>();
        services.AddScoped<IRolePermissionRepository, RolePermissionRepository>();

        // JWT Service'i ekle
        services.AddScoped<IJwtService, JwtService>();

        // Password Service'i ekle
        services.AddScoped<IPasswordService, PasswordService>();

        // File Upload Service'i ekle
        services.AddScoped<IFileUploadService, FileUploadService>();

        // Cache Service'i ekle
        services.AddMemoryCache();
        services.AddScoped<ICacheService, Services.Cache.MemoryCacheService>();

        // Search Service'i ekle
        services.AddScoped<ISearchService, SearchService>();

        // Notification Service'i ekle
        services.AddScoped<INotificationService, NotificationService>();

        // Cargo Services'i ekle
        services.AddScoped<ICargoService, Services.Cargo.CargoService>();
        services.AddScoped<ICargoCompanyService, Services.Cargo.CargoCompanyService>();
        services.AddScoped<ICargoTrackingService, Services.Cargo.CargoTrackingService>();

        // Seed Data Service'i ekle
        services.AddHostedService<SeedDataService>();

        // Favori liste servislerini ekle
        services.AddWishlistServices(configuration);

        // Analitik servislerini ekle
        services.AddScoped<ECommerce.Application.Common.Interfaces.IAnalyticsService, Services.Analytics.BasicAnalyticsService>();

        return services;
    }
}
