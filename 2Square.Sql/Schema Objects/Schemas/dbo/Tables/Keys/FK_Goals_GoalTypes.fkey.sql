﻿ALTER TABLE [dbo].[Goals]
    ADD CONSTRAINT [FK_Goals_GoalTypes] FOREIGN KEY ([GoalTypeId]) REFERENCES [dbo].[GoalTypes] ([id]) ON DELETE NO ACTION ON UPDATE NO ACTION;

