namespace ECommerce.Application.DTOs.Analytics;

/// <summary>
/// Dashboard metrikleri DTO'su
/// </summary>
public class DashboardMetricsDto
{
    /// <summary>
    /// Tarih
    /// </summary>
    public DateTime Date { get; set; }

    /// <summary>
    /// Metrik türü
    /// </summary>
    public string MetricType { get; set; } = string.Empty;

    /// <summary>
    /// Toplam gelir
    /// </summary>
    public decimal TotalRevenue { get; set; }

    /// <summary>
    /// Toplam sipariş sayısı
    /// </summary>
    public int TotalOrders { get; set; }

    /// <summary>
    /// Toplam müşteri sayısı
    /// </summary>
    public int TotalCustomers { get; set; }

    /// <summary>
    /// Yeni müşteri sayısı
    /// </summary>
    public int NewCustomers { get; set; }

    /// <summary>
    /// Aktif müşteri sayısı
    /// </summary>
    public int ActiveCustomers { get; set; }

    /// <summary>
    /// Toplam ürün sayısı
    /// </summary>
    public int TotalProducts { get; set; }

    /// <summary>
    /// Aktif ürün sayısı
    /// </summary>
    public int ActiveProducts { get; set; }

    /// <summary>
    /// Stokta olmayan ürün sayısı
    /// </summary>
    public int OutOfStockProducts { get; set; }

    /// <summary>
    /// Toplam kategori sayısı
    /// </summary>
    public int TotalCategories { get; set; }

    /// <summary>
    /// Ortalama sipariş değeri
    /// </summary>
    public decimal AverageOrderValue { get; set; }

    /// <summary>
    /// Müşteri başına ortalama sipariş sayısı
    /// </summary>
    public decimal AverageOrdersPerCustomer { get; set; }

    /// <summary>
    /// Müşteri başına ortalama harcama
    /// </summary>
    public decimal AverageSpendingPerCustomer { get; set; }

    /// <summary>
    /// Dönüşüm oranı
    /// </summary>
    public decimal ConversionRate { get; set; }

    /// <summary>
    /// Sepeti terk etme oranı
    /// </summary>
    public decimal CartAbandonmentRate { get; set; }

    /// <summary>
    /// İptal oranı
    /// </summary>
    public decimal CancellationRate { get; set; }

    /// <summary>
    /// İade oranı
    /// </summary>
    public decimal ReturnRate { get; set; }

    /// <summary>
    /// Toplam kargo ücreti
    /// </summary>
    public decimal TotalShippingCost { get; set; }

    /// <summary>
    /// Toplam vergi
    /// </summary>
    public decimal TotalTax { get; set; }

    /// <summary>
    /// Toplam indirim
    /// </summary>
    public decimal TotalDiscount { get; set; }

    /// <summary>
    /// Net kar marjı
    /// </summary>
    public decimal NetProfitMargin { get; set; }

    /// <summary>
    /// Brüt kar marjı
    /// </summary>
    public decimal GrossProfitMargin { get; set; }

    /// <summary>
    /// En çok satan ürün adı
    /// </summary>
    public string? TopSellingProductName { get; set; }

    /// <summary>
    /// En çok satan ürün satış adedi
    /// </summary>
    public int TopSellingProductQuantity { get; set; }

    /// <summary>
    /// En popüler kategori adı
    /// </summary>
    public string? TopCategoryName { get; set; }

    /// <summary>
    /// En popüler kategori sipariş sayısı
    /// </summary>
    public int TopCategoryOrderCount { get; set; }

    /// <summary>
    /// Net gelir (toplam - iptal - iade)
    /// </summary>
    public decimal NetRevenue => TotalRevenue - (TotalRevenue * CancellationRate / 100) - (TotalRevenue * ReturnRate / 100);

    /// <summary>
    /// Müşteri büyüme oranı
    /// </summary>
    public decimal CustomerGrowthRate { get; set; }

    /// <summary>
    /// Gelir büyüme oranı
    /// </summary>
    public decimal RevenueGrowthRate { get; set; }

    /// <summary>
    /// Sipariş büyüme oranı
    /// </summary>
    public decimal OrderGrowthRate { get; set; }
}

/// <summary>
/// Dashboard KPI kartı DTO'su
/// </summary>
public class DashboardKPIDto
{
    /// <summary>
    /// KPI başlığı
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// KPI değeri
    /// </summary>
    public string Value { get; set; } = string.Empty;

    /// <summary>
    /// KPI sayısal değeri
    /// </summary>
    public decimal NumericValue { get; set; }

