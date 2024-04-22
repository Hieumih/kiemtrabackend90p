# ToDo
1. Tối ưu chức năng `delete`
1. Tách thành phần của page `Location` thành 1 thành phần riêng
1. Thêm tìm kiếm vào selection trong trường hợp danh sách dài
1. Thêm hoạt ảnh loading
1. Tối ưu hoạt ảnh cho toast! `<TransitionGroup>`

# Công cụ chỉ ở chế độ DEV
- byPassLogin: skip Login vào giao diện chính
    - Không thể gọi một số API vì chưa có token


# Chỉnh sửa gần đây

## build 10
1. Thay đổi một số icon
1. Sửa lỗi:
    - Dashboard: `Uncaught (in promise) TypeError: Cannot set properties of null (setting '__draggable_context')`

## build 10
1. BETA: Thêm trang lịch
    - Thêm giao diện cho trang lịch
    - Chưa có thông tin gửi đến server
    - Có thể thêm lịch bằng cách ấn vào ô lịch hoặc di chuột vào đó

## build 9
1. Thêm tính năng đăng nhập

## build 8
1. BETA: Đăng nhập
1. Thêm animation loading dành cho một số page
1. Chặn một số route nếu người dùng đã đăng nhập hoặc chưa đăng nhập

## build 7
1. Thêm giao diện đăng nhập
1. Thêm Hoạt ảnh cho router
1. BETA: Loading

## build 6
1. Thêm chức năng xóa nhiều

## build 5
1. Thêm chức năng sort cho bảng

## build 4
1. Thêm Thông báo `toast`
    - Có 4 loại `warn`, `info`, `error`, `success`
    - Có thể thông báo qua các router sử dụng `mitt`

## build 3
1. Sửa 1 số lỗi
1. Tối ưu hóa chức năng `add`, `delete`
    - Nếu user save hoặc edit thì nút `Save` sẽ `disabled` kích hoạt trở lại sau khi thực hiện xong event

## build 2
1. Sửa 1 số lỗi
1. Tối ưu hóa `import`
    - Thêm 1 định dạng `xlsx` file cho import
1. Thêm chức năng `export`
    - Có thể sửa tên file
    - Có thể chọn 2 định dạng `csv` và `xlsx`

## build 1
1. Sửa lỗi `modal` không hiện đúng
1. Thêm chức năng sửa `delete`
1. Tối ưu hóa router
1. Thêm chức năng `refresh`
1. Thêm chức năng `import` chấp nhập csv
    - Thêm `inputModal`