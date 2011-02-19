ALTER TABLE [dbo].[ProjectWorkers]
    ADD CONSTRAINT [FK_ProjectWorkers_Projects] FOREIGN KEY ([ProjectId]) REFERENCES [dbo].[Projects] ([id]) ON DELETE NO ACTION ON UPDATE NO ACTION;

