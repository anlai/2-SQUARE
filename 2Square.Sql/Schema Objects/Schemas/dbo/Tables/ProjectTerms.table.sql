CREATE TABLE [dbo].[ProjectTerms] (
    [id]           INT           IDENTITY (1, 1) NOT NULL,
    [Term]         VARCHAR (100) NOT NULL,
    [Definition]   VARCHAR (MAX) NOT NULL,
    [Source]       VARCHAR (200) NOT NULL,
    [ProjectId]    INT           NOT NULL,
    [SquareTypeId] INT           NOT NULL
);

