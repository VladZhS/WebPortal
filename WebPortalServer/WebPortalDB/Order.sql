CREATE TABLE [dbo].[Order]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1, 1), 
    [CreationDate] DATETIME2 NOT NULL, 
    [StatusId] INT NOT NULL, 
    [CustomerId] INT NOT NULL, 
    [Description] NVARCHAR(MAX) NULL,
    [Archived] BIT NOT NULL DEFAULT 0,

    CONSTRAINT FK_Order_StatusId FOREIGN KEY (StatusId) REFERENCES OrderStatus(Id),
    CONSTRAINT FK_Order_CustomerId FOREIGN KEY (CustomerId) REFERENCES Customer(Id)
)
