CREATE TABLE [dbo].[RiskLevels] (
    [id]          CHAR (1)       NOT NULL,
    [Name]        VARCHAR (50)   NOT NULL,
    [SLikelihood] DECIMAL (4, 1) NOT NULL,
    [PLikelihood] INT            NOT NULL,
    [Impact]      INT            NOT NULL,
    [Damage]      INT            NOT NULL,
    [Order]       INT            NOT NULL,
    [Color]       VARCHAR (10)   NOT NULL
);



