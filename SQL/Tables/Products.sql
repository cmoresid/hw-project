CREATE TABLE [dbo].[Products](
	[abcid] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY DEFAULT NEWID(),
	[title] VARCHAR(100) NOT NULL,
	[description] VARCHAR(500),
	[vendor] VARCHAR(100) NOT NULL,
	[list_price] DECIMAL(14,2) NOT NULL,
	[cost] DECIMAL(14,2) NOT NULL,
	[status] VARCHAR(25) NOT NULL,
	[location] VARCHAR(100) NOT NULL,
	[date_created] DATE NOT NULL,
	[date_received] DATE
);