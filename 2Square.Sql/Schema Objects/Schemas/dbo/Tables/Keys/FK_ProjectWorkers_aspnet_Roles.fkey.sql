ALTER TABLE [dbo].[ProjectWorkers]
    ADD CONSTRAINT [FK_ProjectWorkers_aspnet_Roles] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[aspnet_Roles] ([RoleId]) ON DELETE NO ACTION ON UPDATE NO ACTION;

