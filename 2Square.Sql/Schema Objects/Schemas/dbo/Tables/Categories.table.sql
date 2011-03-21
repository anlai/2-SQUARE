CREATE TABLE [dbo].[Categories] (
    [id]        INT           IDENTITY (1, 1) NOT NULL,
    [Name]      VARCHAR (100) NOT NULL,
    [ProjectId] INT           NOT NULL,
    [Order]     INT           NOT NULL
);

