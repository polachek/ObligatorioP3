/****************************************************************************/
/****************************************************************************/
/****************************************************************************/
/****************************************************************************/
/*                      CREACIÓN DE BASE DE DATOS							*/
/****************************************************************************/
/****************************************************************************/
/****************************************************************************/
/****************************************************************************/

-- Crear la DataBase
CREATE DATABASE ObligatorioP3

/****************************************************************************/
/****************************************************************************/
/****************************************************************************/
/****************************************************************************/
/*							CREACIÓN DE TABLAS								*/
/****************************************************************************/
/****************************************************************************/
/****************************************************************************/
/****************************************************************************/

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

)

CREATE TABLE ProveedorServicios(
	idServicio INT REFERENCES Servicio (idServicio),
	rutProveedor VARCHAR(12) REFERENCES Proveedor,
	PRIMARY KEY (idServicio, rutProveedor),
	nombre VARCHAR(50),
	descripcion VARCHAR(150),
	imagen VARCHAR(2083),
)

-- Tabla TipoEvento
CREATE TABLE TipoEvento(
	idTipoEvento INT IDENTITY(1,1) PRIMARY KEY,
	nombre VARCHAR (50),
	descripcion VARCHAR(250)	
)

-- Tabla TipoEventoYServicio
CREATE TABLE TipoEventoYServicio(
	idServicio int REFERENCES Servicio (idServicio),
	idTipoEvento int REFERENCES TipoEvento (idTipoEvento),
	PRIMARY KEY (idServicio, idTipoEvento)
)

/****************************************************************************/
/****************************************************************************/
/****************************************************************************/
/****************************************************************************/
/*							INSERTAR DATOS									*/
/****************************************************************************/
/****************************************************************************/
/****************************************************************************/
/****************************************************************************/


-- Insertar Admin - Clave: Administrador
insert into Usuario Values('admin','750F9277BEF0489D9D309F267435F5874F4D173EA0E178F513D43EB86B7CA296DE51669E1BD167EC50F81D7AEF7DE10FF3F682028BE02D7815839DB33D6EB3D0', 1, 'guillermollana@gmail.com');


/****************************************************************************/
/****************************************************************************/
/****************************************************************************/
/****************************************************************************/
/*						MODIFICACIONES DE TABLAS							*/
/****************************************************************************/
/****************************************************************************/
/****************************************************************************/
/****************************************************************************/


-- Agregar columna a tabla 
--Alter table NombreTabla add NombreAtributo TipoDatoAtributo
--- Editar campo Tabla
/*UPDATE Servicio
SET imagen = '~/images/servicios/wedding-planner.png'
WHERE idServicio = 3;*/


/****************************************************************************/
/****************************************************************************/
/****************************************************************************/
/****************************************************************************/
/*								CONSULTAS									*/
/****************************************************************************/
/****************************************************************************/
/****************************************************************************/
/****************************************************************************/
/*
Select * From Usuario
Select * From Proveedor
Select * From ProveedorVip
Select * From Servicio

Select * From ProveedorComun
Select * From TipoEvento
Select * From ProveedorServicios
SELECT * From ProveedorVip WHERE rutProveedor = '111111111111'
SELECT * From ProveedorVip WHERE rutProveedor = '111111111111'

SELECT RUT, t.nombre, t.descripcion, t.imagen
FROM provServicios
INNER JOIN Servicio AS t ON t.idServicio = provServicios.idServicio
WHERE RUT = '111111111111'

-- Seleccionar servicios con tipos de evento asociados a cada servicio
SELECT t.nombre, t.descripcion, t.idTipoEvento
                                FROM Servicio AS s 
                                INNER JOIN TipoEventoYServicio AS e ON s.idServicio = e.idServicio
                                INNER JOIN TipoEvento AS t ON e.idTipoEvento = t.idTipoEvento
                                WHERE s.nombre = 'Fotografia'

SELECT s.IdServicio AS IdServicio, s.nombre AS Servicio, s.descripcion AS 'Descripcion del servicio', s.imagen as 'Foto', t.nombre as 'Tipo de evento'
                                FROM Servicio AS s 
                                INNER JOIN TipoEventoYServicio AS e ON s.idServicio = e.idServicio
                                INNER JOIN TipoEvento AS t ON e.idTipoEvento = t.idTipoEvento

SELECT s.nombre, s.descripcion, s.imagen, t.nombre 
FROM Servicio AS s 
INNER JOIN TipoEventoYServicio AS e ON s.idServicio = e.idServicio
INNER JOIN TipoEvento AS t ON e.idTipoEvento = t.idTipoEvento

SELECT *
FROM Parametros
*/



/****************************************************************************/
/****************************************************************************/
/****************************************************************************/
/****************************************************************************/
/*							DATOS DE PRUEBA 								*/
/****************************************************************************/
/****************************************************************************/
/****************************************************************************/
/****************************************************************************/

-- Proveedor Test 1
INSERT INTO Usuario 
VALUES('999999999991','750F9277BEF0489D9D309F267435F5874F4D173EA0E178F513D43EB86B7CA296DE51669E1BD167EC50F81D7AEF7DE10FF3F682028BE02D7815839DB33D6EB3D0', 2, 'provtest1@provtest.com');

