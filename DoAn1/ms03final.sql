Create database [MyStore]
go
USE [MyStore]
GO
/****** Object:  Table [dbo].[Account]    Script Date: 22/1/2021 10:42:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Account](
	[username] [nvarchar](50) NOT NULL,
	[rolename] [nvarchar](50) NULL,
 CONSTRAINT [PK_Account] PRIMARY KEY CLUSTERED 
(
	[username] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Category]    Script Date: 22/1/2021 10:42:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Category](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [ntext] NULL,
 CONSTRAINT [PK_Category] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Customer]    Script Date: 22/1/2021 10:42:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customer](
	[Customer_Name] [nvarchar](50) NULL,
	[Tel] [nvarchar](50) NOT NULL,
	[Address] [ntext] NULL,
	[Email] [ntext] NULL,
 CONSTRAINT [PK_Customer] PRIMARY KEY CLUSTERED 
(
	[Tel] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Product]    Script Date: 22/1/2021 10:42:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CatId] [int] NULL,
	[Name] [ntext] NULL,
	[Price] [decimal](19, 4) NULL,
	[Quantity] [int] NULL,
	[Description] [ntext] NULL,
	[Image] [ntext] NULL,
	[Author] [nvarchar](50) NULL,
 CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Product_Images]    Script Date: 22/1/2021 10:42:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product_Images](
	[id] [int] NOT NULL,
	[ProductId] [int] NOT NULL,
	[Name] [nvarchar](50) NULL,
 CONSTRAINT [PK_Product_Images_1] PRIMARY KEY CLUSTERED 
(
	[id] ASC,
	[ProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Purchase]    Script Date: 22/1/2021 10:42:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Purchase](
	[Purchase_ID] [int] IDENTITY(1,1) NOT NULL,
	[Total] [decimal](19, 4) NULL,
	[Created_At] [datetime] NULL,
	[Status] [int] NULL,
	[Customer_Tel] [nvarchar](50) NULL,
 CONSTRAINT [PK_Purchase] PRIMARY KEY CLUSTERED 
(
	[Purchase_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PurchaseDetail]    Script Date: 22/1/2021 10:42:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PurchaseDetail](
	[PurchaseDetail_ID] [int] IDENTITY(1,1) NOT NULL,
	[Purchase_ID] [int] NOT NULL,
	[Product_ID] [int] NOT NULL,
	[Price] [decimal](19, 4) NOT NULL,
	[Quantity] [int] NOT NULL,
	[Total] [int] NOT NULL,
 CONSTRAINT [PK_PurchaseDetail] PRIMARY KEY CLUSTERED 
(
	[PurchaseDetail_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PurchaseStatusEnum]    Script Date: 22/1/2021 10:42:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PurchaseStatusEnum](
	[EnumKey] [nvarchar](50) NOT NULL,
	[Value] [int] NOT NULL,
	[Description] [ntext] NULL,
 CONSTRAINT [PK_PurchaseStatusEnum] PRIMARY KEY CLUSTERED 
(
	[Value] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[Product]  WITH CHECK ADD  CONSTRAINT [FK_Product_Category] FOREIGN KEY([CatId])
REFERENCES [dbo].[Category] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Product] CHECK CONSTRAINT [FK_Product_Category]
GO
ALTER TABLE [dbo].[Product_Images]  WITH CHECK ADD  CONSTRAINT [FK_Product_Images_Product] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Product] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Product_Images] CHECK CONSTRAINT [FK_Product_Images_Product]
GO
ALTER TABLE [dbo].[Purchase]  WITH CHECK ADD  CONSTRAINT [FK_Purchase_Customer] FOREIGN KEY([Customer_Tel])
REFERENCES [dbo].[Customer] ([Tel])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Purchase] CHECK CONSTRAINT [FK_Purchase_Customer]
GO
ALTER TABLE [dbo].[Purchase]  WITH CHECK ADD  CONSTRAINT [FK_Purchase_PurchaseStatusEnum] FOREIGN KEY([Status])
REFERENCES [dbo].[PurchaseStatusEnum] ([Value])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Purchase] CHECK CONSTRAINT [FK_Purchase_PurchaseStatusEnum]
GO
ALTER TABLE [dbo].[PurchaseDetail]  WITH CHECK ADD  CONSTRAINT [FK_PurchaseDetail_Product] FOREIGN KEY([Product_ID])
REFERENCES [dbo].[Product] ([Id])
GO
ALTER TABLE [dbo].[PurchaseDetail] CHECK CONSTRAINT [FK_PurchaseDetail_Product]
GO
ALTER TABLE [dbo].[PurchaseDetail]  WITH CHECK ADD  CONSTRAINT [FK_PurchaseDetail_Purchase] FOREIGN KEY([Purchase_ID])
REFERENCES [dbo].[Purchase] ([Purchase_ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PurchaseDetail] CHECK CONSTRAINT [FK_PurchaseDetail_Purchase]
GO
/****** Object:  StoredProcedure [dbo].[sp_BestSellerPurchase]    Script Date: 22/1/2021 10:42:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[sp_BestSellerPurchase]
as
begin

select TOP(5) Product_ID,sum(Quantity) as TotalQuanlity from PurchaseDetail
group by Product_ID
order by TotalQuanlity desc
end
GO
/****** Object:  StoredProcedure [dbo].[sp_BestSellerPurchasePrice]    Script Date: 22/1/2021 10:42:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE proc [dbo].[sp_BestSellerPurchasePrice]
as
begin
	select TOP(5) Product_ID,sum(Total) as Total from PurchaseDetail
	group by Product_ID
end
GO
/****** Object:  StoredProcedure [dbo].[sp_InsertPurchase]    Script Date: 22/1/2021 10:42:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[sp_InsertPurchase]
@Customer_Tel nvarchar(50),@Created_At datetime, @Total decimal, @Status int = null,
@id int output
as
begin
	insert into Purchase(Customer_Tel, Created_At, Total, Status) values(@Customer_Tel,@Created_At, @Total, @Status)
	set @id = (select Purchase_ID from Purchase where Customer_Tel = @Customer_Tel and Created_At = @Created_At)
	return @id
end
GO
/****** Object:  StoredProcedure [dbo].[sp_NewPurchase]    Script Date: 22/1/2021 10:42:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE proc [dbo].[sp_NewPurchase]
as
begin
select TOP(5) * from Purchase
order by (Created_At) desc
end
GO
/****** Object:  StoredProcedure [dbo].[sp_SumAtMonth]    Script Date: 22/1/2021 10:42:45 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_SumAtMonth]
@Year_Purchase int
as
begin
declare @Month int = 1
declare @TotalMonth decimal
declare @MonthTable table(Thang int , TotalByMonth decimal)
	while(@Month <=12)
	begin
		set @TotalMonth = (Select sum(Total) as tootal from Purchase
		where MONTH(Created_At) = @Month
		and Year(Created_At) = @Year_Purchase)
		if(@TotalMonth is not NULL)
		begin
			Insert into @MonthTable values(@Month,@TotalMonth)
		end
		else
			Insert into @MonthTable values(@Month,0)
		
		set @Month += 1

	end
select * from @MonthTable
end
GO
