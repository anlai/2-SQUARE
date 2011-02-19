ALTER TABLE [dbo].[Definitions]
    ADD CONSTRAINT [DF_Definitions_IsActive] DEFAULT ((1)) FOR [IsActive];

