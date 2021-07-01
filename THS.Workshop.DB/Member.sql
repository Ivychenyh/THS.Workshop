CREATE TABLE [dbo].[Member]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY NONCLUSTERED, 
    [SequenceId] BIGINT NOT NULL, 
    [Name] NVARCHAR(50) NOT NULL, 
    [Age] INT NULL, 
    [Email] NVARCHAR(50) NOT NULL, 
    [Remark] NVARCHAR(100) NULL
)

GO

CREATE CLUSTERED INDEX [CLIX_Member_sequenceId] ON [dbo].[Member] ([SequenceId])
