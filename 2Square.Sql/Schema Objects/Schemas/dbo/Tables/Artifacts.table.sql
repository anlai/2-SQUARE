CREATE TABLE [dbo].[Artifacts] (
    [id]             INT             IDENTITY (1, 1) NOT NULL,
    [Name]           VARCHAR (100)   NOT NULL,
    [Description]    VARCHAR (MAX)   NULL,
    [Data]           VARBINARY (MAX) NULL,
    [ArtifactTypeId] INT             NOT NULL,
    [ProjectId]      INT             NOT NULL
);

