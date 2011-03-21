CREATE TABLE [dbo].[ElicitationTypes] (
    [id]           INT           IDENTITY (1, 1) NOT NULL,
    [Name]         VARCHAR (50)  NOT NULL,
    [SquareTypeId] INT           NOT NULL,
    [Controller]   VARCHAR (50)  NOT NULL,
    [Description]  VARCHAR (MAX) NULL,
    [Strengths]    VARCHAR (MAX) NULL,
    [Weaknesses]   VARCHAR (MAX) NULL
);

