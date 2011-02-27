ALTER TABLE [dbo].[ArtifactTypes]
    ADD CONSTRAINT [FK_ArtifactTypes_SquareTypes] FOREIGN KEY ([SquareTypeId]) REFERENCES [dbo].[SquareTypes] ([id]) ON DELETE NO ACTION ON UPDATE NO ACTION;

