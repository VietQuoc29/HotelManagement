USE [master]
GO
/****** Object:  Database [DEVHotelManager]    Script Date: 5/20/2022 10:35:10 AM ******/
CREATE DATABASE [DEVHotelManager]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'DEVHotelManager', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\DEVHotelManager.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'DEVHotelManager_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\DEVHotelManager_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [DEVHotelManager] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [DEVHotelManager].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [DEVHotelManager] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [DEVHotelManager] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [DEVHotelManager] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [DEVHotelManager] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [DEVHotelManager] SET ARITHABORT OFF 
GO
ALTER DATABASE [DEVHotelManager] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [DEVHotelManager] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [DEVHotelManager] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [DEVHotelManager] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [DEVHotelManager] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [DEVHotelManager] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [DEVHotelManager] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [DEVHotelManager] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [DEVHotelManager] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [DEVHotelManager] SET  ENABLE_BROKER 
GO
ALTER DATABASE [DEVHotelManager] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [DEVHotelManager] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [DEVHotelManager] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [DEVHotelManager] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [DEVHotelManager] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [DEVHotelManager] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [DEVHotelManager] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [DEVHotelManager] SET RECOVERY FULL 
GO
ALTER DATABASE [DEVHotelManager] SET  MULTI_USER 
GO
ALTER DATABASE [DEVHotelManager] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [DEVHotelManager] SET DB_CHAINING OFF 
GO
ALTER DATABASE [DEVHotelManager] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [DEVHotelManager] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [DEVHotelManager] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [DEVHotelManager] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'DEVHotelManager', N'ON'
GO
ALTER DATABASE [DEVHotelManager] SET QUERY_STORE = OFF
GO
USE [DEVHotelManager]
GO
/****** Object:  Schema [letiendung130196_SQLLogin_2]    Script Date: 5/20/2022 10:35:10 AM ******/
CREATE SCHEMA [letiendung130196_SQLLogin_2]
GO
/****** Object:  UserDefinedFunction [dbo].[ufnRemoveMark]    Script Date: 5/20/2022 10:35:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE FUNCTION [dbo].[ufnRemoveMark] (
	@text nvarchar(max)
)
	RETURNS nvarchar(max)
AS
BEGIN
	SET @text = LOWER(@text)
		DECLARE @textLen int = LEN(@text)
	IF @textLen > 0
	BEGIN
		DECLARE @index int = 1
		DECLARE @lPos int
		DECLARE @SIGN_CHARS nvarchar(100) = N'ăâđêôơưàảãạáằẳẵặắầẩẫậấèẻẽẹéềểễệếìỉĩịíòỏõọóồổỗộốờởỡợớùủũụúừửữựứỳỷỹỵýđð'
		DECLARE @UNSIGN_CHARS varchar(100) = 'aadeoouaaaaaaaaaaaaaaaeeeeeeeeeeiiiiiooooooooooooooouuuuuuuuuuyyyyydd'

	WHILE @index <= @textLen
		BEGIN
			SET @lPos = CHARINDEX(SUBSTRING(@text,@index,1),@SIGN_CHARS)
			IF @lPos > 0
				BEGIN
					SET @text = STUFF(@text,@index,1,SUBSTRING(@UNSIGN_CHARS,@lPos,1))
				END
			SET @index = @index + 1
		END
	END
	RETURN @text
END
GO
/****** Object:  Table [dbo].[Customers]    Script Date: 5/20/2022 10:35:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customers](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[FullName] [nvarchar](50) NULL,
	[IDCard] [varchar](12) NULL,
	[PhoneNumber] [varchar](11) NULL,
	[Address] [nvarchar](500) NULL,
	[DateOfBirth] [datetime] NULL,
	[Note] [nvarchar](500) NULL,
	[SexId] [bigint] NULL,
	[CreatedBy] [varchar](50) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedBy] [varchar](50) NULL,
	[ModifiedDate] [datetime] NULL,
	[DeletedBy] [varchar](50) NULL,
	[DeletedDate] [datetime] NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_Customers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Floors]    Script Date: 5/20/2022 10:35:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Floors](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[CreatedBy] [varchar](50) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedBy] [varchar](50) NULL,
	[ModifiedDate] [datetime] NULL,
	[DeletedBy] [varchar](50) NULL,
	[DeletedDate] [datetime] NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_Floors] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[HotelImages]    Script Date: 5/20/2022 10:35:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HotelImages](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[ImageLink] [nvarchar](255) NULL,
	[RoomId] [bigint] NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_HotelImages] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Hotels]    Script Date: 5/20/2022 10:35:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Hotels](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Address] [nvarchar](255) NULL,
	[Title] [nvarchar](150) NULL,
	[Introduce] [nvarchar](max) NULL,
	[Star] [int] NULL,
	[Note] [nvarchar](500) NULL,
	[ProvinceId] [bigint] NULL,
	[CreatedBy] [varchar](50) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedBy] [varchar](50) NULL,
	[ModifiedDate] [datetime] NULL,
	[DeletedBy] [varchar](50) NULL,
	[DeletedDate] [datetime] NULL,
	[IsDeleted] [bit] NOT NULL,
	[Image] [nvarchar](255) NULL,
 CONSTRAINT [PK_Hotels] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderRoom]    Script Date: 5/20/2022 10:35:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderRoom](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[CustomerId] [bigint] NULL,
	[RoomId] [bigint] NULL,
	[StartTime] [datetime] NULL,
	[EndTime] [datetime] NULL,
	[TotalPayment] [bigint] NULL,
	[Status] [bit] NOT NULL,
	[CreatedBy] [varchar](50) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedBy] [varchar](50) NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_OrderRoom] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Provinces]    Script Date: 5/20/2022 10:35:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Provinces](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[ImageLink] [nvarchar](255) NULL,
	[CreatedBy] [varchar](50) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedBy] [varchar](50) NULL,
	[ModifiedDate] [datetime] NULL,
	[DeletedBy] [varchar](50) NULL,
	[DeletedDate] [datetime] NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_Provinces] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RegisterRooms]    Script Date: 5/20/2022 10:35:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RegisterRooms](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[FullName] [nvarchar](50) NULL,
	[Email] [nvarchar](50) NULL,
	[PhoneNumber] [nvarchar](11) NULL,
	[RoomId] [bigint] NULL,
	[TimeFrom] [datetime] NULL,
	[TimeTo] [datetime] NULL,
	[Note] [nvarchar](500) NULL,
	[Message] [nvarchar](500) NULL,
	[Status] [bit] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedBy] [varchar](50) NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_RegisterRooms] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Roles]    Script Date: 5/20/2022 10:35:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
 CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RoomCategories]    Script Date: 5/20/2022 10:35:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RoomCategories](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[CreatedBy] [varchar](50) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedBy] [varchar](50) NULL,
	[ModifiedDate] [datetime] NULL,
	[DeletedBy] [varchar](50) NULL,
	[DeletedDate] [datetime] NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_RoomCategories] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Rooms]    Script Date: 5/20/2022 10:35:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Rooms](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Price] [bigint] NULL,
	[PromotionalPrice] [bigint] NULL,
	[Star] [int] NULL,
	[IsActive] [bit] NOT NULL,
	[RoomStatusId] [bigint] NULL,
	[RoomCategoriesId] [bigint] NULL,
	[HotelId] [bigint] NULL,
	[FloorId] [bigint] NULL,
	[CreatedBy] [varchar](50) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedBy] [varchar](50) NULL,
	[ModifiedDate] [datetime] NULL,
	[DeletedBy] [varchar](50) NULL,
	[DeletedDate] [datetime] NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_Rooms] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RoomStatus]    Script Date: 5/20/2022 10:35:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RoomStatus](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
 CONSTRAINT [PK_StatusRooms] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ServiceCategories]    Script Date: 5/20/2022 10:35:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ServiceCategories](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[CreatedBy] [varchar](50) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedBy] [varchar](50) NULL,
	[ModifiedDate] [datetime] NULL,
	[DeletedBy] [varchar](50) NULL,
	[DeletedDate] [datetime] NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_ServiceCategories] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Services]    Script Date: 5/20/2022 10:35:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Services](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Price] [bigint] NULL,
	[Unit] [nvarchar](50) NULL,
	[Status] [bit] NOT NULL,
	[Note] [nvarchar](500) NULL,
	[ServiceCategoriesId] [bigint] NULL,
	[CreatedBy] [varchar](50) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedBy] [varchar](50) NULL,
	[ModifiedDate] [datetime] NULL,
	[DeletedBy] [varchar](50) NULL,
	[DeletedDate] [datetime] NULL,
	[IsDeleted] [bit] NOT NULL,
	[Image] [varchar](255) NULL,
 CONSTRAINT [PK_Services] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Sex]    Script Date: 5/20/2022 10:35:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sex](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
 CONSTRAINT [PK_Sex] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Transactions]    Script Date: 5/20/2022 10:35:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Transactions](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[OrderRoomId] [bigint] NULL,
	[ServiceId] [bigint] NULL,
	[Quantity] [bigint] NULL,
	[CreatedBy] [varchar](50) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Transactions] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserProfiles]    Script Date: 5/20/2022 10:35:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserProfiles](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[UserName] [varchar](50) NULL,
	[PassHash] [varchar](255) NULL,
	[FullName] [nvarchar](50) NULL,
	[DateOfBirth] [datetime] NULL,
	[PhoneNumber] [varchar](11) NULL,
	[Address] [nvarchar](150) NULL,
	[Email] [varchar](150) NULL,
	[Facebook] [varchar](50) NULL,
	[Zalo] [varchar](50) NULL,
	[Active] [bit] NOT NULL,
	[SexId] [bigint] NULL,
	[RoleId] [bigint] NULL,
	[CreatedBy] [varchar](50) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedBy] [varchar](50) NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_UserProfiles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Customers] ON 

INSERT [dbo].[Customers] ([Id], [FullName], [IDCard], [PhoneNumber], [Address], [DateOfBirth], [Note], [SexId], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedBy], [DeletedDate], [IsDeleted]) VALUES (1, N'd', NULL, NULL, N'd', NULL, NULL, NULL, N'admin', CAST(N'2022-04-11T23:15:02.860' AS DateTime), NULL, NULL, N'admin', CAST(N'2022-04-16T09:16:36.573' AS DateTime), 0)
INSERT [dbo].[Customers] ([Id], [FullName], [IDCard], [PhoneNumber], [Address], [DateOfBirth], [Note], [SexId], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedBy], [DeletedDate], [IsDeleted]) VALUES (2, N'le tien dung', N'1421050317', N'222222222', NULL, NULL, NULL, NULL, N'admin', CAST(N'2022-04-16T09:44:35.433' AS DateTime), NULL, NULL, NULL, NULL, 0)
INSERT [dbo].[Customers] ([Id], [FullName], [IDCard], [PhoneNumber], [Address], [DateOfBirth], [Note], [SexId], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedBy], [DeletedDate], [IsDeleted]) VALUES (3, N'ádas', N'1421050317s', NULL, NULL, NULL, NULL, NULL, N'admin', CAST(N'2022-04-16T09:44:50.133' AS DateTime), NULL, NULL, NULL, NULL, 0)
INSERT [dbo].[Customers] ([Id], [FullName], [IDCard], [PhoneNumber], [Address], [DateOfBirth], [Note], [SexId], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedBy], [DeletedDate], [IsDeleted]) VALUES (4, N'ád', N'ád', NULL, NULL, NULL, NULL, NULL, N'admin', CAST(N'2022-04-16T09:47:13.933' AS DateTime), NULL, NULL, NULL, NULL, 0)
INSERT [dbo].[Customers] ([Id], [FullName], [IDCard], [PhoneNumber], [Address], [DateOfBirth], [Note], [SexId], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedBy], [DeletedDate], [IsDeleted]) VALUES (5, N'ád', N'ádd', NULL, NULL, NULL, NULL, NULL, N'admin', CAST(N'2022-04-16T09:50:53.073' AS DateTime), NULL, NULL, NULL, NULL, 0)
INSERT [dbo].[Customers] ([Id], [FullName], [IDCard], [PhoneNumber], [Address], [DateOfBirth], [Note], [SexId], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedBy], [DeletedDate], [IsDeleted]) VALUES (6, N'ád', N'01', NULL, NULL, NULL, NULL, NULL, N'admin', CAST(N'2022-04-16T10:02:40.113' AS DateTime), N'admin', CAST(N'2022-04-16T10:03:00.777' AS DateTime), NULL, NULL, 0)
INSERT [dbo].[Customers] ([Id], [FullName], [IDCard], [PhoneNumber], [Address], [DateOfBirth], [Note], [SexId], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedBy], [DeletedDate], [IsDeleted]) VALUES (7, N'1233', N'122222222', N'123', NULL, NULL, NULL, NULL, N'admin', CAST(N'2022-04-16T10:05:00.157' AS DateTime), NULL, NULL, NULL, NULL, 0)
INSERT [dbo].[Customers] ([Id], [FullName], [IDCard], [PhoneNumber], [Address], [DateOfBirth], [Note], [SexId], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedBy], [DeletedDate], [IsDeleted]) VALUES (8, N'ưe', N'123333333', NULL, NULL, NULL, NULL, NULL, N'admin', CAST(N'2022-04-16T10:14:47.910' AS DateTime), NULL, NULL, NULL, NULL, 0)
INSERT [dbo].[Customers] ([Id], [FullName], [IDCard], [PhoneNumber], [Address], [DateOfBirth], [Note], [SexId], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedBy], [DeletedDate], [IsDeleted]) VALUES (9, N'123', N'1233333333', N'123', NULL, NULL, NULL, 5, N'admin', CAST(N'2022-04-16T10:15:04.813' AS DateTime), N'admin', CAST(N'2022-04-21T11:31:16.853' AS DateTime), NULL, NULL, 0)
INSERT [dbo].[Customers] ([Id], [FullName], [IDCard], [PhoneNumber], [Address], [DateOfBirth], [Note], [SexId], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedBy], [DeletedDate], [IsDeleted]) VALUES (10, N'qưe', N'222222222', N'33333333333', N'12312', CAST(N'2022-04-17T00:00:00.000' AS DateTime), N'123123', 4, N'admin', CAST(N'2022-04-17T06:43:27.917' AS DateTime), N'admin', CAST(N'2022-04-21T11:31:12.547' AS DateTime), NULL, NULL, 0)
INSERT [dbo].[Customers] ([Id], [FullName], [IDCard], [PhoneNumber], [Address], [DateOfBirth], [Note], [SexId], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedBy], [DeletedDate], [IsDeleted]) VALUES (12, N'ádda', N'1233333331', NULL, NULL, NULL, NULL, NULL, N'admin', CAST(N'2022-04-21T11:43:36.393' AS DateTime), NULL, NULL, NULL, NULL, 0)
INSERT [dbo].[Customers] ([Id], [FullName], [IDCard], [PhoneNumber], [Address], [DateOfBirth], [Note], [SexId], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedBy], [DeletedDate], [IsDeleted]) VALUES (13, N'Dung', N'0949234086', N'0949234086', N'0949234086', CAST(N'1996-01-13T00:00:00.000' AS DateTime), N'dddddddddddddd', 4, N'admin', CAST(N'2022-04-26T23:55:41.087' AS DateTime), NULL, NULL, NULL, NULL, 0)
SET IDENTITY_INSERT [dbo].[Customers] OFF
GO
SET IDENTITY_INSERT [dbo].[Floors] ON 

INSERT [dbo].[Floors] ([Id], [Name], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedBy], [DeletedDate], [IsDeleted]) VALUES (1, N'123', N'admin', CAST(N'2022-04-12T13:29:36.040' AS DateTime), N'admin', CAST(N'2022-05-08T13:31:03.823' AS DateTime), NULL, NULL, 0)
INSERT [dbo].[Floors] ([Id], [Name], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedBy], [DeletedDate], [IsDeleted]) VALUES (2, N'123d', N'admin', CAST(N'2022-04-17T07:50:08.883' AS DateTime), N'admin', CAST(N'2022-04-17T07:50:19.240' AS DateTime), N'admin', CAST(N'2022-04-17T07:50:26.317' AS DateTime), 1)
SET IDENTITY_INSERT [dbo].[Floors] OFF
GO
SET IDENTITY_INSERT [dbo].[HotelImages] ON 

INSERT [dbo].[HotelImages] ([Id], [ImageLink], [RoomId], [IsDeleted]) VALUES (1, NULL, 0, 0)
INSERT [dbo].[HotelImages] ([Id], [ImageLink], [RoomId], [IsDeleted]) VALUES (2, NULL, 0, 0)
INSERT [dbo].[HotelImages] ([Id], [ImageLink], [RoomId], [IsDeleted]) VALUES (3, NULL, 0, 0)
INSERT [dbo].[HotelImages] ([Id], [ImageLink], [RoomId], [IsDeleted]) VALUES (4, NULL, 0, 0)
INSERT [dbo].[HotelImages] ([Id], [ImageLink], [RoomId], [IsDeleted]) VALUES (5, NULL, 0, 0)
INSERT [dbo].[HotelImages] ([Id], [ImageLink], [RoomId], [IsDeleted]) VALUES (6, NULL, 0, 0)
INSERT [dbo].[HotelImages] ([Id], [ImageLink], [RoomId], [IsDeleted]) VALUES (10, NULL, 0, 0)
INSERT [dbo].[HotelImages] ([Id], [ImageLink], [RoomId], [IsDeleted]) VALUES (11, NULL, 1, 0)
INSERT [dbo].[HotelImages] ([Id], [ImageLink], [RoomId], [IsDeleted]) VALUES (12, NULL, 2, 0)
INSERT [dbo].[HotelImages] ([Id], [ImageLink], [RoomId], [IsDeleted]) VALUES (13, NULL, 0, 0)
INSERT [dbo].[HotelImages] ([Id], [ImageLink], [RoomId], [IsDeleted]) VALUES (14, NULL, 0, 0)
INSERT [dbo].[HotelImages] ([Id], [ImageLink], [RoomId], [IsDeleted]) VALUES (15, NULL, 3, 0)
INSERT [dbo].[HotelImages] ([Id], [ImageLink], [RoomId], [IsDeleted]) VALUES (16, NULL, 4, 0)
INSERT [dbo].[HotelImages] ([Id], [ImageLink], [RoomId], [IsDeleted]) VALUES (17, NULL, 5, 0)
INSERT [dbo].[HotelImages] ([Id], [ImageLink], [RoomId], [IsDeleted]) VALUES (18, NULL, 5, 0)
INSERT [dbo].[HotelImages] ([Id], [ImageLink], [RoomId], [IsDeleted]) VALUES (19, NULL, 6, 0)
INSERT [dbo].[HotelImages] ([Id], [ImageLink], [RoomId], [IsDeleted]) VALUES (20, NULL, 6, 0)
INSERT [dbo].[HotelImages] ([Id], [ImageLink], [RoomId], [IsDeleted]) VALUES (21, NULL, 7, 0)
INSERT [dbo].[HotelImages] ([Id], [ImageLink], [RoomId], [IsDeleted]) VALUES (22, NULL, 7, 0)
INSERT [dbo].[HotelImages] ([Id], [ImageLink], [RoomId], [IsDeleted]) VALUES (23, NULL, 0, 0)
INSERT [dbo].[HotelImages] ([Id], [ImageLink], [RoomId], [IsDeleted]) VALUES (24, NULL, 0, 0)
INSERT [dbo].[HotelImages] ([Id], [ImageLink], [RoomId], [IsDeleted]) VALUES (25, NULL, 0, 0)
INSERT [dbo].[HotelImages] ([Id], [ImageLink], [RoomId], [IsDeleted]) VALUES (26, NULL, 0, 0)
INSERT [dbo].[HotelImages] ([Id], [ImageLink], [RoomId], [IsDeleted]) VALUES (27, NULL, 8, 0)
INSERT [dbo].[HotelImages] ([Id], [ImageLink], [RoomId], [IsDeleted]) VALUES (28, NULL, 8, 0)
INSERT [dbo].[HotelImages] ([Id], [ImageLink], [RoomId], [IsDeleted]) VALUES (29, NULL, 9, 0)
INSERT [dbo].[HotelImages] ([Id], [ImageLink], [RoomId], [IsDeleted]) VALUES (30, NULL, 9, 0)
INSERT [dbo].[HotelImages] ([Id], [ImageLink], [RoomId], [IsDeleted]) VALUES (31, NULL, 10, 0)
INSERT [dbo].[HotelImages] ([Id], [ImageLink], [RoomId], [IsDeleted]) VALUES (32, NULL, 10, 0)
INSERT [dbo].[HotelImages] ([Id], [ImageLink], [RoomId], [IsDeleted]) VALUES (33, NULL, 11, 0)
INSERT [dbo].[HotelImages] ([Id], [ImageLink], [RoomId], [IsDeleted]) VALUES (34, NULL, 11, 0)
INSERT [dbo].[HotelImages] ([Id], [ImageLink], [RoomId], [IsDeleted]) VALUES (35, NULL, 12, 0)
INSERT [dbo].[HotelImages] ([Id], [ImageLink], [RoomId], [IsDeleted]) VALUES (36, NULL, 12, 0)
INSERT [dbo].[HotelImages] ([Id], [ImageLink], [RoomId], [IsDeleted]) VALUES (37, NULL, 13, 0)
INSERT [dbo].[HotelImages] ([Id], [ImageLink], [RoomId], [IsDeleted]) VALUES (38, NULL, 13, 0)
INSERT [dbo].[HotelImages] ([Id], [ImageLink], [RoomId], [IsDeleted]) VALUES (39, NULL, 14, 0)
INSERT [dbo].[HotelImages] ([Id], [ImageLink], [RoomId], [IsDeleted]) VALUES (40, NULL, 14, 0)
INSERT [dbo].[HotelImages] ([Id], [ImageLink], [RoomId], [IsDeleted]) VALUES (41, NULL, 15, 0)
INSERT [dbo].[HotelImages] ([Id], [ImageLink], [RoomId], [IsDeleted]) VALUES (42, NULL, 15, 0)
INSERT [dbo].[HotelImages] ([Id], [ImageLink], [RoomId], [IsDeleted]) VALUES (43, NULL, 16, 0)
INSERT [dbo].[HotelImages] ([Id], [ImageLink], [RoomId], [IsDeleted]) VALUES (44, NULL, 16, 0)
INSERT [dbo].[HotelImages] ([Id], [ImageLink], [RoomId], [IsDeleted]) VALUES (45, NULL, 17, 0)
INSERT [dbo].[HotelImages] ([Id], [ImageLink], [RoomId], [IsDeleted]) VALUES (46, NULL, 17, 0)
INSERT [dbo].[HotelImages] ([Id], [ImageLink], [RoomId], [IsDeleted]) VALUES (47, NULL, 18, 0)
INSERT [dbo].[HotelImages] ([Id], [ImageLink], [RoomId], [IsDeleted]) VALUES (48, NULL, 18, 0)
INSERT [dbo].[HotelImages] ([Id], [ImageLink], [RoomId], [IsDeleted]) VALUES (49, NULL, 22, 0)
INSERT [dbo].[HotelImages] ([Id], [ImageLink], [RoomId], [IsDeleted]) VALUES (50, NULL, 22, 0)
INSERT [dbo].[HotelImages] ([Id], [ImageLink], [RoomId], [IsDeleted]) VALUES (51, NULL, 24, 0)
INSERT [dbo].[HotelImages] ([Id], [ImageLink], [RoomId], [IsDeleted]) VALUES (52, NULL, 24, 0)
INSERT [dbo].[HotelImages] ([Id], [ImageLink], [RoomId], [IsDeleted]) VALUES (53, NULL, 24, 0)
INSERT [dbo].[HotelImages] ([Id], [ImageLink], [RoomId], [IsDeleted]) VALUES (54, NULL, 24, 0)
INSERT [dbo].[HotelImages] ([Id], [ImageLink], [RoomId], [IsDeleted]) VALUES (55, NULL, 24, 0)
INSERT [dbo].[HotelImages] ([Id], [ImageLink], [RoomId], [IsDeleted]) VALUES (56, NULL, 24, 0)
INSERT [dbo].[HotelImages] ([Id], [ImageLink], [RoomId], [IsDeleted]) VALUES (57, NULL, 24, 0)
INSERT [dbo].[HotelImages] ([Id], [ImageLink], [RoomId], [IsDeleted]) VALUES (58, NULL, 24, 0)
INSERT [dbo].[HotelImages] ([Id], [ImageLink], [RoomId], [IsDeleted]) VALUES (59, NULL, 24, 0)
INSERT [dbo].[HotelImages] ([Id], [ImageLink], [RoomId], [IsDeleted]) VALUES (60, NULL, 26, 0)
INSERT [dbo].[HotelImages] ([Id], [ImageLink], [RoomId], [IsDeleted]) VALUES (61, NULL, 26, 0)
INSERT [dbo].[HotelImages] ([Id], [ImageLink], [RoomId], [IsDeleted]) VALUES (62, NULL, 26, 0)
INSERT [dbo].[HotelImages] ([Id], [ImageLink], [RoomId], [IsDeleted]) VALUES (63, NULL, 26, 0)
INSERT [dbo].[HotelImages] ([Id], [ImageLink], [RoomId], [IsDeleted]) VALUES (64, NULL, 26, 0)
INSERT [dbo].[HotelImages] ([Id], [ImageLink], [RoomId], [IsDeleted]) VALUES (65, NULL, 26, 0)
INSERT [dbo].[HotelImages] ([Id], [ImageLink], [RoomId], [IsDeleted]) VALUES (66, NULL, 26, 0)
INSERT [dbo].[HotelImages] ([Id], [ImageLink], [RoomId], [IsDeleted]) VALUES (67, NULL, 26, 0)
INSERT [dbo].[HotelImages] ([Id], [ImageLink], [RoomId], [IsDeleted]) VALUES (68, NULL, 26, 0)
INSERT [dbo].[HotelImages] ([Id], [ImageLink], [RoomId], [IsDeleted]) VALUES (69, NULL, 26, 0)
INSERT [dbo].[HotelImages] ([Id], [ImageLink], [RoomId], [IsDeleted]) VALUES (70, NULL, 26, 0)
INSERT [dbo].[HotelImages] ([Id], [ImageLink], [RoomId], [IsDeleted]) VALUES (71, NULL, 25, 0)
INSERT [dbo].[HotelImages] ([Id], [ImageLink], [RoomId], [IsDeleted]) VALUES (72, NULL, 25, 0)
INSERT [dbo].[HotelImages] ([Id], [ImageLink], [RoomId], [IsDeleted]) VALUES (73, NULL, 25, 0)
INSERT [dbo].[HotelImages] ([Id], [ImageLink], [RoomId], [IsDeleted]) VALUES (74, NULL, 25, 0)
INSERT [dbo].[HotelImages] ([Id], [ImageLink], [RoomId], [IsDeleted]) VALUES (75, NULL, 25, 0)
INSERT [dbo].[HotelImages] ([Id], [ImageLink], [RoomId], [IsDeleted]) VALUES (76, NULL, 25, 0)
INSERT [dbo].[HotelImages] ([Id], [ImageLink], [RoomId], [IsDeleted]) VALUES (77, NULL, 25, 0)
INSERT [dbo].[HotelImages] ([Id], [ImageLink], [RoomId], [IsDeleted]) VALUES (78, NULL, 25, 0)
INSERT [dbo].[HotelImages] ([Id], [ImageLink], [RoomId], [IsDeleted]) VALUES (79, NULL, 25, 0)
INSERT [dbo].[HotelImages] ([Id], [ImageLink], [RoomId], [IsDeleted]) VALUES (80, NULL, 25, 0)
INSERT [dbo].[HotelImages] ([Id], [ImageLink], [RoomId], [IsDeleted]) VALUES (81, NULL, 24, 0)
INSERT [dbo].[HotelImages] ([Id], [ImageLink], [RoomId], [IsDeleted]) VALUES (82, NULL, 24, 0)
INSERT [dbo].[HotelImages] ([Id], [ImageLink], [RoomId], [IsDeleted]) VALUES (83, NULL, 24, 0)
INSERT [dbo].[HotelImages] ([Id], [ImageLink], [RoomId], [IsDeleted]) VALUES (84, NULL, 24, 0)
INSERT [dbo].[HotelImages] ([Id], [ImageLink], [RoomId], [IsDeleted]) VALUES (85, NULL, 23, 0)
INSERT [dbo].[HotelImages] ([Id], [ImageLink], [RoomId], [IsDeleted]) VALUES (86, NULL, 23, 0)
INSERT [dbo].[HotelImages] ([Id], [ImageLink], [RoomId], [IsDeleted]) VALUES (87, NULL, 23, 0)
INSERT [dbo].[HotelImages] ([Id], [ImageLink], [RoomId], [IsDeleted]) VALUES (88, NULL, 22, 0)
INSERT [dbo].[HotelImages] ([Id], [ImageLink], [RoomId], [IsDeleted]) VALUES (89, NULL, 26, 0)
INSERT [dbo].[HotelImages] ([Id], [ImageLink], [RoomId], [IsDeleted]) VALUES (90, NULL, 26, 0)
INSERT [dbo].[HotelImages] ([Id], [ImageLink], [RoomId], [IsDeleted]) VALUES (91, NULL, 26, 0)
INSERT [dbo].[HotelImages] ([Id], [ImageLink], [RoomId], [IsDeleted]) VALUES (92, NULL, 26, 0)
INSERT [dbo].[HotelImages] ([Id], [ImageLink], [RoomId], [IsDeleted]) VALUES (93, NULL, 26, 0)
INSERT [dbo].[HotelImages] ([Id], [ImageLink], [RoomId], [IsDeleted]) VALUES (94, NULL, 25, 0)
INSERT [dbo].[HotelImages] ([Id], [ImageLink], [RoomId], [IsDeleted]) VALUES (95, NULL, 27, 0)
INSERT [dbo].[HotelImages] ([Id], [ImageLink], [RoomId], [IsDeleted]) VALUES (96, N'Screenshot 2022-05-19 165724.png', 28, 0)
SET IDENTITY_INSERT [dbo].[HotelImages] OFF
GO
SET IDENTITY_INSERT [dbo].[Hotels] ON 

INSERT [dbo].[Hotels] ([Id], [Name], [Address], [Title], [Introduce], [Star], [Note], [ProvinceId], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedBy], [DeletedDate], [IsDeleted], [Image]) VALUES (1, N'213213', NULL, NULL, NULL, NULL, NULL, NULL, N'admin', CAST(N'2022-04-12T22:13:24.943' AS DateTime), NULL, NULL, NULL, NULL, 0, NULL)
INSERT [dbo].[Hotels] ([Id], [Name], [Address], [Title], [Introduce], [Star], [Note], [ProvinceId], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedBy], [DeletedDate], [IsDeleted], [Image]) VALUES (2, N'DD', NULL, NULL, NULL, NULL, NULL, NULL, N'admin', CAST(N'2022-04-12T22:30:52.587' AS DateTime), NULL, NULL, NULL, NULL, 0, NULL)
INSERT [dbo].[Hotels] ([Id], [Name], [Address], [Title], [Introduce], [Star], [Note], [ProvinceId], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedBy], [DeletedDate], [IsDeleted], [Image]) VALUES (3, N'DD', NULL, NULL, NULL, NULL, NULL, NULL, N'admin', CAST(N'2022-04-12T22:30:56.810' AS DateTime), NULL, NULL, NULL, NULL, 0, NULL)
INSERT [dbo].[Hotels] ([Id], [Name], [Address], [Title], [Introduce], [Star], [Note], [ProvinceId], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedBy], [DeletedDate], [IsDeleted], [Image]) VALUES (4, N'DD', NULL, NULL, NULL, NULL, NULL, NULL, N'admin', CAST(N'2022-04-12T22:34:55.783' AS DateTime), NULL, NULL, NULL, NULL, 0, NULL)
INSERT [dbo].[Hotels] ([Id], [Name], [Address], [Title], [Introduce], [Star], [Note], [ProvinceId], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedBy], [DeletedDate], [IsDeleted], [Image]) VALUES (5, N'DD', N'HÀ NỘI', NULL, NULL, NULL, NULL, NULL, N'admin', CAST(N'2022-04-12T22:35:49.193' AS DateTime), NULL, NULL, NULL, NULL, 0, NULL)
INSERT [dbo].[Hotels] ([Id], [Name], [Address], [Title], [Introduce], [Star], [Note], [ProvinceId], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedBy], [DeletedDate], [IsDeleted], [Image]) VALUES (6, N'lolol', N'ádasd', NULL, NULL, NULL, NULL, NULL, N'admin', CAST(N'2022-04-13T13:21:24.920' AS DateTime), NULL, NULL, NULL, NULL, 0, NULL)
INSERT [dbo].[Hotels] ([Id], [Name], [Address], [Title], [Introduce], [Star], [Note], [ProvinceId], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedBy], [DeletedDate], [IsDeleted], [Image]) VALUES (7, N'lolol', N'ádasdd', NULL, NULL, NULL, NULL, NULL, N'admin', CAST(N'2022-04-13T13:24:21.253' AS DateTime), NULL, NULL, NULL, NULL, 0, NULL)
INSERT [dbo].[Hotels] ([Id], [Name], [Address], [Title], [Introduce], [Star], [Note], [ProvinceId], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedBy], [DeletedDate], [IsDeleted], [Image]) VALUES (8, N'lolol', N'ádasdda', NULL, NULL, NULL, NULL, NULL, N'admin', CAST(N'2022-04-13T13:28:17.487' AS DateTime), N'admin', CAST(N'2022-04-13T13:28:48.207' AS DateTime), NULL, NULL, 0, NULL)
INSERT [dbo].[Hotels] ([Id], [Name], [Address], [Title], [Introduce], [Star], [Note], [ProvinceId], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedBy], [DeletedDate], [IsDeleted], [Image]) VALUES (9, N'ưer', N'kk', NULL, NULL, NULL, NULL, NULL, N'admin', CAST(N'2022-04-13T14:35:19.217' AS DateTime), NULL, NULL, NULL, NULL, 0, NULL)
INSERT [dbo].[Hotels] ([Id], [Name], [Address], [Title], [Introduce], [Star], [Note], [ProvinceId], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedBy], [DeletedDate], [IsDeleted], [Image]) VALUES (10, N'ưer', N'kkk', NULL, NULL, NULL, NULL, NULL, N'admin', CAST(N'2022-04-13T14:39:44.967' AS DateTime), NULL, NULL, NULL, NULL, 0, NULL)
INSERT [dbo].[Hotels] ([Id], [Name], [Address], [Title], [Introduce], [Star], [Note], [ProvinceId], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedBy], [DeletedDate], [IsDeleted], [Image]) VALUES (11, N'ưer', N'kkkf', NULL, NULL, NULL, NULL, NULL, N'admin', CAST(N'2022-04-13T14:53:12.770' AS DateTime), NULL, NULL, NULL, NULL, 0, NULL)
INSERT [dbo].[Hotels] ([Id], [Name], [Address], [Title], [Introduce], [Star], [Note], [ProvinceId], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedBy], [DeletedDate], [IsDeleted], [Image]) VALUES (12, N'ưer', N'kkkf1', NULL, NULL, NULL, NULL, NULL, N'admin', CAST(N'2022-04-13T14:54:44.733' AS DateTime), NULL, NULL, N'admin', CAST(N'2022-04-22T00:52:12.857' AS DateTime), 1, NULL)
INSERT [dbo].[Hotels] ([Id], [Name], [Address], [Title], [Introduce], [Star], [Note], [ProvinceId], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedBy], [DeletedDate], [IsDeleted], [Image]) VALUES (13, N'Mường Thanh', N'Thanh Hóa', N'123', N'<p><strong>Kh&aacute;ch Sạn&nbsp;</strong>Được thiết kế cho cả c&aacute;c chuyến du lịch nghỉ ngơi v&agrave; c&ocirc;ng t&aacute;c, tọa lạc 17 H&agrave; Bổng, quận Sơn Tr&agrave;, Đ&agrave; Nẵng; một trong những khu vực nổi tiếng của th&agrave;nh phố. Kh&aacute;ch sạn n&agrave;y n&agrave;y c&oacute; vị tr&iacute; v&ocirc; c&ugrave;ng thuận lợi v&agrave; dễ tiếp cận c&aacute;c địa điểm lớn của th&agrave;nh phố n&agrave;y.&nbsp;</p>

<p>Thiết bị v&agrave; dịch vụ cung cấp bởi&nbsp;<strong>Kh&aacute;ch Sạn&nbsp;</strong>bảo đảm k&igrave; nghỉ dễ chịu cho du kh&aacute;ch. Kh&aacute;ch sạn cung cấp Wi-Fi ở khu vực c&ocirc;ng cộng, đưa đ&oacute;n kh&aacute;ch sạn/s&acirc;n bay, dịch vụ giặt l&agrave;/giặt&thinsp;kh&ocirc;, dịch vụ du lịch, ph&ograve;ng gia đ&igrave;nh để đảm bảo kh&aacute;ch của họ được thoải m&aacute;i nhất.&nbsp;</p>

<p>Chất lượng&nbsp;<strong>Kh&aacute;ch Sạn&nbsp;</strong>được phản &aacute;nh qua mỗi ph&ograve;ng. truy cập internet kh&ocirc;ng d&acirc;y, truy cập internet kh&ocirc;ng d&acirc;y (miễn ph&iacute;), v&ograve;i hoa sen, b&agrave;n, truyền h&igrave;nh c&aacute;p l&agrave; một số thiết bị m&agrave; bạn c&oacute; thể sử dụng v&agrave; h&agrave;i l&ograve;ng. B&ecirc;n cạnh đ&oacute;, kh&aacute;ch sạn c&ograve;n gợi &yacute; cho bạn những hoạt động vui chơi giải tr&iacute; bảo đảm bạn lu&ocirc;n thấy hứng th&uacute; trong suốt k&igrave; nghỉ.&nbsp;<strong>Kh&aacute;ch Sạn&nbsp;</strong>l&agrave; một sự lựa chọn th&ocirc;ng minh cho du kh&aacute;ch khi đến Đ&agrave; Nẵng, nơi mang lại cho họ một k&igrave; nghỉ thư gi&atilde;n v&agrave; thoải m&aacute;i.</p>

<p>Nếu bạn l&agrave; người th&iacute;ch kh&aacute;m ph&aacute; những di sản phi vật thể hay t&igrave;m hiểu trải nghiệm với thi&ecirc;n nhi&ecirc;n,&nbsp;<strong>Kh&aacute;ch Sạn&nbsp;</strong>&nbsp; c&oacute; những dịch vụ đặt tour tham quan phố cổ Hội An, c&ugrave; lao Ch&agrave;m, cố đ&ocirc; Huế, th&aacute;nh địa Mỹ Sơn...</p>

<p>Với phong cảnh hữu t&igrave;nh, n&ecirc;n thơ, ph&ograve;ng nghỉ hiện đại, cao cấp v&agrave; phong c&aacute;ch phục vụ chu đ&aacute;o, chuy&ecirc;n nghiệp, đặt ph&ograve;ng<strong>&nbsp;Kh&aacute;ch Sạn&nbsp;</strong>ch&iacute;nh l&agrave; lựa chọn đ&uacute;ng đắn cho kỳ nghỉ ngơi v&agrave; thư gi&atilde;n của bạn.</p>

<p>Với sự hỗ trợ của , bạn sẽ c&oacute; cơ hội đặt ph&ograve;ng gi&aacute; rẻ nhất c&ugrave;n<a name="_GoBack"></a>g nhiều chương tr&igrave;nh khuyến mại d&agrave;nh ri&ecirc;ng cho kh&aacute;ch h&agrave;ng của .&nbsp;</p>
', 5, N'123', 6, N'admin', CAST(N'2022-04-22T23:29:17.157' AS DateTime), N'admin', CAST(N'2022-05-16T15:56:54.290' AS DateTime), NULL, NULL, 0, NULL)
SET IDENTITY_INSERT [dbo].[Hotels] OFF
GO
SET IDENTITY_INSERT [dbo].[OrderRoom] ON 

INSERT [dbo].[OrderRoom] ([Id], [CustomerId], [RoomId], [StartTime], [EndTime], [TotalPayment], [Status], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (1, 10, 26, CAST(N'2022-04-27T00:30:59.987' AS DateTime), CAST(N'2022-05-20T10:20:24.083' AS DateTime), 1754674606, 1, N'admin', CAST(N'2022-04-27T00:31:00.427' AS DateTime), N'admin', CAST(N'2022-05-20T10:20:36.393' AS DateTime))
INSERT [dbo].[OrderRoom] ([Id], [CustomerId], [RoomId], [StartTime], [EndTime], [TotalPayment], [Status], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (2, 10, 25, CAST(N'2022-04-27T00:31:31.197' AS DateTime), NULL, NULL, 0, N'admin', CAST(N'2022-04-27T00:31:31.637' AS DateTime), NULL, NULL)
INSERT [dbo].[OrderRoom] ([Id], [CustomerId], [RoomId], [StartTime], [EndTime], [TotalPayment], [Status], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (3, 10, 16, CAST(N'2022-04-27T12:26:29.813' AS DateTime), NULL, NULL, 0, N'admin', CAST(N'2022-04-27T12:26:30.430' AS DateTime), NULL, NULL)
INSERT [dbo].[OrderRoom] ([Id], [CustomerId], [RoomId], [StartTime], [EndTime], [TotalPayment], [Status], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (4, 10, 24, CAST(N'2022-04-27T12:28:06.157' AS DateTime), NULL, NULL, 0, N'admin', CAST(N'2022-04-27T12:28:07.307' AS DateTime), NULL, NULL)
INSERT [dbo].[OrderRoom] ([Id], [CustomerId], [RoomId], [StartTime], [EndTime], [TotalPayment], [Status], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (5, 7, 20, CAST(N'2022-05-05T15:24:15.367' AS DateTime), CAST(N'2022-05-05T23:48:48.790' AS DateTime), 1000001412566, 1, N'admin', CAST(N'2022-05-05T15:24:16.270' AS DateTime), N'admin', CAST(N'2022-05-05T23:49:40.040' AS DateTime))
INSERT [dbo].[OrderRoom] ([Id], [CustomerId], [RoomId], [StartTime], [EndTime], [TotalPayment], [Status], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (6, 10, 20, CAST(N'2022-05-05T23:50:59.923' AS DateTime), CAST(N'2022-05-05T23:52:24.620' AS DateTime), 4104, 1, N'admin', CAST(N'2022-05-05T23:51:00.403' AS DateTime), N'admin', CAST(N'2022-05-05T23:52:34.287' AS DateTime))
INSERT [dbo].[OrderRoom] ([Id], [CustomerId], [RoomId], [StartTime], [EndTime], [TotalPayment], [Status], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (7, 10, 20, CAST(N'2022-05-05T23:54:06.050' AS DateTime), CAST(N'2022-05-05T23:55:01.910' AS DateTime), 1202052, 1, N'admin', CAST(N'2022-05-05T23:54:06.517' AS DateTime), N'admin', CAST(N'2022-05-05T23:55:15.370' AS DateTime))
INSERT [dbo].[OrderRoom] ([Id], [CustomerId], [RoomId], [StartTime], [EndTime], [TotalPayment], [Status], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (8, 7, 20, CAST(N'2022-05-06T14:19:15.483' AS DateTime), CAST(N'2022-05-06T15:16:30.133' AS DateTime), 131967, 1, N'admin', CAST(N'2022-05-06T14:19:16.577' AS DateTime), N'admin', CAST(N'2022-05-06T15:16:41.330' AS DateTime))
INSERT [dbo].[OrderRoom] ([Id], [CustomerId], [RoomId], [StartTime], [EndTime], [TotalPayment], [Status], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (9, 10, 20, CAST(N'2022-05-06T15:18:59.487' AS DateTime), CAST(N'2022-05-06T15:42:02.490' AS DateTime), 2049249, 1, N'admin', CAST(N'2022-05-06T15:19:00.210' AS DateTime), N'admin', CAST(N'2022-05-06T15:42:12.390' AS DateTime))
INSERT [dbo].[OrderRoom] ([Id], [CustomerId], [RoomId], [StartTime], [EndTime], [TotalPayment], [Status], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (10, 10, 27, CAST(N'2022-05-19T16:19:23.870' AS DateTime), NULL, NULL, 0, N'admin', CAST(N'2022-05-19T16:19:23.873' AS DateTime), NULL, NULL)
INSERT [dbo].[OrderRoom] ([Id], [CustomerId], [RoomId], [StartTime], [EndTime], [TotalPayment], [Status], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (11, 7, 26, CAST(N'2022-05-20T10:15:24.487' AS DateTime), NULL, NULL, 0, N'admin', CAST(N'2022-05-20T10:15:24.490' AS DateTime), NULL, NULL)
INSERT [dbo].[OrderRoom] ([Id], [CustomerId], [RoomId], [StartTime], [EndTime], [TotalPayment], [Status], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (12, 7, 26, CAST(N'2022-05-20T10:21:15.640' AS DateTime), NULL, NULL, 0, N'admin', CAST(N'2022-05-20T10:21:15.640' AS DateTime), NULL, NULL)
SET IDENTITY_INSERT [dbo].[OrderRoom] OFF
GO
SET IDENTITY_INSERT [dbo].[Provinces] ON 

INSERT [dbo].[Provinces] ([Id], [Name], [ImageLink], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedBy], [DeletedDate], [IsDeleted]) VALUES (5, N'123', NULL, N'admin', CAST(N'2022-04-11T22:51:26.407' AS DateTime), N'admin', CAST(N'2022-04-16T08:36:19.623' AS DateTime), NULL, NULL, 0)
INSERT [dbo].[Provinces] ([Id], [Name], [ImageLink], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedBy], [DeletedDate], [IsDeleted]) VALUES (6, N'd', NULL, N'admin', CAST(N'2022-04-13T11:07:15.297' AS DateTime), N'admin', CAST(N'2022-04-16T08:36:14.057' AS DateTime), NULL, NULL, 0)
INSERT [dbo].[Provinces] ([Id], [Name], [ImageLink], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedBy], [DeletedDate], [IsDeleted]) VALUES (7, N'd1', NULL, N'admin', CAST(N'2022-04-15T02:25:05.803' AS DateTime), N'admin', CAST(N'2022-04-16T08:36:27.960' AS DateTime), NULL, NULL, 0)
INSERT [dbo].[Provinces] ([Id], [Name], [ImageLink], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedBy], [DeletedDate], [IsDeleted]) VALUES (8, N'a12', NULL, N'admin', CAST(N'2022-04-15T03:53:19.317' AS DateTime), N'admin', CAST(N'2022-04-15T04:12:16.783' AS DateTime), NULL, NULL, 0)
INSERT [dbo].[Provinces] ([Id], [Name], [ImageLink], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedBy], [DeletedDate], [IsDeleted]) VALUES (9, N'1233', NULL, N'admin', CAST(N'2022-04-15T09:35:24.133' AS DateTime), N'admin', CAST(N'2022-04-16T08:36:06.287' AS DateTime), NULL, NULL, 0)
SET IDENTITY_INSERT [dbo].[Provinces] OFF
GO
SET IDENTITY_INSERT [dbo].[RegisterRooms] ON 

INSERT [dbo].[RegisterRooms] ([Id], [FullName], [Email], [PhoneNumber], [RoomId], [TimeFrom], [TimeTo], [Note], [Message], [Status], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (1, NULL, NULL, N'33333333333', NULL, NULL, NULL, NULL, NULL, 0, CAST(N'2022-05-19T11:43:59.580' AS DateTime), NULL, NULL)
INSERT [dbo].[RegisterRooms] ([Id], [FullName], [Email], [PhoneNumber], [RoomId], [TimeFrom], [TimeTo], [Note], [Message], [Status], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (2, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0, CAST(N'2022-05-19T11:44:58.097' AS DateTime), NULL, NULL)
INSERT [dbo].[RegisterRooms] ([Id], [FullName], [Email], [PhoneNumber], [RoomId], [TimeFrom], [TimeTo], [Note], [Message], [Status], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (3, N'123', NULL, N'123', NULL, NULL, NULL, NULL, NULL, 0, CAST(N'2022-05-19T11:50:41.313' AS DateTime), NULL, NULL)
INSERT [dbo].[RegisterRooms] ([Id], [FullName], [Email], [PhoneNumber], [RoomId], [TimeFrom], [TimeTo], [Note], [Message], [Status], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (4, N'undefined', NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0, CAST(N'2022-05-19T11:50:52.740' AS DateTime), NULL, NULL)
INSERT [dbo].[RegisterRooms] ([Id], [FullName], [Email], [PhoneNumber], [RoomId], [TimeFrom], [TimeTo], [Note], [Message], [Status], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (5, N'123', NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0, CAST(N'2022-05-19T11:51:28.747' AS DateTime), NULL, NULL)
INSERT [dbo].[RegisterRooms] ([Id], [FullName], [Email], [PhoneNumber], [RoomId], [TimeFrom], [TimeTo], [Note], [Message], [Status], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (6, N'33', NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0, CAST(N'2022-05-19T11:52:29.933' AS DateTime), NULL, NULL)
INSERT [dbo].[RegisterRooms] ([Id], [FullName], [Email], [PhoneNumber], [RoomId], [TimeFrom], [TimeTo], [Note], [Message], [Status], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (7, N'123', N'123@gmail.com', N'11111111131', 27, NULL, NULL, N'undefined', NULL, 1, CAST(N'2022-05-19T11:54:25.840' AS DateTime), NULL, NULL)
INSERT [dbo].[RegisterRooms] ([Id], [FullName], [Email], [PhoneNumber], [RoomId], [TimeFrom], [TimeTo], [Note], [Message], [Status], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (8, N'123', N'1@gmail.com', N'11111111111', 27, NULL, NULL, NULL, NULL, 0, CAST(N'2022-05-19T13:08:18.607' AS DateTime), NULL, NULL)
INSERT [dbo].[RegisterRooms] ([Id], [FullName], [Email], [PhoneNumber], [RoomId], [TimeFrom], [TimeTo], [Note], [Message], [Status], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (9, N'12', N'1@gmail.com', N'11111111111', 27, NULL, NULL, NULL, NULL, 0, CAST(N'2022-05-19T13:08:46.163' AS DateTime), NULL, NULL)
INSERT [dbo].[RegisterRooms] ([Id], [FullName], [Email], [PhoneNumber], [RoomId], [TimeFrom], [TimeTo], [Note], [Message], [Status], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (10, N'123', N'1@gmail.com', N'11111111111', 27, CAST(N'2022-05-19T13:21:10.000' AS DateTime), CAST(N'2022-05-19T13:21:12.000' AS DateTime), N'111111111111', N'ádasd', 1, CAST(N'2022-05-19T13:21:16.507' AS DateTime), N'admin', CAST(N'2022-05-19T14:56:58.327' AS DateTime))
INSERT [dbo].[RegisterRooms] ([Id], [FullName], [Email], [PhoneNumber], [RoomId], [TimeFrom], [TimeTo], [Note], [Message], [Status], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (11, N'123', N'123@gmail.com', N'9875678344', 27, CAST(N'2022-05-19T16:15:02.000' AS DateTime), CAST(N'2022-05-19T16:15:05.000' AS DateTime), N'd', NULL, 1, CAST(N'2022-05-19T16:15:13.833' AS DateTime), N'admin', CAST(N'2022-05-20T10:26:39.693' AS DateTime))
SET IDENTITY_INSERT [dbo].[RegisterRooms] OFF
GO
SET IDENTITY_INSERT [dbo].[Roles] ON 

INSERT [dbo].[Roles] ([Id], [Name]) VALUES (1, N'Administrator')
INSERT [dbo].[Roles] ([Id], [Name]) VALUES (2, N'Nhân Viên')
SET IDENTITY_INSERT [dbo].[Roles] OFF
GO
SET IDENTITY_INSERT [dbo].[RoomCategories] ON 

INSERT [dbo].[RoomCategories] ([Id], [Name], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedBy], [DeletedDate], [IsDeleted]) VALUES (1, N'hihi', N'admin', CAST(N'2022-04-12T21:55:13.723' AS DateTime), N'admin', CAST(N'2022-04-17T07:43:32.047' AS DateTime), NULL, NULL, 0)
INSERT [dbo].[RoomCategories] ([Id], [Name], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedBy], [DeletedDate], [IsDeleted]) VALUES (2, N'ád', N'admin', CAST(N'2022-04-17T07:43:36.683' AS DateTime), NULL, NULL, N'admin', CAST(N'2022-04-17T07:43:48.307' AS DateTime), 1)
INSERT [dbo].[RoomCategories] ([Id], [Name], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedBy], [DeletedDate], [IsDeleted]) VALUES (3, N'hihis', N'admin', CAST(N'2022-04-17T07:43:44.723' AS DateTime), N'admin', CAST(N'2022-05-08T11:41:52.537' AS DateTime), NULL, NULL, 0)
SET IDENTITY_INSERT [dbo].[RoomCategories] OFF
GO
SET IDENTITY_INSERT [dbo].[Rooms] ON 

INSERT [dbo].[Rooms] ([Id], [Name], [Price], [PromotionalPrice], [Star], [IsActive], [RoomStatusId], [RoomCategoriesId], [HotelId], [FloorId], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedBy], [DeletedDate], [IsDeleted]) VALUES (1, N'io', 1234, 3123123, 1, 0, 1, 1, 13, 1, N'admin', CAST(N'2022-04-13T14:28:57.407' AS DateTime), NULL, NULL, NULL, NULL, 0)
INSERT [dbo].[Rooms] ([Id], [Name], [Price], [PromotionalPrice], [Star], [IsActive], [RoomStatusId], [RoomCategoriesId], [HotelId], [FloorId], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedBy], [DeletedDate], [IsDeleted]) VALUES (2, N'io1', 1234, 3123123, 1, 0, 1, 1, 13, 1, N'admin', CAST(N'2022-04-13T14:31:55.577' AS DateTime), NULL, NULL, NULL, NULL, 0)
INSERT [dbo].[Rooms] ([Id], [Name], [Price], [PromotionalPrice], [Star], [IsActive], [RoomStatusId], [RoomCategoriesId], [HotelId], [FloorId], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedBy], [DeletedDate], [IsDeleted]) VALUES (3, N'io11', 1234, 3123123, 1, 0, 1, 1, 13, 1, N'admin', CAST(N'2022-04-13T14:40:38.723' AS DateTime), NULL, NULL, NULL, NULL, 0)
INSERT [dbo].[Rooms] ([Id], [Name], [Price], [PromotionalPrice], [Star], [IsActive], [RoomStatusId], [RoomCategoriesId], [HotelId], [FloorId], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedBy], [DeletedDate], [IsDeleted]) VALUES (4, N'io111', 1234, 3123123, 1, 0, 1, 1, 13, 1, N'admin', CAST(N'2022-04-13T14:48:02.980' AS DateTime), NULL, NULL, NULL, NULL, 0)
INSERT [dbo].[Rooms] ([Id], [Name], [Price], [PromotionalPrice], [Star], [IsActive], [RoomStatusId], [RoomCategoriesId], [HotelId], [FloorId], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedBy], [DeletedDate], [IsDeleted]) VALUES (5, N'io1111', 1234, 3123123, 1, 0, 1, 1, 13, 1, N'admin', CAST(N'2022-04-13T14:48:46.970' AS DateTime), NULL, NULL, NULL, NULL, 0)
INSERT [dbo].[Rooms] ([Id], [Name], [Price], [PromotionalPrice], [Star], [IsActive], [RoomStatusId], [RoomCategoriesId], [HotelId], [FloorId], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedBy], [DeletedDate], [IsDeleted]) VALUES (6, N'io11111', 1234, 3123123, 1, 0, 1, 1, 13, 1, N'admin', CAST(N'2022-04-13T14:50:56.317' AS DateTime), NULL, NULL, NULL, NULL, 0)
INSERT [dbo].[Rooms] ([Id], [Name], [Price], [PromotionalPrice], [Star], [IsActive], [RoomStatusId], [RoomCategoriesId], [HotelId], [FloorId], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedBy], [DeletedDate], [IsDeleted]) VALUES (7, N'io111111', 1234, 3123123, 1, 0, 1, 1, 13, 1, N'admin', CAST(N'2022-04-13T14:52:34.483' AS DateTime), NULL, NULL, NULL, NULL, 0)
INSERT [dbo].[Rooms] ([Id], [Name], [Price], [PromotionalPrice], [Star], [IsActive], [RoomStatusId], [RoomCategoriesId], [HotelId], [FloorId], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedBy], [DeletedDate], [IsDeleted]) VALUES (8, N'io1111111', 1234, 3123123, 1, 0, 1, 1, 13, 1, N'admin', CAST(N'2022-04-13T14:55:06.227' AS DateTime), NULL, NULL, NULL, NULL, 0)
INSERT [dbo].[Rooms] ([Id], [Name], [Price], [PromotionalPrice], [Star], [IsActive], [RoomStatusId], [RoomCategoriesId], [HotelId], [FloorId], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedBy], [DeletedDate], [IsDeleted]) VALUES (9, N'io11111111', 1234, 3123123, 1, 0, 1, 1, 13, 1, N'admin', CAST(N'2022-04-13T14:57:38.320' AS DateTime), NULL, NULL, NULL, NULL, 0)
INSERT [dbo].[Rooms] ([Id], [Name], [Price], [PromotionalPrice], [Star], [IsActive], [RoomStatusId], [RoomCategoriesId], [HotelId], [FloorId], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedBy], [DeletedDate], [IsDeleted]) VALUES (10, N'io11111111x', 1234, 3123123, 1, 0, 1, 1, 13, 1, N'admin', CAST(N'2022-04-13T15:01:19.517' AS DateTime), NULL, NULL, NULL, NULL, 0)
INSERT [dbo].[Rooms] ([Id], [Name], [Price], [PromotionalPrice], [Star], [IsActive], [RoomStatusId], [RoomCategoriesId], [HotelId], [FloorId], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedBy], [DeletedDate], [IsDeleted]) VALUES (11, N'1', 1234, 3123123, 1, 0, 1, 1, 13, 1, N'admin', CAST(N'2022-04-13T15:04:28.927' AS DateTime), NULL, NULL, NULL, NULL, 0)
INSERT [dbo].[Rooms] ([Id], [Name], [Price], [PromotionalPrice], [Star], [IsActive], [RoomStatusId], [RoomCategoriesId], [HotelId], [FloorId], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedBy], [DeletedDate], [IsDeleted]) VALUES (12, N'12', 1234, 3123123, 1, 0, 1, 1, 13, 1, N'admin', CAST(N'2022-04-13T15:06:33.737' AS DateTime), NULL, NULL, NULL, NULL, 0)
INSERT [dbo].[Rooms] ([Id], [Name], [Price], [PromotionalPrice], [Star], [IsActive], [RoomStatusId], [RoomCategoriesId], [HotelId], [FloorId], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedBy], [DeletedDate], [IsDeleted]) VALUES (13, N'123', 1234, 3123123, 1, 0, 1, 1, 13, 1, N'admin', CAST(N'2022-04-13T15:11:12.147' AS DateTime), NULL, NULL, NULL, NULL, 0)
INSERT [dbo].[Rooms] ([Id], [Name], [Price], [PromotionalPrice], [Star], [IsActive], [RoomStatusId], [RoomCategoriesId], [HotelId], [FloorId], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedBy], [DeletedDate], [IsDeleted]) VALUES (14, N'1234', 1234, 3123123, 1, 0, 1, 1, 13, 1, N'admin', CAST(N'2022-04-13T15:15:28.580' AS DateTime), NULL, NULL, NULL, NULL, 0)
INSERT [dbo].[Rooms] ([Id], [Name], [Price], [PromotionalPrice], [Star], [IsActive], [RoomStatusId], [RoomCategoriesId], [HotelId], [FloorId], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedBy], [DeletedDate], [IsDeleted]) VALUES (15, N'12345', 1234, 3123123, 1, 0, 1, 1, 13, 1, N'admin', CAST(N'2022-04-13T15:19:53.987' AS DateTime), NULL, NULL, NULL, NULL, 0)
INSERT [dbo].[Rooms] ([Id], [Name], [Price], [PromotionalPrice], [Star], [IsActive], [RoomStatusId], [RoomCategoriesId], [HotelId], [FloorId], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedBy], [DeletedDate], [IsDeleted]) VALUES (16, N'123456', 1234, 3123123, 1, 0, 1, 1, 13, 1, N'admin', CAST(N'2022-04-13T15:20:38.537' AS DateTime), N'admin', CAST(N'2022-04-27T12:26:27.670' AS DateTime), NULL, NULL, 0)
INSERT [dbo].[Rooms] ([Id], [Name], [Price], [PromotionalPrice], [Star], [IsActive], [RoomStatusId], [RoomCategoriesId], [HotelId], [FloorId], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedBy], [DeletedDate], [IsDeleted]) VALUES (17, N'1234567', 1234, 3123123, 1, 0, 1, 1, 13, 1, N'admin', CAST(N'2022-04-13T15:21:00.880' AS DateTime), NULL, NULL, N'admin', CAST(N'2022-04-23T04:22:41.220' AS DateTime), 0)
INSERT [dbo].[Rooms] ([Id], [Name], [Price], [PromotionalPrice], [Star], [IsActive], [RoomStatusId], [RoomCategoriesId], [HotelId], [FloorId], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedBy], [DeletedDate], [IsDeleted]) VALUES (18, N'12345678', 1234, 3123123, 1, 1, 1, 1, 13, 1, N'admin', CAST(N'2022-04-13T15:22:33.117' AS DateTime), N'admin', CAST(N'2022-04-26T15:11:47.873' AS DateTime), NULL, NULL, 0)
INSERT [dbo].[Rooms] ([Id], [Name], [Price], [PromotionalPrice], [Star], [IsActive], [RoomStatusId], [RoomCategoriesId], [HotelId], [FloorId], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedBy], [DeletedDate], [IsDeleted]) VALUES (19, N'12345678', 1234, 3123123, 1, 1, 1, 1, 13, 1, N'admin', CAST(N'2022-04-23T04:21:34.083' AS DateTime), N'admin', CAST(N'2022-04-24T21:02:29.630' AS DateTime), NULL, NULL, 0)
INSERT [dbo].[Rooms] ([Id], [Name], [Price], [PromotionalPrice], [Star], [IsActive], [RoomStatusId], [RoomCategoriesId], [HotelId], [FloorId], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedBy], [DeletedDate], [IsDeleted]) VALUES (20, N'ád', 1234, 3123123, 1, 1, 1, 1, 13, 1, N'admin', CAST(N'2022-04-23T04:27:07.363' AS DateTime), N'admin', CAST(N'2022-05-07T15:53:39.300' AS DateTime), NULL, NULL, 0)
INSERT [dbo].[Rooms] ([Id], [Name], [Price], [PromotionalPrice], [Star], [IsActive], [RoomStatusId], [RoomCategoriesId], [HotelId], [FloorId], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedBy], [DeletedDate], [IsDeleted]) VALUES (21, N'234', 1234, 3123123, 1, 1, 1, 1, 13, 1, N'admin', CAST(N'2022-04-24T21:17:42.430' AS DateTime), NULL, NULL, NULL, NULL, 0)
INSERT [dbo].[Rooms] ([Id], [Name], [Price], [PromotionalPrice], [Star], [IsActive], [RoomStatusId], [RoomCategoriesId], [HotelId], [FloorId], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedBy], [DeletedDate], [IsDeleted]) VALUES (22, N'dfg', 1234, 3123123, 1, 1, 1, 1, 13, 1, N'admin', CAST(N'2022-04-24T21:26:55.077' AS DateTime), N'admin', CAST(N'2022-04-27T00:32:54.643' AS DateTime), NULL, NULL, 0)
INSERT [dbo].[Rooms] ([Id], [Name], [Price], [PromotionalPrice], [Star], [IsActive], [RoomStatusId], [RoomCategoriesId], [HotelId], [FloorId], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedBy], [DeletedDate], [IsDeleted]) VALUES (23, N'123', 1234, 3123123, 1, 1, 1, 1, 13, 1, N'admin', CAST(N'2022-04-24T21:38:48.013' AS DateTime), N'admin', CAST(N'2022-04-27T00:32:00.397' AS DateTime), NULL, NULL, 0)
INSERT [dbo].[Rooms] ([Id], [Name], [Price], [PromotionalPrice], [Star], [IsActive], [RoomStatusId], [RoomCategoriesId], [HotelId], [FloorId], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedBy], [DeletedDate], [IsDeleted]) VALUES (24, N'fg', 1234, 3123123, 1, 0, 1, 1, 13, 1, N'admin', CAST(N'2022-04-24T21:41:05.337' AS DateTime), N'admin', CAST(N'2022-04-27T12:28:04.773' AS DateTime), NULL, NULL, 0)
INSERT [dbo].[Rooms] ([Id], [Name], [Price], [PromotionalPrice], [Star], [IsActive], [RoomStatusId], [RoomCategoriesId], [HotelId], [FloorId], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedBy], [DeletedDate], [IsDeleted]) VALUES (25, N'ád', 1234, 3123123, 1, 1, 1, 1, 13, 1, N'admin', CAST(N'2022-04-24T21:44:56.670' AS DateTime), N'admin', CAST(N'2022-04-27T00:31:30.510' AS DateTime), NULL, NULL, 0)
INSERT [dbo].[Rooms] ([Id], [Name], [Price], [PromotionalPrice], [Star], [IsActive], [RoomStatusId], [RoomCategoriesId], [HotelId], [FloorId], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedBy], [DeletedDate], [IsDeleted]) VALUES (26, N'ádp', 1234, 3123123, 1, 1, 2, 1, 13, 1, N'admin', CAST(N'2022-04-24T21:45:22.993' AS DateTime), N'admin', CAST(N'2022-05-20T10:21:15.627' AS DateTime), NULL, NULL, 0)
INSERT [dbo].[Rooms] ([Id], [Name], [Price], [PromotionalPrice], [Star], [IsActive], [RoomStatusId], [RoomCategoriesId], [HotelId], [FloorId], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedBy], [DeletedDate], [IsDeleted]) VALUES (27, N'12', 1234, 3123123, 1, 0, 2, 1, 13, 1, N'admin', CAST(N'2022-05-05T13:24:10.110' AS DateTime), N'admin', CAST(N'2022-05-19T16:19:23.857' AS DateTime), NULL, NULL, 0)
INSERT [dbo].[Rooms] ([Id], [Name], [Price], [PromotionalPrice], [Star], [IsActive], [RoomStatusId], [RoomCategoriesId], [HotelId], [FloorId], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedBy], [DeletedDate], [IsDeleted]) VALUES (28, N'1', NULL, 11, 1, 1, 1, NULL, 13, NULL, N'admin', CAST(N'2022-05-20T09:47:02.087' AS DateTime), N'admin', CAST(N'2022-05-20T09:47:33.820' AS DateTime), NULL, NULL, 0)
SET IDENTITY_INSERT [dbo].[Rooms] OFF
GO
SET IDENTITY_INSERT [dbo].[RoomStatus] ON 

INSERT [dbo].[RoomStatus] ([Id], [Name]) VALUES (1, N'Sẵn sàng sử dụng')
INSERT [dbo].[RoomStatus] ([Id], [Name]) VALUES (2, N'Đang sử dụng')
INSERT [dbo].[RoomStatus] ([Id], [Name]) VALUES (3, N'Đang dọn dẹp')
INSERT [dbo].[RoomStatus] ([Id], [Name]) VALUES (4, N'Tạm dừng sửa chữa')
INSERT [dbo].[RoomStatus] ([Id], [Name]) VALUES (5, N'Khác')
SET IDENTITY_INSERT [dbo].[RoomStatus] OFF
GO
SET IDENTITY_INSERT [dbo].[ServiceCategories] ON 

INSERT [dbo].[ServiceCategories] ([Id], [Name], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedBy], [DeletedDate], [IsDeleted]) VALUES (1, N'đ', N'admin', CAST(N'2022-04-12T13:53:05.960' AS DateTime), N'admin', CAST(N'2022-04-21T11:12:10.220' AS DateTime), NULL, NULL, 0)
INSERT [dbo].[ServiceCategories] ([Id], [Name], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedBy], [DeletedDate], [IsDeleted]) VALUES (2, N'ádasdfff', N'admin', CAST(N'2022-04-20T10:28:12.113' AS DateTime), N'admin', CAST(N'2022-04-20T10:28:18.800' AS DateTime), N'admin', CAST(N'2022-04-20T10:28:22.813' AS DateTime), 1)
INSERT [dbo].[ServiceCategories] ([Id], [Name], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedBy], [DeletedDate], [IsDeleted]) VALUES (3, N'hihi', N'admin', CAST(N'2022-04-21T11:44:56.470' AS DateTime), NULL, NULL, NULL, NULL, 0)
SET IDENTITY_INSERT [dbo].[ServiceCategories] OFF
GO
SET IDENTITY_INSERT [dbo].[Services] ON 

INSERT [dbo].[Services] ([Id], [Name], [Price], [Unit], [Status], [Note], [ServiceCategoriesId], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedBy], [DeletedDate], [IsDeleted], [Image]) VALUES (1, N'123', 1000000333333, N'1', 1, N'123', 1, N'admin', CAST(N'2022-04-12T14:37:18.647' AS DateTime), N'admin', CAST(N'2022-04-21T11:32:45.723' AS DateTime), NULL, NULL, 0, NULL)
INSERT [dbo].[Services] ([Id], [Name], [Price], [Unit], [Status], [Note], [ServiceCategoriesId], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedBy], [DeletedDate], [IsDeleted], [Image]) VALUES (2, N'Bánh mỳ', 15000, N'Cái', 1, N'1', 1, N'admin', CAST(N'2022-04-21T11:12:57.203' AS DateTime), N'admin', CAST(N'2022-04-27T15:53:01.977' AS DateTime), NULL, NULL, 0, NULL)
INSERT [dbo].[Services] ([Id], [Name], [Price], [Unit], [Status], [Note], [ServiceCategoriesId], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedBy], [DeletedDate], [IsDeleted], [Image]) VALUES (3, N'Phở bò', 35000, N'Bát', 1, NULL, 1, N'admin', CAST(N'2022-04-21T11:36:12.097' AS DateTime), N'admin', CAST(N'2022-04-21T11:36:21.853' AS DateTime), N'admin', CAST(N'2022-04-21T11:44:46.753' AS DateTime), 1, NULL)
INSERT [dbo].[Services] ([Id], [Name], [Price], [Unit], [Status], [Note], [ServiceCategoriesId], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedBy], [DeletedDate], [IsDeleted], [Image]) VALUES (4, N'3123', 123, NULL, 1, NULL, 1, N'admin', CAST(N'2022-04-27T22:30:57.847' AS DateTime), NULL, NULL, NULL, NULL, 0, NULL)
INSERT [dbo].[Services] ([Id], [Name], [Price], [Unit], [Status], [Note], [ServiceCategoriesId], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedBy], [DeletedDate], [IsDeleted], [Image]) VALUES (5, N'11', NULL, NULL, 1, NULL, 1, N'admin', CAST(N'2022-04-27T22:31:09.577' AS DateTime), NULL, NULL, NULL, NULL, 0, NULL)
INSERT [dbo].[Services] ([Id], [Name], [Price], [Unit], [Status], [Note], [ServiceCategoriesId], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedBy], [DeletedDate], [IsDeleted], [Image]) VALUES (6, N'22', NULL, NULL, 1, NULL, 1, N'admin', CAST(N'2022-04-27T22:38:18.413' AS DateTime), NULL, NULL, NULL, NULL, 0, NULL)
INSERT [dbo].[Services] ([Id], [Name], [Price], [Unit], [Status], [Note], [ServiceCategoriesId], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [DeletedBy], [DeletedDate], [IsDeleted], [Image]) VALUES (7, N'Spa', 1000000, N'Lần', 1, NULL, 3, N'admin', CAST(N'2022-04-27T22:38:28.263' AS DateTime), N'admin', CAST(N'2022-04-28T15:48:38.907' AS DateTime), NULL, NULL, 0, NULL)
SET IDENTITY_INSERT [dbo].[Services] OFF
GO
SET IDENTITY_INSERT [dbo].[Sex] ON 

INSERT [dbo].[Sex] ([Id], [Name]) VALUES (1, N'Nam')
INSERT [dbo].[Sex] ([Id], [Name]) VALUES (2, N'Nữ')
INSERT [dbo].[Sex] ([Id], [Name]) VALUES (3, N'Không xác định')
SET IDENTITY_INSERT [dbo].[Sex] OFF
GO
SET IDENTITY_INSERT [dbo].[UserProfiles] ON 

INSERT [dbo].[UserProfiles] ([Id], [UserName], [PassHash], [FullName], [DateOfBirth], [PhoneNumber], [Address], [Email], [Facebook], [Zalo], [Active], [SexId], [RoleId], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (1, N'admin', N'21-23-2F-29-7A-57-A5-A7-43-89-4A-0E-4A-80-1F-C3', N'Administrator', NULL, NULL, NULL, NULL, NULL, NULL, 1, NULL, 1, N'System', CAST(N'2022-05-08T14:22:57.723' AS DateTime), NULL, NULL)
INSERT [dbo].[UserProfiles] ([Id], [UserName], [PassHash], [FullName], [DateOfBirth], [PhoneNumber], [Address], [Email], [Facebook], [Zalo], [Active], [SexId], [RoleId], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (2, NULL, N'20-2C-B9-62-AC-59-07-5B-96-4B-07-15-2D-23-4B-70', N'123', NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, 1, N'admin', CAST(N'2022-05-08T14:23:52.537' AS DateTime), N'admin', CAST(N'2022-05-08T14:50:48.910' AS DateTime))
INSERT [dbo].[UserProfiles] ([Id], [UserName], [PassHash], [FullName], [DateOfBirth], [PhoneNumber], [Address], [Email], [Facebook], [Zalo], [Active], [SexId], [RoleId], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (3, NULL, N'11-E8-EE-63-53-8E-1E-12-A0-88-05-77-3B-15-5D-60', N'dunglt19', NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, 1, N'admin', CAST(N'2022-05-08T14:24:39.463' AS DateTime), N'admin', CAST(N'2022-05-08T14:50:51.130' AS DateTime))
INSERT [dbo].[UserProfiles] ([Id], [UserName], [PassHash], [FullName], [DateOfBirth], [PhoneNumber], [Address], [Email], [Facebook], [Zalo], [Active], [SexId], [RoleId], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (4, N'dunglt19', N'11-E8-EE-63-53-8E-1E-12-A0-88-05-77-3B-15-5D-60', N'dunglt19', CAST(N'2022-05-08T00:00:00.000' AS DateTime), N'0988988766', N'dunglt19', N'dunglt19', N'dunglt19', N'dunglt19', 0, 1, 1, N'admin', CAST(N'2022-05-08T14:25:17.577' AS DateTime), N'admin', CAST(N'2022-05-08T14:50:53.373' AS DateTime))
SET IDENTITY_INSERT [dbo].[UserProfiles] OFF
GO
/****** Object:  StoredProcedure [dbo].[uspCustomers_GetAll]    Script Date: 5/20/2022 10:35:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[uspCustomers_GetAll]
(
    @Key NVARCHAR(50),
    @Page INT,
    @PageSize INT
)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE
        @strErrorMessage NVARCHAR(4000),
        @intErrorSeverity INT,
        @intErrorState INT,
        @intErrorLine INT;

    BEGIN TRY

	    WITH TempResult AS 
		(
			SELECT
			   ROW_NUMBER() OVER(ORDER BY c.[Id] DESC) AS STT
			  ,c.[Id]
			  ,c.[FullName]
			  ,c.[IDCard]
			  ,c.[PhoneNumber]
			  ,c.[Address]
			  ,c.[DateOfBirth]
			  ,c.[Note]
			  ,c.[SexId]
			  ,s.[Name] AS SexName
			  ,c.[CreatedBy]
			  ,c.[CreatedDate]
			  ,c.[ModifiedBy]
			  ,c.[ModifiedDate]
			FROM Customers AS c
			LEFT JOIN Sex AS s ON s.Id = c.[SexId]
			WHERE (
					ISNULL(@Key, '') = '' OR 
					dbo.ufnRemoveMark(TRIM(UPPER(c.[IDCard]))) LIKE CONCAT('%',dbo.ufnRemoveMark(TRIM(UPPER(@Key))),'%')
				  )
				  AND c.IsDeleted = 0 
		),
		TempCount (TotalRow)
    
		AS (SELECT COUNT(*) FROM TempResult)
		SELECT * FROM TempResult r, TempCount
		ORDER BY r.Id DESC OFFSET (@PageSize * (@Page - 1)) ROWS FETCH NEXT @PageSize ROWS ONLY;

    END TRY
    BEGIN CATCH
        SELECT
            @strErrorMessage = ERROR_MESSAGE()
                    + ' Line:'
                    + CONVERT(VARCHAR(5), ERROR_LINE()),
            @intErrorSeverity = ERROR_SEVERITY(),
            @intErrorState = ERROR_STATE();
        RAISERROR(
                @strErrorMessage,   -- Message text.
                @intErrorSeverity,  -- Severity.
                @intErrorState      -- State.
        );
    END CATCH;
END



GO
/****** Object:  StoredProcedure [dbo].[uspFloors_GetAll]    Script Date: 5/20/2022 10:35:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create PROCEDURE [dbo].[uspFloors_GetAll]
(
    @Key NVARCHAR(50),
    @Page INT,
    @PageSize INT
)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE
        @strErrorMessage NVARCHAR(4000),
        @intErrorSeverity INT,
        @intErrorState INT,
        @intErrorLine INT;

    BEGIN TRY

	    WITH TempResult AS 
		(
			SELECT
			   ROW_NUMBER() OVER(ORDER BY [Id] DESC) AS STT
			  ,[Id]
			  ,[Name]
			  ,[CreatedBy]
			  ,[CreatedDate]
			  ,[ModifiedBy]
			  ,[ModifiedDate]
			FROM Floors
			WHERE (
					ISNULL(@Key, '') = '' OR 
					dbo.ufnRemoveMark(TRIM(UPPER([Name]))) LIKE CONCAT('%',dbo.ufnRemoveMark(TRIM(UPPER(@Key))),'%')
				  )
				  AND IsDeleted = 0 
		),
		TempCount (TotalRow)
    
		AS (SELECT COUNT(*) FROM TempResult)
		SELECT * FROM TempResult r, TempCount
		ORDER BY r.Id DESC OFFSET (@PageSize * (@Page - 1)) ROWS FETCH NEXT @PageSize ROWS ONLY;

    END TRY
    BEGIN CATCH
        SELECT
            @strErrorMessage = ERROR_MESSAGE()
                    + ' Line:'
                    + CONVERT(VARCHAR(5), ERROR_LINE()),
            @intErrorSeverity = ERROR_SEVERITY(),
            @intErrorState = ERROR_STATE();
        RAISERROR(
                @strErrorMessage,   -- Message text.
                @intErrorSeverity,  -- Severity.
                @intErrorState      -- State.
        );
    END CATCH;
END



GO
/****** Object:  StoredProcedure [dbo].[uspHotels_GetAll]    Script Date: 5/20/2022 10:35:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[uspHotels_GetAll]
(
    @Key NVARCHAR(50),
    @Page INT,
    @PageSize INT
)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE
        @strErrorMessage NVARCHAR(4000),
        @intErrorSeverity INT,
        @intErrorState INT,
        @intErrorLine INT;

    BEGIN TRY

	    WITH TempResult AS 
		(
			SELECT
			   ROW_NUMBER() OVER(ORDER BY h.[Id] DESC) AS STT
			  ,h.[Id]
			  ,h.[Name]
              ,h.[Image]
			  ,h.[Address]
			  ,h.[Title]
			  ,h.[Introduce]
			  ,h.[Star]
			  ,h.[Note]
			  ,h.[ProvinceId]
			  ,p.[Name] AS ProvinceName
			  ,h.[CreatedBy]
			  ,h.[CreatedDate]
			  ,h.[ModifiedBy]
			  ,h.[ModifiedDate]
			FROM Hotels AS h
			LEFT JOIN Provinces AS p ON p.Id = h.[ProvinceId] AND p.IsDeleted = 0
			WHERE (
					ISNULL(@Key, '') = '' OR 
					dbo.ufnRemoveMark(TRIM(UPPER(h.[Name]))) LIKE CONCAT('%',dbo.ufnRemoveMark(TRIM(UPPER(@Key))),'%')
				  )
				  AND h.IsDeleted = 0 
		),
		TempCount (TotalRow)
    
		AS (SELECT COUNT(*) FROM TempResult)
		SELECT * FROM TempResult r, TempCount
		ORDER BY r.Id DESC OFFSET (@PageSize * (@Page - 1)) ROWS FETCH NEXT @PageSize ROWS ONLY;

    END TRY
    BEGIN CATCH
        SELECT
            @strErrorMessage = ERROR_MESSAGE()
                    + ' Line:'
                    + CONVERT(VARCHAR(5), ERROR_LINE()),
            @intErrorSeverity = ERROR_SEVERITY(),
            @intErrorState = ERROR_STATE();
        RAISERROR(
                @strErrorMessage,   -- Message text.
                @intErrorSeverity,  -- Severity.
                @intErrorState      -- State.
        );
    END CATCH;
END



GO
/****** Object:  StoredProcedure [dbo].[uspOrderRoom_GetAll]    Script Date: 5/20/2022 10:35:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[uspOrderRoom_GetAll]
(
    @Key NVARCHAR(50),
    @Page INT,
    @PageSize INT
)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE
        @strErrorMessage NVARCHAR(4000),
        @intErrorSeverity INT,
        @intErrorState INT,
        @intErrorLine INT;

    BEGIN TRY

	    WITH TempResult AS 
		(
			SELECT
			   ROW_NUMBER() OVER(ORDER BY oro.[Id] DESC) AS STT
			  ,oro.[Id]
			  ,oro.[CustomerId]
              ,c.[FullName]
              ,c.[PhoneNumber]
              ,c.[IDCard]
			  ,oro.[RoomId]
              ,r.[Name] AS RoomName
              ,h.[Name] AS HotelName
              ,p.[Name] AS ProvincesName
			  ,oro.[StartTime]
			  ,oro.[EndTime]
			  ,oro.[TotalPayment]
              ,CASE
                    WHEN oro.[Status] = 1 THEN N'Đã thanh toán'
                    ELSE N'Chưa thanh toán'
               END AS StatusName
              ,oro.[Status]
			  ,oro.[CreatedBy]
			  ,oro.[CreatedDate]
			  ,oro.[ModifiedBy]
			  ,oro.[ModifiedDate]
			FROM OrderRoom AS oro
			LEFT JOIN Customers AS c ON c.Id = oro.[CustomerId] AND c.IsDeleted = 0
            LEFT JOIN Rooms AS r ON r.Id = oro.RoomId AND r.IsDeleted = 0
            LEFT JOIN Hotels AS h ON h.Id = r.[HotelId] AND h.[IsDeleted] = 0
            LEFT JOIN Provinces AS p ON p.[Id] = h.[ProvinceId] AND p.IsDeleted = 0
			WHERE (
					ISNULL(@Key, '') = '' OR 
					dbo.ufnRemoveMark(TRIM(UPPER(c.[IDCard]))) LIKE CONCAT('%',dbo.ufnRemoveMark(TRIM(UPPER(@Key))),'%')
				  )
		),
		TempCount (TotalRow)
    
		AS (SELECT COUNT(*) FROM TempResult)
		SELECT * FROM TempResult r, TempCount
		ORDER BY r.[Id] DESC OFFSET (@PageSize * (@Page - 1)) ROWS FETCH NEXT @PageSize ROWS ONLY;

    END TRY
    BEGIN CATCH
        SELECT
            @strErrorMessage = ERROR_MESSAGE()
                    + ' Line:'
                    + CONVERT(VARCHAR(5), ERROR_LINE()),
            @intErrorSeverity = ERROR_SEVERITY(),
            @intErrorState = ERROR_STATE();
        RAISERROR(
                @strErrorMessage,   -- Message text.
                @intErrorSeverity,  -- Severity.
                @intErrorState      -- State.
        );
    END CATCH;
END



GO
/****** Object:  StoredProcedure [dbo].[uspProvinces_GetAll]    Script Date: 5/20/2022 10:35:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[uspProvinces_GetAll]
(
    @Key NVARCHAR(50),
    @Page INT,
    @PageSize INT
)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE
        @strErrorMessage NVARCHAR(4000),
        @intErrorSeverity INT,
        @intErrorState INT,
        @intErrorLine INT;

    BEGIN TRY

	    WITH TempResult AS 
		(
			SELECT
			   ROW_NUMBER() OVER(ORDER BY [Id] DESC) AS STT
			  ,[Id]
			  ,[Name]
			  ,[ImageLink]
			  ,[CreatedBy]
			  ,[CreatedDate]
			  ,[ModifiedBy]
			  ,[ModifiedDate]
			FROM Provinces
			WHERE (
					ISNULL(@Key, '') = '' OR 
					dbo.ufnRemoveMark(TRIM(UPPER([Name]))) LIKE CONCAT('%',dbo.ufnRemoveMark(TRIM(UPPER(@Key))),'%')
				  )
				  AND IsDeleted = 0 
		),
		TempCount (TotalRow)
    
		AS (SELECT COUNT(*) FROM TempResult)
		SELECT * FROM TempResult r, TempCount
		ORDER BY r.Id DESC OFFSET (@PageSize * (@Page - 1)) ROWS FETCH NEXT @PageSize ROWS ONLY;

    END TRY
    BEGIN CATCH
        SELECT
            @strErrorMessage = ERROR_MESSAGE()
                    + ' Line:'
                    + CONVERT(VARCHAR(5), ERROR_LINE()),
            @intErrorSeverity = ERROR_SEVERITY(),
            @intErrorState = ERROR_STATE();
        RAISERROR(
                @strErrorMessage,   -- Message text.
                @intErrorSeverity,  -- Severity.
                @intErrorState      -- State.
        );
    END CATCH;
END



GO
/****** Object:  StoredProcedure [dbo].[uspRegisterRooms_GetAll]    Script Date: 5/20/2022 10:35:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[uspRegisterRooms_GetAll]
(
    @Key NVARCHAR(50),
    @Page INT,
    @PageSize INT
)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE
        @strErrorMessage NVARCHAR(4000),
        @intErrorSeverity INT,
        @intErrorState INT,
        @intErrorLine INT;

    BEGIN TRY

	    WITH TempResult AS 
		(
			SELECT
			   ROW_NUMBER() OVER(ORDER BY rr.[Status] ASC) AS STT
			  ,rr.[Id]
			  ,rr.[FullName]
			  ,rr.[Email]
			  ,rr.[PhoneNumber]
			  ,rr.[RoomId]
			  ,r.[Name] AS RoomName
			  ,r.[PromotionalPrice]
			  ,rr.[TimeFrom]
			  ,rr.[TimeTo]
			  ,rr.[Note]
			  ,rr.[Message]
			  ,rr.[Status]
			  ,CASE
				WHEN rr.[Status] = 1 THEN N'Đã xử lý'
				ELSE N'Chưa xử lý'
			  END AS StatusName
			  ,rr.[CreatedDate]
			  ,rr.[ModifiedBy]
			  ,rr.[ModifiedDate]
			FROM [RegisterRooms] AS rr
			LEFT JOIN Rooms AS r ON r.Id = rr.RoomId AND r.IsDeleted = 0
			WHERE (
					ISNULL(@Key, '') = '' OR 
					dbo.ufnRemoveMark(TRIM(UPPER(rr.[PhoneNumber]))) LIKE CONCAT('%',dbo.ufnRemoveMark(TRIM(UPPER(@Key))),'%')
				  )
		),
		TempCount (TotalRow)
    
		AS (SELECT COUNT(*) FROM TempResult)
		SELECT * FROM TempResult r, TempCount
		ORDER BY r.[Status] ASC OFFSET (@PageSize * (@Page - 1)) ROWS FETCH NEXT @PageSize ROWS ONLY;

    END TRY
    BEGIN CATCH
        SELECT
            @strErrorMessage = ERROR_MESSAGE()
                    + ' Line:'
                    + CONVERT(VARCHAR(5), ERROR_LINE()),
            @intErrorSeverity = ERROR_SEVERITY(),
            @intErrorState = ERROR_STATE();
        RAISERROR(
                @strErrorMessage,   -- Message text.
                @intErrorSeverity,  -- Severity.
                @intErrorState      -- State.
        );
    END CATCH;
END



GO
/****** Object:  StoredProcedure [dbo].[uspRoomCategories_GetAll]    Script Date: 5/20/2022 10:35:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create PROCEDURE [dbo].[uspRoomCategories_GetAll]
(
    @Key NVARCHAR(50),
    @Page INT,
    @PageSize INT
)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE
        @strErrorMessage NVARCHAR(4000),
        @intErrorSeverity INT,
        @intErrorState INT,
        @intErrorLine INT;

    BEGIN TRY

	    WITH TempResult AS 
		(
			SELECT
			   ROW_NUMBER() OVER(ORDER BY [Id] DESC) AS STT
			  ,[Id]
			  ,[Name]
			  ,[CreatedBy]
			  ,[CreatedDate]
			  ,[ModifiedBy]
			  ,[ModifiedDate]
			FROM RoomCategories
			WHERE (
					ISNULL(@Key, '') = '' OR 
					dbo.ufnRemoveMark(TRIM(UPPER([Name]))) LIKE CONCAT('%',dbo.ufnRemoveMark(TRIM(UPPER(@Key))),'%')
				  )
				  AND IsDeleted = 0 
		),
		TempCount (TotalRow)
    
		AS (SELECT COUNT(*) FROM TempResult)
		SELECT * FROM TempResult r, TempCount
		ORDER BY r.Id DESC OFFSET (@PageSize * (@Page - 1)) ROWS FETCH NEXT @PageSize ROWS ONLY;

    END TRY
    BEGIN CATCH
        SELECT
            @strErrorMessage = ERROR_MESSAGE()
                    + ' Line:'
                    + CONVERT(VARCHAR(5), ERROR_LINE()),
            @intErrorSeverity = ERROR_SEVERITY(),
            @intErrorState = ERROR_STATE();
        RAISERROR(
                @strErrorMessage,   -- Message text.
                @intErrorSeverity,  -- Severity.
                @intErrorState      -- State.
        );
    END CATCH;
END



GO
/****** Object:  StoredProcedure [dbo].[uspRooms_GetAll]    Script Date: 5/20/2022 10:35:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[uspRooms_GetAll]
(
    @Key NVARCHAR(50),
	@HotelId BIGINT,
    @Page INT,
    @PageSize INT
)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE
        @strErrorMessage NVARCHAR(4000),
        @intErrorSeverity INT,
        @intErrorState INT,
        @intErrorLine INT;

    BEGIN TRY

	    WITH TempResult AS 
		(
			SELECT
                ROW_NUMBER() OVER(ORDER BY r.[Id] DESC) AS STT
               ,r.[Id]
			   ,r.[Name]
			   ,r.[Price]
			   ,r.[PromotionalPrice]
			   ,r.[Star]
			   ,r.[IsActive]
			   ,r.[RoomStatusId]
               ,rs.[Name] AS RoomStatusName
			   ,r.[RoomCategoriesId]
               ,rc.[Name] AS RoomCategoriesName
			   ,r.[HotelId]
               ,h.[Name] AS HotelName
			   ,r.[FloorId]
               ,f.[Name] AS FloorName
			   ,r.[CreatedBy]
			   ,r.[CreatedDate]
			   ,r.[ModifiedBy]
			   ,r.[ModifiedDate]
			FROM Rooms AS r
			LEFT JOIN RoomStatus AS rs ON rs.Id = r.[RoomStatusId]
			LEFT JOIN RoomCategories AS rc ON rc.Id = r.[RoomCategoriesId] AND rc.IsDeleted = 0
			LEFT JOIN Hotels AS h ON h.Id = r.[HotelId] AND h.IsDeleted = 0
			LEFT JOIN Floors AS f ON f.Id = r.[FloorId] AND f.IsDeleted = 0
			WHERE (
					ISNULL(@Key, '') = '' OR 
					dbo.ufnRemoveMark(TRIM(UPPER(r.[Name]))) LIKE CONCAT('%',dbo.ufnRemoveMark(TRIM(UPPER(@Key))),'%')
				  )
				  AND r.[HotelId] = @HotelId
				  AND r.IsDeleted = 0 
		),
		TempCount (TotalRow)
    
		AS (SELECT COUNT(*) FROM TempResult)
		SELECT * FROM TempResult r, TempCount
		ORDER BY r.Id DESC OFFSET (@PageSize * (@Page - 1)) ROWS FETCH NEXT @PageSize ROWS ONLY;

    END TRY
    BEGIN CATCH
        SELECT
            @strErrorMessage = ERROR_MESSAGE()
                    + ' Line:'
                    + CONVERT(VARCHAR(5), ERROR_LINE()),
            @intErrorSeverity = ERROR_SEVERITY(),
            @intErrorState = ERROR_STATE();
        RAISERROR(
                @strErrorMessage,   -- Message text.
                @intErrorSeverity,  -- Severity.
                @intErrorState      -- State.
        );
    END CATCH;
END



GO
/****** Object:  StoredProcedure [dbo].[uspRooms_GetAllRoomByHotel]    Script Date: 5/20/2022 10:35:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[uspRooms_GetAllRoomByHotel]
(
    @HotelId BIGINT,
    @RoomStatusId BIGINT,
    @RoomCategoriesId BIGINT,
	@Star INT,
    @Page INT,
    @PageSize INT
)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE
        @strErrorMessage NVARCHAR(4000),
        @intErrorSeverity INT,
        @intErrorState INT,
        @intErrorLine INT;

    BEGIN TRY

	    WITH TempResult AS 
		(
			SELECT
                r.[Id]
			   ,r.[Name]
			   ,r.[Price]
			   ,r.[PromotionalPrice]
			   ,r.[Star]
			   ,r.[IsActive]
			   ,r.[RoomStatusId]
               ,rs.[Name] AS RoomStatusName
			   ,r.[RoomCategoriesId]
               ,rc.[Name] AS RoomCategoriesName
			   ,r.[HotelId]
               ,h.[Name] AS HotelName
			   ,r.[FloorId]
               ,f.[Name] AS FloorName
			FROM Rooms AS r
			LEFT JOIN RoomStatus AS rs ON rs.Id = r.[RoomStatusId]
			LEFT JOIN RoomCategories AS rc ON rc.Id = r.[RoomCategoriesId] AND rc.IsDeleted = 0
			LEFT JOIN Hotels AS h ON h.Id = r.[HotelId] AND h.IsDeleted = 0
			LEFT JOIN Floors AS f ON f.Id = r.[FloorId] AND f.IsDeleted = 0
			WHERE (
					ISNULL(@RoomStatusId, '') = '' OR 
					r.[RoomStatusId] = @RoomStatusId
				  )
                  AND
				  (
					ISNULL(@RoomCategoriesId, '') = '' OR 
					r.[RoomCategoriesId] = @RoomCategoriesId
				  )
                  AND
				  (
					ISNULL(@Star, '') = '' OR 
					r.[Star] = @Star
				  )
                  AND r.HotelId = @HotelId
				  AND r.IsDeleted = 0 
		),
		TempCount (TotalRow)
    
		AS (SELECT COUNT(*) FROM TempResult)
		SELECT * FROM TempResult r, TempCount
		ORDER BY r.Id DESC OFFSET (@PageSize * (@Page - 1)) ROWS FETCH NEXT @PageSize ROWS ONLY;

    END TRY
    BEGIN CATCH
        SELECT
            @strErrorMessage = ERROR_MESSAGE()
                    + ' Line:'
                    + CONVERT(VARCHAR(5), ERROR_LINE()),
            @intErrorSeverity = ERROR_SEVERITY(),
            @intErrorState = ERROR_STATE();
        RAISERROR(
                @strErrorMessage,   -- Message text.
                @intErrorSeverity,  -- Severity.
                @intErrorState      -- State.
        );
    END CATCH;
END



GO
/****** Object:  StoredProcedure [dbo].[uspRooms_GetAllRoomDetail]    Script Date: 5/20/2022 10:35:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[uspRooms_GetAllRoomDetail]
(
    @RoomId BIGINT
)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE
        @strErrorMessage NVARCHAR(4000),
        @intErrorSeverity INT,
        @intErrorState INT,
        @intErrorLine INT;

    BEGIN TRY

		SELECT
			 r.[Id]
			,r.[Name]
			,r.[Star]
			,r.[HotelId]
			,h.[Name] AS HotelName
			,h.[Introduce]
			,r.[FloorId]
			,f.[Name] AS FloorName
		FROM Rooms AS r
		LEFT JOIN Hotels AS h ON h.Id = r.[HotelId] AND h.IsDeleted = 0
		LEFT JOIN Floors AS f ON f.Id = r.[FloorId] AND f.IsDeleted = 0
		WHERE r.[Id] = @RoomId
		AND r.IsDeleted = 0 

    END TRY
    BEGIN CATCH
        SELECT
            @strErrorMessage = ERROR_MESSAGE()
                    + ' Line:'
                    + CONVERT(VARCHAR(5), ERROR_LINE()),
            @intErrorSeverity = ERROR_SEVERITY(),
            @intErrorState = ERROR_STATE();
        RAISERROR(
                @strErrorMessage,   -- Message text.
                @intErrorSeverity,  -- Severity.
                @intErrorState      -- State.
        );
    END CATCH;
END



GO
/****** Object:  StoredProcedure [dbo].[uspRooms_GetInfo]    Script Date: 5/20/2022 10:35:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[uspRooms_GetInfo]
(
    @HotelId BIGINT,
	@RoomStatusId BIGINT,
    @Page INT,
    @PageSize INT
)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE
        @strErrorMessage NVARCHAR(4000),
        @intErrorSeverity INT,
        @intErrorState INT,
        @intErrorLine INT;

    BEGIN TRY

	    WITH TempResult AS 
		(
			SELECT
                r.[Id]
			   ,r.[Name]
			   ,r.[Price]
			   ,r.[PromotionalPrice]
			   ,r.[Star]
			   ,r.[IsActive]
			   ,r.[RoomStatusId]
               ,rs.[Name] AS RoomStatusName
			   ,r.[RoomCategoriesId]
               ,rc.[Name] AS RoomCategoriesName
			   ,r.[HotelId]
               ,h.[Name] AS HotelName
			   ,r.[FloorId]
               ,f.[Name] AS FloorName
               ,oro.Id AS OrderRoomId
               ,oro.StartTime
               ,c.[FullName]
               ,c.PhoneNumber
               ,c.IDCard
			FROM Rooms AS r
			LEFT JOIN RoomStatus AS rs ON rs.Id = r.[RoomStatusId]
			LEFT JOIN RoomCategories AS rc ON rc.Id = r.[RoomCategoriesId] AND rc.IsDeleted = 0
			LEFT JOIN Hotels AS h ON h.Id = r.[HotelId] AND h.IsDeleted = 0
			LEFT JOIN Floors AS f ON f.Id = r.[FloorId] AND f.IsDeleted = 0
            LEFT JOIN OrderRoom AS oro ON oro.RoomId = r.Id AND oro.[Status] = 0
            LEFT JOIN Customers AS c ON c.Id = oro.CustomerId AND c.IsDeleted = 0
			WHERE (
					ISNULL(@RoomStatusId, '') = '' OR 
					r.[RoomStatusId] = @RoomStatusId
				  )
                  AND r.HotelId = @HotelId
				  AND r.IsDeleted = 0 
		),
		TempCount (TotalRow)
    
		AS (SELECT COUNT(*) FROM TempResult)
		SELECT * FROM TempResult r, TempCount
		ORDER BY r.Id DESC OFFSET (@PageSize * (@Page - 1)) ROWS FETCH NEXT @PageSize ROWS ONLY;

    END TRY
    BEGIN CATCH
        SELECT
            @strErrorMessage = ERROR_MESSAGE()
                    + ' Line:'
                    + CONVERT(VARCHAR(5), ERROR_LINE()),
            @intErrorSeverity = ERROR_SEVERITY(),
            @intErrorState = ERROR_STATE();
        RAISERROR(
                @strErrorMessage,   -- Message text.
                @intErrorSeverity,  -- Severity.
                @intErrorState      -- State.
        );
    END CATCH;
END



GO
/****** Object:  StoredProcedure [dbo].[uspRooms_Return]    Script Date: 5/20/2022 10:35:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[uspRooms_Return]
(
    @Id BIGINT,
    @IsPayment BIT,
    @OrderRoomId BIGINT
)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE
        @strErrorMessage NVARCHAR(4000),
        @intErrorSeverity INT,
        @intErrorState INT,
        @intErrorLine INT,
        @time_now DATETIME = DATEADD(HOUR, +7, GETUTCDATE());
    BEGIN TRY
        IF(@IsPayment = 1)
            SELECT
                r.[Id]
                ,p.[Name] AS ProvinceName
                ,h.[Name] AS HotelName
                ,r.[Name]
                ,f.[Name] AS FloorName
                ,c.[FullName]
                ,c.[PhoneNumber]
                ,c.[IDCard]
                ,r.Price
                ,r.PromotionalPrice
                ,oro.Id AS OrderRoomId
                ,oro.StartTime
                ,@time_now AS EndTime
                ,CAST((ABS(DATEDIFF(mi, oro.StartTime, @time_now) / 60)) AS VARCHAR(10)) AS TotalHour
                ,CAST((ABS(DATEDIFF(mi, oro.StartTime, @time_now) % 60)) AS VARCHAR(2)) AS TotalMinutes
                ,oro.[CreatedBy]
            FROM Rooms AS r
            LEFT JOIN Hotels AS h ON h.Id = r.[HotelId] AND h.IsDeleted = 0
            LEFT JOIN Provinces AS p ON p.Id = h.[ProvinceId] AND p.IsDeleted = 0
            LEFT JOIN Floors AS f ON f.Id = r.[FloorId] AND f.IsDeleted = 0
            LEFT JOIN OrderRoom AS oro ON oro.RoomId = r.Id AND oro.[Status] = 0
            LEFT JOIN Customers AS c ON c.Id = oro.CustomerId AND c.IsDeleted = 0
            WHERE r.Id = @Id AND r.IsDeleted = 0 
        ELSE
            SELECT
                r.[Id]
                ,p.[Name] AS ProvinceName
                ,h.[Name] AS HotelName
                ,r.[Name]
                ,f.[Name] AS FloorName
                ,c.[FullName]
                ,c.[PhoneNumber]
                ,c.[IDCard]
                ,r.Price
                ,r.PromotionalPrice
                ,oro.Id AS OrderRoomId
                ,oro.StartTime
                ,oro.EndTime
                ,CAST((ABS(DATEDIFF(mi, oro.StartTime, oro.EndTime) / 60)) AS VARCHAR(10)) AS TotalHour
                ,CAST((ABS(DATEDIFF(mi, oro.StartTime, oro.EndTime) % 60)) AS VARCHAR(2)) AS TotalMinutes
                ,oro.[CreatedBy]
                ,oro.[Status]
            FROM Rooms AS r
            LEFT JOIN Hotels AS h ON h.Id = r.[HotelId] AND h.IsDeleted = 0
            LEFT JOIN Provinces AS p ON p.Id = h.[ProvinceId] AND p.IsDeleted = 0
            LEFT JOIN Floors AS f ON f.Id = r.[FloorId] AND f.IsDeleted = 0
            LEFT JOIN OrderRoom AS oro ON oro.RoomId = r.Id AND oro.[Id] = @OrderRoomId
            LEFT JOIN Customers AS c ON c.Id = oro.CustomerId AND c.IsDeleted = 0
            WHERE r.Id = @Id AND r.IsDeleted = 0 

    END TRY
    BEGIN CATCH
        SELECT
            @strErrorMessage = ERROR_MESSAGE()
                    + ' Line:'
                    + CONVERT(VARCHAR(5), ERROR_LINE()),
            @intErrorSeverity = ERROR_SEVERITY(),
            @intErrorState = ERROR_STATE();
        RAISERROR(
                @strErrorMessage,   -- Message text.
                @intErrorSeverity,  -- Severity.
                @intErrorState      -- State.
        );
    END CATCH;
END



GO
/****** Object:  StoredProcedure [dbo].[uspServiceCategories_GetAll]    Script Date: 5/20/2022 10:35:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create PROCEDURE [dbo].[uspServiceCategories_GetAll]
(
    @Key NVARCHAR(50),
    @Page INT,
    @PageSize INT
)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE
        @strErrorMessage NVARCHAR(4000),
        @intErrorSeverity INT,
        @intErrorState INT,
        @intErrorLine INT;

    BEGIN TRY

	    WITH TempResult AS 
		(
			SELECT
			   ROW_NUMBER() OVER(ORDER BY [Id] DESC) AS STT
			  ,[Id]
			  ,[Name]
			  ,[CreatedBy]
			  ,[CreatedDate]
			  ,[ModifiedBy]
			  ,[ModifiedDate]
			FROM ServiceCategories
			WHERE (
					ISNULL(@Key, '') = '' OR 
					dbo.ufnRemoveMark(TRIM(UPPER([Name]))) LIKE CONCAT('%',dbo.ufnRemoveMark(TRIM(UPPER(@Key))),'%')
				  )
				  AND IsDeleted = 0 
		),
		TempCount (TotalRow)
    
		AS (SELECT COUNT(*) FROM TempResult)
		SELECT * FROM TempResult r, TempCount
		ORDER BY r.Id DESC OFFSET (@PageSize * (@Page - 1)) ROWS FETCH NEXT @PageSize ROWS ONLY;

    END TRY
    BEGIN CATCH
        SELECT
            @strErrorMessage = ERROR_MESSAGE()
                    + ' Line:'
                    + CONVERT(VARCHAR(5), ERROR_LINE()),
            @intErrorSeverity = ERROR_SEVERITY(),
            @intErrorState = ERROR_STATE();
        RAISERROR(
                @strErrorMessage,   -- Message text.
                @intErrorSeverity,  -- Severity.
                @intErrorState      -- State.
        );
    END CATCH;
END



GO
/****** Object:  StoredProcedure [dbo].[uspServices_GetAll]    Script Date: 5/20/2022 10:35:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[uspServices_GetAll]
(
    @Key NVARCHAR(50),
    @Page INT,
    @PageSize INT
)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE
        @strErrorMessage NVARCHAR(4000),
        @intErrorSeverity INT,
        @intErrorState INT,
        @intErrorLine INT;

    BEGIN TRY

	    WITH TempResult AS 
		(
			SELECT
			   ROW_NUMBER() OVER(ORDER BY s.[Id] DESC) AS STT
			  ,s.[Id]
			  ,s.[Name]
			  ,s.[Price]
			  ,s.[Unit]
			  ,s.[Status]
			  ,s.[Note]
              ,s.[Image]
			  ,s.[ServiceCategoriesId]
              ,sc.[Name] AS ServiceCategoriesName
			  ,s.[CreatedBy]
			  ,s.[CreatedDate]
			  ,s.[ModifiedBy]
			  ,s.[ModifiedDate]
			FROM Services as s
            LEFT JOIN ServiceCategories AS sc ON sc.Id = s.ServiceCategoriesId AND sc.IsDeleted = 0
			WHERE (
					ISNULL(@Key, '') = '' OR 
					dbo.ufnRemoveMark(TRIM(UPPER(s.[Name]))) LIKE CONCAT('%',dbo.ufnRemoveMark(TRIM(UPPER(@Key))),'%')
				  )
				  AND s.IsDeleted = 0 
		),
		TempCount (TotalRow)
    
		AS (SELECT COUNT(*) FROM TempResult)
		SELECT * FROM TempResult r, TempCount
		ORDER BY r.Id DESC OFFSET (@PageSize * (@Page - 1)) ROWS FETCH NEXT @PageSize ROWS ONLY;

    END TRY
    BEGIN CATCH
        SELECT
            @strErrorMessage = ERROR_MESSAGE()
                    + ' Line:'
                    + CONVERT(VARCHAR(5), ERROR_LINE()),
            @intErrorSeverity = ERROR_SEVERITY(),
            @intErrorState = ERROR_STATE();
        RAISERROR(
                @strErrorMessage,   -- Message text.
                @intErrorSeverity,  -- Severity.
                @intErrorState      -- State.
        );
    END CATCH;
END



GO
/****** Object:  StoredProcedure [dbo].[uspUserProfiles_GetAll]    Script Date: 5/20/2022 10:35:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[uspUserProfiles_GetAll]
(
    @Key NVARCHAR(50),
    @Page INT,
    @PageSize INT
)
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE
        @strErrorMessage NVARCHAR(4000),
        @intErrorSeverity INT,
        @intErrorState INT,
        @intErrorLine INT;

    BEGIN TRY

	    WITH TempResult AS 
		(
			SELECT
			   ROW_NUMBER() OVER(ORDER BY up.[Id] DESC) AS STT
			  ,up.[Id]
			  ,up.[UserName]
			  ,up.[FullName]
			  ,up.[DateOfBirth]
			  ,up.[PhoneNumber]
			  ,up.[Address]
			  ,up.[Email]
			  ,up.[Facebook]
			  ,up.[Zalo]
			  ,up.[Active]
			  ,up.[SexId]
			  ,s.[Name] AS SexName
			  ,up.[RoleId]
			  ,r.[Name] AS RoleName
			  ,up.[CreatedBy]
			  ,up.[CreatedDate]
			  ,up.[ModifiedBy]
			  ,up.[ModifiedDate]
			FROM [UserProfiles] AS up
			LEFT JOIN Sex AS s ON s.Id = up.[SexId]
			LEFT JOIN Roles AS r ON r.Id = up.[RoleId]
			WHERE (
					ISNULL(@Key, '') = '' OR 
					dbo.ufnRemoveMark(TRIM(UPPER(up.[UserName]))) LIKE CONCAT('%',dbo.ufnRemoveMark(TRIM(UPPER(@Key))),'%')
				  )
		),
		TempCount (TotalRow)
    
		AS (SELECT COUNT(*) FROM TempResult)
		SELECT * FROM TempResult r, TempCount
		ORDER BY r.Id DESC OFFSET (@PageSize * (@Page - 1)) ROWS FETCH NEXT @PageSize ROWS ONLY;

    END TRY
    BEGIN CATCH
        SELECT
            @strErrorMessage = ERROR_MESSAGE()
                    + ' Line:'
                    + CONVERT(VARCHAR(5), ERROR_LINE()),
            @intErrorSeverity = ERROR_SEVERITY(),
            @intErrorState = ERROR_STATE();
        RAISERROR(
                @strErrorMessage,   -- Message text.
                @intErrorSeverity,  -- Severity.
                @intErrorState      -- State.
        );
    END CATCH;
END



GO
USE [master]
GO
ALTER DATABASE [DEVHotelManager] SET  READ_WRITE 
GO
