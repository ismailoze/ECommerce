namespace ECommerce.Application.DTOs.Analytics;

/// <summary>
/// Ürün analitikleri DTO'su
/// </summary>
public class ProductAnalyticsDto
{
    /// <summary>
    /// Ürün ID'si
    /// </summary>
    public Guid ProductId { get; set; }

    /// <summary>
    /// Ürün adı
    /// </summary>
    public string ProductName { get; set; } = string.Empty;

    /// <summary>
    /// Ürün SKU'su
    /// </summary>
    public string Sku { get; set; } = string.Empty;

    /// <summary>
    /// Kategori adı
    /// </summary>
    public string CategoryName { get; set; } = string.Empty;

    /// <summary>
    /// Marka adı
    /// </summary>
    public string? BrandName { get; set; }

    /// <summary>
    /// Toplam satış adedi
    /// </summary>
    public int TotalSalesQuantity { get; set; }

    /// <summary>
    /// Toplam satış tutarı
    /// </summary>
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
    /// Dönüşüm oranı
    /// </summary>
    public decimal ConversionRate { get; set; }

    /// <summary>
    /// Tıklama oranı
    /// </summary>
    public decimal ClickThroughRate { get; set; }

    /// <summary>
    /// Sepete ekleme oranı
    /// </summary>
    public decimal AddToCartRate { get; set; }

    /// <summary>
    /// Ortalama değerlendirme puanı
    /// </summary>
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
    public decimal ReturnRate { get; set; }

    /// <summary>
    /// Stokta kalma süresi
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
    /// Trend yönü
    /// </summary>
    public string TrendDirection { get; set; } = string.Empty;

    /// <summary>
    /// Trend yüzdesi
    /// </summary>
    public decimal TrendPercentage { get; set; }

    /// <summary>
    /// Ürün performans skoru
    /// </summary>
    public int PerformanceScore { get; set; }

    /// <summary>
    /// Ürün fiyatı
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    /// İndirimli fiyat
    /// </summary>
    public decimal? DiscountedPrice { get; set; }

    /// <summary>
    /// Stok miktarı
    /// </summary>
    public int StockQuantity { get; set; }

    /// <summary>
    /// Ürün aktif mi?
    /// </summary>
    public bool IsActive { get; set; }

    /// <summary>
    /// Ana resim URL'si
    /// </summary>
    public string? MainImageUrl { get; set; }

    /// <summary>
    /// Değerlendirme dağılımı
    /// </summary>
    public Dictionary<int, int> RatingDistribution { get; set; } = new();

    /// <summary>
    /// Performans seviyesi (Excellent, Good, Average, Poor)
    /// </summary>
    public string PerformanceLevel { get; set; } = "Average";

    /// <summary>
    /// Performans rengi
    /// </summary>
    public string PerformanceColor { get; set; } = "#6B7280";
}

/// <summary>
/// Ürün performans sıralaması DTO'su
/// </summary>
public class ProductPerformanceDto
{
    /// <summary>
    /// Sıralama
    /// </summary>
    public int Rank { get; set; }

    /// <summary>
    /// Ürün ID'si
    /// </summary>
    public Guid ProductId { get; set; }

    /// <summary>
    /// Ürün adı
    /// </summary>
    public string ProductName { get; set; } = string.Empty;

    /// <summary>
    /// SKU
    /// </summary>
    public string Sku { get; set; } = string.Empty;

    /// <summary>
    /// Kategori
    /// </summary>
    public string CategoryName { get; set; } = string.Empty;

    /// <summary>
    /// Satış adedi
    /// </summary>
    public int SalesQuantity { get; set; }

    /// <summary>
    /// Satış tutarı
    /// </summary>
    public decimal SalesAmount { get; set; }

    /// <summary>
    /// Performans skoru
    /// </summary>
    public int PerformanceScore { get; set; }

    /// <summary>
    /// Dönüşüm oranı
    /// </summary>
    public decimal ConversionRate { get; set; }

