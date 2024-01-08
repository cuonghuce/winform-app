USE [ShopSimple]
GO
/****** Object:  Table [dbo].[Admin]    Script Date: 22/07/2023 12:55:57 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Admin](
	[Username] [varchar](100) NOT NULL,
	[Password] [varchar](max) NOT NULL,
	[Name] [nvarchar](80) NOT NULL,
 CONSTRAINT [PK_Admin] PRIMARY KEY CLUSTERED 
(
	[Username] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Catalog]    Script Date: 22/07/2023 12:55:57 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Catalog](
	[CatalogCode] [varchar](5) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_Catalog] PRIMARY KEY CLUSTERED 
(
	[CatalogCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Customer]    Script Date: 22/07/2023 12:55:57 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customer](
	[Phone] [varchar](11) NOT NULL,
	[Name] [nvarchar](80) NULL,
 CONSTRAINT [PK_Customer] PRIMARY KEY CLUSTERED 
(
	[Phone] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Import]    Script Date: 22/07/2023 12:55:57 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Import](
	[ImportCode] [varchar](25) NOT NULL,
	[Date] [datetime] NOT NULL,
	[UserID] [varchar](100) NOT NULL,
	[Total] [int] NOT NULL,
 CONSTRAINT [PK_Import] PRIMARY KEY CLUSTERED 
(
	[ImportCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ImportDetail]    Script Date: 22/07/2023 12:55:57 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ImportDetail](
	[ImportID] [varchar](25) NOT NULL,
	[ProductID] [varchar](25) NOT NULL,
	[Quantity] [int] NOT NULL,
	[Price] [int] NOT NULL,
 CONSTRAINT [PK_ImportDetail] PRIMARY KEY CLUSTERED 
(
	[ImportID] ASC,
	[ProductID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Invoice]    Script Date: 22/07/2023 12:55:57 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Invoice](
	[InvoiceCode] [varchar](25) NOT NULL,
	[UserID] [varchar](100) NOT NULL,
	[CustomerPhone] [varchar](11) NOT NULL,
	[Date] [datetime] NOT NULL,
	[Total] [int] NOT NULL,
 CONSTRAINT [PK_Invoice] PRIMARY KEY CLUSTERED 
(
	[InvoiceCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[InvoiceDetail]    Script Date: 22/07/2023 12:55:57 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[InvoiceDetail](
	[InvoiceID] [varchar](25) NOT NULL,
	[ProductID] [varchar](25) NOT NULL,
	[Quantity] [int] NOT NULL,
	[Price] [int] NOT NULL,
 CONSTRAINT [PK_InvoiceDetail] PRIMARY KEY CLUSTERED 
(
	[InvoiceID] ASC,
	[ProductID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Product]    Script Date: 22/07/2023 12:55:57 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product](
	[ProductCode] [varchar](25) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Image] [nvarchar](max) NULL,
	[CatalogID] [varchar](5) NOT NULL,
	[SupplierID] [varchar](5) NOT NULL,
	[Amount] [int] NOT NULL,
	[Price] [int] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[Status] [bit] NOT NULL,
 CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED 
(
	[ProductCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Supplier]    Script Date: 22/07/2023 12:55:57 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Supplier](
	[SupplierCode] [varchar](5) NOT NULL,
	[Name] [nvarchar](200) NOT NULL,
	[Email] [varchar](500) NULL,
	[Phone] [varchar](12) NULL,
 CONSTRAINT [PK_Supplier] PRIMARY KEY CLUSTERED 
(
	[SupplierCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 22/07/2023 12:55:57 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[Username] [varchar](100) NOT NULL,
	[Password] [varchar](max) NOT NULL,
	[Name] [nvarchar](80) NOT NULL,
	[Phone] [varchar](11) NOT NULL,
	[Address] [nvarchar](500) NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[Username] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
INSERT [dbo].[Admin] ([Username], [Password], [Name]) VALUES (N'admin', N'123', N'Chủ Cửa Hàng')
INSERT [dbo].[Admin] ([Username], [Password], [Name]) VALUES (N'importad', N'123', N'Nhân Viên Nhập Hàng')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00001', N'Loại sản phẩm 1')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00002', N'Loại sản phẩm 2')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00003', N'Loại sản phẩm 3')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00004', N'Loại sản phẩm 4')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00005', N'Loại sản phẩm 5')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00006', N'Loại sản phẩm 6')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00007', N'Loại sản phẩm 7')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00008', N'Loại sản phẩm 8')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00009', N'Loại sản phẩm 9')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00010', N'Loại sản phẩm 10')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00011', N'Loại sản phẩm 11')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00012', N'Loại sản phẩm 12')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00013', N'Loại sản phẩm 13')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00014', N'Loại sản phẩm 14')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00015', N'Loại sản phẩm 15')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00016', N'Loại sản phẩm 16')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00017', N'Loại sản phẩm 17')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00018', N'Loại sản phẩm 18')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00019', N'Loại sản phẩm 19')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00020', N'Loại sản phẩm 20')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00021', N'Loại sản phẩm 21')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00022', N'Loại sản phẩm 22')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00023', N'Loại sản phẩm 23')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00024', N'Loại sản phẩm 24')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00025', N'Loại sản phẩm 25')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00026', N'Loại sản phẩm 26')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00027', N'Loại sản phẩm 27')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00028', N'Loại sản phẩm 28')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00029', N'Loại sản phẩm 29')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00030', N'Loại sản phẩm 30')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00031', N'Loại sản phẩm 31')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00032', N'Loại sản phẩm 32')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00033', N'Loại sản phẩm 33')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00034', N'Loại sản phẩm 34')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00035', N'Loại sản phẩm 35')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00036', N'Loại sản phẩm 36')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00037', N'Loại sản phẩm 37')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00038', N'Loại sản phẩm 38')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00039', N'Loại sản phẩm 39')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00040', N'Loại sản phẩm 40')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00041', N'Loại sản phẩm 41')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00042', N'Loại sản phẩm 42')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00043', N'Loại sản phẩm 43')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00044', N'Loại sản phẩm 44')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00045', N'Loại sản phẩm 45')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00046', N'Loại sản phẩm 46')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00047', N'Loại sản phẩm 47')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00048', N'Loại sản phẩm 48')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00049', N'Loại sản phẩm 49')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00050', N'Loại sản phẩm 50')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00051', N'Loại sản phẩm 51')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00052', N'Loại sản phẩm 52')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00053', N'Loại sản phẩm 53')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00054', N'Loại sản phẩm 54')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00055', N'Loại sản phẩm 55')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00056', N'Loại sản phẩm 56')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00057', N'Loại sản phẩm 57')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00058', N'Loại sản phẩm 58')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00059', N'Loại sản phẩm 59')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00060', N'Loại sản phẩm 60')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00061', N'Loại sản phẩm 61')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00062', N'Loại sản phẩm 62')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00063', N'Loại sản phẩm 63')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00064', N'Loại sản phẩm 64')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00065', N'Loại sản phẩm 65')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00066', N'Loại sản phẩm 66')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00067', N'Loại sản phẩm 67')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00068', N'Loại sản phẩm 68')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00069', N'Loại sản phẩm 69')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00070', N'Loại sản phẩm 70')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00071', N'Loại sản phẩm 71')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00072', N'Loại sản phẩm 72')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00073', N'Loại sản phẩm 73')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00074', N'Loại sản phẩm 74')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00075', N'Loại sản phẩm 75')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00076', N'Loại sản phẩm 76')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00077', N'Loại sản phẩm 77')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00078', N'Loại sản phẩm 78')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00079', N'Loại sản phẩm 79')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00080', N'Loại sản phẩm 80')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00081', N'Loại sản phẩm 81')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00082', N'Loại sản phẩm 82')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00083', N'Loại sản phẩm 83')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00084', N'Loại sản phẩm 84')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00085', N'Loại sản phẩm 85')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00086', N'Loại sản phẩm 86')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00087', N'Loại sản phẩm 87')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00088', N'Loại sản phẩm 88')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00089', N'Loại sản phẩm 89')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00090', N'Loại sản phẩm 90')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00091', N'Loại sản phẩm 91')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00092', N'Loại sản phẩm 92')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00093', N'Loại sản phẩm 93')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00094', N'Loại sản phẩm 94')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00095', N'Loại sản phẩm 95')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00096', N'Loại sản phẩm 96')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00097', N'Loại sản phẩm 97')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00098', N'Loại sản phẩm 98')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00099', N'Loại sản phẩm 99')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00100', N'Loại sản phẩm 100')
GO
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00101', N'Loại sản phẩm 101')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00102', N'Loại sản phẩm 102')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00103', N'Loại sản phẩm 103')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00104', N'Loại sản phẩm 104')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00105', N'Loại sản phẩm 105')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00106', N'Loại sản phẩm 106')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00107', N'Loại sản phẩm 107')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00108', N'Loại sản phẩm 108')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00109', N'Loại sản phẩm 109')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00110', N'Loại sản phẩm 110')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00111', N'Loại sản phẩm 111')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00112', N'Loại sản phẩm 112')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00113', N'Loại sản phẩm 113')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00114', N'Loại sản phẩm 114')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00115', N'Loại sản phẩm 115')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00116', N'Loại sản phẩm 116')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00117', N'Loại sản phẩm 117')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00118', N'Loại sản phẩm 118')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00119', N'Loại sản phẩm 119')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00120', N'Loại sản phẩm 120')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00121', N'Loại sản phẩm 121')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00122', N'Loại sản phẩm 122')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00123', N'Loại sản phẩm 123')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00124', N'Loại sản phẩm 124')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00125', N'Loại sản phẩm 125')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00126', N'Loại sản phẩm 126')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00127', N'Loại sản phẩm 127')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00128', N'Loại sản phẩm 128')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00129', N'Loại sản phẩm 129')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00130', N'Loại sản phẩm 130')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00131', N'Loại sản phẩm 131')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00132', N'Loại sản phẩm 132')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00133', N'Loại sản phẩm 133')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00134', N'Loại sản phẩm 134')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00135', N'Loại sản phẩm 135')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00136', N'Loại sản phẩm 136')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00137', N'Loại sản phẩm 137')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00138', N'Loại sản phẩm 138')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00139', N'Loại sản phẩm 139')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00140', N'Loại sản phẩm 140')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00141', N'Loại sản phẩm 141')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00142', N'Loại sản phẩm 142')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00143', N'Loại sản phẩm 143')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00144', N'Loại sản phẩm 144')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00145', N'Loại sản phẩm 145')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00146', N'Loại sản phẩm 146')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00147', N'Loại sản phẩm 147')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00148', N'Loại sản phẩm 148')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00149', N'Loại sản phẩm 149')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00150', N'Loại sản phẩm 150')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00151', N'Loại sản phẩm 151')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00152', N'Loại sản phẩm 152')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00153', N'Loại sản phẩm 153')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00154', N'Loại sản phẩm 154')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00155', N'Loại sản phẩm 155')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00156', N'Loại sản phẩm 156')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00157', N'Loại sản phẩm 157')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00158', N'Loại sản phẩm 158')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00159', N'Loại sản phẩm 159')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00160', N'Loại sản phẩm 160')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00161', N'Loại sản phẩm 161')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00162', N'Loại sản phẩm 162')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00163', N'Loại sản phẩm 163')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00164', N'Loại sản phẩm 164')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00165', N'Loại sản phẩm 165')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00166', N'Loại sản phẩm 166')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00167', N'Loại sản phẩm 167')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00168', N'Loại sản phẩm 168')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00169', N'Loại sản phẩm 169')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00170', N'Loại sản phẩm 170')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00171', N'Loại sản phẩm 171')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00172', N'Loại sản phẩm 172')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00173', N'Loại sản phẩm 173')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00174', N'Loại sản phẩm 174')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00175', N'Loại sản phẩm 175')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00176', N'Loại sản phẩm 176')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00177', N'Loại sản phẩm 177')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00178', N'Loại sản phẩm 178')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00179', N'Loại sản phẩm 179')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00180', N'Loại sản phẩm 180')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00181', N'Loại sản phẩm 181')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00182', N'Loại sản phẩm 182')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00183', N'Loại sản phẩm 183')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00184', N'Loại sản phẩm 184')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00185', N'Loại sản phẩm 185')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00186', N'Loại sản phẩm 186')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00187', N'Loại sản phẩm 187')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00188', N'Loại sản phẩm 188')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00189', N'Loại sản phẩm 189')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00190', N'Loại sản phẩm 190')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00191', N'Loại sản phẩm 191')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00192', N'Loại sản phẩm 192')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00193', N'Loại sản phẩm 193')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00194', N'Loại sản phẩm 194')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00195', N'Loại sản phẩm 195')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00196', N'Loại sản phẩm 196')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00197', N'Loại sản phẩm 197')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00198', N'Loại sản phẩm 198')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00199', N'Loại sản phẩm 199')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00200', N'Loại sản phẩm 200')
GO
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00201', N'Loại sản phẩm 201')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00202', N'Loại sản phẩm 202')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00203', N'Loại sản phẩm 203')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00204', N'Loại sản phẩm 204')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00205', N'Loại sản phẩm 205')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00206', N'Loại sản phẩm 206')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00207', N'Loại sản phẩm 207')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00208', N'Loại sản phẩm 208')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00209', N'Loại sản phẩm 209')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00210', N'Loại sản phẩm 210')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00211', N'Loại sản phẩm 211')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00212', N'Loại sản phẩm 212')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00213', N'Loại sản phẩm 213')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00214', N'Loại sản phẩm 214')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00215', N'Loại sản phẩm 215')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00216', N'Loại sản phẩm 216')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00217', N'Loại sản phẩm 217')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00218', N'Loại sản phẩm 218')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00219', N'Loại sản phẩm 219')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00220', N'Loại sản phẩm 220')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00221', N'Loại sản phẩm 221')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00222', N'Loại sản phẩm 222')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00223', N'Loại sản phẩm 223')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00224', N'Loại sản phẩm 224')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00225', N'Loại sản phẩm 225')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00226', N'Loại sản phẩm 226')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00227', N'Loại sản phẩm 227')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00228', N'Loại sản phẩm 228')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00229', N'Loại sản phẩm 229')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00230', N'Loại sản phẩm 230')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00231', N'Loại sản phẩm 231')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00232', N'Loại sản phẩm 232')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00233', N'Loại sản phẩm 233')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00234', N'Loại sản phẩm 234')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00235', N'Loại sản phẩm 235')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00236', N'Loại sản phẩm 236')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00237', N'Loại sản phẩm 237')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00238', N'Loại sản phẩm 238')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00239', N'Loại sản phẩm 239')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00240', N'Loại sản phẩm 240')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00241', N'Loại sản phẩm 241')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00242', N'Loại sản phẩm 242')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00243', N'Loại sản phẩm 243')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00244', N'Loại sản phẩm 244')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00245', N'Loại sản phẩm 245')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00246', N'Loại sản phẩm 246')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00247', N'Loại sản phẩm 247')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00248', N'Loại sản phẩm 248')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00249', N'Loại sản phẩm 249')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00250', N'Loại sản phẩm 250')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00251', N'Loại sản phẩm 251')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00252', N'Loại sản phẩm 252')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00253', N'Loại sản phẩm 253')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00254', N'Loại sản phẩm 254')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00255', N'Loại sản phẩm 255')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00256', N'Loại sản phẩm 256')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00257', N'Loại sản phẩm 257')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00258', N'Loại sản phẩm 258')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00259', N'Loại sản phẩm 259')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00260', N'Loại sản phẩm 260')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00261', N'Loại sản phẩm 261')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00262', N'Loại sản phẩm 262')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00263', N'Loại sản phẩm 263')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00264', N'Loại sản phẩm 264')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00265', N'Loại sản phẩm 265')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00266', N'Loại sản phẩm 266')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00267', N'Loại sản phẩm 267')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00268', N'Loại sản phẩm 268')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00269', N'Loại sản phẩm 269')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00270', N'Loại sản phẩm 270')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00271', N'Loại sản phẩm 271')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00272', N'Loại sản phẩm 272')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00273', N'Loại sản phẩm 273')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00274', N'Loại sản phẩm 274')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00275', N'Loại sản phẩm 275')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00276', N'Loại sản phẩm 276')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00277', N'Loại sản phẩm 277')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00278', N'Loại sản phẩm 278')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00279', N'Loại sản phẩm 279')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00280', N'Loại sản phẩm 280')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00281', N'Loại sản phẩm 281')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00282', N'Loại sản phẩm 282')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00283', N'Loại sản phẩm 283')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00284', N'Loại sản phẩm 284')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00285', N'Loại sản phẩm 285')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00286', N'Loại sản phẩm 286')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00287', N'Loại sản phẩm 287')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00288', N'Loại sản phẩm 288')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00289', N'Loại sản phẩm 289')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00290', N'Loại sản phẩm 290')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00291', N'Loại sản phẩm 291')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00292', N'Loại sản phẩm 292')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00293', N'Loại sản phẩm 293')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00294', N'Loại sản phẩm 294')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00295', N'Loại sản phẩm 295')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00296', N'Loại sản phẩm 296')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00297', N'Loại sản phẩm 297')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00298', N'Loại sản phẩm 298')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00299', N'Loại sản phẩm 299')
INSERT [dbo].[Catalog] ([CatalogCode], [Name]) VALUES (N'00300', N'Loại sản phẩm 300')
GO
INSERT [dbo].[Customer] ([Phone], [Name]) VALUES (N'0903456789', N'Trần Văn C')
INSERT [dbo].[Customer] ([Phone], [Name]) VALUES (N'0904567890', N'Trần Thị D')
INSERT [dbo].[Customer] ([Phone], [Name]) VALUES (N'0905678901', N'Lê Văn E')
INSERT [dbo].[Customer] ([Phone], [Name]) VALUES (N'0906789012', N'Lê Thị F')
INSERT [dbo].[Customer] ([Phone], [Name]) VALUES (N'0907890123', N'Hoàng Văn G')
INSERT [dbo].[Customer] ([Phone], [Name]) VALUES (N'0908901234', N'Hoàng Thị H')
INSERT [dbo].[Customer] ([Phone], [Name]) VALUES (N'0909012345', N'Phạm Văn I')
INSERT [dbo].[Customer] ([Phone], [Name]) VALUES (N'0910012345', N'Phạm Thị J')
INSERT [dbo].[Customer] ([Phone], [Name]) VALUES (N'0911012345', N'Đặng Văn K')
INSERT [dbo].[Customer] ([Phone], [Name]) VALUES (N'0912012345', N'Đặng Thị L')
INSERT [dbo].[Customer] ([Phone], [Name]) VALUES (N'0912345678', N'Nguyễn Thị B')
INSERT [dbo].[Customer] ([Phone], [Name]) VALUES (N'0913012345', N'Nguyễn Văn M')
INSERT [dbo].[Customer] ([Phone], [Name]) VALUES (N'0914012345', N'Nguyễn Thị N')
INSERT [dbo].[Customer] ([Phone], [Name]) VALUES (N'0915012345', N'Trần Văn O')
INSERT [dbo].[Customer] ([Phone], [Name]) VALUES (N'0916012345', N'Trần Thị P')
INSERT [dbo].[Customer] ([Phone], [Name]) VALUES (N'0917012345', N'Lê Văn Q')
INSERT [dbo].[Customer] ([Phone], [Name]) VALUES (N'0918012345', N'Lê Thị R')
INSERT [dbo].[Customer] ([Phone], [Name]) VALUES (N'0919012345', N'Hoàng Văn S')
INSERT [dbo].[Customer] ([Phone], [Name]) VALUES (N'0920012345', N'Hoàng Thị T')
INSERT [dbo].[Customer] ([Phone], [Name]) VALUES (N'0921012345', N'Phạm Văn U')
INSERT [dbo].[Customer] ([Phone], [Name]) VALUES (N'0922012345', N'Phạm Thị V')
INSERT [dbo].[Customer] ([Phone], [Name]) VALUES (N'0923012345', N'Đặng Văn W')
INSERT [dbo].[Customer] ([Phone], [Name]) VALUES (N'0924012345', N'Đặng Thị X')
INSERT [dbo].[Customer] ([Phone], [Name]) VALUES (N'0925012345', N'Nguyễn Văn Y')
INSERT [dbo].[Customer] ([Phone], [Name]) VALUES (N'0926012345', N'Nguyễn Thị Z')
INSERT [dbo].[Customer] ([Phone], [Name]) VALUES (N'0927012345', N'Trần Văn AA')
INSERT [dbo].[Customer] ([Phone], [Name]) VALUES (N'0928012345', N'Trần Thị BB')
INSERT [dbo].[Customer] ([Phone], [Name]) VALUES (N'0929012345', N'Lê Văn CC')
INSERT [dbo].[Customer] ([Phone], [Name]) VALUES (N'0930012345', N'Lê Thị DD')
INSERT [dbo].[Customer] ([Phone], [Name]) VALUES (N'0931012345', N'Hoàng Văn EE')
INSERT [dbo].[Customer] ([Phone], [Name]) VALUES (N'0932012345', N'Hoàng Thị FF')
INSERT [dbo].[Customer] ([Phone], [Name]) VALUES (N'0933012345', N'Phạm Văn GG')
INSERT [dbo].[Customer] ([Phone], [Name]) VALUES (N'0934012345', N'Phạm Thị HH')
INSERT [dbo].[Customer] ([Phone], [Name]) VALUES (N'0935012345', N'Đặng Văn II')
INSERT [dbo].[Customer] ([Phone], [Name]) VALUES (N'0936012345', N'Đặng Thị JJ')
INSERT [dbo].[Customer] ([Phone], [Name]) VALUES (N'0937012345', N'Nguyễn Văn KK')
INSERT [dbo].[Customer] ([Phone], [Name]) VALUES (N'0938012345', N'Nguyễn Thị LL')
INSERT [dbo].[Customer] ([Phone], [Name]) VALUES (N'0939012345', N'Trần Văn MM')
INSERT [dbo].[Customer] ([Phone], [Name]) VALUES (N'0940012345', N'Trần Thị NN')
INSERT [dbo].[Customer] ([Phone], [Name]) VALUES (N'0941012345', N'Lê Văn OO')
INSERT [dbo].[Customer] ([Phone], [Name]) VALUES (N'0942012345', N'Lê Thị PP')
INSERT [dbo].[Customer] ([Phone], [Name]) VALUES (N'0943012345', N'Hoàng Văn QQ')
INSERT [dbo].[Customer] ([Phone], [Name]) VALUES (N'0944012345', N'Hoàng Thị RR')
INSERT [dbo].[Customer] ([Phone], [Name]) VALUES (N'0945012345', N'Phạm Văn SS')
INSERT [dbo].[Customer] ([Phone], [Name]) VALUES (N'0946012345', N'Phạm Thị TT')
INSERT [dbo].[Customer] ([Phone], [Name]) VALUES (N'0987654321', N'Nguyễn Văn A')
INSERT [dbo].[Customer] ([Phone], [Name]) VALUES (N'0998376111', N'Khách hàng 02')
INSERT [dbo].[Customer] ([Phone], [Name]) VALUES (N'0999847361', N'Khách hàng 03')
INSERT [dbo].[Customer] ([Phone], [Name]) VALUES (N'0999873615', N'Khách hàng 01')
INSERT [dbo].[Import] ([ImportCode], [Date], [UserID], [Total]) VALUES (N'NH20230702161039', CAST(N'2023-07-02T16:10:39.293' AS DateTime), N'admin', 5640000)
INSERT [dbo].[Import] ([ImportCode], [Date], [UserID], [Total]) VALUES (N'NH20230716145741', CAST(N'2023-07-16T14:57:41.750' AS DateTime), N'admin', 5720000)
INSERT [dbo].[Import] ([ImportCode], [Date], [UserID], [Total]) VALUES (N'NH20230716150148', CAST(N'2023-07-16T15:01:48.827' AS DateTime), N'admin', 4320000)
INSERT [dbo].[ImportDetail] ([ImportID], [ProductID], [Quantity], [Price]) VALUES (N'NH20230702161039', N'SP20230702161041', 12, 120000)
INSERT [dbo].[ImportDetail] ([ImportID], [ProductID], [Quantity], [Price]) VALUES (N'NH20230702161039', N'SP20230702161152', 12, 350000)
INSERT [dbo].[ImportDetail] ([ImportID], [ProductID], [Quantity], [Price]) VALUES (N'NH20230716145741', N'SP20230716145746', 12, 130000)
INSERT [dbo].[ImportDetail] ([ImportID], [ProductID], [Quantity], [Price]) VALUES (N'NH20230716145741', N'SP20230716145830', 12, 180000)
INSERT [dbo].[ImportDetail] ([ImportID], [ProductID], [Quantity], [Price]) VALUES (N'NH20230716145741', N'SP20230716145918', 20, 100000)
INSERT [dbo].[ImportDetail] ([ImportID], [ProductID], [Quantity], [Price]) VALUES (N'NH20230716150148', N'SP20230716150154', 12, 120000)
INSERT [dbo].[ImportDetail] ([ImportID], [ProductID], [Quantity], [Price]) VALUES (N'NH20230716150148', N'SP20230716150228', 12, 120000)
INSERT [dbo].[ImportDetail] ([ImportID], [ProductID], [Quantity], [Price]) VALUES (N'NH20230716150148', N'SP20230716150239', 12, 120000)
INSERT [dbo].[Invoice] ([InvoiceCode], [UserID], [CustomerPhone], [Date], [Total]) VALUES (N'NH20230719092732', N'nv001', N'0999873615', CAST(N'2023-07-19T09:27:32.880' AS DateTime), 1380000)
INSERT [dbo].[Invoice] ([InvoiceCode], [UserID], [CustomerPhone], [Date], [Total]) VALUES (N'NH20230719093412', N'nv001', N'0998376111', CAST(N'2023-07-19T09:34:12.287' AS DateTime), 2440000)
INSERT [dbo].[Invoice] ([InvoiceCode], [UserID], [CustomerPhone], [Date], [Total]) VALUES (N'NH20230721133429', N'nv001', N'0987654321', CAST(N'2023-07-21T13:34:29.663' AS DateTime), 1120000)
INSERT [dbo].[Invoice] ([InvoiceCode], [UserID], [CustomerPhone], [Date], [Total]) VALUES (N'NH20230721133518', N'nv001', N'0999847361', CAST(N'2023-07-21T13:35:18.747' AS DateTime), 980000)
INSERT [dbo].[Invoice] ([InvoiceCode], [UserID], [CustomerPhone], [Date], [Total]) VALUES (N'NH20230721133642', N'nv001', N'0999847361', CAST(N'2023-07-21T13:36:42.333' AS DateTime), 2010000)
INSERT [dbo].[InvoiceDetail] ([InvoiceID], [ProductID], [Quantity], [Price]) VALUES (N'NH20230719092732', N'SP20230702161041', 4, 480000)
INSERT [dbo].[InvoiceDetail] ([InvoiceID], [ProductID], [Quantity], [Price]) VALUES (N'NH20230719092732', N'SP20230716145830', 5, 900000)
INSERT [dbo].[InvoiceDetail] ([InvoiceID], [ProductID], [Quantity], [Price]) VALUES (N'NH20230719093412', N'SP20230702161152', 4, 1400000)
INSERT [dbo].[InvoiceDetail] ([InvoiceID], [ProductID], [Quantity], [Price]) VALUES (N'NH20230719093412', N'SP20230716145746', 8, 1040000)
INSERT [dbo].[InvoiceDetail] ([InvoiceID], [ProductID], [Quantity], [Price]) VALUES (N'NH20230721133429', N'SP20230716145918', 10, 1000000)
INSERT [dbo].[InvoiceDetail] ([InvoiceID], [ProductID], [Quantity], [Price]) VALUES (N'NH20230721133429', N'SP20230716150154', 1, 120000)
INSERT [dbo].[InvoiceDetail] ([InvoiceID], [ProductID], [Quantity], [Price]) VALUES (N'NH20230721133518', N'SP20230702161041', 4, 480000)
INSERT [dbo].[InvoiceDetail] ([InvoiceID], [ProductID], [Quantity], [Price]) VALUES (N'NH20230721133518', N'SP20230716145918', 5, 500000)
INSERT [dbo].[InvoiceDetail] ([InvoiceID], [ProductID], [Quantity], [Price]) VALUES (N'NH20230721133642', N'SP20230702161152', 5, 1750000)
INSERT [dbo].[InvoiceDetail] ([InvoiceID], [ProductID], [Quantity], [Price]) VALUES (N'NH20230721133642', N'SP20230716145746', 2, 260000)
INSERT [dbo].[Product] ([ProductCode], [Name], [Image], [CatalogID], [SupplierID], [Amount], [Price], [CreateDate], [Status]) VALUES (N'SP20230702161041', N'Sản phẩm 01', N'giohoa3.jpg', N'00001', N'ncc01', 4, 120000, CAST(N'2023-07-02T16:10:39.293' AS DateTime), 1)
INSERT [dbo].[Product] ([ProductCode], [Name], [Image], [CatalogID], [SupplierID], [Amount], [Price], [CreateDate], [Status]) VALUES (N'SP20230702161152', N'Sản phẩm 02', N'giohoa1.jpg', N'00010', N'ncc02', 3, 350000, CAST(N'2023-07-02T16:10:39.293' AS DateTime), 0)
INSERT [dbo].[Product] ([ProductCode], [Name], [Image], [CatalogID], [SupplierID], [Amount], [Price], [CreateDate], [Status]) VALUES (N'SP20230716145746', N'Sản phẩm 03', N'giohoa1 - Copy.jpg', N'00010', N'ncc01', 2, 130000, CAST(N'2023-07-16T14:57:41.750' AS DateTime), 1)
INSERT [dbo].[Product] ([ProductCode], [Name], [Image], [CatalogID], [SupplierID], [Amount], [Price], [CreateDate], [Status]) VALUES (N'SP20230716145830', N'Sản phẩm 04', N'giohoa1 - Copy.jpg', N'00010', N'ncc01', 7, 180000, CAST(N'2023-07-16T14:57:41.750' AS DateTime), 1)
INSERT [dbo].[Product] ([ProductCode], [Name], [Image], [CatalogID], [SupplierID], [Amount], [Price], [CreateDate], [Status]) VALUES (N'SP20230716145918', N'Sản phẩm 05', N'giohoa1 - Copy.jpg', N'00102', N'ncc02', 5, 100000, CAST(N'2023-07-16T14:57:41.750' AS DateTime), 1)
INSERT [dbo].[Product] ([ProductCode], [Name], [Image], [CatalogID], [SupplierID], [Amount], [Price], [CreateDate], [Status]) VALUES (N'SP20230716150154', N'Sản phẩm 06', N'giohoa3 - Copy.jpg', N'00101', N'ncc02', 11, 120000, CAST(N'2023-07-16T15:01:48.827' AS DateTime), 1)
INSERT [dbo].[Product] ([ProductCode], [Name], [Image], [CatalogID], [SupplierID], [Amount], [Price], [CreateDate], [Status]) VALUES (N'SP20230716150228', N'Sản phẩm 07', N'giohoa3 - Copy.jpg', N'00109', N'ncc02', 12, 120000, CAST(N'2023-07-16T15:01:48.827' AS DateTime), 1)
INSERT [dbo].[Product] ([ProductCode], [Name], [Image], [CatalogID], [SupplierID], [Amount], [Price], [CreateDate], [Status]) VALUES (N'SP20230716150239', N'Sản phẩm 06', N'giohoa3 - Copy.jpg', N'00122', N'ncc03', 12, 120000, CAST(N'2023-07-16T15:01:48.827' AS DateTime), 1)
INSERT [dbo].[Supplier] ([SupplierCode], [Name], [Email], [Phone]) VALUES (N'ncc01', N'Nhà cung cấp 01', N'ncc01@gmail.com', N'0999876512')
INSERT [dbo].[Supplier] ([SupplierCode], [Name], [Email], [Phone]) VALUES (N'ncc02', N'Nhà cung cấp 02', N'ncc02@gmail.com', N'0987123212')
INSERT [dbo].[Supplier] ([SupplierCode], [Name], [Email], [Phone]) VALUES (N'ncc03', N'Nhà cung cấp 03', N'ncc03@gmail.com', N'0987123290')
INSERT [dbo].[Supplier] ([SupplierCode], [Name], [Email], [Phone]) VALUES (N'ncc04', N'Công ty DA', N'd@company.com', N'0904567890')
INSERT [dbo].[Supplier] ([SupplierCode], [Name], [Email], [Phone]) VALUES (N'ncc05', N'Công ty E', N'e@company.com', N'0905678901')
INSERT [dbo].[Supplier] ([SupplierCode], [Name], [Email], [Phone]) VALUES (N'ncc06', N'Công ty F', N'f@company.com', N'0906789012')
INSERT [dbo].[Supplier] ([SupplierCode], [Name], [Email], [Phone]) VALUES (N'ncc07', N'Công ty G', N'g@company.com', N'0907890123')
INSERT [dbo].[Supplier] ([SupplierCode], [Name], [Email], [Phone]) VALUES (N'ncc08', N'Công ty H', N'h@company.com', N'0908901234')
INSERT [dbo].[Supplier] ([SupplierCode], [Name], [Email], [Phone]) VALUES (N'ncc09', N'Công ty I', N'i@company.com', N'0909012345')
INSERT [dbo].[Supplier] ([SupplierCode], [Name], [Email], [Phone]) VALUES (N'ncc10', N'Công ty J', N'j@company.com', N'0910012345')
INSERT [dbo].[Supplier] ([SupplierCode], [Name], [Email], [Phone]) VALUES (N'ncc11', N'Công ty K', N'k@company.com', N'0911012345')
INSERT [dbo].[Supplier] ([SupplierCode], [Name], [Email], [Phone]) VALUES (N'ncc12', N'Công ty L', N'l@company.com', N'0912012345')
INSERT [dbo].[Supplier] ([SupplierCode], [Name], [Email], [Phone]) VALUES (N'ncc13', N'Công ty M', N'm@company.com', N'0913012345')
INSERT [dbo].[Supplier] ([SupplierCode], [Name], [Email], [Phone]) VALUES (N'ncc14', N'Công ty N', N'n@company.com', N'0914012345')
INSERT [dbo].[Supplier] ([SupplierCode], [Name], [Email], [Phone]) VALUES (N'ncc15', N'Công ty O', N'o@company.com', N'0915012345')
INSERT [dbo].[Supplier] ([SupplierCode], [Name], [Email], [Phone]) VALUES (N'ncc16', N'Công ty P', N'p@company.com', N'0916012345')
INSERT [dbo].[Supplier] ([SupplierCode], [Name], [Email], [Phone]) VALUES (N'ncc17', N'Công ty Q', N'q@company.com', N'0917012345')
INSERT [dbo].[Supplier] ([SupplierCode], [Name], [Email], [Phone]) VALUES (N'ncc18', N'Công ty R', N'r@company.com', N'0918012345')
INSERT [dbo].[Supplier] ([SupplierCode], [Name], [Email], [Phone]) VALUES (N'ncc19', N'Công ty S', N's@company.com', N'0919012345')
INSERT [dbo].[Supplier] ([SupplierCode], [Name], [Email], [Phone]) VALUES (N'ncc20', N'Công ty T', N't@company.com', N'0920012345')
INSERT [dbo].[User] ([Username], [Password], [Name], [Phone], [Address]) VALUES (N'nv001', N'12', N'Nhân viên 01', N'0999872371', N'Quận 1')
INSERT [dbo].[User] ([Username], [Password], [Name], [Phone], [Address]) VALUES (N'nv002', N'1111', N'Nhân viên 02', N'0988726541', N'Quận 2')
ALTER TABLE [dbo].[Import]  WITH CHECK ADD  CONSTRAINT [FK_Import_Admin] FOREIGN KEY([UserID])
REFERENCES [dbo].[Admin] ([Username])
GO
ALTER TABLE [dbo].[Import] CHECK CONSTRAINT [FK_Import_Admin]
GO
ALTER TABLE [dbo].[ImportDetail]  WITH CHECK ADD  CONSTRAINT [FK_ImportDetail_Import] FOREIGN KEY([ImportID])
REFERENCES [dbo].[Import] ([ImportCode])
GO
ALTER TABLE [dbo].[ImportDetail] CHECK CONSTRAINT [FK_ImportDetail_Import]
GO
ALTER TABLE [dbo].[ImportDetail]  WITH CHECK ADD  CONSTRAINT [FK_ImportDetail_Product] FOREIGN KEY([ProductID])
REFERENCES [dbo].[Product] ([ProductCode])
GO
ALTER TABLE [dbo].[ImportDetail] CHECK CONSTRAINT [FK_ImportDetail_Product]
GO
ALTER TABLE [dbo].[Invoice]  WITH CHECK ADD  CONSTRAINT [FK_Invoice_Customer] FOREIGN KEY([CustomerPhone])
REFERENCES [dbo].[Customer] ([Phone])
GO
ALTER TABLE [dbo].[Invoice] CHECK CONSTRAINT [FK_Invoice_Customer]
GO
ALTER TABLE [dbo].[Invoice]  WITH CHECK ADD  CONSTRAINT [FK_Invoice_User] FOREIGN KEY([UserID])
REFERENCES [dbo].[User] ([Username])
GO
ALTER TABLE [dbo].[Invoice] CHECK CONSTRAINT [FK_Invoice_User]
GO
ALTER TABLE [dbo].[InvoiceDetail]  WITH CHECK ADD  CONSTRAINT [FK_InvoiceDetail_Invoice] FOREIGN KEY([InvoiceID])
REFERENCES [dbo].[Invoice] ([InvoiceCode])
GO
ALTER TABLE [dbo].[InvoiceDetail] CHECK CONSTRAINT [FK_InvoiceDetail_Invoice]
GO
ALTER TABLE [dbo].[InvoiceDetail]  WITH CHECK ADD  CONSTRAINT [FK_InvoiceDetail_Product] FOREIGN KEY([ProductID])
REFERENCES [dbo].[Product] ([ProductCode])
GO
ALTER TABLE [dbo].[InvoiceDetail] CHECK CONSTRAINT [FK_InvoiceDetail_Product]
GO
ALTER TABLE [dbo].[Product]  WITH CHECK ADD  CONSTRAINT [FK_Product_Catalog] FOREIGN KEY([CatalogID])
REFERENCES [dbo].[Catalog] ([CatalogCode])
GO
ALTER TABLE [dbo].[Product] CHECK CONSTRAINT [FK_Product_Catalog]
GO
ALTER TABLE [dbo].[Product]  WITH CHECK ADD  CONSTRAINT [FK_Product_Supplier] FOREIGN KEY([SupplierID])
REFERENCES [dbo].[Supplier] ([SupplierCode])
GO
ALTER TABLE [dbo].[Product] CHECK CONSTRAINT [FK_Product_Supplier]
GO
