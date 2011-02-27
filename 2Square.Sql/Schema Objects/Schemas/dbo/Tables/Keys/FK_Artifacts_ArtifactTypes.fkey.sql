ALTER TABLE [dbo].[Artifacts]
    ADD CONSTRAINT [FK_Artifacts_ArtifactTypes] FOREIGN KEY ([ArtifactTypeId]) REFERENCES [dbo].[ArtifactTypes] ([id]) ON DELETE NO ACTION ON UPDATE NO ACTION;

