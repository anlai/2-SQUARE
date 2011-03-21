ALTER TABLE [dbo].[Categories]
    ADD CONSTRAINT [FK_Categories_Projects] FOREIGN KEY ([ProjectId]) REFERENCES [dbo].[Projects] ([id]) ON DELETE NO ACTION ON UPDATE NO ACTION;

