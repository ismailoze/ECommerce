using ECommerce.Application.Common.Mappings;
using ECommerce.Application.Common.Extensions;
using ECommerce.Application.Common.Interfaces;
using ECommerce.Application.Common.Messaging;
using ECommerce.Application.Common.Behaviors;
using ECommerce.Application.Common.Decorators;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace ECommerce.Application;

/// <summary>
/// Application katmanı için dependency injection yapılandırması
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Application servislerini ekle
    /// </summary>
    /// <param name="services">Service collection</param>
    /// <returns>Service collection</returns>
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        // AutoMapper ekle
        services.AddAutoMapper(cfg => {
            cfg.AddProfile<MappingProfile>();
        });

        // FluentValidation ekle
        services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly, includeInternalTypes: true);

        // Cache servisini ekle
        services.AddMemoryCache();
        // ICacheService implementasyonu Infrastructure katmanında olacak

        // Analitik servislerini ekle (Infrastructure katmanında kayıt edilecek)

        // Scrutor ile otomatik handler kayıtları
        services.Scan(scan => scan
            .FromAssembliesOf(typeof(DependencyInjection))
            .AddClasses(classes => classes.AssignableTo(typeof(IQueryHandler<,>)), publicOnly: false)
            .AsImplementedInterfaces()
            .WithScopedLifetime());

        services.Scan(scan => scan
            .FromAssembliesOf(typeof(DependencyInjection))
            .AddClasses(classes => classes.AssignableTo(typeof(ICommandHandler<>)), publicOnly: false)
                .AsImplementedInterfaces()
            .AddClasses(classes => classes.AssignableTo(typeof(ICommandHandler<,>)), publicOnly: false)
                .AsImplementedInterfaces()
            .WithScopedLifetime());

        // Validation decorators
        services.Decorate(typeof(ICommandHandler<,>), typeof(ValidationDecorator.CommandHandler<,>));

        // Performance decorators
        services.Decorate(typeof(IQueryHandler<,>), typeof(PerformanceDecorator.QueryHandler<,>));
        services.Decorate(typeof(ICommandHandler<,>), typeof(PerformanceDecorator.CommandHandler<,>));

        // Logging decorators
        services.Decorate(typeof(IQueryHandler<,>), typeof(LoggingDecorator.QueryHandler<,>));
        services.Decorate(typeof(ICommandHandler<,>), typeof(LoggingDecorator.CommandHandler<,>));

        // Caching decorator (only for queries)
        services.Decorate(typeof(IQueryHandler<,>), typeof(CachingDecorator.QueryHandler<,>));

        // Retry decorators
        services.Decorate(typeof(ICommandHandler<,>), typeof(RetryDecorator.CommandHandler<,>));
        services.Decorate(typeof(IQueryHandler<,>), typeof(RetryDecorator.QueryHandler<,>));

        return services;
    }
}
