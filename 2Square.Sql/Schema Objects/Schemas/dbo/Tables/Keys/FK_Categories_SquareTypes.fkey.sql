ALTER TABLE [dbo].[Categories]
    ADD CONSTRAINT [FK_Categories_SquareTypes] FOREIGN KEY ([SquareTypeId]) REFERENCES [dbo].[SquareTypes] ([id]) ON DELETE NO ACTION ON UPDATE NO ACTION;

