ALTER TABLE [dbo].[Risks]
    ADD CONSTRAINT [FK_Risks_RiskLevels3] FOREIGN KEY ([RiskLevel]) REFERENCES [dbo].[RiskLevels] ([id]) ON DELETE NO ACTION ON UPDATE NO ACTION;

