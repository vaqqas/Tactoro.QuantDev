USE QuantDevDB
GO

IF OBJECT_ID('PPL.Customer', 'U') IS NOT NULL 
  DROP TABLE PPL.Customer

CREATE TABLE PPL.Customer (
	ID INT identity(1, 1) Primary key,
	UserID int not null references ppl.[User](ID),
	[Level] NVARCHAR(100) NOT NULL,
	ManagerId INT NULL REFERENCES PPL.Manager(Id)
)



