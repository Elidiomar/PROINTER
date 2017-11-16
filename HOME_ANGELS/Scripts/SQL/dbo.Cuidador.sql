CREATE TABLE [dbo].[Cuidador] (
    [Id] int IDENTITY(1,1) PRIMARY KEY NOT NULL,
    [Nome]     VARCHAR (100)    NOT NULL,
    [CPF]      VARCHAR (11)     NOT NULL,
    [Telefone] VARCHAR (14)     NOT NULL,
    [CodUser]  UNIQUEIDENTIFIER NOT NULL,
    [Endereco] VARCHAR (200)    NOT NULL,
    [Cidade]   VARCHAR (200)    NOT NULL,
    [Estado]   VARCHAR (200)    NOT NULL,
	[Email]       NVARCHAR (150)   NULL,
    CONSTRAINT [FK_Cuidador_ToTable] FOREIGN KEY ([CodUser]) REFERENCES [dbo].[Users] ([UserId])
);

