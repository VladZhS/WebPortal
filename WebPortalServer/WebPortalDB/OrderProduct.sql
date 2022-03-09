﻿CREATE TABLE [dbo].[OrderProduct]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1, 1), 
    [OderId] INT NOT NULL FOREIGN KEY REFERENCES [Order](Id), 
    [ProductId] INT NOT NULL FOREIGN KEY REFERENCES Product(Id), 
    [Quantity] INT NOT NULL DEFAULT 1
)