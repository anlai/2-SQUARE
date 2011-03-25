CREATE TABLE [dbo].[PRETRequirements] (
    [id]          INT           IDENTITY (1, 1) NOT NULL,
    [Name]        VARCHAR (100) NOT NULL,
    [Requirement] VARCHAR (MAX) NOT NULL,
    [LawId]       INT           NOT NULL,
    [Source]      VARCHAR (100) NOT NULL
);

