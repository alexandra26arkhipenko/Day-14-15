CREATE TABLE [dbo].[AccountOwner] (
    [AccountOwnerId] INT           NOT NULL,
    [FirstName]      NVARCHAR (50) NOT NULL,
    [LastName]       NVARCHAR (50) NOT NULL,
    [Email]          NVARCHAR (50) NOT NULL,
    CONSTRAINT [PK_AccountOwner] PRIMARY KEY CLUSTERED ([AccountOwnerId] ASC)
);

