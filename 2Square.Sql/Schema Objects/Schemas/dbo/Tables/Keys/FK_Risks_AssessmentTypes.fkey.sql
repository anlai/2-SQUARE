ALTER TABLE [dbo].[Risks]
    ADD CONSTRAINT [FK_Risks_AssessmentTypes] FOREIGN KEY ([AssessmentTypeId]) REFERENCES [dbo].[AssessmentTypes] ([id]) ON DELETE NO ACTION ON UPDATE NO ACTION;

