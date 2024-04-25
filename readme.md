# Giới thiệu
- ĐÂY LÀ PROJECT DÙNG ĐỂ KIỂM TRA


# Yêu cầu
1. <img src="https://dotnet.microsoft.com/favicon.ico" style="height: 20px; width:20px;"/> [.Net6](https://dotnet.microsoft.com/en-us/download/dotnet/6.0) > 6.0.28
1. <img src="https://nodejs.org/static/images/logo-hexagon-card.png" style="height: 20px; width:20px;"/> [Nodejs](https://nodejs.org/en/download) > 20.10.0

# Hướng dẫn debug

### Đối với Microsoft Visual Studio 2022
1. Mở visual studio 2022 với `CRM_CMC.sln`
1. Mở Developer PowerShell
1. Vào thư mục frontend `cd frontend`
1. Chạy lệnh `npm i` để cài module
1. Cài đặt chế độ Debug thành `Multiple startup project`
    - Chuột phải vào `solution`
    - Click vào `Configure Startup Project`
    - Tíck vào `Multiple startup project`
1. Launch thì 2 cửa sổ CMD xuất hiện
1. Vào `http://localhost:5173/` để trải nghiệm

### Đối với Visual Studio Code
1. Đặt đúng workspace chứa 2 thư mục project
1. Mở backend
    - Mở 1 cửa sổ terminal
    - Vào thư mục CRM_CMC `cd CRM_CMC`
    - Chạy ứng dụng `dotnet run`
1. Mở frontend
    - Mở 1 cửa sổ terminal
    - Vào thư mục frontend `cd frontend` 
    - Chạy lệnh `npm run dev` để mở frontend ở chế độ dev
1. Vào `http://localhost:5173/` để trải nghiệm

###
* Lưu ý chỉ dùng ở `http://localhost:5173/location`

# Hướng dẫn cho production

## Phía Server
1. Vào Solution 
1. Chuột phải vào project `CRM_CMC`
1. Ấn vào Publish
1. Hiện ra màn hình `CRM_CMC:Publish` -> Ấn Publish
1. Vào đường dẫn [Publish](./CRM_CMC/bin/Release/net6.0/publish/) -> Bản build của bạn ở đây
1. Nhớ vào `appsettings.json` để sửa lại sâu nhé

## Phía client:
1. Mở developer powershell
1. Cd `frontend`
1. Chạy lệnh `npm run build`
1. Copy thư mục `dist` vào [Publish](./CRM_CMC/bin/Release/net6.0/publish/) -> DONE

## 
- Sau khi xong thì bạn có thể zip lại thư mục và giải nén ở đâu tùy ý
- Nếu bạn sử dụng termial để chạy phần mềm:
    1. Để termial ở vị trí thư mục chứa phần mềm
    1. Sử dụng lệnh `dotnet [TÊN PROJECT].dll` dể chạy

