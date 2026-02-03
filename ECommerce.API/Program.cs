using ECommerce.API.Endpoints;
using ECommerce.API.Common.Extensions;
using ECommerce.API.Hubs;
using ECommerce.Infrastructure.Configuration;
using ECommerce.Application;
using ECommerce.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Serilog yapılandırması (en başta olmalı)
builder.AddSerilogConfiguration();

// OpenTelemetry yapılandırması
builder.AddOpenTelemetryConfiguration();

// OpenSearch health check ekle
builder.Services.AddOpenSearchHealthCheck(builder.Configuration);

// Add services to the container
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { 
        Title = "E-Commerce API", 
        Version = "v1",
        Description = "Modern E-Commerce API with Clean Architecture, CQRS, and Decorator Pattern"
    });
    
    // JWT Authentication için Swagger yapılandırması
    c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
        Name = "Authorization",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    
    c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

// Add application services
builder.Services.AddApplication();

// Add infrastructure services
builder.Services.AddInfrastructure(builder.Configuration);

// Add Payment Gateway services
builder.Services.AddPaymentGatewayServices(builder.Configuration);

// Add Email services
builder.Services.AddEmailServices(builder.Configuration);

// Add Inventory services
builder.Services.AddInventoryServices(builder.Configuration);

// Coupon servisleri
builder.Services.AddCouponServices(builder.Configuration);

// Product Review servisleri
builder.Services.AddProductReviewServices(builder.Configuration);

// JWT Configuration
builder.Services.Configure<JwtConfiguration>(builder.Configuration.GetSection("Jwt"));

// File Upload Configuration
builder.Services.Configure<FileUploadConfiguration>(builder.Configuration.GetSection("FileUpload"));

// JWT Authentication
var jwtConfig = builder.Configuration.GetSection("Jwt").Get<JwtConfiguration>() ?? new JwtConfiguration();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfig.SecretKey)),
            ValidateIssuer = true,
            ValidIssuer = jwtConfig.Issuer,
            ValidateAudience = true,
            ValidAudience = jwtConfig.Audience,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.FromMinutes(jwtConfig.ClockSkewMinutes)
        };
    });

builder.Services.AddAuthorization();

// Add SignalR
builder.Services.AddSignalR();

// Add Rate Limiting
builder.Services.AddRateLimiting(builder.Configuration);

// Add CORS
builder.Services.AddCors(options =>
{
    // Development ortamı için güvenli CORS
    options.AddPolicy("Development", policy =>
    {
        policy.WithOrigins("http://localhost:3000", "http://localhost:3001", "https://localhost:3000", "https://localhost:3001", "http://localhost:2020", "https://localhost:2021")
              .AllowAnyMethod()
              .AllowAnyHeader()
              .AllowCredentials()
              .SetPreflightMaxAge(TimeSpan.FromMinutes(10));
    });

    // Production ortamı için güvenli CORS
    options.AddPolicy("Production", policy =>
    {
        policy.WithOrigins("https://yourdomain.com", "https://www.yourdomain.com")
              .WithMethods("GET", "POST", "PUT", "DELETE", "OPTIONS")
              .WithHeaders("Content-Type", "Authorization", "X-Requested-With")
              .AllowCredentials()
              .SetPreflightMaxAge(TimeSpan.FromMinutes(10));
    });

    // API için özel CORS (Swagger ve diğer API araçları)
    options.AddPolicy("Api", policy =>
    {
        policy.WithOrigins("https://localhost:7047", "http://localhost:5000")
              .AllowAnyMethod()
              .AllowAnyHeader()
              .AllowCredentials();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "E-Commerce API v1");
        c.RoutePrefix = string.Empty; // Swagger UI'ı root'ta göster
    });
}

app.UseHttpsRedirection();

// CORS'u ortama göre yapılandır
if (app.Environment.IsDevelopment())
{
    app.UseCors("Development");
}
else
{
    app.UseCors("Production");
}

// Rate Limiting middleware
app.UseRateLimiting();

// Input Validation middleware
app.UseMiddleware<ECommerce.API.Common.Middleware.InputValidationMiddleware>();

// Security Headers middleware
app.UseMiddleware<ECommerce.API.Common.Middleware.SecurityHeadersMiddleware>();

// Logging middleware'leri
app.UseRequestLogging();
app.UsePerformanceLogging();

// Authentication & Authorization
app.UseAuthentication();
app.UseAuthorization();

// Global exception handling middleware
app.UseGlobalExceptionHandler();

// Map endpoints
app.MapProductsEndpoints();
app.MapAuthEndpoints();
app.MapCategoriesEndpoints();
app.MapOrdersEndpoints();
app.MapUsersEndpoints();
app.MapFileUploadEndpoints();
app.MapPermissionsEndpoints();
app.MapCartEndpoints();
app.MapPaymentEndpoints();
app.MapEmailEndpoints();
app.MapInventoryEndpoints();

// Coupon endpoints
app.MapCouponsEndpoints();

// Product Review endpoints
app.MapProductReviewsEndpoints();

// Wishlist endpoints
app.MapWishlistsEndpoints();

// Search endpoints
app.MapSearchEndpoints();

// Notification endpoints
app.MapNotificationsEndpoints();

// Cargo endpoints
app.MapCargoEndpoints();
app.MapCargoCompanyEndpoints();

// Analytics endpoints
app.MapAnalyticsEndpoints();

// SignalR Hub
app.MapHub<NotificationHub>("/notificationHub");

// Health check endpoint
app.MapGet("/health", () => Results.Ok(new { Status = "Healthy", Timestamp = DateTime.UtcNow }))
   .WithName("HealthCheck")
   .WithTags("Health");

app.Run();
