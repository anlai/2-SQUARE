﻿ALTER TABLE [dbo].[PRETQuestions]
    ADD CONSTRAINT [FK_PRETQuestions_PRETQuestions] FOREIGN KEY ([ParentQuestion]) REFERENCES [dbo].[PRETQuestions] ([Id]) ON DELETE NO ACTION ON UPDATE NO ACTION;

