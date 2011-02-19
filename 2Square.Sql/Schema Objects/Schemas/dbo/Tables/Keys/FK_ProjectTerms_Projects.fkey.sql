ALTER TABLE [dbo].[ProjectTerms]
    ADD CONSTRAINT [FK_ProjectTerms_Projects] FOREIGN KEY ([ProjectId]) REFERENCES [dbo].[Projects] ([id]) ON DELETE NO ACTION ON UPDATE NO ACTION;

