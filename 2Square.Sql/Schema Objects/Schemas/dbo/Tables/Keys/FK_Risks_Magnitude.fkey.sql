ALTER TABLE [dbo].[Risks]
    ADD CONSTRAINT [FK_Risks_Magnitude] FOREIGN KEY ([MagnitudeId]) REFERENCES [dbo].[RiskLevels] ([id]) ON DELETE NO ACTION ON UPDATE NO ACTION;

