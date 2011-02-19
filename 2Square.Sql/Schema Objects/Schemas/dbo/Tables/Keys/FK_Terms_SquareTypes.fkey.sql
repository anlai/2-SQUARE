ALTER TABLE [dbo].[Terms]
    ADD CONSTRAINT [FK_Terms_SquareTypes] FOREIGN KEY ([SquareTypeId]) REFERENCES [dbo].[SquareTypes] ([id]) ON DELETE NO ACTION ON UPDATE NO ACTION;

