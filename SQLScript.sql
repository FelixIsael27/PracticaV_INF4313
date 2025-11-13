create database Actividad5LengProg3
use Actividad5LengProg3

CREATE TABLE Recintos (
Codigo NVARCHAR(100) NOT NULL PRIMARY KEY,
Nombre NVARCHAR(100) NOT NULL,
Direccion NVARCHAR(100) NOT NULL
)

CREATE TABLE Carreras (
Codigo NVARCHAR(100) NOT NULL PRIMARY KEY,
Nombre NVARCHAR(100) NOT NULL,
CantidadCreditos INT NOT NULL,
CantidadMaterias INT NOT NULL
)

CREATE TABLE Estudiantes (
Matricula NVARCHAR(20) NOT NULL,
NombreCompleto NVARCHAR(100) NOT NULL,
Carrera NVARCHAR(100) NOT NULL,
Recinto NVARCHAR(100) NOT NULL,
CorreoInstitucional NVARCHAR(100) NOT NULL,
Celular NVARCHAR(20) NULL,
Telefono NVARCHAR(20) NULL,
Direccion NVARCHAR(150) NULL,
FechaNacimiento DATE NOT NULL,
Genero NVARCHAR(30) NOT NULL,
Turno NVARCHAR(30) NOT NULL,
EstaBecado BIT NOT NULL DEFAULT(0),
PorcentajeBeca int NULL,

CONSTRAINT FK_Estudiantes_Carreras FOREIGN KEY (Carrera)
REFERENCES Carreras(Codigo)
ON DELETE CASCADE
ON UPDATE CASCADE,

CONSTRAINT FK_Estudiantes_Recintos FOREIGN KEY (Recinto)
REFERENCES Recintos(Codigo)
ON DELETE CASCADE
ON UPDATE CASCADE
)

INSERT INTO Recintos (Codigo, Nombre, Direccion)
VALUES ('R001', 'Recinto Central', 'Av. Principal 123'),
('R002', 'Recinto Norte', 'Calle 45, Zona Norte');

INSERT INTO Carreras (Codigo, Nombre, CantidadCreditos, CantidadMaterias)
VALUES ('C001', 'Ingeniería en Sistemas', 3, 3),
('C002', 'Administración de Empresas', 4, 5);

INSERT INTO Estudiantes (Matricula, NombreCompleto, Carrera, Recinto, CorreoInstitucional, Celular, Telefono, Direccion, FechaNacimiento, Genero, Turno, EstaBecado, PorcentajeBeca)
VALUES ('SD-2020-05892', 'Juan Gonzalez', 'C001', 'R001', 'juanperez@email.com', '8095551234', '8293044321', 'Av. Duarte 45', '2004-01-23', 'Masculino', 'Mañana', '0', '0'), 
('SD-2022-06043', 'María Jimenez', 'C002', 'R002', 'mariagomez@email.com', '8095556789', '8095439854', 'Calle 10 #15', '2005-05-19', 'Femenino', 'Noche', '1', '75');

select * from Recintos
select * from Carreras
select * from Estudiantes

UPDATE Recintos
SET Nombre = 'Recinto Santo Domingo', Direccion = 'Av. Isabela Aguiar'
WHERE Codigo = 'R001';

DELETE FROM Recintos WHERE Codigo = 'R001';