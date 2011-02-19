ALTER TABLE [dbo].[ProjectTerms]
    ADD CONSTRAINT [FK_ProjectTerms_SquareTypes] FOREIGN KEY ([SquareTypeId]) REFERENCES [dbo].[SquareTypes] ([id]) ON DELETE NO ACTION ON UPDATE NO ACTION;

