ALTER TABLE [dbo].[Risks]
    ADD CONSTRAINT [FK_Risks_Projects] FOREIGN KEY ([ProjectId]) REFERENCES [dbo].[Projects] ([id]) ON DELETE NO ACTION ON UPDATE NO ACTION;

