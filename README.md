# 🚀 .NET Minimal API – Katmanlı Mimari Örnek Proje

Bu proje, **katmanlı mimarinin (Layered Architecture) en iyi uygulamalarını** takip ederek geliştirilmiş, modern bir **.NET Minimal API** uygulamasıdır. Amaç; sürdürülebilir, test edilebilir, genişletilebilir ve temiz bir backend mimarisi ortaya koymaktır.

## 🧱 Mimari Yapı

Proje klasik **Layered Architecture** yaklaşımını izler:

```
📦 NTierArchitecture.WebAPI - Modules - 
📦 NTierArchitecture.Business - Business Rules, Services-
📦 NTierArchitecture.DataAccess - Context - 
📦 NTierArchitecture.Entity  - Entites, Enums -
```

Katmanlar birbirine **yalnızca abstraction (interface / contract)** üzerinden bağlıdır.

---

## ✨ Kullanılan Teknolojiler

* **.NET 8 / ASP.NET Core Minimal API**
* **Entity Framework Core**
* **Microsoft Identity**
* **JWT (JSON Web Token)** Authentication
* **FluentValidation**
* **Mapster** (Object Mapping)
* **SQL Server** (EF Core uyumlu)
* **Ts.Result** For Result Pattern


---

## 🧩 Uygulanan Tasarım Paternleri


### ✅ Result Pattern

* Exception yerine kontrollü sonuç döndürülmesini sağlar
* Başarılı / başarısız durumları standartlaştırır

```csharp
Result<T>
```

### ✅ Options Pattern

* Konfigürasyonların strongly-typed yönetilmesini sağlar
* Özellikle **JwtOptions** için kullanılmıştır

```csharp
builder.Services.Configure<JwtOptions>(configuration.GetSection("Jwt"));
```

---

## 🔐 Authentication & Authorization

### 🔑 JWT Authentication

* Token tabanlı kimlik doğrulama
* Access Token üzerinden yetkilendirme

### 👤 Microsoft Identity

* Kullanıcı, rol ve yetki yönetimi
* Identity tabloları üzerinden rol kontrolü

---

## 🧠 Dinamik Rol Kontrolü (Endpoint Filter)

Controller yerine **Minimal API + Endpoint Filters** kullanılmıştır.

Aşağıdaki filtre, kullanıcının rolünü **veritabanı üzerinden dinamik olarak** kontrol eder:

```csharp
RequireRoleFromDb("Admin")
```

### Özellikler:

* Token doğrulaması
* UserId üzerinden rol kontrolü
* Rol isimleri hard-coded değildir
* Veritabanı merkezli yetkilendirme

Bu yaklaşım klasik `[Authorize(Roles = "Admin")]` kullanımına göre daha esnektir.

---

## ✅ Validation (FluentValidation + Endpoint Filter)

Request doğrulama işlemleri **endpoint filter** üzerinden merkezi olarak yapılır.

### Avantajları:

* Controller / endpoint içi validation karmaşası yok
* Tüm validation hataları tek noktadan yönetilir
* Custom exception ile global error handling uyumlu

```csharp
ValidationFilter<T>
```

Validation hataları şu formatta döner:

```json
{
  "Name": ["Name is required"]
}
```

---

## 🗺️ Mapster Kullanımı

DTO ↔ Entity dönüşümleri için **Mapster** tercih edilmiştir.

### Neden Mapster?

* AutoMapper'a göre daha performanslı
* Minimal konfigürasyon
* Clean code yaklaşımına uygun

```csharp
Product product = request.Adapt<Product>();
```

---

## 📦 Product Service (Örnek İş Katmanı)

ProductService, iş kurallarının **Application katmanında** nasıl konumlandığını gösterir.

### İçerdiği Özellikler:

* Duplicate kayıt kontrolü
* Soft / Hard delete uyumu
* Pagination
* Çoklu tablo join işlemleri
* Result Pattern kullanımı

```csharp
Task<Result<ProductResultDto>>
```

Tüm business logic **endpointlerden tamamen izole edilmiştir**.

---

## 📄 Pagination & Search

* Generic pagination extension
* Search + OrderBy desteği
* Büyük veri setleri için optimize sorgular

```csharp
Pagination(request, token)
```

---

## 🛡️ Global Exception Handling

* ValidationException
* ArgumentException
* Yetkisiz erişim hataları

Tüm hatalar **tek bir middleware** üzerinden yönetilir.

---

## 🎯 Projenin Kazandırdıkları

✔ Clean Architecture bilinci
✔ Minimal API ileri seviye kullanımı
✔ Identity + JWT entegrasyonu
✔ Gerçek hayata uygun rol yönetimi
✔ Genişletilebilir ve ölçeklenebilir yapı

---

## 🧪 Geliştirilebilir Alanlar

* Refresh Token mekanizması
* Rate Limiting
* Caching (Redis)
* Integration / Unit Tests
* CQRS & MediatR

---

## 👨‍💻 Geliştirici

**Ömer Üren**
.NET Backend Developer

---

📌 *Bu proje, kurumsal backend mimarileri için referans alınabilecek örnek bir çalışmadır.*
