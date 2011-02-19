CREATE TABLE [dbo].[Steps] (
    [id]           INT           IDENTITY (1, 1) NOT NULL,
    [Order]        INT           NOT NULL,
    [Name]         VARCHAR (50)  NOT NULL,
    [Description]  VARCHAR (500) NULL,
    [SquareTypeId] INT           NOT NULL,
    [Controller]   VARCHAR (50)  NOT NULL,
    [Action]       VARCHAR (50)  NOT NULL
);

