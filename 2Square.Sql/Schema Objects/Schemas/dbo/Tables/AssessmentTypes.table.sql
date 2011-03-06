CREATE TABLE [dbo].[AssessmentTypes] (
    [id]           INT          IDENTITY (1, 1) NOT NULL,
    [Name]         VARCHAR (50) NOT NULL,
    [Source]       VARCHAR (50) NULL,
    [SquareTypeId] INT          NOT NULL,
    [Controller]   VARCHAR (50) NOT NULL
);

