ALTER TABLE [dbo].[RiskRecommendation]
    ADD CONSTRAINT [FK_RiskControls_Risks] FOREIGN KEY ([RiskId]) REFERENCES [dbo].[Risks] ([id]) ON DELETE NO ACTION ON UPDATE NO ACTION;

