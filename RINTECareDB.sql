USE [master]
GO
/****** Object:  Database [RINTECareDB]    Script Date: 16/01/2021 20:16:47 ******/
CREATE DATABASE [RINTECareDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'RINTECareDB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\RINTECareDB.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'RINTECareDB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\RINTECareDB_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [RINTECareDB] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [RINTECareDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [RINTECareDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [RINTECareDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [RINTECareDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [RINTECareDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [RINTECareDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [RINTECareDB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [RINTECareDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [RINTECareDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [RINTECareDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [RINTECareDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [RINTECareDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [RINTECareDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [RINTECareDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [RINTECareDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [RINTECareDB] SET  DISABLE_BROKER 
GO
ALTER DATABASE [RINTECareDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [RINTECareDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [RINTECareDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [RINTECareDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [RINTECareDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [RINTECareDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [RINTECareDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [RINTECareDB] SET RECOVERY FULL 
GO
ALTER DATABASE [RINTECareDB] SET  MULTI_USER 
GO
ALTER DATABASE [RINTECareDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [RINTECareDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [RINTECareDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [RINTECareDB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [RINTECareDB] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'RINTECareDB', N'ON'
GO
ALTER DATABASE [RINTECareDB] SET QUERY_STORE = OFF
GO
USE [RINTECareDB]
GO
/****** Object:  Table [dbo].[Administrator]    Script Date: 16/01/2021 20:16:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Administrator](
	[idAdministrator] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [varchar](100) NULL,
	[LastName] [varchar](100) NULL,
	[Gender] [varchar](10) NULL,
	[Age] [int] NULL,
	[TaxNumber] [int] NULL,
	[PhoneNumber] [int] NULL,
	[Location] [varchar](100) NULL,
	[Address] [varchar](100) NULL,
	[ZipCode] [varchar](8) NULL,
	[Username] [varchar](50) NULL,
 CONSTRAINT [PK_Administrator] PRIMARY KEY CLUSTERED 
(
	[idAdministrator] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Doctor]    Script Date: 16/01/2021 20:16:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Doctor](
	[idDoctor] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [varchar](100) NULL,
	[LastName] [varchar](100) NULL,
	[Gender] [varchar](10) NULL,
	[Age] [int] NULL,
	[TaxNumber] [int] NULL,
	[ProfessionalLicenseNumber] [int] NULL,
	[PhoneNumber] [nchar](9) NULL,
	[Location] [varchar](100) NULL,
	[Address] [varchar](100) NULL,
	[ZipCode] [nchar](8) NULL,
	[idSpecialization] [int] NOT NULL,
	[Username] [varchar](50) NULL,
 CONSTRAINT [PK_Doctor] PRIMARY KEY CLUSTERED 
(
	[idDoctor] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DoctorAppointment]    Script Date: 16/01/2021 20:16:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DoctorAppointment](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[idDoctorAvailability] [int] NULL,
	[idPatient] [int] NULL,
	[DateAppointment] [date] NULL,
	[State] [int] NULL,
 CONSTRAINT [PK_DoctorAppointment] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DoctorAvailability]    Script Date: 16/01/2021 20:16:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DoctorAvailability](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[idDoctor] [int] NULL,
	[idWeekDay] [int] NULL,
	[idSlot] [int] NULL,
	[DayStart] [date] NULL,
	[DayEnd] [date] NULL,
	[active] [bit] NULL,
 CONSTRAINT [PK_DoctorAvailability] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Patient]    Script Date: 16/01/2021 20:16:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Patient](
	[idPatient] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [varchar](100) NULL,
	[LastName] [varchar](100) NULL,
	[Gender] [varchar](10) NULL,
	[Age] [int] NULL,
	[Height] [nchar](3) NULL,
	[Weight] [nchar](3) NULL,
	[BloodType] [nchar](3) NULL,
	[Allergies] [varchar](100) NULL,
	[Job] [varchar](100) NULL,
	[TaxNumber] [int] NULL,
	[PhoneNumber] [int] NULL,
	[Location] [varchar](100) NULL,
	[Address] [varchar](100) NULL,
	[ZipCode] [nchar](8) NULL,
	[Description] [varchar](200) NULL,
	[Username] [varchar](50) NULL,
 CONSTRAINT [PK_Patient] PRIMARY KEY CLUSTERED 
(
	[idPatient] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Slots]    Script Date: 16/01/2021 20:16:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Slots](
	[idSlot] [int] IDENTITY(1,1) NOT NULL,
	[StartHour] [time](0) NULL,
	[EndHour] [time](0) NULL,
 CONSTRAINT [PK_Slots] PRIMARY KEY CLUSTERED 
(
	[idSlot] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Specialization]    Script Date: 16/01/2021 20:16:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Specialization](
	[idSpecialization] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NULL,
 CONSTRAINT [PK_Specialization] PRIMARY KEY CLUSTERED 
(
	[idSpecialization] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserApplication]    Script Date: 16/01/2021 20:16:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserApplication](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Username] [varchar](50) NOT NULL,
	[Password] [varchar](50) NOT NULL,
	[Role] [varchar](20) NOT NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [PK_UserApplication] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [UQ__UserAppl__536C85E4ADD3FDA3] UNIQUE NONCLUSTERED 
(
	[Username] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[WeekDay]    Script Date: 16/01/2021 20:16:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WeekDay](
	[idWeekDay] [int] IDENTITY(1,1) NOT NULL,
	[WeekDay] [int] NULL,
	[Name] [varchar](20) NULL,
 CONSTRAINT [PK_WeekDay] PRIMARY KEY CLUSTERED 
(
	[idWeekDay] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[DoctorAppointment] ADD  CONSTRAINT [DF_DoctorAppointment_State]  DEFAULT ((0)) FOR [State]
GO
ALTER TABLE [dbo].[DoctorAvailability] ADD  CONSTRAINT [DF_DoctorAvailability_active]  DEFAULT ((1)) FOR [active]
GO
ALTER TABLE [dbo].[UserApplication] ADD  CONSTRAINT [DF_UserApplication_Active]  DEFAULT ((1)) FOR [Active]
GO
ALTER TABLE [dbo].[Administrator]  WITH CHECK ADD  CONSTRAINT [FK__Administr__Usern__19DFD96B] FOREIGN KEY([Username])
REFERENCES [dbo].[UserApplication] ([Username])
ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[Administrator] CHECK CONSTRAINT [FK__Administr__Usern__19DFD96B]
GO
ALTER TABLE [dbo].[Doctor]  WITH CHECK ADD  CONSTRAINT [FK__Doctor__idSpecia__38996AB5] FOREIGN KEY([idSpecialization])
REFERENCES [dbo].[Specialization] ([idSpecialization])
GO
ALTER TABLE [dbo].[Doctor] CHECK CONSTRAINT [FK__Doctor__idSpecia__38996AB5]
GO
ALTER TABLE [dbo].[Doctor]  WITH CHECK ADD  CONSTRAINT [FK__Doctor__Username__18EBB532] FOREIGN KEY([Username])
REFERENCES [dbo].[UserApplication] ([Username])
ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[Doctor] CHECK CONSTRAINT [FK__Doctor__Username__18EBB532]
GO
ALTER TABLE [dbo].[DoctorAppointment]  WITH CHECK ADD FOREIGN KEY([idDoctorAvailability])
REFERENCES [dbo].[DoctorAvailability] ([id])
GO
ALTER TABLE [dbo].[DoctorAppointment]  WITH CHECK ADD  CONSTRAINT [FK__DoctorApp__idPat__3C69FB99] FOREIGN KEY([idPatient])
REFERENCES [dbo].[Patient] ([idPatient])
GO
ALTER TABLE [dbo].[DoctorAppointment] CHECK CONSTRAINT [FK__DoctorApp__idPat__3C69FB99]
GO
ALTER TABLE [dbo].[DoctorAppointment]  WITH CHECK ADD  CONSTRAINT [FK__DoctorApp__idPat__3D5E1FD2] FOREIGN KEY([idPatient])
REFERENCES [dbo].[Patient] ([idPatient])
GO
ALTER TABLE [dbo].[DoctorAppointment] CHECK CONSTRAINT [FK__DoctorApp__idPat__3D5E1FD2]
GO
ALTER TABLE [dbo].[DoctorAvailability]  WITH CHECK ADD  CONSTRAINT [FK__DoctorAva__idDoc__3E52440B] FOREIGN KEY([idDoctor])
REFERENCES [dbo].[Doctor] ([idDoctor])
GO
ALTER TABLE [dbo].[DoctorAvailability] CHECK CONSTRAINT [FK__DoctorAva__idDoc__3E52440B]
GO
ALTER TABLE [dbo].[DoctorAvailability]  WITH CHECK ADD FOREIGN KEY([idSlot])
REFERENCES [dbo].[Slots] ([idSlot])
GO
ALTER TABLE [dbo].[DoctorAvailability]  WITH CHECK ADD FOREIGN KEY([idWeekDay])
REFERENCES [dbo].[WeekDay] ([idWeekDay])
GO
ALTER TABLE [dbo].[Patient]  WITH CHECK ADD  CONSTRAINT [FK__Patient__Usernam__1AD3FDA4] FOREIGN KEY([Username])
REFERENCES [dbo].[UserApplication] ([Username])
ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[Patient] CHECK CONSTRAINT [FK__Patient__Usernam__1AD3FDA4]
GO
ALTER TABLE [dbo].[DoctorAppointment]  WITH CHECK ADD  CONSTRAINT [CHK_State] CHECK  (([State]=(4) OR [State]=(3) OR [State]=(2) OR [State]=(1) OR [State]=(0)))
GO
ALTER TABLE [dbo].[DoctorAppointment] CHECK CONSTRAINT [CHK_State]
GO
ALTER TABLE [dbo].[UserApplication]  WITH CHECK ADD  CONSTRAINT [CHK_Role] CHECK  (([Role]='Doctor' OR [Role]='Patient' OR [Role]='Administrator'))
GO
ALTER TABLE [dbo].[UserApplication] CHECK CONSTRAINT [CHK_Role]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0 - appointment, 1 - consultation completed, 2 - Canceled by Client, 3 - Canceled  by Doctor' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DoctorAppointment', @level2type=N'COLUMN',@level2name=N'State'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Devia ser validada a data se respeita o dia da semana do availability ... trigger' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DoctorAppointment'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Ter cuidado para não ter Entradas sobrepostas...Teria de ser com um trigger... Talvez garantir que um conjunto de FK está activo em determinado momento... para futuro' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DoctorAvailability'
GO
USE [master]
GO
ALTER DATABASE [RINTECareDB] SET  READ_WRITE 
GO
