﻿ALTER TABLE [dbo].[Risks]
    ADD CONSTRAINT [FK_Risks_Impacts] FOREIGN KEY ([ImpactId]) REFERENCES [dbo].[Impacts] ([id]) ON DELETE NO ACTION ON UPDATE NO ACTION;

