CREATE TABLE [dbo].[Account] (
    [AccountId]      NVARCHAR (50) NOT NULL,
    [Amount]         DECIMAL (18)  NULL,
    [Points]         INT           NULL,
    [AccountOwnerId] INT           NULL,
    [AccountTypeId]  INT           NULL,
    CONSTRAINT [PK_Account] PRIMARY KEY CLUSTERED ([AccountId] ASC),
    CONSTRAINT [FK_Account_AccountOwner] FOREIGN KEY ([AccountOwnerId]) REFERENCES [dbo].[AccountOwner] ([AccountOwnerId]),
    CONSTRAINT [FK_Account_AccountType] FOREIGN KEY ([AccountTypeId]) REFERENCES [dbo].[AccountType] ([AccountTypeId])
);

