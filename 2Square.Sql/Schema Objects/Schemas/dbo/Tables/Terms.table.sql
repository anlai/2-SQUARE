CREATE TABLE [dbo].[Terms] (
    [id]           INT           IDENTITY (1, 1) NOT NULL,
    [Name]         VARCHAR (100) NOT NULL,
    [SquareTypeId] INT           NOT NULL,
    [IsActive]     BIT           NOT NULL
);

