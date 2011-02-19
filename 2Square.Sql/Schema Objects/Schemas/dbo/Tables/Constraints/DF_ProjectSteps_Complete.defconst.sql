ALTER TABLE [dbo].[ProjectSteps]
    ADD CONSTRAINT [DF_ProjectSteps_Complete] DEFAULT ((0)) FOR [Complete];

