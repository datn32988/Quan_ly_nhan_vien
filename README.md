# 👥 Hệ Thống Quản Lý Nhân Viên (Employee Management System)

Dự án Backend quản lý nhân sự được xây dựng trên nền tảng **.NET Core Web API**, tuân thủ nghiêm ngặt nguyên lý **Clean Architecture** nhằm đảm bảo tính linh hoạt, dễ bảo trì và dễ mở rộng.

---

## 🏗 Kiến Trúc Hệ Thống (Clean Architecture)

Dự án được chia thành các tầng độc lập, đảm bảo nguyên tắc tách biệt trách nhiệm (Separation of Concerns):

### 🔹 Domain Layer
- Chứa Entities, Enums, Value Objects
- Định nghĩa Interfaces (Repository, UnitOfWork)
- Không phụ thuộc vào bất kỳ framework nào
- Là trung tâm của hệ thống

### 🔹 Application Layer
- Chứa Business Logic (Use Cases)
- DTOs
- Mapping Profiles (AutoMapper)
- Interfaces cho Services
- FluentValidation xử lý validate dữ liệu

### 🔹 Infrastructure Layer
- Triển khai Repository Pattern & Unit of Work
- Kết nối Database bằng Entity Framework Core
- Quản lý Migrations
- Tích hợp các dịch vụ bên ngoài (Email, File System...)

### 🔹 WebAPI (Presentation Layer)
- Controllers
- Middleware
- Swagger
- Dependency Injection
- Entry Point (Program.cs)

---

## 🛠 Công Nghệ Sử Dụng

- **Framework:** .NET 6.0 / .NET 8.0
- **Database:** Microsoft SQL Server
- **ORM:** Entity Framework Core (Code First)
- **Pattern:** Repository Pattern & Unit of Work
- **Mapping:** AutoMapper
- **Validation:** FluentValidation
- **Documentation:** Swagger UI

---

## 📌 Tính Năng Chính

- ✅ Quản lý Nhân viên (CRUD nâng cao)
- ✅ Quản lý Phòng ban
- ✅ Quản lý Chức vụ
- ✅ Xử lý nghiệp vụ tập trung tại tầng Application
- ✅ API chuẩn RESTful
- ✅ Dễ dàng tích hợp Frontend (React, Angular, Vue)

---

## 🚀 Hướng Dẫn Cài Đặt

### 1️⃣ Yêu cầu hệ thống

- .NET SDK 6.0 hoặc 8.0
- SQL Server
- SQL Server Management Studio (SSMS)
- Visual Studio hoặc VS Code

---

### 2️⃣ Clone Repository

```bash
git clone https://github.com/datn32988/Quan_ly_nhan_vien.git
cd Quan_ly_nhan_vien
