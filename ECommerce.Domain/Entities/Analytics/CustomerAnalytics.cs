using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerce.Domain.Entities.Analytics;

/// <summary>
/// Müşteri analitikleri entity'si
/// </summary>
public class CustomerAnalytics : BaseEntity
{
    /// <summary>
    /// Müşteri ID'si
    /// </summary>
    public Guid CustomerId { get; set; }

    /// <summary>
    /// Toplam sipariş sayısı
    /// </summary>
    public int TotalOrders { get; set; }

    /// <summary>
    /// Toplam harcama tutarı
    /// </summary>
    [Column(TypeName = "decimal(18,2)")]
    public decimal TotalSpent { get; set; }

    /// <summary>
    /// Ortalama sipariş değeri
    /// </summary>
    [Column(TypeName = "decimal(18,2)")]
    public decimal AverageOrderValue { get; set; }

    /// <summary>
    /// Son sipariş tarihi
    /// </summary>
    public DateTime? LastOrderDate { get; set; }

    /// <summary>
    /// İlk sipariş tarihi
    /// </summary>
    public DateTime? FirstOrderDate { get; set; }

    /// <summary>
    /// Müşteri yaşam döngüsü değeri (CLV)
    /// </summary>
    [Column(TypeName = "decimal(18,2)")]
    public decimal CustomerLifetimeValue { get; set; }

    /// <summary>
    /// Müşteri segmenti (VIP, Regular, New, At-Risk)
    /// </summary>
    [MaxLength(20)]
    public string CustomerSegment { get; set; } = "Regular";

    /// <summary>
    /// Müşteri puanı (0-100)
    /// </summary>
    public int CustomerScore { get; set; }

    /// <summary>
    /// En çok satın alınan kategori ID'si
    /// </summary>
    public Guid? FavoriteCategoryId { get; set; }

    /// <summary>
    /// En çok satın alınan ürün ID'si
    /// </summary>
    public Guid? FavoriteProductId { get; set; }

    /// <summary>
    /// Toplam ürün değerlendirme sayısı
    /// </summary>
    public int TotalReviews { get; set; }

    /// <summary>
    /// Ortalama değerlendirme puanı
    /// </summary>
    [Column(TypeName = "decimal(3,2)")]
    public decimal AverageRating { get; set; }

    /// <summary>
    /// Wishlist'teki ürün sayısı
    /// </summary>
    public int WishlistItemsCount { get; set; }

    /// <summary>
    /// Kullanılan kupon sayısı
    /// </summary>
    public int CouponsUsed { get; set; }

    /// <summary>
    /// Toplam kupon indirimi
    /// </summary>
    [Column(TypeName = "decimal(18,2)")]
    public decimal TotalCouponDiscount { get; set; }

    /// <summary>
    /// Müşteri aktif mi?
    /// </summary>
    public bool IsActive { get; set; } = true;

    /// <summary>
    /// Son aktivite tarihi
    /// </summary>
    public DateTime? LastActivityDate { get; set; }

    /// <summary>
    /// Müşteri kayıt tarihinden itibaren geçen gün sayısı
    /// </summary>
    public int DaysSinceRegistration { get; set; }

    /// <summary>
    /// Son siparişten itibaren geçen gün sayısı
    /// </summary>
    public int DaysSinceLastOrder { get; set; }

    /// <summary>
    /// Müşteri
    /// </summary>
    public virtual User Customer { get; set; } = null!;
}
