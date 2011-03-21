ALTER TABLE [dbo].[Projects]
    ADD CONSTRAINT [FK_Projects_PrivacyElicitationTypes] FOREIGN KEY ([PrivacyElicitationId]) REFERENCES [dbo].[ElicitationTypes] ([id]) ON DELETE NO ACTION ON UPDATE NO ACTION;

