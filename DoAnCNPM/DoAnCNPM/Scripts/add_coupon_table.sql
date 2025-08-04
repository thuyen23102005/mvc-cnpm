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

-- Thêm dữ liệu mẫu
INSERT INTO [dbo].[Coupon] ([Code], [Description], [DiscountType], [DiscountValue], [MinimumOrderValue], [Quantity], [StartDate], [EndDate], [IsActive])
VALUES 
('WELCOME10', N'Giảm 10% cho khách hàng mới', 'Percentage', 10.00, 100000.00, 100, '2024-01-01', '2024-12-31', 1),
('SAVE50K', N'Giảm 50,000 VNĐ cho đơn hàng từ 200,000 VNĐ', 'Fixed', 50000.00, 200000.00, 50, '2024-01-01', '2024-12-31', 1),
('SUMMER20', N'Giảm 20% mùa hè', 'Percentage', 20.00, 150000.00, 200, '2024-06-01', '2024-08-31', 1),
('FREESHIP', N'Miễn phí vận chuyển', 'Fixed', 30000.00, 300000.00, 100, '2024-01-01', '2024-12-31', 1);

-- Tạo index cho Code để tìm kiếm nhanh
CREATE INDEX IX_Coupon_Code ON [dbo].[Coupon] ([Code]); 