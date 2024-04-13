# Cập nhật
# build 1
- Thêm upload cho locations (test)


# Todo
## Hướng dẫn lấy modal từ CSDL
```powershell
scaffold-dbcontext "Data Source=localhost\sqlexpress;Initial Catalog=CRM_CMC;User ID=sa;Password=123;Encrypt=True;Trust Server Certificate=True" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models -ContextDir Contexts -force
```