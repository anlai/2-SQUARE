ALTER TABLE [dbo].[Risks]
    ADD CONSTRAINT [FK_Risks_RiskLevel] FOREIGN KEY ([RiskLevelId]) REFERENCES [dbo].[RiskLevels] ([id]) ON DELETE NO ACTION ON UPDATE NO ACTION;

