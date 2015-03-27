CREATE TABLE [dbo].[Files] (
    [Id]        INT             IDENTITY (1, 1) NOT NULL,
    [Updated]   DATETIME        NULL,
    [Name]      NVARCHAR (MAX)  NOT NULL,
    [Extension] NVARCHAR (MAX)  NOT NULL,
    [CommitId]  INT             NOT NULL,
    [Data]      VARBINARY (MAX) NULL,
    [Version]   INT             NULL,
    [Status]    NVARCHAR (MAX)  NOT NULL,
    CONSTRAINT [PK_Files] PRIMARY KEY CLUSTERED ([Id] ASC)
);

