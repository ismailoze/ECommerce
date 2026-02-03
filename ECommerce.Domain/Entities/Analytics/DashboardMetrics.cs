using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerce.Domain.Entities.Analytics;

/// <summary>
/// Dashboard metrikleri entity'si
/// </summary>
public class DashboardMetrics : BaseEntity
{
    /// <summary>
    /// Metrik tarihi
    /// </summary>
    public DateTime Date { get; set; }

    /// <summary>
    /// Metrik türü (Daily, Weekly, Monthly, Yearly)
    /// </summary>
    [Required]
    [MaxLength(20)]
    public string MetricType { get; set; } = "Daily";

    /// <summary>
    /// Toplam gelir
    /// </summary>
    [Column(TypeName = "decimal(18,2)")]
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
    [Column(TypeName = "decimal(18,2)")]
    public decimal AverageOrderValue { get; set; }

    /// <summary>
    /// Müşteri başına ortalama sipariş sayısı
    /// </summary>
    [Column(TypeName = "decimal(5,2)")]
    public decimal AverageOrdersPerCustomer { get; set; }

    /// <summary>
    /// Müşteri başına ortalama harcama
    /// </summary>
    [Column(TypeName = "decimal(18,2)")]
    public decimal AverageSpendingPerCustomer { get; set; }

    /// <summary>
    /// Dönüşüm oranı
    /// </summary>
    [Column(TypeName = "decimal(5,4)")]
    public decimal ConversionRate { get; set; }

    /// <summary>
    /// Sepeti terk etme oranı
    /// </summary>
    [Column(TypeName = "decimal(5,4)")]
    public decimal CartAbandonmentRate { get; set; }

    /// <summary>
    /// İptal oranı
    /// </summary>
    [Column(TypeName = "decimal(5,4)")]
    public decimal CancellationRate { get; set; }

    /// <summary>
    /// İade oranı
    /// </summary>
    [Column(TypeName = "decimal(5,4)")]
    public decimal ReturnRate { get; set; }

    /// <summary>
    /// Toplam kargo ücreti
    /// </summary>
    [Column(TypeName = "decimal(18,2)")]
    public decimal TotalShippingCost { get; set; }

    /// <summary>
    /// Toplam vergi
    /// </summary>
    [Column(TypeName = "decimal(18,2)")]
    public decimal TotalTax { get; set; }

    /// <summary>
    /// Toplam indirim
    /// </summary>
    [Column(TypeName = "decimal(18,2)")]
    public decimal TotalDiscount { get; set; }

    /// <summary>
    /// Net kar marjı
    /// </summary>
    [Column(TypeName = "decimal(5,2)")]
    public decimal NetProfitMargin { get; set; }

    /// <summary>
    /// Brüt kar marjı
    /// </summary>
    [Column(TypeName = "decimal(5,2)")]
    public decimal GrossProfitMargin { get; set; }

    /// <summary>
    /// En çok satan ürün ID'si
    /// </summary>
    public Guid? TopSellingProductId { get; set; }

    /// <summary>
    /// En çok satan ürün adı
    /// </summary>
    [MaxLength(200)]
    public string? TopSellingProductName { get; set; }

    /// <summary>
    /// En çok satan ürün satış adedi
    /// </summary>
    public int TopSellingProductQuantity { get; set; }

    /// <summary>
    /// En popüler kategori ID'si
    /// </summary>
    public Guid? TopCategoryId { get; set; }

    /// <summary>
    /// En popüler kategori adı
    /// </summary>
    [MaxLength(100)]
    public string? TopCategoryName { get; set; }

    /// <summary>
    /// En popüler kategori sipariş sayısı
    /// </summary>
    public int TopCategoryOrderCount { get; set; }

    /// <summary>
    /// Ek metrikler (JSON formatında)
    /// </summary>
    [MaxLength(4000)]
    public string? AdditionalMetrics { get; set; }
}
