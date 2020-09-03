USE QuantDevDB
GO

IF OBJECT_ID('PPL.Customer', 'U') IS NOT NULL 
  DROP TABLE PPL.Customer

IF OBJECT_ID('PPL.Manager', 'U') IS NOT NULL 
  DROP TABLE PPL.Manager

IF OBJECT_ID('PPL.[User]', 'U') IS NOT NULL 
  DROP TABLE  PPL.[User]


CREATE TABLE PPL.[User] (
	ID INT identity(1, 1) NOT NULL primary key,
	UserName NVARCHAR(200) UNIQUE NOT NULL,
	Email NVARCHAR(1000) NULL,
	Alias NVARCHAR(1000) NULL,
	FirstName NVARCHAR(1000) NULL,
	LastName NVARCHAR(1000) NULL
)
Go

CREATE TABLE PPL.Manager (
	ID INT identity(1, 1) Primary key,
	UserID int not null references ppl.[User](ID),
	Position NVARCHAR(100) NOT NULL
)
Go

CREATE TABLE PPL.Customer (
	ID INT identity(1, 1) Primary key,
	UserID int not null references ppl.[User](ID),
	[Level] NVARCHAR(100) NOT NULL,
	ManagerId INT NULL REFERENCES PPL.Manager(Id)
)
Go

