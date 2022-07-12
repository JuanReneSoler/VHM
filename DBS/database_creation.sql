
create database VHM;
use VHM;

--tablas
--------------------------------------------------------------------------
create table Empleado
(
    Id int not null primary key identity(1, 1),
    Names varchar(50) not null,
    Surnames varchar(25) not null,
    UserId varchar(255) not null,
    Birthday datetime2 not null,
    DocId varchar(11) unique,
    IsDeleted bit not null default 0,
    FullName as Names + ' ' + Surnames
)

create table Providers
(
    Id int not null primary key identity(1, 1),
    Nombre varchar(50),
    RNC varchar(11) not null,
    Description text null,
    IsDeleted bit not null default 0
)

create table ProductsType
(
    Id int not null primary key identity(1, 1),
    Description varchar(25) not null,
    IsDeleted bit not null default 0
)

create table Products
(
    Id int not null primary key identity(1,1),
    Name varchar(50) not null,
    PriceUnit decimal not null,
    ProveedorId int not null FOREIGN KEY REFERENCES Providers(Id),
    TypeId int not null foreign key references ProductsType(Id),
    Description text null,
    IsDeleted bit not null default 0
)
------------------------------------------------------------------------------

--insercion de datos

--proveedores
insert into Providers
values (
    'Proveedor 1',
    '40222222222',
    null,
    0
)

insert into Providers
values (
    'Proveedor 2',
    '40222222223',
    null,
    0
)

insert into Providers
values (
    'Proveedor 3',
    '40222222224',
    null,
    0
)

--types
insert into ProductsType
values (
    'Tipo A',
    0
) 

insert into ProductsType
values (
    'Tipo B',
    0
) 
