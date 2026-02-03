using ECommerce.Application.Common.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;
using System.Text.Json;

namespace ECommerce.Infrastructure.Services.Cache;

/// <summary>
/// Memory cache servisi implementasyonu
/// </summary>
public class MemoryCacheService : ICacheService
{
    private readonly IMemoryCache _memoryCache;
    private readonly ILogger<MemoryCacheService> _logger;
    private readonly ConcurrentDictionary<string, CacheEntry> _cacheEntries;
    private readonly CacheStatistics _statistics;

    public MemoryCacheService(IMemoryCache memoryCache, ILogger<MemoryCacheService> logger)
    {
        _memoryCache = memoryCache;
        _logger = logger;
        _cacheEntries = new ConcurrentDictionary<string, CacheEntry>();
        _statistics = new CacheStatistics();
    }

    public async Task<T?> GetAsync<T>(string key, CancellationToken cancellationToken = default) where T : class
    {
        try
        {
            _statistics.TotalRequests++;

            if (_memoryCache.TryGetValue(key, out var cachedValue))
            {
                _statistics.HitCount++;
                _statistics.HitRate = _statistics.TotalRequests > 0 ? (decimal)_statistics.HitCount / _statistics.TotalRequests : 0;
                _statistics.MissRate = _statistics.TotalRequests > 0 ? (decimal)_statistics.MissCount / _statistics.TotalRequests : 0;

                _logger.LogDebug("Cache hit: {Key}", key);
                return cachedValue as T;
            }

            _statistics.MissCount++;
            _statistics.HitRate = _statistics.TotalRequests > 0 ? (decimal)_statistics.HitCount / _statistics.TotalRequests : 0;
            _statistics.MissRate = _statistics.TotalRequests > 0 ? (decimal)_statistics.MissCount / _statistics.TotalRequests : 0;

            _logger.LogDebug("Cache miss: {Key}", key);
            return null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Cache'den değer getirilirken hata oluştu. Key: {Key}", key);
            return null;
        }
    }

    public async Task SetAsync<T>(string key, T value, TimeSpan? expiration = null, CancellationToken cancellationToken = default) where T : class
    {
        try
        {
            var options = new MemoryCacheEntryOptions();

            if (expiration.HasValue)
            {
                options.AbsoluteExpirationRelativeToNow = expiration;
            }
            else
            {
                // Varsayılan süre sonu: 1 saat
                options.AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1);
            }

            // Cache entry bilgilerini kaydet
            var entry = new CacheEntry
            {
                Key = key,
                CreatedAt = DateTime.UtcNow,
                ExpiresAt = DateTime.UtcNow.Add(expiration ?? TimeSpan.FromHours(1)),
                Size = CalculateSize(value)
            };

            _cacheEntries.AddOrUpdate(key, entry, (k, v) => entry);

            _memoryCache.Set(key, value, options);

            _logger.LogDebug("Cache'e değer kaydedildi. Key: {Key}, Expiration: {Expiration}", key, expiration);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Cache'e değer kaydedilirken hata oluştu. Key: {Key}", key);
        }
    }

    public async Task RemoveAsync(string key, CancellationToken cancellationToken = default)
    {
        try
        {
            _memoryCache.Remove(key);
            _cacheEntries.TryRemove(key, out _);

            _logger.LogDebug("Cache'den değer silindi. Key: {Key}", key);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Cache'den değer silinirken hata oluştu. Key: {Key}", key);
        }
    }

    public async Task<int> RemoveByPatternAsync(string pattern, CancellationToken cancellationToken = default)
    {
        try
        {
            var keysToRemove = _cacheEntries.Keys
                .Where(key => IsMatch(key, pattern))
                .ToList();

            var removedCount = 0;
            foreach (var key in keysToRemove)
            {
                _memoryCache.Remove(key);
                if (_cacheEntries.TryRemove(key, out _))
                {
                    removedCount++;
                }
            }

            _logger.LogInformation("Pattern'e uyan {Count} anahtar silindi. Pattern: {Pattern}", removedCount, pattern);
            return removedCount;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Pattern'e uyan anahtarlar silinirken hata oluştu. Pattern: {Pattern}", pattern);
            return 0;
        }
    }

    public async Task<bool> ExistsAsync(string key, CancellationToken cancellationToken = default)
    {
        try
        {
            return _memoryCache.TryGetValue(key, out _);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Cache anahtar varlığı kontrol edilirken hata oluştu. Key: {Key}", key);
            return false;
        }
    }

    public async Task ClearAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            if (_memoryCache is MemoryCache mc)
            {
                mc.Compact(1.0); // %100 temizle
            }

            _cacheEntries.Clear();
            _statistics.TotalKeys = 0;
            _statistics.UsedMemory = 0;

            _logger.LogInformation("Cache tamamen temizlendi");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Cache temizlenirken hata oluştu");
        }
    }

    public async Task<CacheStatistics> GetStatisticsAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            _statistics.TotalKeys = _cacheEntries.Count;
            _statistics.UsedMemory = _cacheEntries.Values.Sum(entry => entry.Size);

            return new CacheStatistics
            {
                TotalKeys = _statistics.TotalKeys,
                UsedMemory = _statistics.UsedMemory,
                HitRate = _statistics.HitRate,
                MissRate = _statistics.MissRate,
                TotalRequests = _statistics.TotalRequests,
                HitCount = _statistics.HitCount,
                MissCount = _statistics.MissCount
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Cache istatistikleri getirilirken hata oluştu");
            return new CacheStatistics();
        }
    }

    private static long CalculateSize<T>(T value) where T : class
    {
        try
        {
            var json = JsonSerializer.Serialize(value);
            return System.Text.Encoding.UTF8.GetByteCount(json);
        }
        catch
        {
            return 0;
        }
    }

    private static bool IsMatch(string key, string pattern)
    {
        if (string.IsNullOrEmpty(pattern))
            return false;

        // Basit wildcard pattern matching
        if (pattern.Contains('*'))
        {
            var regexPattern = pattern.Replace("*", ".*");
            return System.Text.RegularExpressions.Regex.IsMatch(key, regexPattern);
        }

        return key.Contains(pattern, StringComparison.OrdinalIgnoreCase);
    }

    private class CacheEntry
    {
        public string Key { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime ExpiresAt { get; set; }
        public long Size { get; set; }
    }
}
