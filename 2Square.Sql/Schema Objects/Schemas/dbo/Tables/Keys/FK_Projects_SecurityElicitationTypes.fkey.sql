ALTER TABLE [dbo].[Projects]
    ADD CONSTRAINT [FK_Projects_SecurityElicitationTypes] FOREIGN KEY ([SecurityElicitationId]) REFERENCES [dbo].[ElicitationTypes] ([id]) ON DELETE NO ACTION ON UPDATE NO ACTION;