    /// <summary>
    /// Ortalama değerlendirme
    /// </summary>
    public decimal AverageRating { get; set; }

    /// <summary>
    /// Trend yönü
    /// </summary>
    public string TrendDirection { get; set; } = string.Empty;

    /// <summary>
    /// Trend yüzdesi
    /// </summary>
    public decimal TrendPercentage { get; set; }

    /// <summary>
    /// Önceki sıralama
    /// </summary>
    public int PreviousRank { get; set; }

    /// <summary>
    /// Sıralama değişimi
    /// </summary>
    public int RankChange { get; set; }

    /// <summary>
    /// Sıralama değişim ikonu
    /// </summary>
    public string RankChangeIcon { get; set; } = "minus";

    /// <summary>
    /// Sıralama değişim rengi
    /// </summary>
    public string RankChangeColor { get; set; } = "#6B7280";
}

/// <summary>
/// Kategori performans analizi DTO'su
/// </summary>
public class CategoryPerformanceDto
{
    /// <summary>
    /// Kategori ID'si
    /// </summary>
    public Guid CategoryId { get; set; }

    /// <summary>
    /// Kategori adı
    /// </summary>
    public string CategoryName { get; set; } = string.Empty;

    /// <summary>
    /// Üst kategori adı
    /// </summary>
    public string? ParentCategoryName { get; set; }

    /// <summary>
    /// Toplam ürün sayısı
    /// </summary>
    public int TotalProducts { get; set; }

    /// <summary>
    /// Aktif ürün sayısı
    /// </summary>
    public int ActiveProducts { get; set; }

    /// <summary>
    /// Toplam satış adedi
    /// </summary>
    public int TotalSalesQuantity { get; set; }

    /// <summary>
    /// Toplam satış tutarı
    /// </summary>
    public decimal TotalSalesAmount { get; set; }

    /// <summary>
    /// Ortalama ürün fiyatı
    /// </summary>
    public decimal AverageProductPrice { get; set; }

    /// <summary>
    /// Ortalama değerlendirme puanı
    /// </summary>
    public decimal AverageRating { get; set; }

    /// <summary>
    /// Toplam değerlendirme sayısı
    /// </summary>
    public int TotalReviews { get; set; }

    /// <summary>
    /// Kategori performans skoru
    /// </summary>
    public int PerformanceScore { get; set; }

    /// <summary>
    /// Trend yönü
    /// </summary>
    public string TrendDirection { get; set; } = string.Empty;

    /// <summary>
    /// Trend yüzdesi
    /// </summary>
    public decimal TrendPercentage { get; set; }

    /// <summary>
    /// Kategori yüzdesi
    /// </summary>
    public decimal CategoryPercentage { get; set; }

    /// <summary>
    /// En çok satan ürün
    /// </summary>
    public string? TopSellingProduct { get; set; }

    /// <summary>
    /// En çok satan ürün satış adedi
    /// </summary>
    public int TopSellingProductQuantity { get; set; }
}

/// <summary>
/// Ürün analitik özeti DTO'su
/// </summary>
public class ProductAnalyticsSummaryDto
{
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
    /// En çok satan ürünler
    /// </summary>
    public List<ProductPerformanceDto> TopSellingProducts { get; set; } = new();

    /// <summary>
    /// En az satan ürünler
    /// </summary>
    public List<ProductPerformanceDto> LowSellingProducts { get; set; } = new();

    /// <summary>
    /// En yüksek değerlendirmeli ürünler
    /// </summary>
    public List<ProductPerformanceDto> TopRatedProducts { get; set; } = new();

    /// <summary>
    /// Kategori performansları
    /// </summary>
    public List<CategoryPerformanceDto> CategoryPerformances { get; set; } = new();

    /// <summary>
    /// Stok uyarıları
    /// </summary>
    public List<ProductAnalyticsDto> StockAlerts { get; set; } = new();

    /// <summary>
    /// Trend analizi
    /// </summary>
    public List<ProductPerformanceDto> TrendingProducts { get; set; } = new();
}
