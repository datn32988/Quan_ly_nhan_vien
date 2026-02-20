# 👥 Hệ Thống Quản Lý Nhân Viên (Employee Management System)

Dự án Backend phục vụ quản lý nhân sự, được xây dựng trên nền tảng **.NET Core Web API**. Đây là dự án thuộc chương trình Thực tập tốt nghiệp (TTDN).

---

## 🛠 Công Nghệ Sử Dụng
* **Framework:** .NET 6.0 / 8.0 (C#)
* **Database:** Microsoft SQL Server
* **ORM:** Entity Framework Core (EF Core)
* **Architecture:** Clean Architecture / Repository Pattern
* **Authentication:** JWT Bearer (Tùy chọn)
* **API Documentation:** Swagger / OpenID

---

## 📌 Tính Năng Chính
- [x] **Quản lý Nhân viên:** Thêm mới, cập nhật thông tin, xóa và danh sách nhân viên.
- [x] **Quản lý Phòng ban:** Tổ chức sơ đồ nhân sự theo phòng ban.
- [x] **Tìm kiếm & Lọc:** Tìm kiếm nhân viên theo tên, mã nhân viên hoặc bộ phận.
- [x] **Phân quyền:** Quản lý quyền hạn truy cập hệ thống (Admin/User).

---

## 🚀 Hướng Dẫn Cài Đặt

### 1. Yêu cầu hệ thống
* [.NET SDK](https://dotnet.microsoft.com/download)
* [SQL Server Management Studio (SSMS)](https://learn.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms)
* Visual Studio 2022 hoặc VS Code

### 2. Cấu hình Database
Do file `appsettings.json` đã được đưa vào `.gitignore` để bảo mật, bạn cần tạo một file `appsettings.json` mới tại thư mục gốc của project với nội dung sau:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=YOUR_SERVER_NAME;Database=QuanLyNhanVienDB;Trusted_Connection=True;TrustServerCertificate=True"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}
3. Khởi chạy ứng dụng
Mở PowerShell hoặc Terminal tại thư mục dự án và chạy các lệnh:

PowerShell
# Khôi phục các thư viện NuGet
dotnet restore

# Cập nhật cơ sở dữ liệu (Migrations)
dotnet ef database update

# Chạy ứng dụng
dotnet run
📂 Cấu trúc thư mục tiêu biểu
Controllers/: Tiếp nhận và xử lý các yêu cầu HTTP.

Models/: Định nghĩa cấu trúc dữ liệu và thực thể (Entities).

Data/: Chứa DbContext và các cấu hình liên quan đến SQL Server.

Services/: Nơi xử lý các logic nghiệp vụ (Business Logic).

DTOs/: (Data Transfer Objects) Các đối tượng chuyển đổi dữ liệu giữa Client và Server.

📝 Thông tin tác giả
Tên dự án: Quản Lý Nhân Viên - TTDN Backend

Repository: datn32988/Quan_ly_nhan_vien

Cảm ơn bạn đã quan tâm đến dự án!


---

### Các lệnh để đẩy file này lên GitHub ngay lập tức:
Sau khi lưu file `README.md`, bạn quay lại PowerShell và chạy:

1. `git add README.md`
2. `git commit -m "Update professional README.md"`
3. `git push origin main`

**Bạn có muốn tôi bổ sung thêm bảng mô tả các API (Endpoints) cụ thể vào file README
