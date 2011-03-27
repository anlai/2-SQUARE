ALTER TABLE [dbo].[RequirementDefects]
    ADD CONSTRAINT [FK_RequirementDefects_Requirements] FOREIGN KEY ([RequirementId]) REFERENCES [dbo].[Requirements] ([id]) ON DELETE NO ACTION ON UPDATE NO ACTION;

