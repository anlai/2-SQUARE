ALTER TABLE [dbo].[GoalTypes]
    ADD CONSTRAINT [DF_GoalTypes_IsActive] DEFAULT ((1)) FOR [IsActive];

