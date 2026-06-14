USE [EstacionamentoDB]
GO

/****** Objeto:  Table [dbo].[Tickets]    Data do Script: 14/06/2026 18:24:19 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Tickets](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Placa] [varchar](10) NOT NULL,
	[Modelo] [varchar](50) NOT NULL,
	[Cor] [varchar](20) NOT NULL,
	[TipoVeiculo] [int] NOT NULL,
	[HorarioEntrada] [datetime] NOT NULL,
	[HorarioSaida] [datetime] NULL,
	[Vaga] [varchar](10) NOT NULL,
	[StatusTicket] [varchar](20) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


