CREATE TABLE Emisores(
nit varchar(14) NOT NULL  PRIMARY KEY,
nombre varchar(max),
codActividad VARCHAR(5),
departamento varchar(max),
municipio varchar(max),
complemento varchar(max),
correo varchar(max)
);

CREATE TABLE Receptores(
nit varchar(14) NOT NULL PRIMARY KEY,
nombre varchar(max),
codActividad VARCHAR(5),
departamento varchar(max),
municipio varchar(max),
complemento varchar(max),
correo varchar(max)
);

CREATE TABLE DTEs (
codigoGeneracion varchar(36) NOT NULL PRIMARY KEY,
nit_emisor varchar(14),
nit_receptor varchar(14),
version_ INT,
tipoDte varchar(2),
numeroControl varchar(31),
fecEmi DATE,
horaEmi TIME,
FOREIGN KEY (nit_emisor) REFERENCES Emisores(nit),
FOREIGN KEY (nit_receptor) REFERENCES Receptores(nit)
);

CREATE TABLE ResumenDTE (
    id_resumen INT NOT NULL PRIMARY KEY IDENTITY,
    codigoGeneracion VARCHAR(36),
    totalNoSuj DECIMAL(18, 2),
    totalExenta DECIMAL(18, 2),
    totalGravada DECIMAL(18, 2),
    subTotalVentas DECIMAL(18, 2),
    descuNoSuj DECIMAL(18, 2),
    descuExenta DECIMAL(18, 2),
    descuGravada DECIMAL(18, 2),
    porcentajeDescuento DECIMAL(18, 2),
    totalDescu DECIMAL(18, 2),
    ivaPerci1 DECIMAL(18, 2),
    ivaRete1 DECIMAL(18, 2),
    reteRenta DECIMAL(18, 2),
    montoTotalOperacion DECIMAL(18, 2),
    totalNoGravado DECIMAL(18, 2),
    totalPagar DECIMAL(18, 2),
    totalLetras VARCHAR(255),
    saldoFavor DECIMAL(18, 2),
    FOREIGN KEY (codigoGeneracion) REFERENCES DTEs(codigoGeneracion)
);