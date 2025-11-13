# 🛒 ECommerce API

Modern, ölçeklenebilir ve güvenli bir e-ticaret API'si. Clean Architecture, CQRS pattern ve .NET 9.0 teknolojileri kullanılarak geliştirilmiştir.

## 📋 İçindekiler

- [Özellikler](#-özellikler)
- [Teknoloji Yığını](#-teknoloji-yığını)
- [Mimari](#-mimari)
- [Kurulum](#-kurulum)
- [API Dokümantasyonu](#-api-dokümantasyonu)
- [Kullanım](#-kullanım)
- [OpenSearch Entegrasyonu](#-opensearch-entegrasyonu)
- [Docker Kurulumu](#-docker-kurulumu)
- [Katkıda Bulunma](#-katkıda-bulunma)
- [Lisans](#-lisans)

## ✨ Özellikler

### 🔐 Kimlik Doğrulama ve Yetkilendirme

- **JWT Token** tabanlı kimlik doğrulama
- **Role-based Authorization** (RBAC)
- **Permission-based Access Control** (PBAC)
- **Refresh Token** mekanizması
- **Password Hashing** (PBKDF2 + SHA256)
- **Email/Phone Verification** - E-posta ve telefon doğrulama
- **Account Lockout** - Başarısız giriş denemesi koruması
- **API Rate Limiting** - DDoS ve brute force koruması (AspNetCoreRateLimit)
- **CORS Security** - Güvenli cross-origin istekleri
- **Input Validation Middleware** - XSS ve injection koruması
- **Security Headers Middleware** - CSP, HSTS ve diğer güvenlik başlıkları
- **Request Validation** - JSON format doğrulama ve tehlikeli içerik tespiti
- **IP-based Rate Limiting** - IP adresine göre istek sınırlama
- **Endpoint-specific Limits** - Endpoint bazında özel rate limit kuralları

### 🛍️ E-Ticaret Özellikleri

- **Ürün Yönetimi** - CRUD işlemleri, resim yükleme, SKU yönetimi
- **Kategori Yönetimi** - Hiyerarşik kategori yapısı, sıralama
- **Sipariş Yönetimi** - Sipariş oluşturma, durum takibi, geçmiş
- **Sepet Yönetimi** - Session tabanlı sepet, miktar güncelleme
- **Wishlist (İstek Listesi)** - Favori ürünler, fiyat takibi
- **Kupon Sistemi** - İndirim kuponları, doğrulama, kullanım takibi
- **Ürün Değerlendirmeleri** - Yıldız puanlama, yorum sistemi
- **Stok Yönetimi** - Envanter takibi, stok hareketleri, uyarılar
- **Ödeme Sistemi** - Iyzico entegrasyonu, 3D Secure, webhook
- **Email Sistemi** - SMTP entegrasyonu, şablon yönetimi
- **Bildirim Sistemi** - Real-time notifications, SignalR hub
- **Dosya Yükleme** - Resim yükleme, dosya validasyonu
- **Kullanıcı Yönetimi** - Profil yönetimi, şifre değiştirme
- **Adres Yönetimi** - Teslimat ve fatura adresleri

### 🏗️ Mimari ve Kalite

- **Clean Architecture** - Katmanlı mimari
- **CQRS Pattern** - Command Query Responsibility Segregation
- **Repository Pattern** - Veri erişim soyutlaması
- **Unit of Work** - İşlem yönetimi
- **Dependency Injection** - Bağımlılık yönetimi

### 🔧 Geliştirici Deneyimi

- **FluentValidation** - Veri doğrulama
- **Global Exception Handling** - Merkezi hata yönetimi
- **Structured Logging** - Serilog + OpenTelemetry
- **OpenSearch Integration** - Log görselleştirme
- **Swagger/OpenAPI** - API dokümantasyonu
- **Health Checks** - Sistem durumu kontrolü

### ⚡ Performans ve Ölçeklenebilirlik

- **In-Memory Caching** - Performans optimizasyonu
- **Async/Await** - Asenkron programlama
- **Soft Delete** - Veri güvenliği
- **Database Migrations** - Veritabanı versiyonlama
- **Seed Data** - Otomatik test verisi

## 🛠️ Teknoloji Yığını

### Backend

- **.NET 9.0** - Framework
- **ASP.NET Core Web API** - Web API
- **Entity Framework Core 9.0** - ORM
- **SQL Server** - Veritabanı
- **AutoMapper 15.0** - Object Mapping
- **Scrutor 6.1** - Dependency Injection

### Authentication & Security

- **JWT Bearer Token** - Kimlik doğrulama
- **BCrypt** - Şifre hashleme
- **FluentValidation 12.0** - Veri doğrulama
- **Role-based Authorization** - Yetkilendirme
- **AspNetCoreRateLimit 5.0** - API rate limiting
- **Input Validation Middleware** - XSS ve injection koruması
- **Security Headers Middleware** - Güvenlik başlıkları

### Payment & External Services

- **Iyzico Payment Gateway** - Ödeme işlemleri
- **SMTP Email Service** - Email gönderimi
- **3D Secure** - Güvenli ödeme

### Logging & Monitoring

- **Serilog 9.0** - Structured logging
- **OpenTelemetry 1.13** - Distributed tracing
- **OpenSearch 2.11** - Log aggregation
- **OpenSearch Dashboard** - Log visualization
- **Health Checks** - Sistem durumu kontrolü
- **Enhanced OpenTelemetry** - Gelişmiş distributed tracing
- **Structured Logging** - JSON formatında loglar
- **Performance Monitoring** - Request/response süre takibi

### Caching & Performance

- **In-Memory Caching** - Performans optimizasyonu
- **Async/Await** - Asenkron programlama
- **Connection Pooling** - Veritabanı optimizasyonu

### Development Tools

- **Swagger/OpenAPI 9.0** - API documentation
- **Docker & Docker Compose** - Containerization
- **Git** - Version control
- **xUnit** - Unit testing

## 🏛️ Mimari

Proje Clean Architecture prensiplerine uygun olarak 4 katmanlı yapıda tasarlanmıştır:

```
ECommerce/
├── ECommerce.Domain/          # Domain Layer
│   ├── Entities/             # Domain entities (35+ entities)
│   ├── Enums/               # Domain enums (12 enums)
│   ├── Interfaces/          # Repository interfaces
│   └── Exceptions/          # Domain exceptions
├── ECommerce.Application/     # Application Layer
│   ├── Features/            # CQRS features (15+ modules)
│   │   ├── Auth/           # Authentication & Authorization
│   │   ├── Products/       # Product management
│   │   ├── Orders/         # Order processing
│   │   ├── Cart/           # Shopping cart
│   │   ├── Payments/       # Payment processing
│   │   ├── Coupons/        # Coupon system
│   │   ├── Wishlists/      # Wishlist management
│   │   ├── Inventory/      # Stock management
│   │   ├── ProductReviews/ # Review system
│   │   ├── Emails/         # Email services
│   │   └── Notifications/  # Notification system
│   ├── DTOs/               # Data transfer objects (40+ DTOs)
│   ├── Common/             # Shared application logic
│   │   ├── Behaviors/      # MediatR behaviors
│   │   ├── Decorators/     # Cross-cutting concerns
│   │   ├── Interfaces/     # Application interfaces
│   │   ├── Results/        # Result patterns
│   │   └── Messaging/      # CQRS messaging
│   └── Mappings/           # AutoMapper profiles
├── ECommerce.Infrastructure/  # Infrastructure Layer
│   ├── Data/               # Database context & migrations
│   ├── Repositories/       # Repository implementations
│   ├── Services/           # External services
│   │   ├── PaymentGateway/ # Iyzico integration
│   │   ├── EmailService/   # SMTP service
│   │   └── FileService/    # File upload service
│   └── Configuration/      # Configuration classes
└── ECommerce.API/           # Presentation Layer
    ├── Endpoints/          # API endpoints (15+ endpoint groups)
    ├── Hubs/              # SignalR hubs
    ├── Common/            # Shared API logic
    │   ├── Extensions/    # Extension methods (RateLimiting, etc.)
    │   ├── Middleware/    # Custom middleware (Security, Validation)
    │   └── ProblemDetails/ # Error handling
    └── Properties/        # Launch settings
```

### CQRS Pattern

- **Commands** - Veri değiştirme işlemleri (Create, Update, Delete)
- **Queries** - Veri okuma işlemleri (Get, List, Search)
- **Handlers** - İş mantığı implementasyonu
- **Validators** - FluentValidation ile veri doğrulama
- **Decorators** - Logging, Caching, Performance monitoring

### Design Patterns

- **Repository Pattern** - Veri erişim soyutlaması
- **Unit of Work** - İşlem yönetimi
- **Decorator Pattern** - Cross-cutting concerns
- **Result Pattern** - Hata yönetimi
- **Dependency Injection** - Bağımlılık yönetimi

## 🚀 Kurulum

### Gereksinimler

- .NET 9.0 SDK
- SQL Server (LocalDB desteklenir)
- Visual Studio 2022 veya VS Code
- Docker (OpenSearch için)

### 1. Repository'yi Klonlayın

```bash
git clone https://github.com/ismailoze/ECommerce.git
cd ECommerce
```

### 2. Veritabanı Bağlantısını Yapılandırın

`ECommerce.API/appsettings.json` dosyasında connection string'i güncelleyin:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=ECommerceDb;Trusted_Connection=true;MultipleActiveResultSets=true"
  }
}
```

### 3. Paketleri Yükleyin

```bash
dotnet restore
```

### 4. Veritabanını Oluşturun

```bash
dotnet ef database update --project ECommerce.Infrastructure --startup-project ECommerce.API
```

### 5. Uygulamayı Çalıştırın

```bash
dotnet run --project ECommerce.API
```

Uygulama `https://localhost:7047` adresinde çalışacaktır.

## 📚 API Dokümantasyonu

### Swagger UI

Uygulama çalıştıktan sonra Swagger UI'ya erişin:

- **Swagger UI**: `https://localhost:7047/swagger`
- **OpenAPI JSON**: `https://localhost:7047/swagger/v1/swagger.json`

### Ana Endpoint'ler

#### 🔐 Authentication

- `POST /api/auth/register` - Kullanıcı kaydı
- `POST /api/auth/login` - Giriş yapma
- `POST /api/auth/refresh-token` - Token yenileme
- `POST /api/auth/logout` - Çıkış yapma
- `POST /api/auth/forgot-password` - Şifre sıfırlama
- `POST /api/auth/reset-password` - Şifre yenileme

#### 👥 Users

- `GET /api/users` - Kullanıcıları listele
- `GET /api/users/{id}` - Kullanıcı detayı
- `PUT /api/users/{id}/profile` - Profil güncelle
- `PUT /api/users/{id}/password` - Şifre değiştir
- `GET /api/users/{id}/addresses` - Kullanıcı adresleri
- `POST /api/users/{id}/addresses` - Adres ekle

#### 🛍️ Products

- `GET /api/products` - Ürünleri listele (filtreleme, arama)
- `GET /api/products/{id}` - Ürün detayı
- `POST /api/products` - Ürün oluştur
- `PUT /api/products/{id}` - Ürün güncelle
- `DELETE /api/products/{id}` - Ürün sil
- `GET /api/products/{id}/reviews` - Ürün değerlendirmeleri

#### 📂 Categories

- `GET /api/categories` - Kategorileri listele
- `GET /api/categories/{id}` - Kategori detayı
- `GET /api/categories/{id}/subcategories` - Alt kategoriler
- `POST /api/categories` - Kategori oluştur
- `PUT /api/categories/{id}` - Kategori güncelle
- `DELETE /api/categories/{id}` - Kategori sil

#### 🛒 Orders

- `GET /api/orders` - Siparişleri listele (Admin)
- `GET /api/orders/my-orders` - Kullanıcının siparişleri
- `GET /api/orders/{id}` - Sipariş detayı
- `POST /api/orders` - Sipariş oluştur
- `PUT /api/orders/{id}/status` - Sipariş durumu güncelle

#### 🛒 Cart (Sepet)

- `GET /api/cart` - Sepeti getir
- `POST /api/cart/add` - Sepete ürün ekle
- `PUT /api/cart/update` - Sepet ürünü güncelle
- `DELETE /api/cart/remove` - Sepetten ürün çıkar
- `DELETE /api/cart/clear` - Sepeti temizle

#### 💳 Payments

- `POST /api/payments` - Ödeme oluştur
- `POST /api/payments/verify-3d-secure` - 3D Secure doğrula
- `GET /api/payments/{id}/status` - Ödeme durumu
- `POST /api/payments/{id}/cancel` - Ödeme iptal et
- `POST /api/payments/{id}/refund` - Ödeme iade et
- `POST /api/payments/webhook` - Webhook (Iyzico)

#### 🎁 Coupons

- `GET /api/coupons` - Kuponları listele
- `GET /api/coupons/{code}` - Kupon detayı
- `POST /api/coupons/validate` - Kupon doğrula
- `POST /api/coupons` - Kupon oluştur (Admin)
- `PUT /api/coupons/{id}` - Kupon güncelle (Admin)

#### ⭐ Product Reviews

- `GET /api/product-reviews` - Değerlendirmeleri listele
- `GET /api/product-reviews/{id}` - Değerlendirme detayı
- `POST /api/product-reviews` - Değerlendirme oluştur
- `PUT /api/product-reviews/{id}` - Değerlendirme güncelle
- `DELETE /api/product-reviews/{id}` - Değerlendirme sil

#### 📋 Wishlists

- `GET /api/wishlists` - İstek listelerini getir
- `POST /api/wishlists` - İstek listesi oluştur
- `POST /api/wishlists/{id}/items` - Ürün ekle
- `DELETE /api/wishlists/{id}/items/{itemId}` - Ürün çıkar
- `GET /api/wishlists/{id}/stats` - İstatistikler

#### 📦 Inventory

- `GET /api/inventory` - Stok durumunu getir
- `POST /api/inventory/stock-in` - Stok girişi
- `POST /api/inventory/stock-out` - Stok çıkışı
- `GET /api/inventory/movements` - Stok hareketleri
- `GET /api/inventory/alerts` - Stok uyarıları

#### 📧 Email

- `POST /api/email/send` - Email gönder
- `GET /api/email/templates` - Email şablonları
- `POST /api/email/templates` - Email şablonu oluştur

#### 🔔 Notifications

- `GET /api/notifications` - Bildirimleri listele
- `GET /api/notifications/{id}` - Bildirim detayı
- `POST /api/notifications` - Bildirim oluştur
- `PUT /api/notifications/{id}/read` - Bildirimi okundu olarak işaretle
- `DELETE /api/notifications/{id}` - Bildirimi sil
- `GET /api/notifications/templates` - Bildirim şablonları
- `POST /api/notifications/templates` - Bildirim şablonu oluştur

#### 🔑 Permissions

- `GET /api/permissions` - Yetkileri listele
- `GET /api/permissions/roles/{roleId}` - Rol yetkileri
- `POST /api/permissions/roles/assign` - Role yetki ata
- `GET /api/permissions/users/{userId}` - Kullanıcı yetkileri

#### 📁 File Upload

- `POST /api/files/upload` - Dosya yükle
- `DELETE /api/files/{id}` - Dosya sil
- `GET /api/files/{id}` - Dosya indir

## 💻 Kullanım

### 1. Kullanıcı Kaydı

```bash
curl -X POST "https://localhost:7047/api/auth/register" \
  -H "Content-Type: application/json" \
  -d '{
    "email": "user@example.com",
    "password": "Password123!",
    "firstName": "John",
    "lastName": "Doe"
  }'
```

### 2. Giriş Yapma

```bash
curl -X POST "https://localhost:7047/api/auth/login" \
  -H "Content-Type: application/json" \
  -d '{
    "email": "user@example.com",
    "password": "Password123!"
  }'
```

### 3. Ürün Oluşturma (JWT Token ile)

```bash
curl -X POST "https://localhost:7047/api/products" \
  -H "Authorization: Bearer YOUR_JWT_TOKEN" \
  -H "Content-Type: application/json" \
  -d '{
    "name": "Örnek Ürün",
    "description": "Ürün açıklaması",
    "sku": "PRD-001",
    "price": 99.99,
    "stockQuantity": 100,
    "categoryId": "category-guid"
  }'
```

### 4. Sepete Ürün Ekleme

```bash
curl -X POST "https://localhost:7047/api/cart/add" \
  -H "Authorization: Bearer YOUR_JWT_TOKEN" \
  -H "Content-Type: application/json" \
  -d '{
    "productId": "product-guid",
    "quantity": 2
  }'
```

### 5. Sipariş Oluşturma

```bash
curl -X POST "https://localhost:7047/api/orders" \
  -H "Authorization: Bearer YOUR_JWT_TOKEN" \
  -H "Content-Type: application/json" \
  -d '{
    "shippingAddressId": "address-guid",
    "billingAddressId": "address-guid",
    "orderItems": [
      {
        "productId": "product-guid",
        "quantity": 2,
        "discountAmount": 10.00
      }
    ],
    "shippingCost": 15.00,
    "taxAmount": 20.00,
    "discountAmount": 10.00,
    "paymentMethod": "CreditCard"
  }'
```

### 6. Ödeme İşlemi

```bash
curl -X POST "https://localhost:7047/api/payments" \
  -H "Authorization: Bearer YOUR_JWT_TOKEN" \
  -H "Content-Type: application/json" \
  -d '{
    "orderId": "order-guid",
    "paymentMethod": "CreditCard",
    "cardNumber": "5555444433332222",
    "cardHolderName": "John Doe",
    "expiryMonth": "12",
    "expiryYear": "2025",
    "cvv": "123"
  }'
```

### 7. Kupon Doğrulama

```bash
curl -X POST "https://localhost:7047/api/coupons/validate" \
  -H "Authorization: Bearer YOUR_JWT_TOKEN" \
  -H "Content-Type: application/json" \
  -d '{
    "couponCode": "WELCOME10",
    "orderAmount": 100.00
  }'
```

### 8. Ürün Değerlendirmesi

```bash
curl -X POST "https://localhost:7047/api/product-reviews" \
  -H "Authorization: Bearer YOUR_JWT_TOKEN" \
  -H "Content-Type: application/json" \
  -d '{
    "productId": "product-guid",
    "rating": 5,
    "title": "Harika ürün!",
    "comment": "Çok memnun kaldım, tavsiye ederim."
  }'
```

### 9. Wishlist'e Ürün Ekleme

```bash
curl -X POST "https://localhost:7047/api/wishlists/items" \
  -H "Authorization: Bearer YOUR_JWT_TOKEN" \
  -H "Content-Type: application/json" \
  -d '{
    "wishlistId": "wishlist-guid",
    "productId": "product-guid"
  }'
```

### 10. Email Gönderme

```bash
curl -X POST "https://localhost:7047/api/email/send" \
  -H "Authorization: Bearer YOUR_JWT_TOKEN" \
  -H "Content-Type: application/json" \
  -d '{
    "to": "customer@example.com",
    "subject": "Sipariş Onayı",
    "templateName": "OrderConfirmation",
    "templateData": {
      "orderNumber": "ORD-001",
      "customerName": "John Doe",
      "totalAmount": 125.00
    }
  }'
```

### 11. Bildirim Oluşturma

```bash
curl -X POST "https://localhost:7047/api/notifications" \
  -H "Authorization: Bearer YOUR_JWT_TOKEN" \
  -H "Content-Type: application/json" \
  -d '{
    "userId": "user-guid",
    "title": "Yeni Sipariş",
    "message": "Siparişiniz başarıyla oluşturuldu.",
    "type": "OrderCreated",
    "priority": "Medium",
    "data": {
      "orderId": "order-guid",
      "orderNumber": "ORD-001"
    }
  }'
```

### 12. SignalR Hub Bağlantısı (JavaScript)

```javascript
// SignalR hub'a bağlan
const connection = new signalR.HubConnectionBuilder()
  .withUrl("/notificationHub")
  .build();

// Bağlantıyı başlat
connection
  .start()
  .then(function () {
    console.log("SignalR bağlantısı kuruldu");
  })
  .catch(function (err) {
    console.error("SignalR bağlantı hatası: " + err.toString());
  });

// Bildirim dinle
connection.on("ReceiveNotification", function (notification) {
  console.log("Yeni bildirim:", notification);
  // Bildirimi UI'da göster
  showNotification(notification);
});
```

## 🔍 OpenSearch Entegrasyonu

### OpenSearch Kurulumu

```bash
# Docker Compose ile OpenSearch başlatın
docker-compose -f docker-compose.opensearch.yml up -d
```

### OpenSearch Dashboard

- **URL**: `http://localhost:5601`
- **Log Index**: `ecommerce-logs-*`
- **Template**: `ecommerce-logs-template`

### Log Görüntüleme

1. OpenSearch Dashboard'ı açın
2. "Discover" sekmesine gidin
3. `ecommerce-logs-*` index'ini seçin
4. Logları filtreleyin ve analiz edin

Detaylı kurulum için [OPENSEARCH_SETUP.md](OPENSEARCH_SETUP.md) dosyasını inceleyin.

## 🐳 Docker Kurulumu

### OpenSearch ve Dashboard

```bash
# OpenSearch servislerini başlat
docker-compose -f docker-compose.opensearch.yml up -d

# Servisleri durdur
docker-compose -f docker-compose.opensearch.yml down
```

### Servisler

- **OpenSearch**: `http://localhost:9200`
- **OpenSearch Dashboard**: `http://localhost:5601`
- **Logstash** (opsiyonel): `http://localhost:5044`

## 🧪 Test

### Unit Testleri Çalıştırma

```bash
dotnet test
```

### Test Coverage

```bash
dotnet test --collect:"XPlat Code Coverage"
```

### Rate Limiting Testleri

#### PowerShell ile Rate Limiting Testi

```powershell
# Rate limiting test script'ini çalıştır
.\test_rate_limiting.ps1
```

Bu script aşağıdaki testleri gerçekleştirir:

- Health check testi
- Rate limiting testi (10 istek)
- Auth login rate limiting testi
- Input validation testi (XSS koruması)
- Security headers kontrolü

#### VS Code Postman Extension ile Test

1. VS Code'da Postman extension'ını yükleyin
2. [VS_Code_Postman_Setup.md](VS_Code_Postman_Setup.md) dosyasındaki adımları takip edin
3. Collection'ı çalıştırarak rate limiting testlerini gerçekleştirin

#### Manuel Test Örnekleri

```bash
# Rate limiting testi - 10 ardışık istek
for i in {1..10}; do
  curl -X GET "https://localhost:7047/health"
  echo "Request $i completed"
done

# Auth rate limiting testi
for i in {1..6}; do
  curl -X POST "https://localhost:7047/api/auth/login" \
    -H "Content-Type: application/json" \
    -d '{"email":"test@example.com","password":"WrongPassword123!"}'
  echo "Auth request $i completed"
done

# XSS koruması testi
curl -X POST "https://localhost:7047/api/auth/login" \
  -H "Content-Type: application/json" \
  -d '{"email":"test@example.com","password":"<script>alert(\"xss\")</script>Password123!"}'
```

### Security Headers Testi

```bash
# Security headers kontrolü
curl -I "https://localhost:7047/health"

# Beklenen headers:
# X-Content-Type-Options: nosniff
# X-Frame-Options: DENY
# X-XSS-Protection: 1; mode=block
# Content-Security-Policy: default-src 'self'...
# Referrer-Policy: strict-origin-when-cross-origin
```

## 📊 Monitoring ve Logging

### Health Checks

- **Health Check**: `GET /health`
- **OpenSearch Health**: OpenSearch bağlantı durumu

### Log Seviyeleri

- **Information**: Genel bilgi logları
- **Warning**: Uyarı logları
- **Error**: Hata logları
- **Critical**: Kritik hata logları

### Structured Logging

Tüm loglar JSON formatında OpenSearch'e gönderilir:

```json
{
  "timestamp": "2025-01-12T10:30:00Z",
  "level": "Information",
  "message": "User login successful",
  "userId": "user-guid",
  "email": "user@example.com",
  "sourceContext": "ECommerce.Application.Features.Auth.Commands.Login.LoginCommandHandler"
}
```

## 🔧 Yapılandırma

### appsettings.json

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=ECommerceDb;Trusted_Connection=true;MultipleActiveResultSets=true"
  },
  "Jwt": {
    "SecretKey": "ECommerce_Super_Secret_Key_That_Should_Be_At_Least_32_Characters_Long_For_Security",
    "Issuer": "ECommerce.API",
    "Audience": "ECommerce.Users",
    "AccessTokenExpirationMinutes": 60,
    "RefreshTokenExpirationDays": 7,
    "ClockSkewMinutes": 5
  },
  "FileUpload": {
    "UploadPath": "wwwroot/uploads",
    "WebBasePath": "/uploads",
    "MaxFileSize": 5242880,
    "AllowedFileTypes": [
      "image/jpeg",
      "image/jpg",
      "image/png",
      "image/gif",
      "image/webp"
    ],
    "AllowedExtensions": [".jpg", ".jpeg", ".png", ".gif", ".webp"],
    "ValidFileNamePattern": "^[a-zA-Z0-9._-]+$",
    "MaxFileNameLength": 100
  },
  "Cache": {
    "Enabled": true,
    "DefaultExpirationMinutes": 30,
    "MaxSizeMB": 100,
    "CleanupIntervalMinutes": 5,
    "ProductCacheMinutes": 15,
    "CategoryCacheMinutes": 30,
    "UserCacheMinutes": 10,
    "OrderCacheMinutes": 5
  },
  "Serilog": {
    "MinimumLevel": "Debug",
    "EnableConsoleSink": true,
    "EnableFileSink": true,
    "LogFilePath": "logs/ecommerce-.log",
    "FileSizeLimitMB": 100,
    "RetainedFileCountLimit": 10,
    "RollingInterval": "Day",
    "UseJsonFormat": true,
    "EnableStructuredLogging": true,
    "EnableRequestLogging": true,
    "EnablePerformanceLogging": true,
    "EnableSensitiveDataMasking": true
  },
  "OpenTelemetry": {
    "Enabled": true,
    "ServiceName": "ECommerce.API",
    "ServiceVersion": "1.0.0",
    "ServiceNamespace": "ECommerce",
    "OtlpEndpoint": null,
    "EnableConsoleExporter": true,
    "EnableAspNetCoreInstrumentation": true,
    "EnableEntityFrameworkCoreInstrumentation": true,
    "EnableHttpClientInstrumentation": true,
    "EnableSqlClientInstrumentation": true,
    "SamplingRatio": 1.0,
    "MaxActivitiesPerSecond": 1000,
    "MaxEventsPerActivity": 100,
    "MaxLinksPerActivity": 100,
    "MaxAttributesPerActivity": 1000
  },
  "OpenSearch": {
    "Enabled": true,
    "NodeUris": ["http://localhost:9200"],
    "IndexFormat": "ecommerce-logs-{0:yyyy.MM.dd}",
    "IndexTemplateName": "ecommerce-logs-template",
    "Username": null,
    "Password": null,
    "ApiKey": null,
    "CertificateFingerprint": null,
    "VerifySsl": true,
    "ConnectionTimeoutSeconds": 30,
    "RequestTimeoutSeconds": 60,
    "BatchSize": 1000,
    "BatchPostingIntervalSeconds": 2,
    "QueueSizeLimit": 10000,
    "AutoRegisterTemplate": true,
    "TemplateLifetimeDays": 30,
    "NumberOfShards": 1,
    "NumberOfReplicas": 0,
    "IndexRefreshInterval": "5s",
    "EnableIndexLifecycleManagement": true,
    "IndexRetentionDays": 30,
    "BufferSizeMB": 10,
    "FlushIntervalSeconds": 5,
    "RetryCount": 3,
    "RetryDelaySeconds": 1,
    "EnableDeadLetterQueue": true,
    "DeadLetterQueuePath": "logs/dead-letter-queue",
    "CustomFields": {
      "Application": "ECommerce.API",
      "Environment": "Development",
      "Version": "1.0.0"
    }
  },
  "PaymentGateway": {
    "Iyzico": {
      "ApiKey": "sandbox-your-api-key",
      "SecretKey": "sandbox-your-secret-key",
      "BaseUrl": "https://sandbox-api.iyzipay.com",
      "IsTestMode": true
    }
  },
  "Email": {
    "Smtp": {
      "Host": "smtp.gmail.com",
      "Port": 587,
      "Username": "your-email@gmail.com",
      "Password": "your-app-password",
      "EnableSsl": true
    },
    "From": {
      "Email": "noreply@yourapp.com",
      "Name": "E-Commerce"
    }
  },
  "RateLimiting": {
    "EnableRateLimiting": true,
    "GeneralRules": [
      {
        "Endpoint": "*",
        "Period": "1m",
        "Limit": 100
      },
      {
        "Endpoint": "*",
        "Period": "1h",
        "Limit": 1000
      }
    ],
    "AuthRules": [
      {
        "Endpoint": "POST:/api/auth/login",
        "Period": "1m",
        "Limit": 5
      },
      {
        "Endpoint": "POST:/api/auth/register",
        "Period": "1m",
        "Limit": 3
      },
      {
        "Endpoint": "POST:/api/auth/forgot-password",
        "Period": "1h",
        "Limit": 3
      }
    ],
    "ApiRules": [
      {
        "Endpoint": "POST:/api/products",
        "Period": "1m",
        "Limit": 10
      },
      {
        "Endpoint": "PUT:/api/products/*",
        "Period": "1m",
        "Limit": 10
      },
      {
        "Endpoint": "DELETE:/api/products/*",
        "Period": "1m",
        "Limit": 5
      }
    ]
  }
}
```

## 🚀 Deployment

### Production Ortamı

1. `appsettings.Production.json` oluşturun
2. Connection string'i güncelleyin
3. JWT secret key'i güvenli bir değerle değiştirin
4. OpenSearch yapılandırmasını güncelleyin

### Docker ile Deployment

```dockerfile
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["ECommerce.API/ECommerce.API.csproj", "ECommerce.API/"]
COPY ["ECommerce.Application/ECommerce.Application.csproj", "ECommerce.Application/"]
COPY ["ECommerce.Infrastructure/ECommerce.Infrastructure.csproj", "ECommerce.Infrastructure/"]
COPY ["ECommerce.Domain/ECommerce.Domain.csproj", "ECommerce.Domain/"]
RUN dotnet restore "ECommerce.API/ECommerce.API.csproj"
COPY . .
WORKDIR "/src/ECommerce.API"
RUN dotnet build "ECommerce.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ECommerce.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ECommerce.API.dll"]
```

## 🤝 Katkıda Bulunma

1. Fork yapın
2. Feature branch oluşturun (`git checkout -b feature/amazing-feature`)
3. Commit yapın (`git commit -m 'Add some amazing feature'`)
4. Push yapın (`git push origin feature/amazing-feature`)
5. Pull Request oluşturun

### Geliştirme Kuralları

- Clean Code prensiplerini takip edin
- Unit test yazın
- Swagger dokümantasyonunu güncelleyin
- Conventional Commits formatını kullanın

## 📝 Lisans

Bu proje MIT lisansı altında lisanslanmıştır. Detaylar için [LICENSE](LICENSE) dosyasına bakın.

## 👨‍💻 Geliştirici

**İsmail Özer**

- GitHub: [@ismailoze](https://github.com/ismailoze)
- LinkedIn: [linkedin.com/in/ismail-ozer-07-antalya](https://www.linkedin.com/in/ismail-ozer-07-antalya)
- Email: ismailozer35041@gmail.com

## 🙏 Teşekkürler

- .NET Community
- Clean Architecture advocates
- Open Source contributors

---

⭐ Bu projeyi beğendiyseniz yıldız vermeyi unutmayın!

## 📈 Roadmap

### Gelecek Özellikler

- [x] Payment Gateway entegrasyonu (Iyzico)
- [x] Email notification sistemi (SMTP)
- [x] Kupon sistemi
- [x] Ürün değerlendirme sistemi
- [x] Wishlist (İstek listesi) yönetimi
- [x] Stok yönetimi ve envanter takibi
- [x] OpenSearch entegrasyonu
- [x] Gelişmiş logging ve monitoring
- [x] Real-time notifications (SignalR)
- [x] API rate limiting ve güvenlik özellikleri
- [x] Input validation middleware (XSS koruması)
- [x] Security headers middleware (CSP, HSTS)
- [x] Enhanced OpenTelemetry entegrasyonu
- [x] Gelişmiş OpenSearch yapılandırması
- [ ] Advanced search (Elasticsearch)
- [ ] Multi-language support
- [ ] Mobile API optimizations
- [ ] GraphQL endpoint
- [ ] Microservices architecture
- [ ] Kubernetes deployment
- [ ] Redis caching
- [ ] Message queues (RabbitMQ/Azure Service Bus)
- [ ] Event sourcing
- [ ] CQRS with separate read/write databases

### Versiyon Geçmişi

- **v1.0.0** - İlk sürüm (Clean Architecture, CQRS, JWT Auth)
- **v1.1.0** - Permission sistemi eklendi
- **v1.2.0** - OpenSearch entegrasyonu
- **v1.3.0** - Caching ve performance optimizasyonları
- **v1.4.0** - Payment Gateway (Iyzico) entegrasyonu
- **v1.5.0** - Email sistemi ve SMTP entegrasyonu
- **v1.6.0** - Kupon sistemi ve doğrulama
- **v1.7.0** - Ürün değerlendirme sistemi
- **v1.8.0** - Wishlist yönetimi
- **v1.9.0** - Stok yönetimi ve envanter takibi
- **v2.0.0** - Gelişmiş logging, monitoring ve OpenTelemetry
- **v2.1.0** - Real-time notifications ve SignalR hub
- **v2.2.0** - API Rate Limiting ve güvenlik özellikleri
- **v2.3.0** - Input validation middleware ve security headers
- **v2.4.0** - Enhanced OpenTelemetry ve OpenSearch yapılandırması

### Proje İstatistikleri

- **37+ Domain Entities** - Kapsamlı domain modeli
- **43+ DTOs** - Veri transfer nesneleri
- **16+ Feature Modules** - CQRS modülleri
- **16+ API Endpoint Groups** - RESTful API'ler
- **17+ Enums** - Domain enum'ları
- **6+ External Services** - Harici servis entegrasyonları
- **3+ Security Middleware** - Güvenlik katmanları
- **100% Async/Await** - Asenkron programlama
- **Clean Architecture** - Katmanlı mimari
- **SOLID Principles** - Temiz kod prensipleri

---

**Son güncelleme**: 2025-10-14
Test contribution by alknbugra

