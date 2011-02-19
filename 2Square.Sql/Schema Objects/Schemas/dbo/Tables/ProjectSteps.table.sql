CREATE TABLE [dbo].[ProjectSteps] (
    [Id]            INT      IDENTITY (1, 1) NOT NULL,
    [ProjectId]     INT      NOT NULL,
    [StepId]        INT      NOT NULL,
    [DateStarted]   DATETIME NULL,
    [DateCompleted] DATETIME NULL,
    [Complete]      BIT      NOT NULL
);

