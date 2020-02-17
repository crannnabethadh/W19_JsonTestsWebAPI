CREATE TABLE [dbo].[Players] (
    [Id]          NVARCHAR (128) NOT NULL,
    [FirstName]   NVARCHAR (50)  NULL,
    [LastName]    NVARCHAR (50)  NULL,
    [Email]       NVARCHAR (256) NOT NULL,
    [DateOfBirth] DATETIME2 (7)  NULL,
    [NickName]    NVARCHAR (50)  NULL,
    [City]        NVARCHAR (50)  NULL,
    [DateJoined]  DATETIME2 (7)  DEFAULT (getdate()) NULL,
    [BlobUri]     NVARCHAR (512) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Players_ToAspNetUsers] FOREIGN KEY ([Id]) REFERENCES [dbo].[AspNetUsers] ([Id])
);