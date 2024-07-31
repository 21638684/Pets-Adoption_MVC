USE [master]
GO
/****** Object:  Database [PetsRescue] ******/
CREATE DATABASE [PetsRescue]

GO
ALTER DATABASE [PetsRescue] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [PetsRescue.[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [PetsRescue] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [PetsRescue] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [PetsRescue] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [PetsRescue] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [PetsRescue] SET ARITHABORT OFF 
GO
ALTER DATABASE [PetsRescue] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [PetsRescue] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [PetsRescue] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [PetsRescue] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [PetsRescue] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [PetsRescue] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [PetsRescue] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [PetsRescue] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [PetsRescue] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [PetsRescue] SET  DISABLE_BROKER 
GO
ALTER DATABASE [PetsRescue] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [PetsRescue] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [PetsRescue] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [PetsRescue] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [PetsRescue] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [PetsRescue] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [PetsRescue] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [PetsRescue] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [PetsRescue] SET  MULTI_USER 
GO
ALTER DATABASE [PetsRescue] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [PetsRescue] SET DB_CHAINING OFF 
GO
ALTER DATABASE [PetsRescue] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [PetsRescue] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [PetsRescue] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [PetsRescue] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [PetsRescue] SET QUERY_STORE = OFF
GO
USE [PetsRescue]
GO
/****** Object:  Table [dbo].[User] ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](40) NOT NULL,
	[LastName] [nvarchar](40) NOT NULL,
	[PhoneNo] [int] NULL,
 CONSTRAINT [PK_USER] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Pet]******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Pet](
    [Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[PetName] [nvarchar](40) NOT NULL,
	[Type] [nvarchar](40) NOT NULL,
	[Breed] [nvarchar](40) NOT NULL,
	[Location] [nvarchar](100) NOT NULL,
	[Age] [int] NOT NULL,
	[Gender] [nvarchar](10) NOT NULL,
	[Weight] [decimal] NOT NULL,
	[PetStory] [nvarchar](100) NOT NULL,
	[Image] [nvarchar](150) NOT NULL,
	[Status] [nvarchar](40) NOT NULL,
 CONSTRAINT [PK_PET] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Adoption] ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Adoption](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[PetId] [int] NOT NULL,
 CONSTRAINT [PK_ADOPTION] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Donation]  ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Donation](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[Amount] [decimal](12, 2) NULL,
 CONSTRAINT [PK_DONATION] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[User] ON 
INSERT [dbo].[User] ([Id], [FirstName], [LastName], [PhoneNo]) VALUES (1, 'Shimy', 'Moso', 0862641614)
INSERT [dbo].[User] ([Id], [FirstName], [LastName], [PhoneNo]) VALUES (2, 'Dkay', 'Mohlots', 0762641614)
INSERT [dbo].[User] ([Id], [FirstName], [LastName], [PhoneNo]) VALUES (3, 'Reev', 'Shaup', 0862641614)
INSERT [dbo].[User] ([Id], [FirstName], [LastName], [PhoneNo]) VALUES (4, 'Van ', 'Dyk', 0776641614)
INSERT [dbo].[User] ([Id], [FirstName], [LastName], [PhoneNo]) VALUES (5, 'Sarah', 'Ramp', 0862641614)
INSERT [dbo].[User] ([Id], [FirstName], [LastName], [PhoneNo]) VALUES (6, 'Calvin', 'Green', 0862641614)
INSERT [dbo].[User] ([Id], [FirstName], [LastName], [PhoneNo]) VALUES (7, 'Bodine', 'GK', 0852641614)
INSERT [dbo].[User] ([Id], [FirstName], [LastName], [PhoneNo]) VALUES (8, 'Evert', 'Comics', 0892641614)
INSERT [dbo].[User] ([Id], [FirstName], [LastName], [PhoneNo]) VALUES (9, 'Banele', 'Prest', 0872641614)
INSERT [dbo].[User] ([Id], [FirstName], [LastName], [PhoneNo]) VALUES (10, 'Robert', 'Vande', 0732641614)
SET IDENTITY_INSERT [dbo].[User] OFF
GO

ALTER TABLE [dbo].[Pet]  WITH CHECK ADD  CONSTRAINT [FK_PET_REFERENCE_USER] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[Pet] CHECK CONSTRAINT [FK_PET_REFERENCE_USER]
GO
ALTER TABLE [dbo].[Adoption]  WITH CHECK ADD  CONSTRAINT [FK_ADOPTION_REFERENCE_PET] FOREIGN KEY([PetId])
REFERENCES [dbo].[Pet] ([Id])
GO
ALTER TABLE [dbo].[Adoption] CHECK CONSTRAINT [FK_ADOPTION_REFERENCE_PET]
GO
ALTER TABLE [dbo].[Adoption]  WITH CHECK ADD  CONSTRAINT [FK_ADOPTION_REFERENCE_USER] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[Adoption] CHECK CONSTRAINT [FK_ADOPTION_REFERENCE_USER]
GO
ALTER TABLE [dbo].[Donation]  WITH CHECK ADD  CONSTRAINT [FK_DONATION_REFERENCE_USER] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[Donation] CHECK CONSTRAINT [FK_DONATION_REFERENCE_USER]
GO
USE [master]
GO
ALTER DATABASE [PetsRescue] SET  READ_WRITE 
GO