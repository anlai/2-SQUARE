ALTER TABLE [dbo].[Projects]
    ADD CONSTRAINT [FK_Projects_SecurityAssessmentTypes] FOREIGN KEY ([SecurityAssessmentId]) REFERENCES [dbo].[AssessmentTypes] ([id]) ON DELETE NO ACTION ON UPDATE NO ACTION;

