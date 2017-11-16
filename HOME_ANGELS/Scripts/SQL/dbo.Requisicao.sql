CREATE TABLE [dbo].[Requisicao]
(
[Id] int IDENTITY(1,1) PRIMARY KEY NOT NULL,
[Nome]     VARCHAR (100)    NOT NULL,
[Telefone] VARCHAR (14)     NOT NULL,	
    [CodUser]  UNIQUEIDENTIFIER NOT NULL,    
    [Cidade]   VARCHAR (200)    NOT NULL,
    [Estado]   VARCHAR (200)    NOT NULL,
	[Email]       NVARCHAR (150)  NOT NULL,    
	[DataInicial] DATETIME         NOT NULL,
    [DataFinal]   DATETIME         NOT NULL,
    [Status]      VARCHAR (53)       NOT NULL,
    [NameCuidador] VARCHAR (200)    NOT NULL,
	CONSTRAINT [FK_User_ToTable] FOREIGN KEY ([CodUser]) REFERENCES [dbo].[Users] ([UserId])
)
