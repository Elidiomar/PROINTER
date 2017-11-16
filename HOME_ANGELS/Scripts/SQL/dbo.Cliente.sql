CREATE TABLE [dbo].[Cliente] (
    [Id] int IDENTITY(1,1) PRIMARY KEY NOT NULL,
    [Nome]        VARCHAR (100)    NOT NULL,
    [CPF]         VARCHAR (11)     NOT NULL,
    [Telefone]    VARCHAR (14)     NOT NULL,
    [CodUser]     UNIQUEIDENTIFIER NOT NULL,
    [Endereco]    NVARCHAR (200)   NOT NULL,
    [Cidade]      NVARCHAR (200)   NOT NULL,
    [Estado]      NVARCHAR (200)   NOT NULL,
    [Email]       NVARCHAR (150)   NULL    
);

