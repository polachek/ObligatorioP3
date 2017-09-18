-- Consultas
Select * From Usuario
Select * From Proveedor
Select * From ProveedorVip
Select * From Servicio

-- Agregar columna a tabla 
--Alter table NombreTabla add NombreAtributo TipoDatoAtributo

-- Insertar Admin - Clave: Administrador
insert into Usuario Values('admin','750F9277BEF0489D9D309F267435F5874F4D173EA0E178F513D43EB86B7CA296DE51669E1BD167EC50F81D7AEF7DE10FF3F682028BE02D7815839DB33D6EB3D0', 1, 'guillermollana@gmail.com');



-- Seleccionar servicios con tipos de evento asociados a cada servicio
SELECT s.nombre, s.descripcion, s.imagen, t.nombre 
FROM Servicio AS s 
INNER JOIN TipoEventoYServicio AS e ON s.idServicio = e.idServicio
INNER JOIN TipoEvento AS t ON e.idTipoEvento = t.idTipoEvento

-- Crear la DataBase
CREATE DATABASE ObligatorioP3

-- Tabla Usuario
CREATE TABLE Usuario(
usuario VARCHAR(50),
password CHAR(128),
rol int,
email VARCHAR(50) Primary key,
)

-- Tabla Proveedor
CREATE TABLE Proveedor(
RUT varchar(12) PRIMARY KEY ,
nombreFantasia VARCHAR(50),
email VARCHAR(50) UNIQUE REFERENCES Usuario,
telefono VARCHAR(50),
fechaRegistro DATE,
esInactivo BIT,
tipo VARCHAR(10),
)

-- Tabla ProveedorVip
CREATE TABLE ProveedorVip(
rutProveedor varchar(12) PRIMARY KEY REFERENCES Proveedor,
porcentExtraAsign INT,
)

-- Tabla ProveedorComun
CREATE TABLE ProveedorComun(
rutProveedor varchar(12) PRIMARY KEY REFERENCES Proveedor,
)



-- Tabla Parametros
CREATE TABLE Parametros(
arancel DECIMAL(7,2),
porcentajeExtra INT,
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

CREATE TABLE ProveedorServicios(
	idServicio INT REFERENCES Servicio (idServicio),
	idProveedor VARCHAR(12) REFERENCES Proveedor,
	PRIMARY KEY (idServicio, idProveedor)
)

-- Insertar Servicios
INSERT INTO Servicio
VALUES ('Fotografía', 'Fotografía integral para fiestas y eventos', '')

INSERT INTO Servicio
VALUES ('Catering', 'Catering para eventos empresariales', '')

INSERT INTO Servicio
VALUES ('Wedding planner', 'Para que tu boda sea tal cual la imaginas', '')

--Insertar Tipos de eventos
INSERT INTO TipoEvento
VALUES ('Boda', 'Servicios para ceremonias religiosas, registro civil y fiestas')

INSERT INTO TipoEvento
VALUES ('Eventos empresariales', 'Servicios para que su empresa tenga los mejores eventos')

INSERT INTO TipoEvento
VALUES ('Fiesta de 15', 'Todo lo que necesitas para celebrar los quince años.')

--Insertar Tipos de eventos y servicios
-- Fotografia y Boda
INSERT INTO TipoEventoYServicio
VALUES (1,1)

--Fotografia y eventos empresariales
INSERT INTO TipoEventoYServicio
VALUES (1,2)

--Fotografia y Fiesta de 15
INSERT INTO TipoEventoYServicio
VALUES (1,3)

--Catering y eventos empresariales
INSERT INTO TipoEventoYServicio
VALUES (2,2)

--Wedding planner y boda
INSERT INTO TipoEventoYServicio
VALUES (3,1)