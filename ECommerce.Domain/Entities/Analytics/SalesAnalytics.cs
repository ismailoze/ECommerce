using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerce.Domain.Entities.Analytics;

/// <summary>
/// Satış analitikleri entity'si
/// </summary>
public class SalesAnalytics : BaseEntity
{
    /// <summary>
    /// Tarih (günlük analitik için)
    /// </summary>
    public DateTime Date { get; set; }

    /// <summary>
    /// Toplam satış tutarı
    /// </summary>
    [Column(TypeName = "decimal(18,2)")]
    public decimal TotalSales { get; set; }

    /// <summary>
    /// Toplam sipariş sayısı
    /// </summary>
    public int TotalOrders { get; set; }

    /// <summary>
    /// Ortalama sipariş değeri
    /// </summary>
    [Column(TypeName = "decimal(18,2)")]
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
    [Column(TypeName = "decimal(18,2)")]
    public decimal CancelledOrderValue { get; set; }

    /// <summary>
    /// İade edilen sipariş sayısı
    /// </summary>
    public int RefundedOrders { get; set; }

    /// <summary>
    /// İade edilen sipariş tutarı
    /// </summary>
    [Column(TypeName = "decimal(18,2)")]
    public decimal RefundedOrderValue { get; set; }

    /// <summary>
    /// Kargo ücreti toplamı
    /// </summary>
    [Column(TypeName = "decimal(18,2)")]
    public decimal TotalShippingCost { get; set; }

    /// <summary>
    /// Vergi toplamı
    /// </summary>
    [Column(TypeName = "decimal(18,2)")]
    public decimal TotalTaxAmount { get; set; }

    /// <summary>
    /// İndirim toplamı
    /// </summary>
    [Column(TypeName = "decimal(18,2)")]
    public decimal TotalDiscountAmount { get; set; }

    /// <summary>
    /// Analitik türü (Daily, Weekly, Monthly, Yearly)
    /// </summary>
    [Required]
    [MaxLength(20)]
    public string AnalyticsType { get; set; } = "Daily";

    /// <summary>
    /// Kategori ID'si (kategori bazlı analitik için)
    /// </summary>
    public Guid? CategoryId { get; set; }

    /// <summary>
    /// Ürün ID'si (ürün bazlı analitik için)
    /// </summary>
    public Guid? ProductId { get; set; }

    /// <summary>
    /// Müşteri ID'si (müşteri bazlı analitik için)
    /// </summary>
    public Guid? CustomerId { get; set; }

    /// <summary>
    /// Ek metrikler (JSON formatında)
    /// </summary>
    [MaxLength(4000)]
    public string? AdditionalMetrics { get; set; }
}
