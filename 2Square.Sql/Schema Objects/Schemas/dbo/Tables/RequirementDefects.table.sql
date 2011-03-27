CREATE TABLE [dbo].[RequirementDefects] (
    [id]            INT           IDENTITY (1, 1) NOT NULL,
    [Description]   VARCHAR (MAX) NOT NULL,
    [RequirementId] INT           NOT NULL,
    [Solved]        BIT           NOT NULL
);

