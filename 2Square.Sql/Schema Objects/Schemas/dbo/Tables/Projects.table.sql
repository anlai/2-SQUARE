﻿CREATE TABLE [dbo].[Projects] (
    [id]                   INT           IDENTITY (1, 1) NOT NULL,
    [Name]                 VARCHAR (50)  NOT NULL,
    [Description]          VARCHAR (MAX) NULL,
    [DateCreated]          DATETIME      NOT NULL,
    [SecurityAssessmentId] INT           NULL,
    [PrivacyAssessmentId]  INT           NULL
);



