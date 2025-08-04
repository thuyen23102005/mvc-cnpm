# Hướng dẫn sử dụng tính năng Mã giảm giá

## Tổng quan
Tính năng mã giảm giá cho phép admin tạo và quản lý các mã giảm giá, khách hàng có thể áp dụng mã khi thanh toán để được giảm giá.

## Cài đặt

### 1. Tạo bảng Coupon trong database
Chạy script SQL trong file `Scripts/add_coupon_table.sql` để tạo bảng và dữ liệu mẫu:

```sql
-- Script tạo bảng Coupon
CREATE TABLE [dbo].[Coupon] (
    [ID] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [Code] NVARCHAR(20) NOT NULL,
    [Description] NVARCHAR(200) NOT NULL,
    [DiscountType] NVARCHAR(20) NOT NULL, -- 'Percentage' hoặc 'Fixed'
    [DiscountValue] DECIMAL(18,2) NOT NULL,
    [MinimumOrderValue] DECIMAL(18,2) NOT NULL,
    [Quantity] INT NOT NULL,
    [UsedQuantity] INT NOT NULL DEFAULT 0,
    [StartDate] DATE NOT NULL,
    [EndDate] DATE NOT NULL,
    [IsActive] BIT NOT NULL DEFAULT 1,
    [CreatedDate] DATETIME NOT NULL DEFAULT GETDATE()
);
```

### 2. Cập nhật Model1.edmx
- Mở file `Models/Model1.edmx`
- Thêm entity Coupon vào model
- Update model từ database

## Tính năng

### Cho Admin (Quản trị viên)

#### 1. Quản lý mã giảm giá
- **URL**: `/UuDai/Index`
- **Chức năng**: 
  - Xem danh sách tất cả mã giảm giá
  - Thêm mã giảm giá mới
  - Chỉnh sửa mã giảm giá
  - Xóa mã giảm giá
  - Theo dõi số lượng sử dụng

#### 2. Thêm mã giảm giá mới
- **URL**: `/UuDai/Create`
- **Thông tin cần nhập**:
  - Mã giảm giá (VD: WELCOME10)
  - Mô tả
  - Loại giảm giá (Phần trăm hoặc Số tiền cố định)
  - Giá trị giảm giá
  - Đơn hàng tối thiểu
  - Số lượng mã
  - Ngày hiệu lực (từ - đến)
  - Trạng thái (kích hoạt/vô hiệu)

#### 3. Chỉnh sửa mã giảm giá
- **URL**: `/UuDai/Edit/{id}`
- Có thể chỉnh sửa tất cả thông tin trừ số lượng đã sử dụng

### Cho Khách hàng

#### 1. Xem mã giảm giá
- **URL**: `/UuDai/UuDai`
- Hiển thị tất cả mã giảm giá đang hoạt động
- Có thể copy mã để sử dụng

#### 2. Áp dụng mã trong giỏ hàng
- **URL**: `/GioHang/Cart`
- Nhập mã giảm giá vào ô "Nhập mã giảm giá"
- Click "Áp dụng" hoặc nhấn Enter
- Hệ thống sẽ kiểm tra và áp dụng giảm giá

## Các loại mã giảm giá

### 1. Giảm giá theo phần trăm
- **Ví dụ**: Giảm 10% cho đơn hàng từ 100,000 VNĐ
- **Cách tính**: Giảm giá = Tổng đơn hàng × 10%

### 2. Giảm giá cố định
- **Ví dụ**: Giảm 50,000 VNĐ cho đơn hàng từ 200,000 VNĐ
- **Cách tính**: Giảm giá = 50,000 VNĐ (cố định)

## Quy tắc áp dụng

1. **Điều kiện áp dụng**:
   - Mã phải còn hiệu lực (trong khoảng thời gian quy định)
   - Mã phải được kích hoạt
   - Còn số lượng mã chưa sử dụng
   - Đơn hàng đạt giá trị tối thiểu

2. **Giới hạn**:
   - Mỗi mã chỉ được sử dụng một lần
   - Giảm giá không vượt quá tổng đơn hàng
   - Mã có thời hạn sử dụng

## API Endpoints

### 1. Validate Coupon
```
POST /UuDai/ValidateCoupon
Parameters: code (string), orderTotal (decimal)
Response: JSON với thông tin mã và giảm giá
```

### 2. Apply Coupon
```
POST /GioHang/ApplyCoupon
Parameters: couponCode (string)
Response: JSON với thông tin áp dụng mã
```

### 3. Remove Coupon
```
POST /GioHang/RemoveCoupon
Response: JSON xác nhận xóa mã
```

## Dữ liệu mẫu

Hệ thống đã có sẵn 4 mã giảm giá mẫu:

1. **WELCOME10**: Giảm 10% cho khách hàng mới
2. **SAVE50K**: Giảm 50,000 VNĐ cho đơn hàng từ 200,000 VNĐ
3. **SUMMER20**: Giảm 20% mùa hè
4. **FREESHIP**: Miễn phí vận chuyển

## Lưu ý kỹ thuật

1. **Session Management**: Thông tin mã giảm giá được lưu trong Session
2. **Validation**: Kiểm tra đầy đủ điều kiện trước khi áp dụng
3. **Database**: Tự động cập nhật số lượng sử dụng khi thanh toán
4. **UI/UX**: Giao diện thân thiện với người dùng, có thông báo rõ ràng

## Troubleshooting

### Lỗi thường gặp:

1. **"Mã giảm giá không tồn tại"**
   - Kiểm tra mã đã nhập đúng chưa
   - Kiểm tra mã có trong database không

2. **"Mã giảm giá đã hết hạn"**
   - Kiểm tra ngày hiệu lực của mã
   - Cập nhật ngày hiệu lực nếu cần

3. **"Đơn hàng chưa đạt giá trị tối thiểu"**
   - Thêm sản phẩm vào giỏ hàng
   - Hoặc tạo mã với giá trị tối thiểu thấp hơn

## Bảo mật

1. **Validation**: Kiểm tra đầy đủ điều kiện ở cả client và server
2. **Anti-forgery**: Sử dụng token để chống CSRF
3. **Authorization**: Chỉ admin mới có quyền quản lý mã giảm giá
4. **Rate Limiting**: Giới hạn số lần thử mã để tránh spam 