ALTER TABLE [dbo].[Projects]
    ADD CONSTRAINT [DF_Projects_DateCreated] DEFAULT (getdate()) FOR [DateCreated];

