USE [ANTIL]
GO

/****** Object:  Table [dbo].[Files]    Script Date: 21.02.2015 22:31:47 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Files](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Updated] [datetime] NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Path] [nvarchar](max) NOT NULL,
	[Extension] [nvarchar](max) NOT NULL,
	[CommitId] [int] NOT NULL,
	[Data] [varbinary](max) NULL,
	[Version] [int] NULL,
	[Status] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Files] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


