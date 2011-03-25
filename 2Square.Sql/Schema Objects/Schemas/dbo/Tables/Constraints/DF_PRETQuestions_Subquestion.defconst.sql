ALTER TABLE [dbo].[PRETQuestions]
    ADD CONSTRAINT [DF_PRETQuestions_Subquestion] DEFAULT ((0)) FOR [Subquestion];

