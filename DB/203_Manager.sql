USE QuantDevDB
GO

IF OBJECT_ID('PPL.Customer', 'U') IS NOT NULL 
  DROP TABLE PPL.Customer

IF OBJECT_ID('PPL.Manager', 'U') IS NOT NULL 
  DROP TABLE PPL.Manager

CREATE TABLE PPL.Manager (
	ID INT identity(1, 1) Primary key,
	UserID int not null references ppl.[User](ID),
	Position NVARCHAR NOT NULL
)


