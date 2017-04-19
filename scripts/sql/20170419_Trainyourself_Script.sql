USE [trainyourself]
GO
ALTER TABLE [dbo].[Score] DROP CONSTRAINT [FK_User_Id]
GO
ALTER TABLE [dbo].[Score] DROP CONSTRAINT [FK_Exercise_Id]
GO
/****** Object:  Table [dbo].[User]    Script Date: 19.04.2017 16:01:58 ******/
DROP TABLE [dbo].[User]
GO
/****** Object:  Table [dbo].[Score]    Script Date: 19.04.2017 16:01:58 ******/
DROP TABLE [dbo].[Score]
GO
/****** Object:  Table [dbo].[Exercise]    Script Date: 19.04.2017 16:01:58 ******/
DROP TABLE [dbo].[Exercise]
GO
/****** Object:  Table [dbo].[Exercise]    Script Date: 19.04.2017 16:01:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Exercise](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Exercise] [varchar](50) NULL,
 CONSTRAINT [PK_Exercise] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Score]    Script Date: 19.04.2017 16:01:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Score](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NULL,
	[Score] [varchar](50) NULL,
	[Date] [date] NULL,
	[Exercise_Id] [int] NULL,
 CONSTRAINT [PK_Score] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[User]    Script Date: 19.04.2017 16:01:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[User](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Lastname] [varchar](50) NULL,
	[Name] [varchar](50) NULL,
	[Email] [varchar](50) NULL,
	[Password] [nchar](10) NULL,
	[Weight] [float] NULL,
	[Height] [float] NULL,
	[RecordPushups] [int] NULL,
	[RecordSquats] [int] NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[Exercise] ON 

INSERT [dbo].[Exercise] ([Id], [Exercise]) VALUES (1, N'Pushups')
INSERT [dbo].[Exercise] ([Id], [Exercise]) VALUES (2, N'Squats')
SET IDENTITY_INSERT [dbo].[Exercise] OFF
ALTER TABLE [dbo].[Score]  WITH CHECK ADD  CONSTRAINT [FK_Exercise_Id] FOREIGN KEY([Exercise_Id])
REFERENCES [dbo].[Exercise] ([Id])
GO
ALTER TABLE [dbo].[Score] CHECK CONSTRAINT [FK_Exercise_Id]
GO
ALTER TABLE [dbo].[Score]  WITH CHECK ADD  CONSTRAINT [FK_User_Id] FOREIGN KEY([UserID])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[Score] CHECK CONSTRAINT [FK_User_Id]
GO
