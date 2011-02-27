ALTER TABLE [dbo].[Artifacts]
    ADD CONSTRAINT [FK_Artifacts_Projects] FOREIGN KEY ([ProjectId]) REFERENCES [dbo].[Projects] ([id]) ON DELETE NO ACTION ON UPDATE NO ACTION;

