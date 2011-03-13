ALTER TABLE [dbo].[Risks]
    ADD CONSTRAINT [FK_Risks_Likelihood] FOREIGN KEY ([LikelihoodId]) REFERENCES [dbo].[RiskLevels] ([id]) ON DELETE NO ACTION ON UPDATE NO ACTION;

