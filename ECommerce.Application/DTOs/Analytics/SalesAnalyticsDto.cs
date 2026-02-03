namespace ECommerce.Application.DTOs.Analytics;

/// <summary>
/// Satış analitikleri DTO'su
/// </summary>
public class SalesAnalyticsDto
{
    /// <summary>
    /// Tarih
    /// </summary>
    public DateTime Date { get; set; }

    /// <summary>
    /// Toplam satış tutarı
    /// </summary>
    public decimal TotalSales { get; set; }

    /// <summary>
    /// Toplam sipariş sayısı
    /// </summary>
    public int TotalOrders { get; set; }

    /// <summary>
    /// Ortalama sipariş değeri
    /// </summary>
    public decimal AverageOrderValue { get; set; }

    /// <summary>
    /// Toplam ürün satış adedi
    /// </summary>
    public int TotalItemsSold { get; set; }

    /// <summary>
    /// Benzersiz müşteri sayısı
    /// </summary>
    public int UniqueCustomers { get; set; }

    /// <summary>
    /// Yeni müşteri sayısı
    /// </summary>
    public int NewCustomers { get; set; }

    /// <summary>
    /// Tekrar eden müşteri sayısı
    /// </summary>
    public int ReturningCustomers { get; set; }

    /// <summary>
    /// İptal edilen sipariş sayısı
    /// </summary>
    public int CancelledOrders { get; set; }

    /// <summary>
    /// İptal edilen sipariş tutarı
    /// </summary>
    public decimal CancelledOrderValue { get; set; }

    /// <summary>
    /// İade edilen sipariş sayısı
    /// </summary>
    public int RefundedOrders { get; set; }

    /// <summary>
    /// İade edilen sipariş tutarı
    /// </summary>
    public decimal RefundedOrderValue { get; set; }

    /// <summary>
    /// Kargo ücreti toplamı
    /// </summary>
    public decimal TotalShippingCost { get; set; }

    /// <summary>
    /// Vergi toplamı
    /// </summary>
    public decimal TotalTaxAmount { get; set; }

    /// <summary>
    /// İndirim toplamı
    /// </summary>
    public decimal TotalDiscountAmount { get; set; }

    /// <summary>
    /// Analitik türü
    /// </summary>
    public string AnalyticsType { get; set; } = string.Empty;

    /// <summary>
    /// Kategori ID'si
    /// </summary>
    public Guid? CategoryId { get; set; }

    /// <summary>
    /// Kategori adı
    /// </summary>
    public string? CategoryName { get; set; }

    /// <summary>
    /// Ürün ID'si
    /// </summary>
    public Guid? ProductId { get; set; }

    /// <summary>
    /// Ürün adı
    /// </summary>
    public string? ProductName { get; set; }

    /// <summary>
    /// Müşteri ID'si
    /// </summary>
    public Guid? CustomerId { get; set; }

    /// <summary>
    /// Müşteri adı
    /// </summary>
    public string? CustomerName { get; set; }

    /// <summary>
    /// Net satış (toplam - iptal - iade)
    /// </summary>
    public decimal NetSales => TotalSales - CancelledOrderValue - RefundedOrderValue;

    /// <summary>
    /// İptal oranı
    /// </summary>
    public decimal CancellationRate => TotalOrders > 0 ? (decimal)CancelledOrders / TotalOrders * 100 : 0;

    /// <summary>
    /// İade oranı
    /// </summary>
    public decimal ReturnRate => TotalOrders > 0 ? (decimal)RefundedOrders / TotalOrders * 100 : 0;

    /// <summary>
    /// Müşteri başına ortalama sipariş
    /// </summary>
    public decimal AverageOrdersPerCustomer => UniqueCustomers > 0 ? (decimal)TotalOrders / UniqueCustomers : 0;

    /// <summary>
    /// Müşteri başına ortalama harcama
    /// </summary>
    public decimal AverageSpendingPerCustomer => UniqueCustomers > 0 ? NetSales / UniqueCustomers : 0;
}

/// <summary>
/// Satış trend analizi DTO'su
/// </summary>
public class SalesTrendDto
{
    /// <summary>
    /// Tarih
    /// </summary>
    public DateTime Date { get; set; }

    /// <summary>
    /// Satış tutarı
    /// </summary>
    public decimal SalesAmount { get; set; }

    /// <summary>
    /// Sipariş sayısı
    /// </summary>
    public int OrderCount { get; set; }

    /// <summary>
    /// Önceki döneme göre değişim yüzdesi
    /// </summary>
    public decimal ChangePercentage { get; set; }

    /// <summary>
    /// Trend yönü (Up, Down, Stable)
    /// </summary>
    public string TrendDirection { get; set; } = string.Empty;
}

/// <summary>
/// Satış özeti DTO'su
/// </summary>
public class SalesSummaryDto
{
    /// <summary>
    /// Bugünkü satışlar
    /// </summary>
    public SalesAnalyticsDto Today { get; set; } = new();

    /// <summary>
    /// Bu haftaki satışlar
    /// </summary>
    public SalesAnalyticsDto ThisWeek { get; set; } = new();

    /// <summary>
    /// Bu ayki satışlar
    /// </summary>
    public SalesAnalyticsDto ThisMonth { get; set; } = new();

    /// <summary>
    /// Bu yılki satışlar
    /// </summary>
    public SalesAnalyticsDto ThisYear { get; set; } = new();

    /// <summary>
    /// Önceki dönemle karşılaştırma
    /// </summary>
    public SalesComparisonDto Comparison { get; set; } = new();
}

/// <summary>
/// Satış karşılaştırma DTO'su
/// </summary>
public class SalesComparisonDto
{
    /// <summary>
    /// Satış değişimi
    /// </summary>
    public decimal SalesChange { get; set; }

    /// <summary>
    /// Satış değişim yüzdesi
    /// </summary>
    public decimal SalesChangePercentage { get; set; }

    /// <summary>
    /// Sipariş değişimi
    /// </summary>
    public int OrderChange { get; set; }

    /// <summary>
    /// Sipariş değişim yüzdesi
    /// </summary>
    public decimal OrderChangePercentage { get; set; }

    /// <summary>
    /// Müşteri değişimi
    /// </summary>
    public int CustomerChange { get; set; }

    /// <summary>
    /// Müşteri değişim yüzdesi
    /// </summary>
    public decimal CustomerChangePercentage { get; set; }

    /// <summary>
    /// AOV değişimi
    /// </summary>
    public decimal AOVChange { get; set; }

    /// <summary>
    /// AOV değişim yüzdesi
    /// </summary>
    public decimal AOVChangePercentage { get; set; }
}
