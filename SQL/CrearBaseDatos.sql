/*
  Ejecutar este script en SQL Server Management Studio (SSMS).
  La tabla se llama exactamente igual que la clase C#: EstudianteNota.
*/
IF DB_ID(N'RegistroNotasPOE') IS NULL
    CREATE DATABASE RegistroNotasPOE;
GO

USE RegistroNotasPOE;
GO

IF OBJECT_ID(N'dbo.EstudianteNota', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.EstudianteNota
    (
        Id INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_EstudianteNota PRIMARY KEY,
        Nombre NVARCHAR(80) NOT NULL,
        Apellido NVARCHAR(80) NOT NULL,
        Codigo NVARCHAR(20) NOT NULL,
        Laboratorio DECIMAL(4,2) NOT NULL,
        Parcial DECIMAL(4,2) NOT NULL,
        Asistencia DECIMAL(4,2) NOT NULL,
        NotaFinal DECIMAL(4,2) NOT NULL,
        CONSTRAINT UQ_EstudianteNota_Codigo UNIQUE (Codigo),
        CONSTRAINT CK_EstudianteNota_Laboratorio CHECK (Laboratorio >= 0 AND Laboratorio <= 10),
        CONSTRAINT CK_EstudianteNota_Parcial CHECK (Parcial >= 0 AND Parcial <= 10),
        CONSTRAINT CK_EstudianteNota_Asistencia CHECK (Asistencia >= 0 AND Asistencia <= 10),
        CONSTRAINT CK_EstudianteNota_NotaFinal CHECK (NotaFinal >= 0 AND NotaFinal <= 10)
    );
END;
GO

SELECT * FROM dbo.EstudianteNota ORDER BY Apellido, Nombre;
GO
