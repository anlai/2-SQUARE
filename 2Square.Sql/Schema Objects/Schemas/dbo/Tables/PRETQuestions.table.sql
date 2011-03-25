CREATE TABLE [dbo].[PRETQuestions] (
    [Id]             INT           IDENTITY (1, 1) NOT NULL,
    [Question]       VARCHAR (MAX) NOT NULL,
    [Order]          INT           NOT NULL,
    [Subquestion]    BIT           NOT NULL,
    [ParentQuestion] INT           NULL
);

