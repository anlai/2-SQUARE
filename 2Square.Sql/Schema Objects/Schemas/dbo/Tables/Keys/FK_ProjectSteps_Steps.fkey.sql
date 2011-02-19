ALTER TABLE [dbo].[ProjectSteps]
    ADD CONSTRAINT [FK_ProjectSteps_Steps] FOREIGN KEY ([StepId]) REFERENCES [dbo].[Steps] ([id]) ON DELETE NO ACTION ON UPDATE NO ACTION;

