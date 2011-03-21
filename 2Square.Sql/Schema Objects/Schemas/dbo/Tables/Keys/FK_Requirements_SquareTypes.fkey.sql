ALTER TABLE [dbo].[Requirements]
    ADD CONSTRAINT [FK_Requirements_SquareTypes] FOREIGN KEY ([SquareTypeId]) REFERENCES [dbo].[SquareTypes] ([id]) ON DELETE NO ACTION ON UPDATE NO ACTION;

