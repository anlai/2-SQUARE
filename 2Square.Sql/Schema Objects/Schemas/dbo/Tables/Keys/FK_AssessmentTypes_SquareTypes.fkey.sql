ALTER TABLE [dbo].[AssessmentTypes]
    ADD CONSTRAINT [FK_AssessmentTypes_SquareTypes] FOREIGN KEY ([SquareTypeId]) REFERENCES [dbo].[SquareTypes] ([id]) ON DELETE NO ACTION ON UPDATE NO ACTION;

