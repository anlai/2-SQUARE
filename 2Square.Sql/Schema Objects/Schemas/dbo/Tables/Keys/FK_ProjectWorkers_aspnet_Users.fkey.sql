ALTER TABLE [dbo].[ProjectWorkers]
    ADD CONSTRAINT [FK_ProjectWorkers_aspnet_Users] FOREIGN KEY ([UserId]) REFERENCES [dbo].[aspnet_Users] ([UserId]) ON DELETE NO ACTION ON UPDATE NO ACTION;

