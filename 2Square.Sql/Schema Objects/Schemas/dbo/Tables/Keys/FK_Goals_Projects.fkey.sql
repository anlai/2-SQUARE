ALTER TABLE [dbo].[Goals]
    ADD CONSTRAINT [FK_Goals_Projects] FOREIGN KEY ([ProjectId]) REFERENCES [dbo].[Projects] ([id]) ON DELETE NO ACTION ON UPDATE NO ACTION;

