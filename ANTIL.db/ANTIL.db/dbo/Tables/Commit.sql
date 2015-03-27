CREATE TABLE [dbo].[Commit] (
    [Id]             INT            IDENTITY (1, 1) NOT NULL,
    [Name]           NVARCHAR (MAX) NOT NULL,
    [ProjectId]      INT            NOT NULL,
    [ParentCommitId] INT            NULL,
    [Comment] NVARCHAR(MAX) NULL, 
    CONSTRAINT [PK_Commit] PRIMARY KEY CLUSTERED ([Id] ASC)
);

