ALTER TABLE [dbo].[Terms]
    ADD CONSTRAINT [DF_Terms_IsActive] DEFAULT ((1)) FOR [IsActive];

