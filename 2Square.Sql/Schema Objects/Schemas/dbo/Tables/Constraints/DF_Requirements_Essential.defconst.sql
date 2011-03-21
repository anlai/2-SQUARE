ALTER TABLE [dbo].[Requirements]
    ADD CONSTRAINT [DF_Requirements_Essential] DEFAULT ((0)) FOR [Essential];

