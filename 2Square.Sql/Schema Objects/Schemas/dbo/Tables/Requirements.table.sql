CREATE TABLE [dbo].[Requirements] (
    [id]            INT           IDENTITY (1, 1) NOT NULL,
    [Name]          VARCHAR (100) NOT NULL,
    [Requirement]   VARCHAR (MAX) NOT NULL,
    [RequirementId] VARCHAR (10)  NULL,
    [ProjectId]     INT           NOT NULL,
    [SquareTypeId]  INT           NOT NULL,
    [CategoryId]    INT           NULL,
    [Priority]      INT           NULL,
    [Essential]     BIT           NULL
);