INSERT INTO Proveedor 
VALUES ('999999999991', 'Proveedor Prueba Uno', 'provtest1@provtest.com', '099999991', '2017-09-21', 0, 'COMUN') SELECT CAST (SCOPE_IDENTITY() AS INT);

INSERT INTO ProveedorServicios VALUES(1, '999999999991', 'Fotografia', 'Proveedor Test 1 descripcion servicio Fotografia', '~/images/servicios-proveedor/Rut_999999999991__ServicioID_1.jpg');

INSERT INTO ProveedorComun VALUES ('999999999991');

-- Proveedor Test 2
INSERT INTO Usuario 
VALUES('999999999992','750F9277BEF0489D9D309F267435F5874F4D173EA0E178F513D43EB86B7CA296DE51669E1BD167EC50F81D7AEF7DE10FF3F682028BE02D7815839DB33D6EB3D0', 2, 'provtest2@provtest.com');

INSERT INTO Proveedor 
VALUES ('999999999992', 'Proveedor Prueba Dos', 'provtest2@provtest.com', '099999992', '2017-09-21', 0, 'VIP') SELECT CAST (SCOPE_IDENTITY() AS INT);

INSERT INTO ProveedorServicios VALUES(2, '999999999992', 'Catering', 'Proveedor Test 2 descripcion servicio Catering', '~/images/servicios-proveedor/Rut_999999999992__ServicioID_2.jpg');
INSERT INTO ProveedorServicios VALUES(3, '999999999992', 'Wedding planner', 'Proveedor Test 2 descripcion servicio Wedding planner', '~/images/servicios-proveedor/Rut_999999999992__ServicioID_3.jpg');

INSERT INTO ProveedorVip VALUES('999999999992',20)


/****************************************************************************/

-- Insertar Servicios
INSERT INTO Servicio
VALUES ('Fotografia', 'Fotografia integral para fiestas y eventos')

INSERT INTO Servicio
VALUES ('Catering', 'Catering para eventos empresariales')

INSERT INTO Servicio
VALUES ('Wedding planner', 'Para que tu boda sea tal cual la imaginas')

INSERT INTO Servicio
VALUES ('Salón de fiestas', 'Salones para todo tipo de eventos: bodas, fiestas de quince, empresariales.')

INSERT INTO Servicio
VALUES ('Catering de pizzas y chivitos.', 'El catering más rico.')

INSERT INTO Servicio
VALUES ('Mozos y asadores', 'Servicio de mozos y asadores. ')

INSERT INTO Servicio
VALUES ('Animadores', 'El servicio de animadores más divertido y original del condado.')

/****************************************************************************/

--Insertar Tipos de eventos
INSERT INTO TipoEvento
VALUES ('Boda', 'Servicios para ceremonias religiosas, registro civil y fiestas')

INSERT INTO TipoEvento
VALUES ('Eventos empresariales', 'Servicios para eventos empresariales')

INSERT INTO TipoEvento
VALUES ('Fiesta de 15', 'Servicios para fiestas de quince')

INSERT INTO TipoEvento
VALUES ('Cumpleaños infantiles', 'Servicios para cumpleaños infantiles')

INSERT INTO TipoEvento
VALUES ('Divorcios', 'Servicios para celebrar los momentos más felices de la vida.')

INSERT INTO TipoEvento
VALUES ('Alquiler de carpas y gazebos', 'Servicios para celebrar los momentos más felices de la vida.')


/****************************************************************************/

--Insertar Tipos de eventos y servicios
--Fotografia y Boda
INSERT INTO TipoEventoYServicio
VALUES (1,1)

--Fotografia y eventos empresariales
INSERT INTO TipoEventoYServicio
VALUES (1,2)

--Fotografia y Fiesta de 15
INSERT INTO TipoEventoYServicio
VALUES (1,3)

--Fotografia y Cumpleaños infantiles
INSERT INTO TipoEventoYServicio
VALUES (1,4)

--Fotografia y Divorcios
INSERT INTO TipoEventoYServicio
VALUES (1,5)

--Catering y eventos empresariales
INSERT INTO TipoEventoYServicio
VALUES (2,2)

--Catering y cumpleaños infantiles
INSERT INTO TipoEventoYServicio
VALUES (2,4)

--Catering y cumpleaños Fiesta de 15
INSERT INTO TipoEventoYServicio
VALUES (2,3)

--Wedding planner y boda
INSERT INTO TipoEventoYServicio
VALUES (3,1)

--Salón de fiesta y boda
INSERT INTO TipoEventoYServicio
VALUES (4,1)

--Salón de fiesta y eventos empresariales
INSERT INTO TipoEventoYServicio
VALUES (4,2)

--Salón de fiesta y cumpleaños infantiles
INSERT INTO TipoEventoYServicio
VALUES (4,4)

--Servicios de pizzas y chivitos y boda
INSERT INTO TipoEventoYServicio
VALUES (5,1)

--Servicios de pizzas y cumpleaños infantiles
INSERT INTO TipoEventoYServicio
VALUES (5,3)

--Servicios de pizzas y cumpleaños infantiles
INSERT INTO TipoEventoYServicio
VALUES (5,4)

--Servicios de mozos y boda
INSERT INTO TipoEventoYServicio
VALUES (6,1)

--Servicios de animadores y cumpleaños infantiles
INSERT INTO TipoEventoYServicio
VALUES (7,4)

/****************************************************************************/