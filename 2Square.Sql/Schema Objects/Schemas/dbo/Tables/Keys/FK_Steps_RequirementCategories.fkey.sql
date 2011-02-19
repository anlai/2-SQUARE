ALTER TABLE [dbo].[Steps]
    ADD CONSTRAINT [FK_Steps_RequirementCategories] FOREIGN KEY ([SquareTypeId]) REFERENCES [dbo].[SquareTypes] ([id]) ON DELETE NO ACTION ON UPDATE NO ACTION;

