﻿CREATE TABLE [dbo].[ProductSize]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1, 1), 
    [Size] NVARCHAR(10) NOT NULL DEFAULT 'Medium'
)