    /// <summary>
    /// Değişim yüzdesi
    /// </summary>
    public decimal ChangePercentage { get; set; }

    /// <summary>
    /// Değişim yönü (Up, Down, Stable)
    /// </summary>
    public string ChangeDirection { get; set; } = "Stable";

    /// <summary>
    /// KPI ikonu
    /// </summary>
    public string Icon { get; set; } = "chart-bar";

    /// <summary>
    /// KPI rengi
    /// </summary>
    public string Color { get; set; } = "#3B82F6";

    /// <summary>
    /// KPI açıklaması
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// KPI türü (Currency, Number, Percentage, Rate)
    /// </summary>
    public string Type { get; set; } = "Number";

    /// <summary>
    /// KPI formatı
    /// </summary>
    public string Format { get; set; } = "N0";

    /// <summary>
    /// KPI birimi
    /// </summary>
    public string Unit { get; set; } = string.Empty;
}

/// <summary>
/// Dashboard grafik verisi DTO'su
/// </summary>
public class DashboardChartDto
{
    /// <summary>
    /// Grafik başlığı
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Grafik türü (Line, Bar, Pie, Area)
    /// </summary>
    public string ChartType { get; set; } = "Line";

    /// <summary>
    /// Grafik verileri
    /// </summary>
    public List<ChartDataPointDto> Data { get; set; } = new();

    /// <summary>
    /// X ekseni etiketi
    /// </summary>
    public string XAxisLabel { get; set; } = string.Empty;

    /// <summary>
    /// Y ekseni etiketi
    /// </summary>
    public string YAxisLabel { get; set; } = string.Empty;

    /// <summary>
    /// Grafik rengi
    /// </summary>
    public string Color { get; set; } = "#3B82F6";

    /// <summary>
    /// Grafik yüksekliği
    /// </summary>
    public int Height { get; set; } = 300;

    /// <summary>
    /// Grafik genişliği
    /// </summary>
    public int Width { get; set; } = 400;
}

/// <summary>
/// Grafik veri noktası DTO'su
/// </summary>
public class ChartDataPointDto
{
    /// <summary>
    /// X değeri (genellikle tarih veya kategori)
    /// </summary>
    public string X { get; set; } = string.Empty;

    /// <summary>
    /// Y değeri (sayısal değer)
    /// </summary>
    public decimal Y { get; set; }

    /// <summary>
    /// Etiket
    /// </summary>
    public string Label { get; set; } = string.Empty;

    /// <summary>
    /// Renk
    /// </summary>
    public string Color { get; set; } = "#3B82F6";

    /// <summary>
    /// Ek veriler
    /// </summary>
    public Dictionary<string, object> AdditionalData { get; set; } = new();
}

/// <summary>
/// Dashboard özeti DTO'su
/// </summary>
public class DashboardSummaryDto
{
    /// <summary>
    /// KPI kartları
    /// </summary>
    public List<DashboardKPIDto> KPIs { get; set; } = new();

    /// <summary>
    /// Grafikler
    /// </summary>
    public List<DashboardChartDto> Charts { get; set; } = new();

    /// <summary>
    /// Güncel metrikler
    /// </summary>
    public DashboardMetricsDto CurrentMetrics { get; set; } = new();

    /// <summary>
    /// Önceki dönem metrikleri
    /// </summary>
    public DashboardMetricsDto PreviousMetrics { get; set; } = new();

    /// <summary>
    /// Karşılaştırma verileri
    /// </summary>
    public DashboardComparisonDto Comparison { get; set; } = new();

    /// <summary>
    /// Son güncelleme tarihi
    /// </summary>
    public DateTime LastUpdated { get; set; }

    /// <summary>
    /// Dashboard versiyonu
    /// </summary>
    public string Version { get; set; } = "1.0";
}

/// <summary>
/// Dashboard karşılaştırma DTO'su
/// </summary>
public class DashboardComparisonDto
{
    /// <summary>
    /// Gelir değişimi
    /// </summary>
    public decimal RevenueChange { get; set; }

    /// <summary>
    /// Gelir değişim yüzdesi
    /// </summary>
    public decimal RevenueChangePercentage { get; set; }

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

    /// <summary>
    /// Dönüşüm oranı değişimi
    /// </summary>
    public decimal ConversionRateChange { get; set; }

    /// <summary>
    /// Dönüşüm oranı değişim yüzdesi
    /// </summary>
    public decimal ConversionRateChangePercentage { get; set; }
}
