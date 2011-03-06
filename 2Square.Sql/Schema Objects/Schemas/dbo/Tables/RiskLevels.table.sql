CREATE TABLE [dbo].[RiskLevels] (
    [id]          CHAR (1)     NOT NULL,
    [Name]        VARCHAR (50) NOT NULL,
    [SLikelihood] DECIMAL (4)  NULL,
    [PLikelihood] INT          NULL,
    [Impact]      INT          NULL,
    [Damage]      INT          NULL
);

