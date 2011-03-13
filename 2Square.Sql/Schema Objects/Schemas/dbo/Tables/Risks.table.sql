CREATE TABLE [dbo].[Risks] (
    [id]               INT           IDENTITY (1, 1) NOT NULL,
    [ProjectId]        INT           NOT NULL,
    [SsquareTypeId]    INT           NOT NULL,
    [AssessmentTypeId] INT           NOT NULL,
    [Name]             VARCHAR (100) NOT NULL,
    [Description]      VARCHAR (MAX) NULL,
    [LikelihoodId]     CHAR (1)      NULL,
    [ImpactId]         INT           NULL,
    [DamageId]         CHAR (1)      NULL,
    [MagnitudeId]      CHAR (1)      NULL,
    [Cost]             INT           NULL,
    [RiskLevelId]      CHAR (1)      NULL,
    [Source]           VARCHAR (MAX) NULL,
    [Vulnerability]    VARCHAR (MAX) NULL,
    [Action]           VARCHAR (MAX) NULL
);



