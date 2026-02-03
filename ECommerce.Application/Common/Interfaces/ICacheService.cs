namespace ECommerce.Application.Common.Interfaces;

/// <summary>
/// Cache servisi interface'i
/// </summary>
public interface ICacheService
{
    /// <summary>
    /// Cache'den değer getir
    /// </summary>
    /// <typeparam name="T">Değer tipi</typeparam>
    /// <param name="key">Cache anahtarı</param>
    /// <param name="cancellationToken">İptal token'ı</param>
    /// <returns>Cache'deki değer</returns>
    Task<T?> GetAsync<T>(string key, CancellationToken cancellationToken = default) where T : class;

    /// <summary>
    /// Cache'e değer kaydet
    /// </summary>
    /// <typeparam name="T">Değer tipi</typeparam>
    /// <param name="key">Cache anahtarı</param>
    /// <param name="value">Kaydedilecek değer</param>
    /// <param name="expiration">Süre sonu</param>
    /// <param name="cancellationToken">İptal token'ı</param>
    /// <returns>İşlem sonucu</returns>
    Task SetAsync<T>(string key, T value, TimeSpan? expiration = null, CancellationToken cancellationToken = default) where T : class;

    /// <summary>
    /// Cache'den değer sil
    /// </summary>
    /// <param name="key">Cache anahtarı</param>
    /// <param name="cancellationToken">İptal token'ı</param>
    /// <returns>İşlem sonucu</returns>
    Task RemoveAsync(string key, CancellationToken cancellationToken = default);

    /// <summary>
    /// Pattern'e uyan tüm anahtarları sil
    /// </summary>
    /// <param name="pattern">Anahtar pattern'i</param>
    /// <param name="cancellationToken">İptal token'ı</param>
    /// <returns>Silinen anahtar sayısı</returns>
    Task<int> RemoveByPatternAsync(string pattern, CancellationToken cancellationToken = default);

    /// <summary>
    /// Cache'de anahtar var mı kontrol et
    /// </summary>
    /// <param name="key">Cache anahtarı</param>
    /// <param name="cancellationToken">İptal token'ı</param>
    /// <returns>Anahtar var mı</returns>
    Task<bool> ExistsAsync(string key, CancellationToken cancellationToken = default);

    /// <summary>
    /// Cache'i temizle
    /// </summary>
    /// <param name="cancellationToken">İptal token'ı</param>
    /// <returns>İşlem sonucu</returns>
    Task ClearAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Cache istatistikleri
    /// </summary>
    /// <param name="cancellationToken">İptal token'ı</param>
    /// <returns>Cache istatistikleri</returns>
    Task<CacheStatistics> GetStatisticsAsync(CancellationToken cancellationToken = default);
}

/// <summary>
/// Cache istatistikleri
/// </summary>
public class CacheStatistics
{
    /// <summary>
    /// Toplam anahtar sayısı
    /// </summary>
    public long TotalKeys { get; set; }

    /// <summary>
    /// Kullanılan bellek (byte)
    /// </summary>
    public long UsedMemory { get; set; }

    /// <summary>
    /// Hit oranı
    /// </summary>
    public decimal HitRate { get; set; }

    /// <summary>
    /// Miss oranı
    /// </summary>
    public decimal MissRate { get; set; }

    /// <summary>
    /// Toplam istek sayısı
    /// </summary>
    public long TotalRequests { get; set; }

    /// <summary>
    /// Başarılı istek sayısı
    /// </summary>
    public long HitCount { get; set; }

    /// <summary>
    /// Başarısız istek sayısı
    /// </summary>
    public long MissCount { get; set; }
}