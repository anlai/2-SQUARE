CREATE TABLE [dbo].[Risks] (
    [id]               INT           NOT NULL,
    [ProjectId]        INT           NOT NULL,
    [SsquareTypeId]    INT           NOT NULL,
    [AssessmentTypeId] INT           NOT NULL,
    [Name]             VARCHAR (100) NOT NULL,
    [Description]      VARCHAR (MAX) NULL,
    [Likelihood]       CHAR (1)      NULL,
    [ImpactId]         INT           NULL,
    [Damage]           CHAR (1)      NULL,
    [Magnitude]        CHAR (1)      NULL,
    [Cost]             INT           NULL,
    [RiskLevel]        CHAR (1)      NULL
);

