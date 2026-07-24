CREATE TABLE Cliente (
	"Id_Cliente"	INTEGER NOT NULL UNIQUE,
	"Nombre"	TEXT NOT NULL,
	"Apellido"	TEXT NOT NULL,
	"Cedula"	TEXT NOT NULL,
	"Telefono"	TEXT NOT NULL,
	PRIMARY KEY("Id_Cliente" AUTOINCREMENT)
);

CREATE TABLE Vehiculo (
	"Id_Vehiculo"	INTEGER NOT NULL UNIQUE,
	"Marca"	TEXT NOT NULL,
	"Modelo"	TEXT NOT NULL,
	"Color"	TEXT NOT NULL,
	"Ano"	INTEGER NOT NULL,
	"PrecioPorDia"	REAL NOT NULL,
	"Disponible"	TEXT NOT NULL,
	PRIMARY KEY("Id_Vehiculo" AUTOINCREMENT)
);

CREATE TABLE Renta_Vehiculo (
	"Id_Renta_Vehiculo"	INTEGER NOT NULL UNIQUE,
	"Cliente_Id"	INTEGER NOT NULL,
	"Vehiculo_Id"	INTEGER NOT NULL,
	PRIMARY KEY("Id_Renta_Vehiculo" AUTOINCREMENT),
	FOREIGN KEY("Cliente_Id") REFERENCES "Cliente"("Id_Cliente"),
	FOREIGN KEY("Vehiculo_Id") REFERENCES "Vehiculo"("Id_Vehiculo")
);

INSERT INTO Cliente 
(Nombre,Apellido,Cedula,Telefono) 
VALUES 
('Carlos','Lazala','40212345678','8095551111');

INSERT INTO Vehiculo
(Marca,Modelo,Color,Ano,PrecioPorDia,Disponible)
VALUES
('Honda', 'Civic', 'Gris', 2015, 2500, 'Si');


INSERT INTO Renta_Vehiculo
(Id_Cliente, Id_)
VALUES
(1, 1);

SELECT * FROM Cliente;

SELECT * FROM Vehiculo;

SELECT * FROM Renta_Vehiculo;

SELECT
    rv.Id_Renta_Vehiculo,
    c.Nombre,
    c.Apellido,
    v.Marca,
    v.Modelo,
    v.PrecioPorDia
FROM Renta_Vehiculo rv
INNER JOIN Cliente c
    ON rv.Cliente_Id = c.Id_Cliente
INNER JOIN Vehiculo v
    ON rv.Vehiculo_Id = v.Id_Vehiculo;