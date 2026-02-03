using ECommerce.Application.Common.Interfaces;
using ECommerce.Application.DTOs.Analytics;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Endpoints;

/// <summary>
/// Analitik endpoint'leri
/// </summary>
public static class AnalyticsEndpoints
{
    /// <summary>
    /// Analitik endpoint'lerini kaydet
    /// </summary>
    /// <param name="app">Web application</param>
    public static void MapAnalyticsEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/api/analytics")
            .WithTags("Analytics")
            .WithOpenApi()
            .RequireAuthorization();

        // Dashboard endpoints
        group.MapGet("/dashboard/summary", GetDashboardSummary)
            .WithName("GetDashboardSummary")
            .WithSummary("Dashboard özetini getir")
            .WithDescription("Genel dashboard metriklerini, KPI'ları ve grafikleri getirir");

        group.MapGet("/dashboard/kpis", GetDashboardKPIs)
            .WithName("GetDashboardKPIs")
            .WithSummary("Dashboard KPI'larını getir")
            .WithDescription("Dashboard için temel performans göstergelerini getirir");

        group.MapGet("/dashboard/charts", GetDashboardCharts)
            .WithName("GetDashboardCharts")
            .WithSummary("Dashboard grafiklerini getir")
            .WithDescription("Dashboard için grafik verilerini getirir");

        // Sales analytics endpoints
        group.MapGet("/sales", GetSalesAnalytics)
            .WithName("GetSalesAnalytics")
            .WithSummary("Satış analitiklerini getir")
            .WithDescription("Belirtilen dönem için satış analitiklerini getirir");

        group.MapGet("/sales/trend", GetSalesTrend)
            .WithName("GetSalesTrend")
            .WithSummary("Satış trend analizini getir")
            .WithDescription("Satış trend analizini ve değişim oranlarını getirir");

        group.MapGet("/sales/summary", GetSalesSummary)
            .WithName("GetSalesSummary")
            .WithSummary("Satış özetini getir")
            .WithDescription("Günlük, haftalık, aylık ve yıllık satış özetlerini getirir");

        // Customer analytics endpoints
        group.MapGet("/customers", GetCustomerAnalytics)
            .WithName("GetCustomerAnalytics")
            .WithSummary("Müşteri analitiklerini getir")
            .WithDescription("Müşteri analitiklerini ve segmentasyon bilgilerini getirir");

        group.MapGet("/customers/segments", GetCustomerSegments)
            .WithName("GetCustomerSegments")
            .WithSummary("Müşteri segmentlerini getir")
            .WithDescription("Müşteri segmentasyon analizini getirir");

        group.MapGet("/customers/geographic", GetCustomerGeographicDistribution)
            .WithName("GetCustomerGeographicDistribution")
            .WithSummary("Müşteri coğrafi dağılımını getir")
            .WithDescription("Müşterilerin coğrafi dağılım analizini getirir");

        group.MapGet("/customers/lifecycle", GetCustomerLifecycleAnalysis)
            .WithName("GetCustomerLifecycleAnalysis")
            .WithSummary("Müşteri yaşam döngüsü analizini getir")
            .WithDescription("Müşteri yaşam döngüsü aşamalarını getirir");

        group.MapGet("/customers/summary", GetCustomerAnalyticsSummary)
            .WithName("GetCustomerAnalyticsSummary")
            .WithSummary("Müşteri analitik özetini getir")
            .WithDescription("Müşteri analitikleri özet bilgilerini getirir");

        // Product analytics endpoints
        group.MapGet("/products", GetProductAnalytics)
            .WithName("GetProductAnalytics")
            .WithSummary("Ürün analitiklerini getir")
            .WithDescription("Ürün performans analitiklerini getirir");

        group.MapGet("/products/performance", GetProductPerformanceRanking)
            .WithName("GetProductPerformanceRanking")
            .WithSummary("Ürün performans sıralamasını getir")
            .WithDescription("Ürün performans sıralamasını getirir");

