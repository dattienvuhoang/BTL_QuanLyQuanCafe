USE [master]
GO
/****** Object:  Database [QuanLyQuanCafe]    Script Date: 4/20/2023 10:01:24 PM ******/
CREATE DATABASE [QuanLyQuanCafe]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'QuanLyQuanCafe', FILENAME = N'D:\SQL\MSSQL15.SQLEXPRESS\MSSQL\DATA\QuanLyQuanCafe.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'QuanLyQuanCafe_log', FILENAME = N'D:\SQL\MSSQL15.SQLEXPRESS\MSSQL\DATA\QuanLyQuanCafe_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [QuanLyQuanCafe].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [QuanLyQuanCafe] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [QuanLyQuanCafe] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [QuanLyQuanCafe] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [QuanLyQuanCafe] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [QuanLyQuanCafe] SET ARITHABORT OFF 
GO
ALTER DATABASE [QuanLyQuanCafe] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [QuanLyQuanCafe] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [QuanLyQuanCafe] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [QuanLyQuanCafe] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [QuanLyQuanCafe] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [QuanLyQuanCafe] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [QuanLyQuanCafe] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [QuanLyQuanCafe] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [QuanLyQuanCafe] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [QuanLyQuanCafe] SET  ENABLE_BROKER 
GO
ALTER DATABASE [QuanLyQuanCafe] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [QuanLyQuanCafe] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [QuanLyQuanCafe] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [QuanLyQuanCafe] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [QuanLyQuanCafe] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [QuanLyQuanCafe] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [QuanLyQuanCafe] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [QuanLyQuanCafe] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [QuanLyQuanCafe] SET  MULTI_USER 
GO
ALTER DATABASE [QuanLyQuanCafe] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [QuanLyQuanCafe] SET DB_CHAINING OFF 
GO
ALTER DATABASE [QuanLyQuanCafe] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [QuanLyQuanCafe] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [QuanLyQuanCafe] SET DELAYED_DURABILITY = DISABLED 
GO
USE [QuanLyQuanCafe]
GO
/****** Object:  Table [dbo].[bill]    Script Date: 4/20/2023 10:01:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[bill](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[check_in] [date] NOT NULL,
	[check_out] [date] NULL,
	[dinner_table_id] [int] NOT NULL,
	[status] [int] NOT NULL,
	[discount] [int] NULL,
	[toltal] [float] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[bill_detail]    Script Date: 4/20/2023 10:01:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[bill_detail](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[bill_id] [int] NOT NULL,
	[food_id] [int] NOT NULL,
	[quantity] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[category]    Script Date: 4/20/2023 10:01:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[category](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[cat_name] [nvarchar](30) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[dinner_table]    Script Date: 4/20/2023 10:01:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[dinner_table](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[dinner_table_name] [nvarchar](30) NOT NULL,
	[status] [nvarchar](20) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[food]    Script Date: 4/20/2023 10:01:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[food](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[food_name] [nvarchar](30) NOT NULL,
	[category_id] [int] NOT NULL,
	[price] [float] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[login]    Script Date: 4/20/2023 10:01:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[login](
	[username] [nvarchar](30) NOT NULL,
	[password] [nvarchar](30) NOT NULL,
	[name] [nvarchar](50) NOT NULL,
	[role] [nvarchar](15) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[username] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[bill] ADD  DEFAULT ((0)) FOR [status]
GO
ALTER TABLE [dbo].[bill] ADD  DEFAULT ((0)) FOR [discount]
GO
ALTER TABLE [dbo].[bill_detail] ADD  DEFAULT ((0)) FOR [quantity]
GO
ALTER TABLE [dbo].[food] ADD  DEFAULT ((0)) FOR [price]
GO
ALTER TABLE [dbo].[bill]  WITH CHECK ADD FOREIGN KEY([dinner_table_id])
REFERENCES [dbo].[dinner_table] ([id])
GO
ALTER TABLE [dbo].[bill_detail]  WITH CHECK ADD FOREIGN KEY([food_id])
REFERENCES [dbo].[food] ([id])
GO
ALTER TABLE [dbo].[bill_detail]  WITH CHECK ADD FOREIGN KEY([bill_id])
REFERENCES [dbo].[bill] ([id])
GO
ALTER TABLE [dbo].[food]  WITH CHECK ADD FOREIGN KEY([category_id])
REFERENCES [dbo].[category] ([id])
GO
/****** Object:  StoredProcedure [dbo].[USP_GetAccountByUsername]    Script Date: 4/20/2023 10:01:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[USP_GetAccountByUsername]
@userName nvarchar(100)
AS
BEGIN
	SELECT * FROM login WHERE username = @userName
END
GO
/****** Object:  StoredProcedure [dbo].[USP_GetDinnerTableList]    Script Date: 4/20/2023 10:01:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[USP_GetDinnerTableList]
AS	SELECT * FROM dinner_table
GO
/****** Object:  StoredProcedure [dbo].[USP_GetListBillByDate]    Script Date: 4/20/2023 10:01:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[USP_GetListBillByDate]
@checkIn DATE , @checkout DATE
AS 
BEGIN
	SELECT t.dinner_table_name, b.toltal, b.check_in, b.check_out, b.discount
	FROM dbo.bill AS b , dbo.dinner_table AS t 
	WHERE b.check_in >= @checkIn AND b.check_out <= @checkout AND b.status = 1 
	AND t.id =b.dinner_table_id 
END
GO
/****** Object:  StoredProcedure [dbo].[USP_InsertBill]    Script Date: 4/20/2023 10:01:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[USP_InsertBill]
@dinner_table_id INT
AS
BEGIN
	INSERT INTO bill(check_in, check_out, dinner_table_id, status, discount) VALUES (GETDATE(), NULL, @dinner_table_id, 0, 0)
END
GO
/****** Object:  StoredProcedure [dbo].[USP_InsertBillDetail]    Script Date: 4/20/2023 10:01:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[USP_InsertBillDetail]
@bill_id INT, @food_id INT, @quantity INT
AS
BEGIN

	DECLARE @isExitsBillDetail INT -- Kiểm tra bill_detail có tồn tại hay ko
	DECLARE @foodQuantity INT = 1
	SELECT @isExitsBillDetail = id, @foodQuantity = bill_detail.quantity FROM bill_detail WHERE bill_id = @bill_id AND food_id = @food_id

	IF(@isExitsBillDetail > 0) -- Nếu tồn tại
	BEGIN
		DECLARE @newQuantity INT = @foodQuantity + @quantity
		IF(@newQuantity > 0) -- Nếu thêm số lượng là số dương thì cộng vào
			UPDATE bill_detail SET quantity = @foodQuantity + @quantity WHERE food_id = @food_id
		ELSE -- Nếu thêm số lượng là số âm thì trừ đi
			DELETE bill_detail WHERE bill_id = @bill_id AND food_id = @food_id
	END

	ELSE -- Nếu chưa tồn tại bill thì insert vào
	BEGIN
		INSERT INTO bill_detail(bill_id, food_id, quantity) VALUES (@bill_id, @food_id, @quantity)
	END

END
GO
/****** Object:  StoredProcedure [dbo].[USP_Login]    Script Date: 4/20/2023 10:01:24 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[USP_Login]
@userName nvarchar(100), @passWord nvarchar(100)
AS
BEGIN
	SELECT * FROM login WHERE username = @userName AND password = @passWord
END
GO
USE [master]
GO
ALTER DATABASE [QuanLyQuanCafe] SET  READ_WRITE 
GO
