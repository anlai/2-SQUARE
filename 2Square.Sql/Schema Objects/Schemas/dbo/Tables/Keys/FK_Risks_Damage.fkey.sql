﻿ALTER TABLE [dbo].[Risks]
    ADD CONSTRAINT [FK_Risks_Damage] FOREIGN KEY ([DamageId]) REFERENCES [dbo].[RiskLevels] ([id]) ON DELETE NO ACTION ON UPDATE NO ACTION;

