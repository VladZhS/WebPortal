﻿CREATE TABLE [dbo].[OrderStatus]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1, 1), 
    [Status] NVARCHAR(10) NOT NULL DEFAULT 'New'
)
