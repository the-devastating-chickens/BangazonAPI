
USE MASTER
GO

IF NOT EXISTS (
    SELECT [name]
    FROM sys.databases
    WHERE [name] = N'BangazonAPI'
)
CREATE DATABASE BangazonAPI
GO

USE BangazonAPI
GO


CREATE TABLE Department (
	Id INTEGER NOT NULL PRIMARY KEY IDENTITY,
	[Name] VARCHAR(55) NOT NULL,
	Budget 	INTEGER NOT NULL
);

CREATE TABLE Employee (
	Id INTEGER NOT NULL PRIMARY KEY IDENTITY,
	FirstName VARCHAR(55) NOT NULL,
	LastName VARCHAR(55) NOT NULL,
	DepartmentId INTEGER NOT NULL,
	IsSuperVisor BIT NOT NULL DEFAULT(0),
    CONSTRAINT FK_EmployeeDepartment FOREIGN KEY(DepartmentId) REFERENCES Department(Id)
);

CREATE TABLE Computer (
	Id INTEGER NOT NULL PRIMARY KEY IDENTITY,
	PurchaseDate DATETIME NOT NULL,
	DecomissionDate DATETIME,
	Make VARCHAR(55) NOT NULL,
	Manufacturer VARCHAR(55) NOT NULL
);

CREATE TABLE ComputerEmployee (
	Id INTEGER NOT NULL PRIMARY KEY IDENTITY,
	EmployeeId INTEGER NOT NULL,
	ComputerId INTEGER NOT NULL,
	AssignDate DATETIME NOT NULL,
	UnassignDate DATETIME,
    CONSTRAINT FK_ComputerEmployee_Employee FOREIGN KEY(EmployeeId) REFERENCES Employee(Id),
    CONSTRAINT FK_ComputerEmployee_Computer FOREIGN KEY(ComputerId) REFERENCES Computer(Id)
);


CREATE TABLE TrainingProgram (
	Id INTEGER NOT NULL PRIMARY KEY IDENTITY,
	[Name] VARCHAR(255) NOT NULL,
	StartDate DATETIME NOT NULL,
	EndDate DATETIME NOT NULL,
	MaxAttendees INTEGER NOT NULL
);

CREATE TABLE EmployeeTraining (
	Id INTEGER NOT NULL PRIMARY KEY IDENTITY,
	EmployeeId INTEGER NOT NULL,
	TrainingProgramId INTEGER NOT NULL,
    CONSTRAINT FK_EmployeeTraining_Employee FOREIGN KEY(EmployeeId) REFERENCES Employee(Id),
    CONSTRAINT FK_EmployeeTraining_Training FOREIGN KEY(TrainingProgramId) REFERENCES TrainingProgram(Id)
);

CREATE TABLE ProductType (
	Id INTEGER NOT NULL PRIMARY KEY IDENTITY,
	[Name] VARCHAR(55) NOT NULL
);

CREATE TABLE Customer (
	Id INTEGER NOT NULL PRIMARY KEY IDENTITY,
	FirstName VARCHAR(55) NOT NULL,
	LastName VARCHAR(55) NOT NULL
);

CREATE TABLE Product (
	Id INTEGER NOT NULL PRIMARY KEY IDENTITY,
	ProductTypeId INTEGER NOT NULL,
	CustomerId INTEGER NOT NULL,
	Price MONEY NOT NULL,
	Title VARCHAR(255) NOT NULL,
	[Description] VARCHAR(255) NOT NULL,
	Quantity INTEGER NOT NULL,
    CONSTRAINT FK_Product_ProductType FOREIGN KEY(ProductTypeId) REFERENCES ProductType(Id),
    CONSTRAINT FK_Product_Customer FOREIGN KEY(CustomerId) REFERENCES Customer(Id)
);


CREATE TABLE PaymentType (
	Id INTEGER NOT NULL PRIMARY KEY IDENTITY,
	AcctNumber INTEGER NOT NULL,
	[Name] VARCHAR(55) NOT NULL,
	CustomerId INTEGER NOT NULL,
    CONSTRAINT FK_PaymentType_Customer FOREIGN KEY(CustomerId) REFERENCES Customer(Id)
);

CREATE TABLE [Order] (
	Id INTEGER NOT NULL PRIMARY KEY IDENTITY,
	CustomerId INTEGER NOT NULL,
	PaymentTypeId INTEGER,
    CONSTRAINT FK_Order_Customer FOREIGN KEY(CustomerId) REFERENCES Customer(Id),
    CONSTRAINT FK_Order_Payment FOREIGN KEY(PaymentTypeId) REFERENCES PaymentType(Id)
);

