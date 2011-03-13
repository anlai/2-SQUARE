CREATE TABLE [dbo].[RiskControls] (
    [id]          INT           IDENTITY (1, 1) NOT NULL,
    [RiskId]      INT           NOT NULL,
    [Controls]    VARCHAR (MAX) NOT NULL,
    [Impact]      VARCHAR (MAX) NULL,
    [Feasibility] VARCHAR (MAX) NULL
);

