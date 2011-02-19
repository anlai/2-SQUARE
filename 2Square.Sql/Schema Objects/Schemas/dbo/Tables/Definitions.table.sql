CREATE TABLE [dbo].[Definitions] (
    [id]          INT           IDENTITY (1, 1) NOT NULL,
    [Description] VARCHAR (MAX) NOT NULL,
    [Source]      VARCHAR (200) NOT NULL,
    [IsActive]    BIT           NOT NULL,
    [TermId]      INT           NOT NULL
);

