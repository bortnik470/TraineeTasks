USE [master]
GO
/****** Object:  Database [University]    Script Date: 4/30/2024 1:32:12 PM ******/
CREATE DATABASE [University]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'University', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\University.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'University_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\University_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [University] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [University].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [University] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [University] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [University] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [University] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [University] SET ARITHABORT OFF 
GO
ALTER DATABASE [University] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [University] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [University] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [University] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [University] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [University] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [University] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [University] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [University] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [University] SET  DISABLE_BROKER 
GO
ALTER DATABASE [University] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [University] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [University] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [University] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [University] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [University] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [University] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [University] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [University] SET  MULTI_USER 
GO
ALTER DATABASE [University] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [University] SET DB_CHAINING OFF 
GO
ALTER DATABASE [University] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [University] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [University] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [University] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [University] SET QUERY_STORE = ON
GO
ALTER DATABASE [University] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [University]
GO
/****** Object:  UserDefinedFunction [dbo].[Summary]    Script Date: 4/30/2024 1:32:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Function [dbo].[Summary](@a int, @b int)
Returns int
as
begin;
	return @a + @b;
end;
GO
/****** Object:  Table [dbo].[Students]    Script Date: 4/30/2024 1:32:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Students](
	[studentId] [int] IDENTITY(1,1) NOT NULL,
	[firstName] [varchar](255) NOT NULL,
	[lastName] [varchar](255) NOT NULL,
	[phoneNumber] [varchar](55) NULL,
	[groupName] [varchar](5) NULL,
PRIMARY KEY CLUSTERED 
(
	[studentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[getStudentData]    Script Date: 4/30/2024 1:32:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create   view [dbo].[getStudentData]
as
	select *
	from Students as s1
	cross apply(select info = (select RTRIM(FirstName) + ' ' + RTRIM(lastName) + 
		' with phone number: ' + RTRIM(phoneNumber) + ' and group name: ' + RTRIM(groupName) + ' '
	from students where studentId = s1.studentId)) as info;
GO
/****** Object:  Table [dbo].[Courses]    Script Date: 4/30/2024 1:32:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Courses](
	[courseId] [int] IDENTITY(1,1) NOT NULL,
	[courseName] [varchar](255) NOT NULL,
	[score] [varchar](5) NULL,
	[startDate] [date] NOT NULL,
	[endDate] [date] NULL,
	[studentId] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[courseId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StudentLog]    Script Date: 4/30/2024 1:32:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StudentLog](
	[studentId] [int] NOT NULL,
	[firstName] [varchar](255) NOT NULL,
	[lastName] [varchar](255) NOT NULL,
	[phoneNumber] [varchar](55) NULL,
	[groupName] [varchar](5) NULL,
	[operation] [varchar](10) NOT NULL,
	[operationDate] [datetime] NOT NULL
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Courses] ON 

INSERT [dbo].[Courses] ([courseId], [courseName], [score], [startDate], [endDate], [studentId]) VALUES (3, N'Math', N'E', CAST(N'2002-02-11' AS Date), CAST(N'2002-06-20' AS Date), 2)
INSERT [dbo].[Courses] ([courseId], [courseName], [score], [startDate], [endDate], [studentId]) VALUES (4, N'English', N'C', CAST(N'2001-09-15' AS Date), CAST(N'2002-01-10' AS Date), 2)
INSERT [dbo].[Courses] ([courseId], [courseName], [score], [startDate], [endDate], [studentId]) VALUES (5, N'Art', N'D', CAST(N'2001-09-15' AS Date), CAST(N'2002-01-10' AS Date), 2)
INSERT [dbo].[Courses] ([courseId], [courseName], [score], [startDate], [endDate], [studentId]) VALUES (6, N'History', N'None', CAST(N'2003-02-17' AS Date), CAST(N'2003-06-18' AS Date), 3)
INSERT [dbo].[Courses] ([courseId], [courseName], [score], [startDate], [endDate], [studentId]) VALUES (7, N'English', N'D', CAST(N'2002-09-01' AS Date), CAST(N'2003-01-11' AS Date), 3)
INSERT [dbo].[Courses] ([courseId], [courseName], [score], [startDate], [endDate], [studentId]) VALUES (8, N'Art', N'None', CAST(N'2003-02-15' AS Date), CAST(N'2003-06-10' AS Date), 4)
INSERT [dbo].[Courses] ([courseId], [courseName], [score], [startDate], [endDate], [studentId]) VALUES (9, N'English', N'D', CAST(N'2002-09-01' AS Date), CAST(N'2003-01-19' AS Date), 4)
INSERT [dbo].[Courses] ([courseId], [courseName], [score], [startDate], [endDate], [studentId]) VALUES (10, N'PE', N'A', CAST(N'2002-09-04' AS Date), CAST(N'2003-01-12' AS Date), 4)
INSERT [dbo].[Courses] ([courseId], [courseName], [score], [startDate], [endDate], [studentId]) VALUES (11, N'Art', N'Fx', CAST(N'2000-02-21' AS Date), CAST(N'2001-06-18' AS Date), 5)
INSERT [dbo].[Courses] ([courseId], [courseName], [score], [startDate], [endDate], [studentId]) VALUES (12, N'Art', N'C', CAST(N'2001-09-24' AS Date), CAST(N'2002-01-04' AS Date), 5)
INSERT [dbo].[Courses] ([courseId], [courseName], [score], [startDate], [endDate], [studentId]) VALUES (13, N'English', N'D', CAST(N'2000-09-17' AS Date), CAST(N'2001-01-24' AS Date), 5)
INSERT [dbo].[Courses] ([courseId], [courseName], [score], [startDate], [endDate], [studentId]) VALUES (14, N'History', N'B', CAST(N'2000-09-15' AS Date), CAST(N'2001-01-15' AS Date), 5)
INSERT [dbo].[Courses] ([courseId], [courseName], [score], [startDate], [endDate], [studentId]) VALUES (20, N'PE', N'A', CAST(N'1992-02-24' AS Date), CAST(N'1993-02-23' AS Date), 19)
INSERT [dbo].[Courses] ([courseId], [courseName], [score], [startDate], [endDate], [studentId]) VALUES (21, N'Art', N'C', CAST(N'1992-02-22' AS Date), CAST(N'1993-02-23' AS Date), 19)
INSERT [dbo].[Courses] ([courseId], [courseName], [score], [startDate], [endDate], [studentId]) VALUES (25, N'Math', N'E', CAST(N'2002-02-11' AS Date), CAST(N'2002-06-20' AS Date), 27)
INSERT [dbo].[Courses] ([courseId], [courseName], [score], [startDate], [endDate], [studentId]) VALUES (26, N'English', N'C', CAST(N'2001-09-15' AS Date), CAST(N'2002-01-10' AS Date), 27)
INSERT [dbo].[Courses] ([courseId], [courseName], [score], [startDate], [endDate], [studentId]) VALUES (33, N'Math', N'E', CAST(N'2002-02-11' AS Date), CAST(N'2002-06-20' AS Date), 40)
INSERT [dbo].[Courses] ([courseId], [courseName], [score], [startDate], [endDate], [studentId]) VALUES (34, N'English', N'C', CAST(N'2001-09-15' AS Date), CAST(N'2002-01-10' AS Date), 40)
INSERT [dbo].[Courses] ([courseId], [courseName], [score], [startDate], [endDate], [studentId]) VALUES (35, N'Art', N'Fx', CAST(N'2002-02-11' AS Date), CAST(N'2002-05-21' AS Date), 40)
INSERT [dbo].[Courses] ([courseId], [courseName], [score], [startDate], [endDate], [studentId]) VALUES (36, N'Art', N'Fx', CAST(N'2002-02-11' AS Date), CAST(N'2002-05-21' AS Date), 40)
SET IDENTITY_INSERT [dbo].[Courses] OFF
GO
INSERT [dbo].[StudentLog] ([studentId], [firstName], [lastName], [phoneNumber], [groupName], [operation], [operationDate]) VALUES (27, N'Lary', N'Young', N'32142152', N'2A', N'UpdateOld', CAST(N'2024-04-29T17:07:52.443' AS DateTime))
INSERT [dbo].[StudentLog] ([studentId], [firstName], [lastName], [phoneNumber], [groupName], [operation], [operationDate]) VALUES (27, N'Lary', N'Young', N'32142152', N'2A', N'UpdateNew', CAST(N'2024-04-29T17:07:52.443' AS DateTime))
INSERT [dbo].[StudentLog] ([studentId], [firstName], [lastName], [phoneNumber], [groupName], [operation], [operationDate]) VALUES (42, N'Denis', N'Kin', N'4561327', N'3G', N'Insert', CAST(N'2024-04-29T17:08:15.303' AS DateTime))
INSERT [dbo].[StudentLog] ([studentId], [firstName], [lastName], [phoneNumber], [groupName], [operation], [operationDate]) VALUES (42, N'Denis', N'Kin', N'4561327', N'3G', N'Delete', CAST(N'2024-04-29T17:08:25.087' AS DateTime))
INSERT [dbo].[StudentLog] ([studentId], [firstName], [lastName], [phoneNumber], [groupName], [operation], [operationDate]) VALUES (27, N'Lary', N'Young', N'32142152', N'2A', N'UpdateOld', CAST(N'2024-04-29T17:09:04.593' AS DateTime))
INSERT [dbo].[StudentLog] ([studentId], [firstName], [lastName], [phoneNumber], [groupName], [operation], [operationDate]) VALUES (27, N'Lary', N'Young', N'32142152', N'2A', N'UpdateNew', CAST(N'2024-04-29T17:09:04.593' AS DateTime))
INSERT [dbo].[StudentLog] ([studentId], [firstName], [lastName], [phoneNumber], [groupName], [operation], [operationDate]) VALUES (27, N'Lary', N'Young', N'32142152', N'2A', N'UpdateOld', CAST(N'2024-04-29T17:09:15.917' AS DateTime))
INSERT [dbo].[StudentLog] ([studentId], [firstName], [lastName], [phoneNumber], [groupName], [operation], [operationDate]) VALUES (27, N'Lary', N'Young', N'32142152', N'2A', N'UpdateNew', CAST(N'2024-04-29T17:09:15.917' AS DateTime))
INSERT [dbo].[StudentLog] ([studentId], [firstName], [lastName], [phoneNumber], [groupName], [operation], [operationDate]) VALUES (27, N'Raly', N'Young', N'32142152', N'2A', N'UpdateOld', CAST(N'2024-04-29T17:09:35.423' AS DateTime))
INSERT [dbo].[StudentLog] ([studentId], [firstName], [lastName], [phoneNumber], [groupName], [operation], [operationDate]) VALUES (27, N'Laty', N'Young', N'32142152', N'2A', N'UpdateNew', CAST(N'2024-04-29T17:09:35.423' AS DateTime))
INSERT [dbo].[StudentLog] ([studentId], [firstName], [lastName], [phoneNumber], [groupName], [operation], [operationDate]) VALUES (43, N'Denis', N'Kin', N'4561327', N'3G', N'Insert', CAST(N'2024-04-29T17:09:55.530' AS DateTime))
INSERT [dbo].[StudentLog] ([studentId], [firstName], [lastName], [phoneNumber], [groupName], [operation], [operationDate]) VALUES (43, N'Denis', N'Kin', N'4561327', N'3G', N'Delete', CAST(N'2024-04-29T17:10:00.127' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[Students] ON 

INSERT [dbo].[Students] ([studentId], [firstName], [lastName], [phoneNumber], [groupName]) VALUES (2, N'David', N'Young', N'942345628', N'2A')
INSERT [dbo].[Students] ([studentId], [firstName], [lastName], [phoneNumber], [groupName]) VALUES (3, N'Andrew', N'Gorestriker', N'438912345', N'3A')
INSERT [dbo].[Students] ([studentId], [firstName], [lastName], [phoneNumber], [groupName]) VALUES (4, N'Ramiro', N'Amos', N'732942112', N'3B')
INSERT [dbo].[Students] ([studentId], [firstName], [lastName], [phoneNumber], [groupName]) VALUES (5, N'Walter', N'Bozzelli', N'654283745', N'6C')
INSERT [dbo].[Students] ([studentId], [firstName], [lastName], [phoneNumber], [groupName]) VALUES (19, N'Vlad', N'Smith', N'3609432642', N'2B')
INSERT [dbo].[Students] ([studentId], [firstName], [lastName], [phoneNumber], [groupName]) VALUES (27, N'Laty', N'Young', N'32142152', N'2A')
INSERT [dbo].[Students] ([studentId], [firstName], [lastName], [phoneNumber], [groupName]) VALUES (40, N'David', N'Young', N'512321', N'2A')
SET IDENTITY_INSERT [dbo].[Students] OFF
GO
/****** Object:  Index [test_index1]    Script Date: 4/30/2024 1:32:12 PM ******/
CREATE NONCLUSTERED INDEX [test_index1] ON [dbo].[Courses]
(
	[studentId] ASC
)
INCLUDE([courseName]) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Students__4849DA0166DC77C5]    Script Date: 4/30/2024 1:32:12 PM ******/
ALTER TABLE [dbo].[Students] ADD UNIQUE NONCLUSTERED 
(
	[phoneNumber] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [test1]    Script Date: 4/30/2024 1:32:12 PM ******/
CREATE NONCLUSTERED INDEX [test1] ON [dbo].[Students]
(
	[studentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Courses]  WITH CHECK ADD FOREIGN KEY([studentId])
REFERENCES [dbo].[Students] ([studentId])
GO
/****** Object:  StoredProcedure [dbo].[addStudent]    Script Date: 4/30/2024 1:32:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE Procedure [dbo].[addStudent]
(
	@firstName varchar(255),
	@lastName varchar(255),
	@phoneNumber varchar(55),
	@groupName varchar(5),
	@studentId int output
)
as
begin;

Set nocount on;

Insert into Students 
values (@firstName, @lastName, @phoneNumber, @groupName);

select @studentId = SCOPE_IDENTITY();

set nocount off;

end;
GO
/****** Object:  StoredProcedure [dbo].[selectCourses]    Script Date: 4/30/2024 1:32:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[selectCourses]
(
	@courseId int output
)
as
begin;

set nocount on;

select @courseId = IDENT_CURRENT('Courses');

select * from Courses;

set nocount off;

end;
GO
/****** Object:  StoredProcedure [dbo].[selectStudent]    Script Date: 4/30/2024 1:32:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[selectStudent]
(
	@studentId int output
)
as
begin;

set nocount on;

select @studentid = IDENT_CURRENT('Students');

select * from students;

set nocount off;

end;
GO
USE [master]
GO
ALTER DATABASE [University] SET  READ_WRITE 
GO
