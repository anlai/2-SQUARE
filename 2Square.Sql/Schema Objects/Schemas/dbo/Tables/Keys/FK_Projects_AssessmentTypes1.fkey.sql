ALTER TABLE [dbo].[Projects]
    ADD CONSTRAINT [FK_Projects_AssessmentTypes1] FOREIGN KEY ([PrivacyAssessmentId]) REFERENCES [dbo].[AssessmentTypes] ([id]) ON DELETE NO ACTION ON UPDATE NO ACTION;

