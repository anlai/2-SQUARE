ALTER TABLE [dbo].[Projects]
    ADD CONSTRAINT [FK_Projects_AssessmentTypes] FOREIGN KEY ([SecurityAssessmentId]) REFERENCES [dbo].[AssessmentTypes] ([id]) ON DELETE NO ACTION ON UPDATE NO ACTION;

