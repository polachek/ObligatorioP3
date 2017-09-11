-- Consultas
Select * From Usuario
Select * From Proveedor
Select * From ProveedorVip

-- Agregar columna a tabla 
--Alter table NombreTabla add NombreAtributo TipoDatoAtributo

-- Insertar Admin - Clave: Administrador
insert into Usuario Values('admin','750F9277BEF0489D9D309F267435F5874F4D173EA0E178F513D43EB86B7CA296DE51669E1BD167EC50F81D7AEF7DE10FF3F682028BE02D7815839DB33D6EB3D0', 1);

-- Seleccionar servicios con tipos de evento asociados a cada servicio
SELECT s.nombre, s.descripcion, s.imagen, t.nombre 
FROM Servicio AS s 
INNER JOIN TipoEventoYServicio AS e ON s.idServicio = e.idServicio
INNER JOIN TipoEvento AS t ON e.idTipoEvento = t.idTipoEvento


-- Crear la DataBase
CREATE DATABASE ObligatorioP3

-- Tabla Proveedor
CREATE TABLE Proveedor(
idProveedor INT IDENTITY(1,1),
RUT varchar(12) PRIMARY KEY,
nombreFantasia VARCHAR(50),
email VARCHAR(50) UNIQUE,
telefono VARCHAR(50),
arancel DECIMAL(7,2),
fechaRegistro DATE,
esInactivo BIT,
esVip BIT,
)

-- Tabla ProveedorVip
CREATE TABLE ProveedorVip(
idProveedor varchar(12) PRIMARY KEY REFERENCES Proveedor,
porcentajeExtra INT,
)

-- Tabla Usuario
CREATE TABLE Usuario(
usuario VARCHAR(50),
password CHAR(128),
rol int,
)

-- Tabla Parametros
CREATE TABLE Parametros(
arancel DECIMAL(7,2),
)

-- Tabla Servicio
CREATE TABLE Servicio(
idServicio INT IDENTITY(1,1) PRIMARY KEY,
nombre VARCHAR(50),
descripcion VARCHAR(150),
imagen VARCHAR(2083),
)

-- Tabla TipoEvento
CREATE TABLE TipoEvento(
	idTipoEvento INT IDENTITY(1,1) PRIMARY KEY,
	nombre VARCHAR (50),
	descripción VARCHAR(250)	
)

-- Tabla TipoEventoYServicio
CREATE TABLE TipoEventoYServicio(
	idServicio int REFERENCES Servicio (idServicio),
	idTipoEvento int REFERENCES TipoEvento (idTipoEvento),
	PRIMARY KEY (idServicio, idTipoEvento)
)

-- Dato de prueba 
/*

*/
