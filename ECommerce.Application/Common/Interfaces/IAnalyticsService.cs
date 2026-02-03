using ECommerce.Application.DTOs.Analytics;

namespace ECommerce.Application.Common.Interfaces;

/// <summary>
/// Analitik servisi interface'i
/// </summary>
public interface IAnalyticsService
{
    #region Satış Analitikleri

    /// <summary>
    /// Satış analitiklerini getir
    /// </summary>
    /// <param name="startDate">Başlangıç tarihi</param>
    /// <param name="endDate">Bitiş tarihi</param>
    /// <param name="analyticsType">Analitik türü</param>
    /// <param name="categoryId">Kategori ID'si (opsiyonel)</param>
    /// <param name="productId">Ürün ID'si (opsiyonel)</param>
    /// <param name="customerId">Müşteri ID'si (opsiyonel)</param>
    /// <param name="cancellationToken">İptal token'ı</param>
    /// <returns>Satış analitikleri</returns>
    Task<List<SalesAnalyticsDto>> GetSalesAnalyticsAsync(
        DateTime startDate, 
        DateTime endDate, 
        string analyticsType = "Daily",
        Guid? categoryId = null,
        Guid? productId = null,
        Guid? customerId = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Satış trend analizini getir
    /// </summary>
    /// <param name="startDate">Başlangıç tarihi</param>
    /// <param name="endDate">Bitiş tarihi</param>
    /// <param name="period">Dönem (Daily, Weekly, Monthly)</param>
    /// <param name="cancellationToken">İptal token'ı</param>
    /// <returns>Satış trend analizi</returns>
    Task<List<SalesTrendDto>> GetSalesTrendAsync(
        DateTime startDate, 
        DateTime endDate, 
        string period = "Daily",
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Satış özetini getir
    /// </summary>
    /// <param name="cancellationToken">İptal token'ı</param>
    /// <returns>Satış özeti</returns>
    Task<SalesSummaryDto> GetSalesSummaryAsync(CancellationToken cancellationToken = default);

    #endregion

    #region Müşteri Analitikleri

    /// <summary>
    /// Müşteri analitiklerini getir
    /// </summary>
    /// <param name="customerId">Müşteri ID'si (opsiyonel)</param>
    /// <param name="segment">Müşteri segmenti (opsiyonel)</param>
    /// <param name="pageNumber">Sayfa numarası</param>
    /// <param name="pageSize">Sayfa boyutu</param>
    /// <param name="cancellationToken">İptal token'ı</param>
    /// <returns>Müşteri analitikleri</returns>
    Task<List<CustomerAnalyticsDto>> GetCustomerAnalyticsAsync(
        Guid? customerId = null,
        string? segment = null,
        int pageNumber = 1,
        int pageSize = 50,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Müşteri segmentlerini getir
    /// </summary>
    /// <param name="cancellationToken">İptal token'ı</param>
    /// <returns>Müşteri segmentleri</returns>
    Task<List<CustomerSegmentDto>> GetCustomerSegmentsAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Müşteri coğrafi dağılımını getir
    /// </summary>
    /// <param name="cancellationToken">İptal token'ı</param>
    /// <returns>Coğrafi dağılım</returns>
    Task<List<CustomerGeographicDto>> GetCustomerGeographicDistributionAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Müşteri yaşam döngüsü analizini getir
    /// </summary>
    /// <param name="cancellationToken">İptal token'ı</param>
    /// <returns>Yaşam döngüsü analizi</returns>
    Task<List<CustomerLifecycleDto>> GetCustomerLifecycleAnalysisAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Müşteri analitik özetini getir
    /// </summary>
    /// <param name="cancellationToken">İptal token'ı</param>
    /// <returns>Müşteri analitik özeti</returns>
    Task<CustomerAnalyticsSummaryDto> GetCustomerAnalyticsSummaryAsync(CancellationToken cancellationToken = default);

    #endregion

    #region Ürün Analitikleri

    /// <summary>
    /// Ürün analitiklerini getir
    /// </summary>
    /// <param name="productId">Ürün ID'si (opsiyonel)</param>
    /// <param name="categoryId">Kategori ID'si (opsiyonel)</param>
    /// <param name="pageNumber">Sayfa numarası</param>
    /// <param name="pageSize">Sayfa boyutu</param>
    /// <param name="cancellationToken">İptal token'ı</param>
    /// <returns>Ürün analitikleri</returns>
    Task<List<ProductAnalyticsDto>> GetProductAnalyticsAsync(
        Guid? productId = null,
        Guid? categoryId = null,
        int pageNumber = 1,
        int pageSize = 50,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Ürün performans sıralamasını getir
    /// </summary>
    /// <param name="sortBy">Sıralama kriteri</param>
    /// <param name="sortDirection">Sıralama yönü</param>
    /// <param name="limit">Limit</param>
    /// <param name="cancellationToken">İptal token'ı</param>
    /// <returns>Ürün performans sıralaması</returns>
    Task<List<ProductPerformanceDto>> GetProductPerformanceRankingAsync(
        string sortBy = "SalesQuantity",
        string sortDirection = "desc",
        int limit = 20,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Kategori performans analizini getir
    /// </summary>
    /// <param name="cancellationToken">İptal token'ı</param>
    /// <returns>Kategori performans analizi</returns>
    Task<List<CategoryPerformanceDto>> GetCategoryPerformanceAnalysisAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Ürün analitik özetini getir
    /// </summary>
    /// <param name="cancellationToken">İptal token'ı</param>
    /// <returns>Ürün analitik özeti</returns>
    Task<ProductAnalyticsSummaryDto> GetProductAnalyticsSummaryAsync(CancellationToken cancellationToken = default);

    #endregion

    #region Dashboard Metrikleri

    /// <summary>
    /// Dashboard metriklerini getir
    /// </summary>
    /// <param name="date">Tarih</param>
    /// <param name="metricType">Metrik türü</param>
    /// <param name="cancellationToken">İptal token'ı</param>
    /// <returns>Dashboard metrikleri</returns>
    Task<DashboardMetricsDto> GetDashboardMetricsAsync(
        DateTime date, 
        string metricType = "Daily",
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Dashboard KPI'larını getir
    /// </summary>
    /// <param name="cancellationToken">İptal token'ı</param>
    /// <returns>Dashboard KPI'ları</returns>
    Task<List<DashboardKPIDto>> GetDashboardKPIsAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Dashboard grafiklerini getir
    /// </summary>
    /// <param name="chartType">Grafik türü</param>
    /// <param name="period">Dönem</param>
    /// <param name="cancellationToken">İptal token'ı</param>
    /// <returns>Dashboard grafikleri</returns>
    Task<List<DashboardChartDto>> GetDashboardChartsAsync(
        string chartType = "Sales",
        string period = "30days",
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Dashboard özetini getir
    /// </summary>
    /// <param name="cancellationToken">İptal token'ı</param>
    /// <returns>Dashboard özeti</returns>
    Task<DashboardSummaryDto> GetDashboardSummaryAsync(CancellationToken cancellationToken = default);

    #endregion

    #region Analitik Hesaplama

    /// <summary>
    /// Analitik verilerini hesapla ve kaydet
    /// </summary>
    /// <param name="date">Tarih</param>
    /// <param name="analyticsType">Analitik türü</param>
    /// <param name="cancellationToken">İptal token'ı</param>
    /// <returns>İşlem sonucu</returns>
    Task<bool> CalculateAndSaveAnalyticsAsync(
        DateTime date, 
        string analyticsType = "Daily",
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Tüm analitik verilerini yeniden hesapla
    /// </summary>
    /// <param name="startDate">Başlangıç tarihi</param>
    /// <param name="endDate">Bitiş tarihi</param>
    /// <param name="cancellationToken">İptal token'ı</param>
    /// <returns>İşlem sonucu</returns>
    Task<bool> RecalculateAllAnalyticsAsync(
        DateTime startDate, 
        DateTime endDate,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Analitik verilerini temizle
    /// </summary>
    /// <param name="beforeDate">Bu tarihten önceki verileri temizle</param>
    /// <param name="cancellationToken">İptal token'ı</param>
    /// <returns>İşlem sonucu</returns>
    Task<bool> CleanupAnalyticsDataAsync(
        DateTime beforeDate,
        CancellationToken cancellationToken = default);

    #endregion
}
