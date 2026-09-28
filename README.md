# HỆ THỐNG QUẢN TRỊ DOANH NGHIỆP TOÀN DIỆN - ACECOOK ERP

Hệ thống ERP đa phân hệ trên nền tảng **C# / Windows Forms (.NET Framework 4.8 & .NET 8)**, kết nối cơ sở dữ liệu điện toán đám mây **Neon PostgreSQL**.

---

## 📂 Cấu Trúc Dự Án

```text
ERP/
├── ERP.sln                 # Solution quản lý toàn bộ 6 phân hệ
├── run_app.bat             # Script 1-click tự động restore, build và chạy ứng dụng
├── ERP_BanHang/
│   ├── ERP_Khach/          # Master Portal điều hướng trung tâm
│   └── ERP_BanHang/        # Phân hệ Bán hàng
├── ERP_NhanSu/             # Phân hệ Quản lý Nhân sự
├── ERPKho/                 # Phân hệ Quản lý Kho hàng
├── ERP_Logistics/          # Phân hệ Logistics & Vận chuyển
├── KE_TOAN_TAI_CHINH/      # Phân hệ Kế toán - Tài chính
└── Goi_Cai_Dat_ERP/        # Bản đóng gói chạy trực tiếp (không cần Visual Studio)
```

---

## 🛠️ Cài Đặt & Thiết Lập

1. **Yêu cầu môi trường:**
   - Hệ điều hành: Windows 10 / 11.
   - Cài đặt **Visual Studio 2022** (chọn workload *.NET desktop development*) hoặc **.NET SDK**.
   - **Internet:** Cần có mạng để truy vấn Database Cloud Neon PostgreSQL (đã cấu hình sẵn trong `App.config`, không cần cài database trên máy).

2. **Tải mã nguồn & Khôi phục thư viện:**
   ```bash
   git clone <URL_REPOSITORY>
   cd ERP
   dotnet restore ERP.sln
   ```

---

## 🚀 Khởi Động Ứng Dụng

- **Cách 1 - Chạy nhanh bằng script (Khuyên dùng):** Nhấp đúp chuột vào file `run_app.bat` (tự động dọn tiến trình cũ, restore thư viện, build và mở ứng dụng).
- **Cách 2 - Chạy bằng Visual Studio:** Mở `ERP.sln` > Chuột phải vào `ERP_Khach` chọn **Set as Startup Project** > Nhấn **F5**.
- **Cách 3 - Chạy bản đóng gói sẵn (Không cần cài công cụ):** Mở thư mục `Goi_Cai_Dat_ERP` > chạy file `ERP_Khach.exe`.

---

## 🔐 Danh Sách Tài Khoản Mẫu

| Phân hệ / Vai trò | Tên đăng nhập | Mật khẩu | Quyền hạn |
| :--- | :--- | :--- | :--- |
| **Quản trị viên** | `admin` | `admin` | Toàn quyền (Tất cả 6 phân hệ) |
| **Kế toán - Tài chính** | `ptha` | `123` | Phân hệ Tài chính |
| **Bán hàng** | `pmduc` | `123` | Phân hệ Bán hàng |
| **Quản lý Kho** | `lmhoang` | `123456` | Phân hệ Kho |
| **Logistics** | `tllan` | `123` | Phân hệ Logistics |
| **Nhân sự** | `ntmai` | `123` | Phân hệ Nhân sự |