        group.MapGet("/products/categories", GetCategoryPerformanceAnalysis)
            .WithName("GetCategoryPerformanceAnalysis")
            .WithSummary("Kategori performans analizini getir")
            .WithDescription("Kategori bazlı performans analizini getirir");

        group.MapGet("/products/summary", GetProductAnalyticsSummary)
            .WithName("GetProductAnalyticsSummary")
            .WithSummary("Ürün analitik özetini getir")
            .WithDescription("Ürün analitikleri özet bilgilerini getirir");

        // Cache management endpoints
        group.MapDelete("/cache/clear", ClearAnalyticsCache)
            .WithName("ClearAnalyticsCache")
            .WithSummary("Analitik cache'ini temizle")
            .WithDescription("Analitik verilerinin cache'ini temizler");

        group.MapGet("/cache/statistics", GetCacheStatistics)
            .WithName("GetCacheStatistics")
            .WithSummary("Cache istatistiklerini getir")
            .WithDescription("Cache performans istatistiklerini getirir");
    }

    #region Dashboard Endpoints

    private static async Task<IResult> GetDashboardSummary(
        IAnalyticsService analyticsService,
        [FromQuery] string dashboardType = "Overview",
        [FromQuery] string period = "30days",
        CancellationToken cancellationToken = default)
    {
        var result = await analyticsService.GetDashboardSummaryAsync(cancellationToken);
        return Results.Ok(result);
    }

    private static async Task<IResult> GetDashboardKPIs(
        IAnalyticsService analyticsService,
        CancellationToken cancellationToken = default)
    {
        var result = await analyticsService.GetDashboardKPIsAsync(cancellationToken);
        return Results.Ok(result);
    }

    private static async Task<IResult> GetDashboardCharts(
        IAnalyticsService analyticsService,
        [FromQuery] string chartType = "Sales",
        [FromQuery] string period = "30days",
        CancellationToken cancellationToken = default)
    {
        var result = await analyticsService.GetDashboardChartsAsync(chartType, period, cancellationToken);
        return Results.Ok(result);
    }

    #endregion

    #region Sales Analytics Endpoints

    private static async Task<IResult> GetSalesAnalytics(
        IAnalyticsService analyticsService,
        [FromQuery] DateTime startDate,
        [FromQuery] DateTime endDate,
        [FromQuery] string analyticsType = "Daily",
        [FromQuery] Guid? categoryId = null,
        [FromQuery] Guid? productId = null,
        [FromQuery] Guid? customerId = null,
        CancellationToken cancellationToken = default)
    {
        var result = await analyticsService.GetSalesAnalyticsAsync(
            startDate,
            endDate,
            analyticsType,
            categoryId,
            productId,
            customerId,
            cancellationToken);

        return Results.Ok(result);
    }

    private static async Task<IResult> GetSalesTrend(
        IAnalyticsService analyticsService,
        [FromQuery] DateTime startDate,
        [FromQuery] DateTime endDate,
        [FromQuery] string period = "Daily",
        CancellationToken cancellationToken = default)
    {
        var result = await analyticsService.GetSalesTrendAsync(startDate, endDate, period, cancellationToken);
        return Results.Ok(result);
    }

    private static async Task<IResult> GetSalesSummary(
        IAnalyticsService analyticsService,
        CancellationToken cancellationToken = default)
    {
        var result = await analyticsService.GetSalesSummaryAsync(cancellationToken);
        return Results.Ok(result);
    }

    #endregion

    #region Customer Analytics Endpoints

    private static async Task<IResult> GetCustomerAnalytics(
        IAnalyticsService analyticsService,
        [FromQuery] Guid? customerId = null,
        [FromQuery] string? segment = null,
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 50,
        [FromQuery] string sortBy = "TotalSpent",
        [FromQuery] string sortDirection = "desc",
        CancellationToken cancellationToken = default)
    {
        var result = await analyticsService.GetCustomerAnalyticsAsync(
            customerId,
            segment,
            pageNumber,
            pageSize,
            cancellationToken);

        return Results.Ok(result);
    }

    private static async Task<IResult> GetCustomerSegments(
        IAnalyticsService analyticsService,
        CancellationToken cancellationToken = default)
    {
        var result = await analyticsService.GetCustomerSegmentsAsync(cancellationToken);
        return Results.Ok(result);
    }

    private static async Task<IResult> GetCustomerGeographicDistribution(
        IAnalyticsService analyticsService,
        CancellationToken cancellationToken = default)
    {
        var result = await analyticsService.GetCustomerGeographicDistributionAsync(cancellationToken);
        return Results.Ok(result);
    }

    private static async Task<IResult> GetCustomerLifecycleAnalysis(
        IAnalyticsService analyticsService,
        CancellationToken cancellationToken = default)
    {
        var result = await analyticsService.GetCustomerLifecycleAnalysisAsync(cancellationToken);
        return Results.Ok(result);
    }

    private static async Task<IResult> GetCustomerAnalyticsSummary(
        IAnalyticsService analyticsService,
        CancellationToken cancellationToken = default)
    {
        var result = await analyticsService.GetCustomerAnalyticsSummaryAsync(cancellationToken);
        return Results.Ok(result);
    }

    #endregion

    #region Product Analytics Endpoints

    private static async Task<IResult> GetProductAnalytics(
        IAnalyticsService analyticsService,
        [FromQuery] Guid? productId = null,
        [FromQuery] Guid? categoryId = null,
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 50,
        [FromQuery] string sortBy = "TotalSalesQuantity",
        [FromQuery] string sortDirection = "desc",
        [FromQuery] string? performanceLevel = null,
        [FromQuery] int? minPerformanceScore = null,
        CancellationToken cancellationToken = default)
    {
        var result = await analyticsService.GetProductAnalyticsAsync(
            productId,
            categoryId,
            pageNumber,
            pageSize,
            cancellationToken);

        return Results.Ok(result);
    }

    private static async Task<IResult> GetProductPerformanceRanking(
        IAnalyticsService analyticsService,
        [FromQuery] string sortBy = "SalesQuantity",
        [FromQuery] string sortDirection = "desc",
        [FromQuery] int limit = 20,
        CancellationToken cancellationToken = default)
    {
        var result = await analyticsService.GetProductPerformanceRankingAsync(sortBy, sortDirection, limit, cancellationToken);
        return Results.Ok(result);
    }

    private static async Task<IResult> GetCategoryPerformanceAnalysis(
        IAnalyticsService analyticsService,
        CancellationToken cancellationToken = default)
    {
        var result = await analyticsService.GetCategoryPerformanceAnalysisAsync(cancellationToken);
        return Results.Ok(result);
    }

    private static async Task<IResult> GetProductAnalyticsSummary(
        IAnalyticsService analyticsService,
        CancellationToken cancellationToken = default)
    {
        var result = await analyticsService.GetProductAnalyticsSummaryAsync(cancellationToken);
        return Results.Ok(result);
    }

    #endregion

    #region Cache Management Endpoints

    private static async Task<IResult> ClearAnalyticsCache(
        ICacheService cacheService,
        [FromQuery] string? pattern = null,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrEmpty(pattern))
        {
            await cacheService.ClearAsync(cancellationToken);
            return Results.Ok(new { message = "Tüm cache temizlendi" });
        }

        var removedCount = await cacheService.RemoveByPatternAsync(pattern, cancellationToken);
        return Results.Ok(new { message = $"{removedCount} anahtar silindi", pattern });
    }

    private static async Task<IResult> GetCacheStatistics(
        ICacheService cacheService,
        CancellationToken cancellationToken = default)
    {
        var result = await cacheService.GetStatisticsAsync(cancellationToken);
        return Results.Ok(result);
    }

    #endregion
}
