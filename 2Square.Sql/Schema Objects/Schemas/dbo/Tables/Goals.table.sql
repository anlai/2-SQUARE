CREATE TABLE [dbo].[Goals] (
    [id]           INT           IDENTITY (1, 1) NOT NULL,
    [Description]  VARCHAR (MAX) NOT NULL,
    [SquareTypeId] INT           NOT NULL,
    [GoalTypeId]   CHAR (1)      NOT NULL,
    [ProjectId]    INT           NOT NULL
);

