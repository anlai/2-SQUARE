ALTER TABLE [dbo].[GoalTypes]
    ADD CONSTRAINT [FK_GoalTypes_SquareTypes] FOREIGN KEY ([SquareTypeId]) REFERENCES [dbo].[SquareTypes] ([id]) ON DELETE NO ACTION ON UPDATE NO ACTION;

