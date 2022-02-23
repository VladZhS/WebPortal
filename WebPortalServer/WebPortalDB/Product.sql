CREATE TABLE [dbo].[Product]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1, 1), 
    [Name] NVARCHAR(10) NOT NULL, 
    [Quantity] INT NOT NULL DEFAULT 0, 
    [Price] MONEY NOT NULL, 
    [Description] NVARCHAR(MAX) NULL, 
    [CreationDate] DATETIME NOT NULL, 
    [CategoryId] INT NOT NULL FOREIGN KEY REFERENCES ProductCategory(Id),
    [SizeId] INT NOT NULL FOREIGN KEY REFERENCES ProductSize(Id)
)
