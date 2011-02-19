ALTER TABLE [dbo].[Definitions]
    ADD CONSTRAINT [FK_Definitions_Terms] FOREIGN KEY ([TermId]) REFERENCES [dbo].[Terms] ([id]) ON DELETE NO ACTION ON UPDATE NO ACTION;

