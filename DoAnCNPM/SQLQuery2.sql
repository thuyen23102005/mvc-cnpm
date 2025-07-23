-- Bảng AdminUser
CREATE TABLE [dbo].[AdminUser] (
    [ID]           INT IDENTITY (1, 1) NOT NULL,
    [UserName]     NVARCHAR (50) NULL,
    [RoleUser]     NVARCHAR (50) NULL,
    [PasswordUser] NCHAR (50)     NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC)
);

--Bang Category
CREATE TABLE [dbo].[Category] (
    [IDCate]   INT IDENTITY (1, 1) NOT NULL,
    [NameCate] NVARCHAR (50) NULL,
    PRIMARY KEY CLUSTERED ([IDCate] ASC)
);
--Bang Customer
CREATE TABLE [dbo].[Customer] (
    [IDCus]    INT  IDENTITY (1, 1) NOT NULL,
    [NameCus]  NVARCHAR (50) NULL,
    [PhoneCus] NVARCHAR (15)  NULL,
    [EmailCus] NVARCHAR (50) NULL,
    [UserName] varchar(50),
    [Password] nvarchar(50),
    PRIMARY KEY CLUSTERED ([IDCus] ASC)
);

--Bang Products
CREATE TABLE [dbo].[Product] (
    [ProductID]     INT		   IDENTITY (1, 1) NOT NULL,
    [NamePro]       NVARCHAR (50)  NULL,
    [DecriptionPro] NVARCHAR (50)  NULL,
    [CateID]        INT      NULL,
    [Price]         DECIMAL (18, 2) NULL,
    [ImagePro]      NVARCHAR (50)   NULL,
    PRIMARY KEY CLUSTERED ([ProductID] ASC),
    CONSTRAINT [FK_Pro_Category] FOREIGN KEY ([CateID]) REFERENCES [dbo].[Category] ([IDCate])
);
--Bang OrderPro
CREATE TABLE [dbo].[OrderPro] (
    [ID]               INT            IDENTITY (1, 1) NOT NULL,
    [DateOrder]        DATE           NULL,
    [IDCus]            INT            NULL,
    [AddressDeliverry] NVARCHAR (50) NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC),
    FOREIGN KEY ([IDCus]) REFERENCES [dbo].[Customer] ([IDCus])
);

--Bang OrderDetail
CREATE TABLE [dbo].[OrderDetail] (
    [ID]        INT        IDENTITY (1, 1) NOT NULL,
    [IDProduct] INT        NULL,
    [IDOrder]   INT        NULL,
    [Quantity]  INT        NULL,
    [UnitPrice] FLOAT (53) NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC),
    FOREIGN KEY ([IDProduct]) REFERENCES [dbo].[Product] ([ProductID]),
    FOREIGN KEY ([IDOrder]) REFERENCES [dbo].[OrderPro] ([ID])
);
