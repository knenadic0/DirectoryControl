CREATE TABLE [dbo].[Directory] (
    [Id]     INT          IDENTITY (1, 1) NOT NULL,
    [Name]   VARCHAR (50) NOT NULL,
    [Parent] INT          NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [AK_Directory_Name_Parent] UNIQUE NONCLUSTERED ([Name] ASC, [Parent] ASC),
    CONSTRAINT [FK_Directory_ToDirectory] FOREIGN KEY ([Parent]) REFERENCES [dbo].[Directory] ([Id]) ON DELETE CASCADE
);

