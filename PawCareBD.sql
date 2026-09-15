CREATE DATABASE PawCareDB;
GO

USE PawCareDB;
GO

CREATE TABLE Mascotas
(
    Id INT IDENTITY(1,1) PRIMARY KEY,
    NombreMascota VARCHAR(100) NOT NULL,
    NombreDueno VARCHAR(100) NOT NULL,
    Tipo VARCHAR(20) NOT NULL,
    Edad INT NOT NULL,
    Telefono VARCHAR(9) NOT NULL,
    Observaciones VARCHAR(500) NULL
);
GO

/* Procedimientos almacenados*/

CREATE PROCEDURE spListarMascotas
AS
BEGIN
    SELECT 
        Id,
        NombreMascota,
        NombreDueno,
        Tipo,
        Edad,
        Telefono,
        Observaciones
    FROM Mascotas;
END;
GO

CREATE PROCEDURE spInsertarMascota
    @NombreMascota VARCHAR(100),
    @NombreDueno VARCHAR(100),
    @Tipo VARCHAR(20),
    @Edad INT,
    @Telefono VARCHAR(9),
    @Observaciones VARCHAR(500)
AS
BEGIN
    INSERT INTO Mascotas
    (
        NombreMascota,
        NombreDueno,
        Tipo,
        Edad,
        Telefono,
        Observaciones
    )
    VALUES
    (
        @NombreMascota,
        @NombreDueno,
        @Tipo,
        @Edad,
        @Telefono,
        @Observaciones
    );
END;
GO

EXEC spListarMascotas;

EXEC spInsertarMascota
    'Firulais',
    'Carlos Perez',
    'Perro',
    5,
    '987654321',
    'Vacunación anual';