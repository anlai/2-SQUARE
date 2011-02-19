ALTER TABLE [dbo].[ProjectSteps]
    ADD CONSTRAINT [FK_ProjectSteps_Projects] FOREIGN KEY ([ProjectId]) REFERENCES [dbo].[Projects] ([id]) ON DELETE NO ACTION ON UPDATE NO ACTION;

