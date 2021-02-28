USE master;
IF DB_ID('Logistics') IS NOT NULL
      DROP DATABASE Logistics;
CREATE DATABASE Logistics collate Ukrainian_CI_AS;

GO
USE Logistics;

---Роли
IF OBJECT_ID('Role', 'U') IS NOT NULL
   DROP TABLE Role;

CREATE TABLE dbo.Role
(
   RoleId int not null IDENTITY(1,1) primary key,
   RoleName NVARCHAR(20) NOT NULL
);
GO
SET IDENTITY_INSERT dbo.Role ON;
INSERT INTO dbo.Role(RoleId, RoleName) 

VALUES 
     (1, 'менеджер'),
     (2, 'логист'),
     (3, 'администратор');
	
SET IDENTITY_INSERT dbo.Role OFF;

--Статус заявки
IF OBJECT_ID('OrderStatus', 'U') IS NOT NULL
   DROP TABLE OrderStatus;

CREATE TABLE dbo.OrderStatus
(
   StatusId int not null IDENTITY(1,1) primary key,
   StatusName NVARCHAR(20) NOT NULL
);
GO

SET IDENTITY_INSERT dbo.OrderStatus ON;
INSERT INTO dbo.OrderStatus(StatusId, StatusName) 

VALUES 
     (1, 'в работе'),
     (2, 'новый'),
     (3, 'выполнен'),
     (4, 'отменен');
	
SET IDENTITY_INSERT dbo.OrderStatus OFF;

--Пользователи
IF OBJECT_ID('User', 'U') IS NOT NULL
   DROP TABLE [User];

CREATE TABLE dbo.[User]
(
   UserId int not null IDENTITY(1,1) primary key,
   UserLastName NVARCHAR(50) NOT NULL,
   UserFirstName NVARCHAR(50) NOT NULL,
   UserPatronymic NVARCHAR(50) NOT NULL,
   UserDrivingLecense NVARCHAR(20) NOT NULL,
   UserRoleId int foreign key REFERENCES dbo.Role(RoleId)
);
GO
SET IDENTITY_INSERT dbo.[User] ON;
INSERT INTO dbo.[User](UserId, UserLastName,UserFirstName,UserPatronymic, UserDrivingLecense, UserRoleId  ) 

VALUES 
     (1, 'Самойлов','Алексей','Иванович','АІВ 182900',2),
     (2, 'Дуров','Олег','Сергеевич','МКВ 138621',2);
SET IDENTITY_INSERT dbo.[User] OFF;

--Виды топлива
IF OBJECT_ID('Fuel', 'U') IS NOT NULL
   DROP TABLE Fuel;

CREATE TABLE dbo.Fuel
(
   FuelId int not null IDENTITY(1,1) primary key,
   FuelName NVARCHAR(20) NOT NULL
);
GO

SET IDENTITY_INSERT dbo.Fuel ON;
INSERT INTO dbo.Fuel(FuelId, FuelName) 

VALUES 
     (1, 'Бензин А-95'),
     (2, 'Бензин А-92'),
     (3, 'ДТ');
	
SET IDENTITY_INSERT dbo.Fuel OFF;

--Автомобили
IF OBJECT_ID('Car', 'U') IS NOT NULL
   DROP TABLE Car;

CREATE TABLE dbo.Car
(
   CarId int not null IDENTITY(1,1) primary key,
   CarName NVARCHAR(20) NOT NULL,
   CarNumber NVARCHAR(20) NOT NULL,
   FuelConsumption decimal NOT NULL,
   FuelId int foreign key REFERENCES dbo.Fuel(FuelId),
   DriverId int foreign key REFERENCES dbo.[User](UserId),
);
GO

SET IDENTITY_INSERT dbo.Car ON;
INSERT INTO dbo.Car(CarId, CarName, CarNumber,FuelConsumption,FuelId,DriverId )

VALUES 
     (1, 'Renault Trafic', 'АВ 1275 СІ', 8.1, 3,1),
     (2, 'Renault Trafic', 'АВ 2556 СІ', 8.1, 1,2)
    
	
SET IDENTITY_INSERT dbo.Fuel OFF;

--Заказы

IF OBJECT_ID('Order', 'U') IS NOT NULL
   DROP TABLE [Order];
CREATE TABLE dbo.[Order]
( 
   OrderId int not null IDENTITY(1,1) primary key,
   Date date NOT NULL,
   FromWhere NVARCHAR(50) NOT NULL,
   ToWhere NVARCHAR(50) NOT NULL,
   Note NVARCHAR(50) NOT NULL,
   ApplicationStatusId int foreign key REFERENCES dbo.OrderStatus(StatusId),
   UserId int foreign key REFERENCES dbo.[User](UserId),
);
GO
