# Cập nhật

## build 5
- Tùy biến xác thực người dùng

## build 4
- Thêm chức năng đăng nhập

## build 3
- Thêm chức năng xóa nhiều

## build 2
- Thêm chức năng sort

## build 1
- Thêm upload cho locations (test)


# Todo
## Hướng dẫn lấy modal từ CSDL
```powershell
scaffold-dbcontext "Data Source=localhost\\sqlexpress;Initial Catalog=BACKEND_CRM;User ID=sa;Password=123;Encrypt=True;Trust Server Certificate=True" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models -ContextDir Contexts -force
```