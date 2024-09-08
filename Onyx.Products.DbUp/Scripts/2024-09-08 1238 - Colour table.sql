CREATE TABLE [dbo].[Colour] (
	[Id] SMALLINT IDENTITY NOT NULL
		CONSTRAINT PK_Colour PRIMARY KEY CLUSTERED ([Id] ASC),
	[Name] VARCHAR(50) NOT NULL
);

GO

SET IDENTITY_INSERT [dbo].[Colour] ON

--Seed with some basic values
INSERT INTO [dbo].[Colour] ([Id], [Name])
VALUES (1, 'Red')
	, (2, 'Yellow')
	, (3, 'Pink')
	, (4, 'Green')

SET IDENTITY_INSERT [dbo].[Colour] OFF
