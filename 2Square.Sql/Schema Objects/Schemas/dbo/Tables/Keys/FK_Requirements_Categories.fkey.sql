ALTER TABLE [dbo].[Requirements]
    ADD CONSTRAINT [FK_Requirements_Categories] FOREIGN KEY ([CategoryId]) REFERENCES [dbo].[Categories] ([id]) ON DELETE NO ACTION ON UPDATE NO ACTION;

