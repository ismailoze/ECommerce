namespace ECommerce.Application.DTOs.Analytics;

/// <summary>
/// Müşteri analitikleri DTO'su
/// </summary>
public class CustomerAnalyticsDto
{
    /// <summary>
    /// Müşteri ID'si
    /// </summary>
    public Guid CustomerId { get; set; }

    /// <summary>
    /// Müşteri adı
    /// </summary>
    public string CustomerName { get; set; } = string.Empty;

    /// <summary>
    /// Müşteri e-posta adresi
    /// </summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// Toplam sipariş sayısı
    /// </summary>
    public int TotalOrders { get; set; }

    /// <summary>
    /// Toplam harcama tutarı
    /// </summary>
    public decimal TotalSpent { get; set; }

    /// <summary>
    /// Ortalama sipariş değeri
    /// </summary>
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
    public decimal CustomerLifetimeValue { get; set; }

    /// <summary>
    /// Müşteri segmenti
    /// </summary>
    public string CustomerSegment { get; set; } = string.Empty;

    /// <summary>
    /// Müşteri puanı (0-100)
    /// </summary>
    public int CustomerScore { get; set; }

    /// <summary>
    /// En çok satın alınan kategori adı
    /// </summary>
    public string? FavoriteCategoryName { get; set; }

    /// <summary>
    /// En çok satın alınan ürün adı
    /// </summary>
    public string? FavoriteProductName { get; set; }

    /// <summary>
    /// Toplam ürün değerlendirme sayısı
    /// </summary>
    public int TotalReviews { get; set; }

    /// <summary>
    /// Ortalama değerlendirme puanı
    /// </summary>
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
    public decimal TotalCouponDiscount { get; set; }

    /// <summary>
    /// Müşteri aktif mi?
    /// </summary>
    public bool IsActive { get; set; }

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
    /// Müşteri risk seviyesi (Low, Medium, High, Churned)
    /// </summary>
    public string RiskLevel { get; set; } = "Low";

    /// <summary>
    /// Müşteri segmenti rengi (UI için)
    /// </summary>
    public string SegmentColor { get; set; } = "#6B7280";

    /// <summary>
    /// Müşteri segmenti ikonu (UI için)
    /// </summary>
    public string SegmentIcon { get; set; } = "user";
}

/// <summary>
/// Müşteri segmenti analizi DTO'su
/// </summary>
public class CustomerSegmentDto
{
    /// <summary>
    /// Segment adı
    /// </summary>
    public string SegmentName { get; set; } = string.Empty;

    /// <summary>
    /// Segment açıklaması
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Müşteri sayısı
    /// </summary>
    public int CustomerCount { get; set; }

    /// <summary>
    /// Toplam harcama
    /// </summary>
    public decimal TotalSpent { get; set; }

    /// <summary>
    /// Ortalama harcama
    /// </summary>
    public decimal AverageSpent { get; set; }

    /// <summary>
    /// Ortalama sipariş sayısı
    /// </summary>
    public decimal AverageOrders { get; set; }

    /// <summary>
    /// Segment yüzdesi
    /// </summary>
    public decimal Percentage { get; set; }

    /// <summary>
    /// Segment rengi
    /// </summary>
    public string Color { get; set; } = "#6B7280";

    /// <summary>
    /// Segment ikonu
    /// </summary>
    public string Icon { get; set; } = "user";
}

/// <summary>
/// Müşteri coğrafi dağılımı DTO'su
/// </summary>
public class CustomerGeographicDto
{
    /// <summary>
    /// Şehir
    /// </summary>
    public string City { get; set; } = string.Empty;

    /// <summary>
    /// İl
    /// </summary>
    public string State { get; set; } = string.Empty;

    /// <summary>
    /// Ülke
    /// </summary>
    public string Country { get; set; } = string.Empty;

    /// <summary>
    /// Müşteri sayısı
    /// </summary>
    public int CustomerCount { get; set; }

    /// <summary>
    /// Toplam harcama
    /// </summary>
    public decimal TotalSpent { get; set; }

    /// <summary>
    /// Ortalama harcama
    /// </summary>
    public decimal AverageSpent { get; set; }

    /// <summary>
    /// Yüzde
    /// </summary>
    public decimal Percentage { get; set; }
}

/// <summary>
/// Müşteri yaşam döngüsü analizi DTO'su
/// </summary>
public class CustomerLifecycleDto
{
    /// <summary>
    /// Aşama adı
    /// </summary>
    public string Stage { get; set; } = string.Empty;

    /// <summary>
    /// Müşteri sayısı
    /// </summary>
    public int CustomerCount { get; set; }

    /// <summary>
    /// Yüzde
    /// </summary>
    public decimal Percentage { get; set; }

    /// <summary>
    /// Ortalama harcama
    /// </summary>
    public decimal AverageSpent { get; set; }

    /// <summary>
    /// Ortalama sipariş sayısı
    /// </summary>
    public decimal AverageOrders { get; set; }

    /// <summary>
    /// Renk
    /// </summary>
    public string Color { get; set; } = "#6B7280";
}

/// <summary>
/// Müşteri analitik özeti DTO'su
/// </summary>
public class CustomerAnalyticsSummaryDto
{
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
    /// Risk altındaki müşteri sayısı
    /// </summary>
    public int AtRiskCustomers { get; set; }

    /// <summary>
    /// Kaybedilen müşteri sayısı
    /// </summary>
    public int ChurnedCustomers { get; set; }

    /// <summary>
    /// Ortalama müşteri yaşam döngüsü değeri
    /// </summary>
    public decimal AverageCLV { get; set; }

    /// <summary>
    /// Müşteri başına ortalama harcama
    /// </summary>
    public decimal AverageSpendingPerCustomer { get; set; }

    /// <summary>
    /// Müşteri başına ortalama sipariş sayısı
    /// </summary>
    public decimal AverageOrdersPerCustomer { get; set; }

    /// <summary>
    /// Müşteri segmentleri
    /// </summary>
    public List<CustomerSegmentDto> Segments { get; set; } = new();

    /// <summary>
    /// Coğrafi dağılım
    /// </summary>
    public List<CustomerGeographicDto> GeographicDistribution { get; set; } = new();

    /// <summary>
    /// Yaşam döngüsü analizi
    /// </summary>
    public List<CustomerLifecycleDto> LifecycleStages { get; set; } = new();
}
