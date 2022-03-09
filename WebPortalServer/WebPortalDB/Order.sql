﻿CREATE TABLE [dbo].[Order]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1, 1), 
    [Date] DATE NOT NULL, 
    [StatusId] INT NOT NULL FOREIGN KEY REFERENCES OrderStatus(Id), 
    [CustomerId] INT NOT NULL FOREIGN KEY REFERENCES [Customer](Id), 
    [Description] NVARCHAR(MAX) NULL,
    [Archived] BIT NOT NULL DEFAULT 0 
)
