ALTER TABLE [dbo].[Risks]
    ADD CONSTRAINT [FK_Risks_RiskLevels2] FOREIGN KEY ([Magnitude]) REFERENCES [dbo].[RiskLevels] ([id]) ON DELETE NO ACTION ON UPDATE NO ACTION;

