using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerce.Domain.Entities.Analytics;

/// <summary>
/// Ürün analitikleri entity'si
/// </summary>
public class ProductAnalytics : BaseEntity
{
    /// <summary>
    /// Ürün ID'si
    /// </summary>
    public Guid ProductId { get; set; }

    /// <summary>
    /// Toplam satış adedi
    /// </summary>
    public int TotalSalesQuantity { get; set; }

    /// <summary>
    /// Toplam satış tutarı
    /// </summary>
    [Column(TypeName = "decimal(18,2)")]
    public decimal TotalSalesAmount { get; set; }

    /// <summary>
    /// Toplam görüntülenme sayısı
    /// </summary>
    public int TotalViews { get; set; }

    /// <summary>
    /// Toplam tıklama sayısı
    /// </summary>
    public int TotalClicks { get; set; }

    /// <summary>
    /// Sepete ekleme sayısı
    /// </summary>
    public int AddToCartCount { get; set; }

    /// <summary>
    /// Wishlist'e ekleme sayısı
    /// </summary>
    public int AddToWishlistCount { get; set; }

    /// <summary>
    /// Dönüşüm oranı (satış/görüntülenme)
    /// </summary>
    [Column(TypeName = "decimal(5,4)")]
    public decimal ConversionRate { get; set; }

    /// <summary>
    /// Tıklama oranı (tıklama/görüntülenme)
    /// </summary>
    [Column(TypeName = "decimal(5,4)")]
    public decimal ClickThroughRate { get; set; }

    /// <summary>
    /// Sepete ekleme oranı (sepet/görüntülenme)
    /// </summary>
    [Column(TypeName = "decimal(5,4)")]
    public decimal AddToCartRate { get; set; }

    /// <summary>
    /// Ortalama değerlendirme puanı
    /// </summary>
    [Column(TypeName = "decimal(3,2)")]
    public decimal AverageRating { get; set; }

    /// <summary>
    /// Toplam değerlendirme sayısı
    /// </summary>
    public int TotalReviews { get; set; }

    /// <summary>
    /// 5 yıldızlı değerlendirme sayısı
    /// </summary>
    public int FiveStarReviews { get; set; }

    /// <summary>
    /// 4 yıldızlı değerlendirme sayısı
    /// </summary>
    public int FourStarReviews { get; set; }

    /// <summary>
    /// 3 yıldızlı değerlendirme sayısı
    /// </summary>
    public int ThreeStarReviews { get; set; }

    /// <summary>
    /// 2 yıldızlı değerlendirme sayısı
    /// </summary>
    public int TwoStarReviews { get; set; }

    /// <summary>
    /// 1 yıldızlı değerlendirme sayısı
    /// </summary>
    public int OneStarReviews { get; set; }

    /// <summary>
    /// İade edilen ürün sayısı
    /// </summary>
    public int ReturnedQuantity { get; set; }

    /// <summary>
    /// İade oranı
    /// </summary>
    [Column(TypeName = "decimal(5,4)")]
    public decimal ReturnRate { get; set; }

    /// <summary>
    /// Stokta kalma süresi (gün)
    /// </summary>
    public int AverageStockDays { get; set; }

    /// <summary>
    /// En çok satılan gün
    /// </summary>
    public DateTime? BestSellingDay { get; set; }

    /// <summary>
    /// En çok satılan günde satılan adet
    /// </summary>
    public int BestSellingDayQuantity { get; set; }

    /// <summary>
    /// Trend yönü (Increasing, Decreasing, Stable)
    /// </summary>
    [MaxLength(20)]
    public string TrendDirection { get; set; } = "Stable";

    /// <summary>
    /// Trend yüzdesi
    /// </summary>
    [Column(TypeName = "decimal(5,2)")]
    public decimal TrendPercentage { get; set; }

    /// <summary>
    /// Ürün performans skoru (0-100)
    /// </summary>
    public int PerformanceScore { get; set; }

    /// <summary>
    /// Ürün
    /// </summary>
    public virtual Product Product { get; set; } = null!;
}
