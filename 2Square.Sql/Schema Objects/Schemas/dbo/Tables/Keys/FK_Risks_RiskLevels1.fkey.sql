ALTER TABLE [dbo].[Risks]
    ADD CONSTRAINT [FK_Risks_RiskLevels1] FOREIGN KEY ([Damage]) REFERENCES [dbo].[RiskLevels] ([id]) ON DELETE NO ACTION ON UPDATE NO ACTION;

