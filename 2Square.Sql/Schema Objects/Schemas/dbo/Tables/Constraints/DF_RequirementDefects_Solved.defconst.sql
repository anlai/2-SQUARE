ALTER TABLE [dbo].[RequirementDefects]
    ADD CONSTRAINT [DF_RequirementDefects_Solved] DEFAULT ((0)) FOR [Solved];

