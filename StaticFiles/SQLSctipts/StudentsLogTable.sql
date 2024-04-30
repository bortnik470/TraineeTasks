USE [University]
GO

/****** Object:  Table [dbo].[StudentLog]    Script Date: 4/30/2024 1:35:40 PM ******/
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