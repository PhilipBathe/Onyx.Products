﻿CREATE TABLE [dbo].[Product](
	[Guid] UNIQUEIDENTIFIER NOT NULL 
		CONSTRAINT DF_Product_Guid DEFAULT NEWID()
		CONSTRAINT PK_Product PRIMARY KEY CLUSTERED ([Guid] ASC),
	[Name] VARCHAR(50) NOT NULL,
	[Description] NVARCHAR(500) NULL,
	[Price] DECIMAL(12,2) NOT NULL,
	[ColourId] SMALLINT NOT NULL 
		CONSTRAINT FK_Product_Colour FOREIGN KEY ([ColourId]) REFERENCES [dbo].[Colour]([Id]),
	[CreatedBy] VARCHAR(50) NOT NULL
)
