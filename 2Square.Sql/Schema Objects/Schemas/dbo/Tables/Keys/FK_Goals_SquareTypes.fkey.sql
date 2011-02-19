ALTER TABLE [dbo].[Goals]
    ADD CONSTRAINT [FK_Goals_SquareTypes] FOREIGN KEY ([SquareTypeId]) REFERENCES [dbo].[SquareTypes] ([id]) ON DELETE NO ACTION ON UPDATE NO ACTION;