CREATE TABLE OrderProduct (
	Id INTEGER NOT NULL PRIMARY KEY IDENTITY,
	OrderId INTEGER NOT NULL,
	ProductId INTEGER NOT NULL,
    CONSTRAINT FK_OrderProduct_Product FOREIGN KEY(ProductId) REFERENCES Product(Id),
    CONSTRAINT FK_OrderProduct_Order FOREIGN KEY(OrderId) REFERENCES [Order](Id)
);

SELECT * FROM Computer;

INSERT INTO Customer (FirstName, LastName) VALUES ('Jonathan', 'Schaffer')
INSERT INTO Customer (FirstName, LastName) VALUES ('Meag', 'Mueller')
INSERT INTO Customer (FirstName, LastName) VALUES ('Michael', 'Yankura')
INSERT INTO Customer (FirstName, LastName) VALUES ('Selam', 'Gebrekidan')
INSERT INTO Customer (FirstName, LastName) VALUES ('Jameka', 'Echols')
SELECT * FROM Customer

INSERT INTO PaymentType (AcctNumber, [Name], CustomerId) VALUES (012, 'Visa', 1)
INSERT INTO PaymentType (AcctNumber, [Name], CustomerId) VALUES (029, 'MasterCrad', 2)
INSERT INTO PaymentType (AcctNumber, [Name], CustomerId) VALUES (0783, 'Visa', 3)
INSERT INTO PaymentType (AcctNumber, [Name], CustomerId) VALUES (0262, 'American Express', 4)
INSERT INTO PaymentType (AcctNumber, [Name], CustomerId) VALUES (182, 'Discover', 5)
INSERT INTO PaymentType (AcctNumber, [Name], CustomerId) VALUES (382, 'American Express', 1)
INSERT INTO PaymentType (AcctNumber, [Name], CustomerId) VALUES (685, 'MasterCard', 1)
INSERT INTO PaymentType (AcctNumber, [Name], CustomerId) VALUES (986, 'Visa', 2)
SELECT * FROM PaymentType

INSERT INTO ProductType (Name) VALUES ('Book')
INSERT INTO ProductType (Name) VALUE ('DVD')

SELECT * FROM ProductType;

-- INSERT INTO Product (Title, [Description], Price, Quantity, ProductTypeId, CustomerId) VALUES ('Lord of the Rings', 'A book about a ring and an epic journey to a volcano.', 10, 1, )
-- INSERT INTO Product (Title, [Description], Price, Quantity, ProductTypeId, CustomerId) VALUES ('Jonathan', 'Schaffer')
-- INSERT INTO Product (Title, [Description], Price, Quantity, ProductTypeId, CustomerId) VALUES ('Jonathan', 'Schaffer')
-- INSERT INTO Product (Title, [Description], Price, Quantity, ProductTypeId, CustomerId) VALUES ('Jonathan', 'Schaffer')
-- INSERT INTO Product (Title, [Description], Price, Quantity, ProductTypeId, CustomerId) VALUES ('Jonathan', 'Schaffer')
SELECT * FROM Product
 
-- SELECT p.Id, p.[Name], p.AcctNumber,p.CustomerId, c.FirstName, c.LastName 
-- FROM PaymentType p
-- JOIN Customer c ON p.CustomerId=c.Id

-- INSERT INTO Customer (FirstName, LastName) VALUES ('Jonathan', 'Schaffer')
-- INSERT INTO Customer (FirstName, LastName) VALUES ('Meag', 'Mueller')
-- INSERT INTO Customer (FirstName, LastName) VALUES ('Michael', 'Yankura')
-- INSERT INTO Customer (FirstName, LastName) VALUES ('Selam', 'Gebrekidan')
-- INSERT INTO Customer (FirstName, LastName) VALUES ('Jameka', 'Echols')
-- SELECT * FROM Customer

-- INSERT INTO PaymentType (AcctNumber, [Name], CustomerId) VALUES (34542, 'Visa', 1)
-- INSERT INTO PaymentType (AcctNumber, [Name], CustomerId) VALUES (29, 'MasterCrad', 2)
-- INSERT INTO PaymentType (AcctNumber, [Name], CustomerId) VALUES (783, 'Visa', 3)
-- INSERT INTO PaymentType (AcctNumber, [Name], CustomerId) VALUES (262, 'American Express', 4)
-- INSERT INTO PaymentType (AcctNumber, [Name], CustomerId) VALUES (182, 'Discover', 5)
-- INSERT INTO PaymentType (AcctNumber, [Name], CustomerId) VALUES (382, 'American Express', 1)
-- INSERT INTO PaymentType (AcctNumber, [Name], CustomerId) VALUES (685, 'MasterCard', 1)
-- INSERT INTO PaymentType (AcctNumber, [Name], CustomerId) VALUES (986, 'Visa', 2)

-- SELECT * FROM PaymentType

