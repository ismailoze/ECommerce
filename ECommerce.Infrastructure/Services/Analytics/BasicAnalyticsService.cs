using ECommerce.Application.Common.Interfaces;
using ECommerce.Application.DTOs.Analytics;
using ECommerce.Domain.Interfaces;
using ECommerce.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace ECommerce.Infrastructure.Services.Analytics;

/// <summary>
/// Temel analitik servisi implementasyonu
/// </summary>
public class BasicAnalyticsService : IAnalyticsService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<BasicAnalyticsService> _logger;

    public BasicAnalyticsService(IUnitOfWork unitOfWork, ILogger<BasicAnalyticsService> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    #region Satış Analitikleri

    public async Task<List<SalesAnalyticsDto>> GetSalesAnalyticsAsync(
        DateTime startDate, 
        DateTime endDate, 
        string analyticsType = "Daily",
        Guid? categoryId = null,
        Guid? productId = null,
        Guid? customerId = null,
        CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Satış analitikleri getiriliyor. StartDate: {StartDate}, EndDate: {EndDate}", startDate, endDate);

            var orders = await _unitOfWork.Orders.GetAllAsync();
            var query = orders.AsQueryable();

            // Tarih filtresi
            query = query.Where(o => o.OrderDate >= startDate && o.OrderDate <= endDate);

            var orderList = query.ToList();

            // Günlük gruplama
            var groupedOrders = orderList.GroupBy(o => o.OrderDate.Date);

            var analytics = new List<SalesAnalyticsDto>();

            foreach (var group in groupedOrders)
            {
                var ordersInGroup = group.ToList();
                var analyticsDto = new SalesAnalyticsDto
                {
                    Date = group.Key,
                    AnalyticsType = analyticsType,
                    CategoryId = categoryId,
                    ProductId = productId,
                    CustomerId = customerId,
                    TotalSales = ordersInGroup.Sum(o => o.TotalAmount),
                    TotalOrders = ordersInGroup.Count,
                    TotalItemsSold = ordersInGroup.Sum(o => o.OrderItems.Sum(oi => oi.Quantity)),
                    UniqueCustomers = ordersInGroup.Select(o => o.UserId).Distinct().Count(),
                    NewCustomers = 0,
                    ReturningCustomers = 0,
                    CancelledOrders = ordersInGroup.Count(o => o.Status == "Cancelled"),
                    CancelledOrderValue = ordersInGroup.Where(o => o.Status == "Cancelled").Sum(o => o.TotalAmount),
                    RefundedOrders = ordersInGroup.Count(o => o.PaymentStatus == "Refunded"),
                    RefundedOrderValue = ordersInGroup.Where(o => o.PaymentStatus == "Refunded").Sum(o => o.TotalAmount),
                    TotalShippingCost = ordersInGroup.Sum(o => o.ShippingCost),
                    TotalTaxAmount = ordersInGroup.Sum(o => o.TaxAmount),
                    TotalDiscountAmount = ordersInGroup.Sum(o => o.DiscountAmount)
                };

                analyticsDto.AverageOrderValue = analyticsDto.TotalOrders > 0 ? analyticsDto.TotalSales / analyticsDto.TotalOrders : 0;

                analytics.Add(analyticsDto);
            }

            return analytics.OrderBy(a => a.Date).ToList();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Satış analitikleri getirilirken hata oluştu");
            throw;
        }
    }

    public async Task<List<SalesTrendDto>> GetSalesTrendAsync(
        DateTime startDate, 
        DateTime endDate, 
        string period = "Daily",
        CancellationToken cancellationToken = default)
    {
        try
        {
            var salesAnalytics = await GetSalesAnalyticsAsync(startDate, endDate, period, cancellationToken: cancellationToken);
            var trends = new List<SalesTrendDto>();

            for (int i = 0; i < salesAnalytics.Count; i++)
            {
                var current = salesAnalytics[i];
                var previous = i > 0 ? salesAnalytics[i - 1] : null;

                var trend = new SalesTrendDto
                {
                    Date = current.Date,
                    SalesAmount = current.TotalSales,
                    OrderCount = current.TotalOrders
                };

                if (previous != null)
                {
                    var salesChange = current.TotalSales - previous.TotalSales;
                    trend.ChangePercentage = previous.TotalSales > 0 ? (salesChange / previous.TotalSales) * 100 : 0;
                    trend.TrendDirection = salesChange > 0 ? "Up" : salesChange < 0 ? "Down" : "Stable";
                }

                trends.Add(trend);
            }

            return trends;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Satış trend analizi getirilirken hata oluştu");
            throw;
        }
    }

    public async Task<SalesSummaryDto> GetSalesSummaryAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            var today = DateTime.Today;
            var thisWeekStart = today.AddDays(-(int)today.DayOfWeek);
            var thisMonthStart = new DateTime(today.Year, today.Month, 1);
            var thisYearStart = new DateTime(today.Year, 1, 1);

            var todayAnalytics = await GetSalesAnalyticsAsync(today, today, "Daily", cancellationToken: cancellationToken);
            var thisWeekAnalytics = await GetSalesAnalyticsAsync(thisWeekStart, today, "Daily", cancellationToken: cancellationToken);
            var thisMonthAnalytics = await GetSalesAnalyticsAsync(thisMonthStart, today, "Daily", cancellationToken: cancellationToken);
            var thisYearAnalytics = await GetSalesAnalyticsAsync(thisYearStart, today, "Daily", cancellationToken: cancellationToken);

            var summary = new SalesSummaryDto
            {
                Today = todayAnalytics.FirstOrDefault() ?? new SalesAnalyticsDto(),
                ThisWeek = new SalesAnalyticsDto 
                { 
                    TotalSales = thisWeekAnalytics.Sum(a => a.TotalSales),
                    TotalOrders = thisWeekAnalytics.Sum(a => a.TotalOrders)
                },
                ThisMonth = new SalesAnalyticsDto 
                { 
                    TotalSales = thisMonthAnalytics.Sum(a => a.TotalSales),
                    TotalOrders = thisMonthAnalytics.Sum(a => a.TotalOrders)
                },
                ThisYear = new SalesAnalyticsDto 
                { 
                    TotalSales = thisYearAnalytics.Sum(a => a.TotalSales),
                    TotalOrders = thisYearAnalytics.Sum(a => a.TotalOrders)
                }
            };

            return summary;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Satış özeti getirilirken hata oluştu");
            throw;
        }
    }

    #endregion

    #region Müşteri Analitikleri

    public async Task<List<CustomerAnalyticsDto>> GetCustomerAnalyticsAsync(
        Guid? customerId = null,
        string? segment = null,
        int pageNumber = 1,
        int pageSize = 50,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var customers = await _unitOfWork.Users.GetAllAsync();
            var query = customers.AsQueryable();

            if (customerId.HasValue)
            {
                query = query.Where(c => c.Id == customerId.Value);
            }

            var customerList = query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            var analytics = new List<CustomerAnalyticsDto>();

            foreach (var customer in customerList)
            {
                var customerOrders = await _unitOfWork.Orders.GetAllAsync();
                var orders = customerOrders.Where(o => o.UserId == customer.Id).ToList();

                var analyticsDto = new CustomerAnalyticsDto
                {
                    CustomerId = customer.Id,
                    CustomerName = customer.FullName,
                    Email = customer.Email,
                    TotalOrders = orders.Count,
                    TotalSpent = orders.Sum(o => o.TotalAmount),
                    FirstOrderDate = orders.OrderBy(o => o.OrderDate).FirstOrDefault()?.OrderDate,
                    LastOrderDate = orders.OrderByDescending(o => o.OrderDate).FirstOrDefault()?.OrderDate,
                    IsActive = customer.IsActive,
                    LastActivityDate = customer.UpdatedAt ?? customer.CreatedAt,
                    DaysSinceRegistration = (int)(DateTime.UtcNow - customer.CreatedAt).TotalDays,
                    DaysSinceLastOrder = orders.Any() ? (int)(DateTime.UtcNow - orders.Max(o => o.OrderDate)).TotalDays : 0
                };

                analyticsDto.AverageOrderValue = analyticsDto.TotalOrders > 0 ? analyticsDto.TotalSpent / analyticsDto.TotalOrders : 0;
                analyticsDto.CustomerLifetimeValue = analyticsDto.TotalSpent;
                analyticsDto.CustomerSegment = analyticsDto.TotalSpent > 1000 ? "VIP" : analyticsDto.TotalSpent > 100 ? "Regular" : "New";
                analyticsDto.CustomerScore = Math.Min((int)(analyticsDto.TotalSpent / 10), 100);
                analyticsDto.RiskLevel = analyticsDto.DaysSinceLastOrder > 90 ? "High" : analyticsDto.DaysSinceLastOrder > 30 ? "Medium" : "Low";

                analytics.Add(analyticsDto);
            }

            return analytics;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Müşteri analitikleri getirilirken hata oluştu");
            throw;
        }
    }

    public async Task<List<CustomerSegmentDto>> GetCustomerSegmentsAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            var customers = await _unitOfWork.Users.GetAllAsync();
            var segments = new List<CustomerSegmentDto>();

            // VIP Müşteriler
            var vipCustomers = customers.Where(c => GetCustomerTotalSpent(c.Id) > 1000).ToList();
            segments.Add(new CustomerSegmentDto
            {
                SegmentName = "VIP",
                Description = "Yüksek değerli müşteriler",
                CustomerCount = vipCustomers.Count,
                TotalSpent = vipCustomers.Sum(c => GetCustomerTotalSpent(c.Id)),
                Color = "#10B981",
                Icon = "star"
            });

            // Regular Müşteriler
            var regularCustomers = customers.Where(c => 
            {
                var spent = GetCustomerTotalSpent(c.Id);
                return spent >= 100 && spent <= 1000;
            }).ToList();
            segments.Add(new CustomerSegmentDto
            {
                SegmentName = "Regular",
                Description = "Orta değerli müşteriler",
                CustomerCount = regularCustomers.Count,
                TotalSpent = regularCustomers.Sum(c => GetCustomerTotalSpent(c.Id)),
                Color = "#3B82F6",
                Icon = "user"
            });

            // New Müşteriler
            var newCustomers = customers.Where(c => GetCustomerTotalSpent(c.Id) < 100).ToList();
            segments.Add(new CustomerSegmentDto
            {
                SegmentName = "New",
                Description = "Yeni müşteriler",
                CustomerCount = newCustomers.Count,
                TotalSpent = newCustomers.Sum(c => GetCustomerTotalSpent(c.Id)),
                Color = "#F59E0B",
                Icon = "user-plus"
            });

            // Yüzdeleri hesapla
            var totalCustomers = customers.Count();
            foreach (var segment in segments)
            {
                segment.Percentage = totalCustomers > 0 ? (decimal)segment.CustomerCount / totalCustomers * 100 : 0;
                segment.AverageSpent = segment.CustomerCount > 0 ? segment.TotalSpent / segment.CustomerCount : 0;
            }

            return segments;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Müşteri segmentleri getirilirken hata oluştu");
            throw;
        }
    }

    public async Task<List<CustomerGeographicDto>> GetCustomerGeographicDistributionAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            var addresses = await _unitOfWork.Addresses.GetAllAsync();
            var distribution = addresses
                .GroupBy(a => new { a.City, a.State, a.Country })
                .Select(g => new CustomerGeographicDto
                {
                    City = g.Key.City,
                    State = g.Key.State,
                    Country = g.Key.Country,
                    CustomerCount = g.Select(a => a.UserId).Distinct().Count(),
                    TotalSpent = 0
                })
                .OrderByDescending(d => d.CustomerCount)
                .ToList();

            var totalCustomers = distribution.Sum(d => d.CustomerCount);
            foreach (var item in distribution)
            {
                item.Percentage = totalCustomers > 0 ? (decimal)item.CustomerCount / totalCustomers * 100 : 0;
                item.AverageSpent = item.CustomerCount > 0 ? item.TotalSpent / item.CustomerCount : 0;
            }

            return distribution;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Müşteri coğrafi dağılımı getirilirken hata oluştu");
            throw;
        }
    }

    public async Task<List<CustomerLifecycleDto>> GetCustomerLifecycleAnalysisAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            var customers = await _unitOfWork.Users.GetAllAsync();
            var stages = new List<CustomerLifecycleDto>();

            // Prospect
            var prospects = customers.Where(c => !GetCustomerOrders(c.Id).Any()).ToList();
            stages.Add(new CustomerLifecycleDto
            {
                Stage = "Prospect",
                CustomerCount = prospects.Count,
                AverageSpent = 0,
                AverageOrders = 0,
                Color = "#6B7280"
            });

            // New
            var newCustomers = customers.Where(c => 
            {
                var firstOrder = GetCustomerFirstOrderDate(c.Id);
                return firstOrder.HasValue && (DateTime.UtcNow - firstOrder.Value).TotalDays <= 30;
            }).ToList();
            stages.Add(new CustomerLifecycleDto
            {
                Stage = "New",
                CustomerCount = newCustomers.Count,
                AverageSpent = newCustomers.Count > 0 ? newCustomers.Sum(c => GetCustomerTotalSpent(c.Id)) / newCustomers.Count : 0,
                AverageOrders = newCustomers.Count > 0 ? newCustomers.Sum(c => GetCustomerOrders(c.Id).Count()) / newCustomers.Count : 0,
                Color = "#10B981"
            });

            // Active
            var activeCustomers = customers.Where(c => 
            {
                var lastOrder = GetCustomerLastOrderDate(c.Id);
                return lastOrder.HasValue && (DateTime.UtcNow - lastOrder.Value).TotalDays <= 30;
            }).ToList();
            stages.Add(new CustomerLifecycleDto
            {
                Stage = "Active",
                CustomerCount = activeCustomers.Count,
                AverageSpent = activeCustomers.Count > 0 ? activeCustomers.Sum(c => GetCustomerTotalSpent(c.Id)) / activeCustomers.Count : 0,
                AverageOrders = activeCustomers.Count > 0 ? activeCustomers.Sum(c => GetCustomerOrders(c.Id).Count()) / activeCustomers.Count : 0,
                Color = "#3B82F6"
            });

            // Yüzdeleri hesapla
            var totalCustomers = customers.Count();
            foreach (var stage in stages)
            {
                stage.Percentage = totalCustomers > 0 ? (decimal)stage.CustomerCount / totalCustomers * 100 : 0;
            }

            return stages;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Müşteri yaşam döngüsü analizi getirilirken hata oluştu");
            throw;
        }
    }

    public async Task<CustomerAnalyticsSummaryDto> GetCustomerAnalyticsSummaryAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            var customers = await _unitOfWork.Users.GetAllAsync();
            var summary = new CustomerAnalyticsSummaryDto
            {
                TotalCustomers = customers.Count(),
                NewCustomers = customers.Count(c => (DateTime.UtcNow - c.CreatedAt).TotalDays <= 30),
                ActiveCustomers = customers.Count(c => 
                {
                    var lastOrder = GetCustomerLastOrderDate(c.Id);
                    return lastOrder.HasValue && (DateTime.UtcNow - lastOrder.Value).TotalDays <= 30;
                }),
                AtRiskCustomers = customers.Count(c => 
                {
                    var lastOrder = GetCustomerLastOrderDate(c.Id);
                    return lastOrder.HasValue && (DateTime.UtcNow - lastOrder.Value).TotalDays > 30 && (DateTime.UtcNow - lastOrder.Value).TotalDays <= 90;
                }),
                ChurnedCustomers = customers.Count(c => 
                {
                    var lastOrder = GetCustomerLastOrderDate(c.Id);
                    return lastOrder.HasValue && (DateTime.UtcNow - lastOrder.Value).TotalDays > 90;
                })
            };

            summary.AverageCLV = customers.Count() > 0 ? customers.Sum(c => GetCustomerTotalSpent(c.Id)) / customers.Count() : 0;
            summary.AverageSpendingPerCustomer = customers.Count() > 0 ? customers.Sum(c => GetCustomerTotalSpent(c.Id)) / customers.Count() : 0;
            summary.AverageOrdersPerCustomer = customers.Count() > 0 ? customers.Sum(c => GetCustomerOrders(c.Id).Count()) / customers.Count() : 0;

            summary.Segments = await GetCustomerSegmentsAsync(cancellationToken);
            summary.GeographicDistribution = await GetCustomerGeographicDistributionAsync(cancellationToken);
            summary.LifecycleStages = await GetCustomerLifecycleAnalysisAsync(cancellationToken);

            return summary;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Müşteri analitik özeti getirilirken hata oluştu");
            throw;
        }
    }

    #endregion

    #region Ürün Analitikleri

    public async Task<List<ProductAnalyticsDto>> GetProductAnalyticsAsync(
        Guid? productId = null,
        Guid? categoryId = null,
        int pageNumber = 1,
        int pageSize = 50,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var products = await _unitOfWork.Products.GetAllAsync();
            var query = products.AsQueryable();

            if (productId.HasValue)
            {
                query = query.Where(p => p.Id == productId.Value);
            }

            if (categoryId.HasValue)
            {
                query = query.Where(p => p.CategoryId == categoryId.Value);
            }

            var productList = query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            var analytics = new List<ProductAnalyticsDto>();

            foreach (var product in productList)
            {
                var orderItems = await _unitOfWork.OrderItems.GetAllAsync();
                var productOrderItems = orderItems.Where(oi => oi.ProductId == product.Id).ToList();

                var analyticsDto = new ProductAnalyticsDto
                {
                    ProductId = product.Id,
                    ProductName = product.Name,
                    Sku = product.Sku,
                    CategoryName = product.Category?.Name ?? "",
                    BrandName = product.Brand?.Name,
                    Price = product.Price,
                    DiscountedPrice = product.DiscountedPrice,
                    StockQuantity = product.StockQuantity,
                    IsActive = product.IsActive,
                    MainImageUrl = product.MainImageUrl,
                    TotalSalesQuantity = productOrderItems.Sum(oi => oi.Quantity),
                    TotalSalesAmount = productOrderItems.Sum(oi => oi.Quantity * oi.UnitPrice),
                    TotalViews = 0,
                    TotalClicks = 0,
                    AddToCartCount = 0,
                    AddToWishlistCount = 0
                };

                analyticsDto.ConversionRate = analyticsDto.TotalViews > 0 ? (decimal)analyticsDto.TotalSalesQuantity / analyticsDto.TotalViews : 0;
                analyticsDto.ClickThroughRate = analyticsDto.TotalViews > 0 ? (decimal)analyticsDto.TotalClicks / analyticsDto.TotalViews : 0;
                analyticsDto.AddToCartRate = analyticsDto.TotalViews > 0 ? (decimal)analyticsDto.AddToCartCount / analyticsDto.TotalViews : 0;

                // Değerlendirmeler
                var reviews = await _unitOfWork.ProductReviews.GetAllAsync();
                var productReviews = reviews.Where(r => r.ProductId == product.Id).ToList();
                analyticsDto.TotalReviews = productReviews.Count;
                analyticsDto.AverageRating = productReviews.Any() ? (decimal)productReviews.Average(r => r.Rating) : 0;

                // Performans skoru
                analyticsDto.PerformanceScore = Math.Min(analyticsDto.TotalSalesQuantity / 10, 100);
                analyticsDto.PerformanceLevel = analyticsDto.PerformanceScore >= 80 ? "Excellent" : 
                                               analyticsDto.PerformanceScore >= 60 ? "Good" : 
                                               analyticsDto.PerformanceScore >= 40 ? "Average" : "Poor";

                analytics.Add(analyticsDto);
            }

            return analytics;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ürün analitikleri getirilirken hata oluştu");
            throw;
        }
    }

    public async Task<List<ProductPerformanceDto>> GetProductPerformanceRankingAsync(
        string sortBy = "SalesQuantity",
        string sortDirection = "desc",
        int limit = 20,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var products = await _unitOfWork.Products.GetAllAsync();
            var rankings = new List<ProductPerformanceDto>();

            foreach (var product in products.Take(limit))
            {
                var analytics = await GetProductAnalyticsAsync(product.Id, cancellationToken: cancellationToken);
                var productAnalytics = analytics.FirstOrDefault();

                if (productAnalytics != null)
                {
                    var ranking = new ProductPerformanceDto
                    {
                        ProductId = product.Id,
                        ProductName = product.Name,
                        Sku = product.Sku,
                        CategoryName = product.Category?.Name ?? "",
                        SalesQuantity = productAnalytics.TotalSalesQuantity,
                        SalesAmount = productAnalytics.TotalSalesAmount,
                        PerformanceScore = productAnalytics.PerformanceScore,
                        ConversionRate = productAnalytics.ConversionRate,
                        AverageRating = productAnalytics.AverageRating,
                        TrendDirection = "Stable",
                        TrendPercentage = 0
                    };

                    rankings.Add(ranking);
                }
            }

            // Sıralama
            rankings = sortBy.ToLower() switch
            {
                "salesquantity" => sortDirection.ToLower() == "desc" 
                    ? rankings.OrderByDescending(r => r.SalesQuantity).ToList()
                    : rankings.OrderBy(r => r.SalesQuantity).ToList(),
                "salesamount" => sortDirection.ToLower() == "desc"
                    ? rankings.OrderByDescending(r => r.SalesAmount).ToList()
                    : rankings.OrderBy(r => r.SalesAmount).ToList(),
                _ => rankings.OrderByDescending(r => r.SalesQuantity).ToList()
            };

            // Sıralama atama
            for (int i = 0; i < rankings.Count; i++)
            {
                rankings[i].Rank = i + 1;
            }

            return rankings;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ürün performans sıralaması getirilirken hata oluştu");
            throw;
        }
    }

    public async Task<List<CategoryPerformanceDto>> GetCategoryPerformanceAnalysisAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            var categories = await _unitOfWork.Categories.GetAllAsync();
            var performances = new List<CategoryPerformanceDto>();

            foreach (var category in categories)
            {
                var products = await _unitOfWork.Products.GetAllAsync();
                var categoryProducts = products.Where(p => p.CategoryId == category.Id).ToList();

                var performance = new CategoryPerformanceDto
                {
                    CategoryId = category.Id,
                    CategoryName = category.Name,
                    ParentCategoryName = category.ParentCategory?.Name,
                    TotalProducts = categoryProducts.Count,
                    ActiveProducts = categoryProducts.Count(p => p.IsActive),
                    AverageProductPrice = categoryProducts.Any() ? categoryProducts.Average(p => p.Price) : 0
                };

                // Satış verileri
                var orderItems = await _unitOfWork.OrderItems.GetAllAsync();
                var categoryOrderItems = orderItems.Where(oi => categoryProducts.Any(p => p.Id == oi.ProductId)).ToList();

                performance.TotalSalesQuantity = categoryOrderItems.Sum(oi => oi.Quantity);
                performance.TotalSalesAmount = categoryOrderItems.Sum(oi => oi.Quantity * oi.UnitPrice);

                // Değerlendirmeler
                var reviews = await _unitOfWork.ProductReviews.GetAllAsync();
                var categoryReviews = reviews.Where(r => categoryProducts.Any(p => p.Id == r.ProductId)).ToList();
                performance.TotalReviews = categoryReviews.Count;
                performance.AverageRating = categoryReviews.Any() ? (decimal)categoryReviews.Average(r => r.Rating) : 0;

                // Performans skoru
                performance.PerformanceScore = Math.Min(performance.TotalSalesQuantity / 100, 100);

                performances.Add(performance);
            }

            return performances.OrderByDescending(p => p.TotalSalesAmount).ToList();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Kategori performans analizi getirilirken hata oluştu");
            throw;
        }
    }

    public async Task<ProductAnalyticsSummaryDto> GetProductAnalyticsSummaryAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            var products = await _unitOfWork.Products.GetAllAsync();
            var summary = new ProductAnalyticsSummaryDto
            {
                TotalProducts = products.Count(),
                ActiveProducts = products.Count(p => p.IsActive),
                OutOfStockProducts = products.Count(p => p.StockQuantity <= 0),
                TotalCategories = (await _unitOfWork.Categories.GetAllAsync()).Count()
            };

            // En çok satan ürünler
            summary.TopSellingProducts = await GetProductPerformanceRankingAsync("SalesQuantity", "desc", 10, cancellationToken);

            // En az satan ürünler
            summary.LowSellingProducts = await GetProductPerformanceRankingAsync("SalesQuantity", "asc", 10, cancellationToken);

            // En yüksek değerlendirmeli ürünler
            summary.TopRatedProducts = await GetProductPerformanceRankingAsync("AverageRating", "desc", 10, cancellationToken);

            // Kategori performansları
            summary.CategoryPerformances = await GetCategoryPerformanceAnalysisAsync(cancellationToken);

            return summary;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ürün analitik özeti getirilirken hata oluştu");
            throw;
        }
    }

    #endregion

    #region Dashboard Metrikleri

    public async Task<DashboardMetricsDto> GetDashboardMetricsAsync(
        DateTime date, 
        string metricType = "Daily",
        CancellationToken cancellationToken = default)
    {
        try
        {
            var metrics = new DashboardMetricsDto
            {
                Date = date,
                MetricType = metricType
            };

            // Temel metrikler
            var users = await _unitOfWork.Users.GetAllAsync();
            var products = await _unitOfWork.Products.GetAllAsync();
            var categories = await _unitOfWork.Categories.GetAllAsync();
            var orders = await _unitOfWork.Orders.GetAllAsync();

            metrics.TotalCustomers = users.Count();
            metrics.NewCustomers = users.Count(u => (date - u.CreatedAt).TotalDays <= 1);
            metrics.ActiveCustomers = users.Count(u => 
            {
                var lastOrder = GetCustomerLastOrderDate(u.Id);
                return lastOrder.HasValue && (date - lastOrder.Value).TotalDays <= 30;
            });

            metrics.TotalProducts = products.Count();
            metrics.ActiveProducts = products.Count(p => p.IsActive);
            metrics.OutOfStockProducts = products.Count(p => p.StockQuantity <= 0);
            metrics.TotalCategories = categories.Count();

            // Satış metrikleri
            var dateOrders = orders.Where(o => o.OrderDate.Date == date.Date).ToList();
            metrics.TotalOrders = dateOrders.Count;
            metrics.TotalRevenue = dateOrders.Sum(o => o.TotalAmount);
            metrics.AverageOrderValue = dateOrders.Any() ? metrics.TotalRevenue / dateOrders.Count : 0;
            metrics.AverageOrdersPerCustomer = metrics.TotalCustomers > 0 ? (decimal)metrics.TotalOrders / metrics.TotalCustomers : 0;
            metrics.AverageSpendingPerCustomer = metrics.TotalCustomers > 0 ? metrics.TotalRevenue / metrics.TotalCustomers : 0;

            // Oranlar
            metrics.ConversionRate = 0;
            metrics.CartAbandonmentRate = 0;
            metrics.CancellationRate = dateOrders.Any() ? (decimal)dateOrders.Count(o => o.Status == "Cancelled") / dateOrders.Count * 100 : 0;
            metrics.ReturnRate = dateOrders.Any() ? (decimal)dateOrders.Count(o => o.PaymentStatus == "Refunded") / dateOrders.Count * 100 : 0;

            // Mali metrikler
            metrics.TotalShippingCost = dateOrders.Sum(o => o.ShippingCost);
            metrics.TotalTax = dateOrders.Sum(o => o.TaxAmount);
            metrics.TotalDiscount = dateOrders.Sum(o => o.DiscountAmount);

            return metrics;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Dashboard metrikleri getirilirken hata oluştu");
            throw;
        }
    }

    public async Task<List<DashboardKPIDto>> GetDashboardKPIsAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            var today = DateTime.Today;
            var yesterday = today.AddDays(-1);

            var todayMetrics = await GetDashboardMetricsAsync(today, "Daily", cancellationToken);
            var yesterdayMetrics = await GetDashboardMetricsAsync(yesterday, "Daily", cancellationToken);

            var kpis = new List<DashboardKPIDto>
            {
                new DashboardKPIDto
                {
                    Title = "Bugünkü Gelir",
                    Value = todayMetrics.TotalRevenue.ToString("C"),
                    NumericValue = todayMetrics.TotalRevenue,
                    ChangePercentage = CalculatePercentageChange(todayMetrics.TotalRevenue, yesterdayMetrics.TotalRevenue),
                    ChangeDirection = todayMetrics.TotalRevenue > yesterdayMetrics.TotalRevenue ? "Up" : "Down",
                    Icon = "currency-dollar",
                    Color = "#10B981",
                    Type = "Currency",
                    Description = "Bugünkü toplam gelir"
                },
                new DashboardKPIDto
                {
                    Title = "Bugünkü Siparişler",
                    Value = todayMetrics.TotalOrders.ToString(),
                    NumericValue = todayMetrics.TotalOrders,
                    ChangePercentage = CalculatePercentageChange(todayMetrics.TotalOrders, yesterdayMetrics.TotalOrders),
                    ChangeDirection = todayMetrics.TotalOrders > yesterdayMetrics.TotalOrders ? "Up" : "Down",
                    Icon = "shopping-cart",
                    Color = "#3B82F6",
                    Type = "Number",
                    Description = "Bugünkü toplam sipariş sayısı"
                },
                new DashboardKPIDto
                {
                    Title = "Ortalama Sipariş Değeri",
                    Value = todayMetrics.AverageOrderValue.ToString("C"),
                    NumericValue = todayMetrics.AverageOrderValue,
                    ChangePercentage = CalculatePercentageChange(todayMetrics.AverageOrderValue, yesterdayMetrics.AverageOrderValue),
                    ChangeDirection = todayMetrics.AverageOrderValue > yesterdayMetrics.AverageOrderValue ? "Up" : "Down",
                    Icon = "chart-bar",
                    Color = "#8B5CF6",
                    Type = "Currency",
                    Description = "Ortalama sipariş değeri"
                },
                new DashboardKPIDto
                {
                    Title = "Toplam Müşteriler",
                    Value = todayMetrics.TotalCustomers.ToString(),
                    NumericValue = todayMetrics.TotalCustomers,
                    ChangePercentage = CalculatePercentageChange(todayMetrics.TotalCustomers, yesterdayMetrics.TotalCustomers),
                    ChangeDirection = todayMetrics.TotalCustomers > yesterdayMetrics.TotalCustomers ? "Up" : "Down",
                    Icon = "users",
                    Color = "#F59E0B",
                    Type = "Number",
                    Description = "Toplam müşteri sayısı"
                }
            };

            return kpis;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Dashboard KPI'ları getirilirken hata oluştu");
            throw;
        }
    }

    public async Task<List<DashboardChartDto>> GetDashboardChartsAsync(
        string chartType = "Sales",
        string period = "30days",
        CancellationToken cancellationToken = default)
    {
        try
        {
            var charts = new List<DashboardChartDto>();

            var endDate = DateTime.Today;
            var startDate = period switch
            {
                "7days" => endDate.AddDays(-7),
                "30days" => endDate.AddDays(-30),
                "90days" => endDate.AddDays(-90),
                "1year" => endDate.AddDays(-365),
                _ => endDate.AddDays(-30)
            };

            var salesTrend = await GetSalesTrendAsync(startDate, endDate, "Daily", cancellationToken);
            
            charts.Add(new DashboardChartDto
            {
                Title = "Satış Trendi",
                ChartType = "Line",
                Data = salesTrend.Select(st => new ChartDataPointDto
                {
                    X = st.Date.ToString("dd/MM"),
                    Y = st.SalesAmount,
                    Label = st.SalesAmount.ToString("C")
                }).ToList(),
                XAxisLabel = "Tarih",
                YAxisLabel = "Satış Tutarı (TL)",
                Color = "#10B981"
            });

            return charts;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Dashboard grafikleri getirilirken hata oluştu");
            throw;
        }
    }

    public async Task<DashboardSummaryDto> GetDashboardSummaryAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            var summary = new DashboardSummaryDto
            {
                LastUpdated = DateTime.UtcNow,
                Version = "1.0"
            };

            // KPI'lar
            summary.KPIs = await GetDashboardKPIsAsync(cancellationToken);

            // Grafikler
            summary.Charts = await GetDashboardChartsAsync("Sales", "30days", cancellationToken);

            // Güncel metrikler
            summary.CurrentMetrics = await GetDashboardMetricsAsync(DateTime.Today, "Daily", cancellationToken);

            // Önceki dönem metrikleri
            summary.PreviousMetrics = await GetDashboardMetricsAsync(DateTime.Today.AddDays(-1), "Daily", cancellationToken);

            // Karşılaştırma
            summary.Comparison = new DashboardComparisonDto
            {
                RevenueChange = summary.CurrentMetrics.TotalRevenue - summary.PreviousMetrics.TotalRevenue,
                RevenueChangePercentage = CalculatePercentageChange(summary.CurrentMetrics.TotalRevenue, summary.PreviousMetrics.TotalRevenue),
                OrderChange = summary.CurrentMetrics.TotalOrders - summary.PreviousMetrics.TotalOrders,
                OrderChangePercentage = CalculatePercentageChange(summary.CurrentMetrics.TotalOrders, summary.PreviousMetrics.TotalOrders),
                CustomerChange = summary.CurrentMetrics.TotalCustomers - summary.PreviousMetrics.TotalCustomers,
                CustomerChangePercentage = CalculatePercentageChange(summary.CurrentMetrics.TotalCustomers, summary.PreviousMetrics.TotalCustomers),
                AOVChange = summary.CurrentMetrics.AverageOrderValue - summary.PreviousMetrics.AverageOrderValue,
                AOVChangePercentage = CalculatePercentageChange(summary.CurrentMetrics.AverageOrderValue, summary.PreviousMetrics.AverageOrderValue),
                ConversionRateChange = summary.CurrentMetrics.ConversionRate - summary.PreviousMetrics.ConversionRate,
                ConversionRateChangePercentage = CalculatePercentageChange(summary.CurrentMetrics.ConversionRate, summary.PreviousMetrics.ConversionRate)
            };

            return summary;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Dashboard özeti getirilirken hata oluştu");
            throw;
        }
    }

    #endregion

    #region Analitik Hesaplama

    public async Task<bool> CalculateAndSaveAnalyticsAsync(
        DateTime date, 
        string analyticsType = "Daily",
        CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Analitik verileri hesaplanıyor. Date: {Date}, Type: {Type}", date, analyticsType);
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Analitik verileri hesaplanırken hata oluştu");
            return false;
        }
    }

    public async Task<bool> RecalculateAllAnalyticsAsync(
        DateTime startDate, 
        DateTime endDate,
        CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Tüm analitik verileri yeniden hesaplanıyor. StartDate: {StartDate}, EndDate: {EndDate}", startDate, endDate);
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Tüm analitik verileri yeniden hesaplanırken hata oluştu");
            return false;
        }
    }

    public async Task<bool> CleanupAnalyticsDataAsync(
        DateTime beforeDate,
        CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation("Analitik verileri temizleniyor. BeforeDate: {BeforeDate}", beforeDate);
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Analitik verileri temizlenirken hata oluştu");
            return false;
        }
    }

    #endregion

    #region Private Helper Methods

    private decimal CalculatePercentageChange(decimal current, decimal previous)
    {
        if (previous == 0) return current > 0 ? 100 : 0;
        return ((current - previous) / previous) * 100;
    }

    private decimal GetCustomerTotalSpent(Guid customerId)
    {
        var orders = GetCustomerOrders(customerId);
        return orders.Sum(o => o.TotalAmount);
    }

    private DateTime? GetCustomerLastOrderDate(Guid customerId)
    {
        var orders = GetCustomerOrders(customerId);
        return orders.OrderByDescending(o => o.OrderDate).FirstOrDefault()?.OrderDate;
    }

    private DateTime? GetCustomerFirstOrderDate(Guid customerId)
    {
        var orders = GetCustomerOrders(customerId);
        return orders.OrderBy(o => o.OrderDate).FirstOrDefault()?.OrderDate;
    }

    private IEnumerable<Order> GetCustomerOrders(Guid customerId)
    {
        var orders = _unitOfWork.Orders.GetAllAsync().Result;
        return orders.Where(o => o.UserId == customerId);
    }

    #endregion
}
