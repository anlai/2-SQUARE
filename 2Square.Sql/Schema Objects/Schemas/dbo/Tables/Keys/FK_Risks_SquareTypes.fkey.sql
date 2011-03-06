ALTER TABLE [dbo].[Risks]
    ADD CONSTRAINT [FK_Risks_SquareTypes] FOREIGN KEY ([SsquareTypeId]) REFERENCES [dbo].[SquareTypes] ([id]) ON DELETE NO ACTION ON UPDATE NO ACTION;

