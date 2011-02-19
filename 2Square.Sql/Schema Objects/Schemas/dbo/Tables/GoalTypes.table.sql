CREATE TABLE [dbo].[GoalTypes] (
    [id]           CHAR (1)     NOT NULL,
    [Name]         VARCHAR (50) NOT NULL,
    [IsActive]     BIT          NOT NULL,
    [SquareTypeId] INT          NULL
);

