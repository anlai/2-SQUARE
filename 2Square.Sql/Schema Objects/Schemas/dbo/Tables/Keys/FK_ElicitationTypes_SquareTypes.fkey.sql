ALTER TABLE [dbo].[ElicitationTypes]
    ADD CONSTRAINT [FK_ElicitationTypes_SquareTypes] FOREIGN KEY ([SquareTypeId]) REFERENCES [dbo].[SquareTypes] ([id]) ON DELETE NO ACTION ON UPDATE NO ACTION;

