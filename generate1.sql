USE [master]
GO
/****** Object:  Database [Wallet]    Script Date: 25/04/2022 13:07:38 ******/
CREATE DATABASE [Wallet]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Wallet', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\Wallet.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Wallet_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\Wallet_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [Wallet] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Wallet].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Wallet] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Wallet] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Wallet] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Wallet] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Wallet] SET ARITHABORT OFF 
GO
ALTER DATABASE [Wallet] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Wallet] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Wallet] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Wallet] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Wallet] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Wallet] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Wallet] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Wallet] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Wallet] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Wallet] SET  ENABLE_BROKER 
GO
ALTER DATABASE [Wallet] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Wallet] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Wallet] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Wallet] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Wallet] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Wallet] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [Wallet] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Wallet] SET RECOVERY FULL 
GO
ALTER DATABASE [Wallet] SET  MULTI_USER 
GO
ALTER DATABASE [Wallet] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Wallet] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Wallet] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Wallet] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Wallet] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Wallet] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'Wallet', N'ON'
GO
ALTER DATABASE [Wallet] SET QUERY_STORE = OFF
GO
USE [Wallet]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 25/04/2022 13:07:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Accounts]    Script Date: 25/04/2022 13:07:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Accounts](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Fortune] [decimal](18, 2) NOT NULL,
 CONSTRAINT [PK_Accounts] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[IncomeItems]    Script Date: 25/04/2022 13:07:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[IncomeItems](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
	[AccountId] [int] NOT NULL,
 CONSTRAINT [PK_IncomeItems] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Transactions]    Script Date: 25/04/2022 13:07:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Transactions](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Sum] [decimal](18, 2) NOT NULL,
	[TransactionTypeId] [int] NOT NULL,
	[IncomeItemId] [int] NOT NULL,
 CONSTRAINT [PK_Transactions] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TransactionType]    Script Date: 25/04/2022 13:07:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TransactionType](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
 CONSTRAINT [PK_TransactionType] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20220415171742_Initial', N'6.0.4')
GO
SET IDENTITY_INSERT [dbo].[Accounts] ON 

INSERT [dbo].[Accounts] ([Id], [Fortune]) VALUES (1, CAST(0.00 AS Decimal(18, 2)))
INSERT [dbo].[Accounts] ([Id], [Fortune]) VALUES (2, CAST(350.00 AS Decimal(18, 2)))
INSERT [dbo].[Accounts] ([Id], [Fortune]) VALUES (3, CAST(300.00 AS Decimal(18, 2)))
SET IDENTITY_INSERT [dbo].[Accounts] OFF
GO
SET IDENTITY_INSERT [dbo].[IncomeItems] ON 

INSERT [dbo].[IncomeItems] ([Id], [Name], [AccountId]) VALUES (1, N'music', 1)
INSERT [dbo].[IncomeItems] ([Id], [Name], [AccountId]) VALUES (2, N'music', 2)
INSERT [dbo].[IncomeItems] ([Id], [Name], [AccountId]) VALUES (3, N'music', 3)
INSERT [dbo].[IncomeItems] ([Id], [Name], [AccountId]) VALUES (4, N'movies', 1)
INSERT [dbo].[IncomeItems] ([Id], [Name], [AccountId]) VALUES (5, N'food', 3)
SET IDENTITY_INSERT [dbo].[IncomeItems] OFF
GO
SET IDENTITY_INSERT [dbo].[Transactions] ON 

INSERT [dbo].[Transactions] ([Id], [Sum], [TransactionTypeId], [IncomeItemId]) VALUES (1, CAST(50.00 AS Decimal(18, 2)), 1, 2)
INSERT [dbo].[Transactions] ([Id], [Sum], [TransactionTypeId], [IncomeItemId]) VALUES (2, CAST(50.00 AS Decimal(18, 2)), 1, 2)
INSERT [dbo].[Transactions] ([Id], [Sum], [TransactionTypeId], [IncomeItemId]) VALUES (3, CAST(-50.00 AS Decimal(18, 2)), 2, 2)
SET IDENTITY_INSERT [dbo].[Transactions] OFF
GO
SET IDENTITY_INSERT [dbo].[TransactionType] ON 

INSERT [dbo].[TransactionType] ([Id], [Name]) VALUES (1, N'Income')
INSERT [dbo].[TransactionType] ([Id], [Name]) VALUES (2, N'Expenditure')
SET IDENTITY_INSERT [dbo].[TransactionType] OFF
GO
/****** Object:  Index [IX_IncomeItems_AccountId]    Script Date: 25/04/2022 13:07:39 ******/
CREATE NONCLUSTERED INDEX [IX_IncomeItems_AccountId] ON [dbo].[IncomeItems]
(
	[AccountId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Transactions_IncomeItemId]    Script Date: 25/04/2022 13:07:39 ******/
CREATE NONCLUSTERED INDEX [IX_Transactions_IncomeItemId] ON [dbo].[Transactions]
(
	[IncomeItemId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Transactions_TransactionTypeId]    Script Date: 25/04/2022 13:07:39 ******/
CREATE NONCLUSTERED INDEX [IX_Transactions_TransactionTypeId] ON [dbo].[Transactions]
(
	[TransactionTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[IncomeItems]  WITH CHECK ADD  CONSTRAINT [FK_IncomeItems_Accounts_AccountId] FOREIGN KEY([AccountId])
REFERENCES [dbo].[Accounts] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[IncomeItems] CHECK CONSTRAINT [FK_IncomeItems_Accounts_AccountId]
GO
ALTER TABLE [dbo].[Transactions]  WITH CHECK ADD  CONSTRAINT [FK_Transactions_IncomeItems_IncomeItemId] FOREIGN KEY([IncomeItemId])
REFERENCES [dbo].[IncomeItems] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Transactions] CHECK CONSTRAINT [FK_Transactions_IncomeItems_IncomeItemId]
GO
ALTER TABLE [dbo].[Transactions]  WITH CHECK ADD  CONSTRAINT [FK_Transactions_TransactionType_TransactionTypeId] FOREIGN KEY([TransactionTypeId])
REFERENCES [dbo].[TransactionType] ([Id])
GO
ALTER TABLE [dbo].[Transactions] CHECK CONSTRAINT [FK_Transactions_TransactionType_TransactionTypeId]
GO
USE [master]
GO
ALTER DATABASE [Wallet] SET  READ_WRITE 
GO
