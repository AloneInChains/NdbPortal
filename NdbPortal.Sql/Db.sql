CREATE DATABASE NDB
ON PRIMARY (
NAME = N'NDB',
FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\NDB.mdf',
SIZE = 10240 KB,
MAXSIZE = UNLIMITED,
FILEGROWTH = 1024 KB
)
LOG ON (
NAME = N'NDB_log',
FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\NDB_log.ldf',
SIZE = 5120 KB,
MAXSIZE = UNLIMITED,
FILEGROWTH = 10 %
)
GO

CREATE TABLE NDB.dbo.Company (
  Id uniqueidentifier NOT NULL,
  Name varchar(50) NOT NULL,
  Address varchar(500) NOT NULL,
  CONSTRAINT PK_Company_Id PRIMARY KEY CLUSTERED (Id)
)
ON [PRIMARY]
GO

CREATE TABLE NDB.dbo.NormativeDocumentConfidentialityLevel (
  Id uniqueidentifier NOT NULL,
  Name varchar(50) NOT NULL,
  Description varchar(500) NULL,
  CONSTRAINT PK_NormativeDocumentConfidentialityLevel_Id PRIMARY KEY CLUSTERED (Id)
)
ON [PRIMARY]
GO

CREATE TABLE NDB.dbo.Employee (
  Id uniqueidentifier NOT NULL,
  Name varchar(50) NOT NULL,
  Surname varchar(50) NOT NULL,
  CompanyId uniqueidentifier NOT NULL,
  Email varchar(50) NOT NULL,
  Password varchar(50) NOT NULL,
  ConfidentialityLevelId uniqueidentifier NULL,
  CONSTRAINT PK_Employee_Id PRIMARY KEY CLUSTERED (Id)
)
ON [PRIMARY]
GO

ALTER TABLE NDB.dbo.Employee
  ADD CONSTRAINT FK_Employee_ConfidentialityLevelId FOREIGN KEY (ConfidentialityLevelId) REFERENCES dbo.NormativeDocumentConfidentialityLevel (Id)
GO

ALTER TABLE NDB.dbo.Employee
  ADD CONSTRAINT FK_Employee_CompanyId FOREIGN KEY (CompanyId) REFERENCES dbo.Company (Id)
GO

CREATE TABLE NDB.dbo.NormativeDocumentType (
  Id uniqueidentifier NOT NULL,
  Name varchar(50) NOT NULL,
  Description varchar(500) NULL,
  CONSTRAINT PK_NormativeDocumentType_Id PRIMARY KEY CLUSTERED (Id)
)
ON [PRIMARY]
GO

CREATE TABLE NDB.dbo.NormativeDocument (
  Id uniqueidentifier NOT NULL,
  DocumentNumber varchar(50) NOT NULL,
  DocumentTypeId uniqueidentifier NULL,
  CreatedOn datetime2 NOT NULL CONSTRAINT DF_NormativeDocument_CreatedOn DEFAULT GETDATE(),
  CreatedById uniqueidentifier NOT NULL,
  Description varchar(2000) NOT NULL,
  CompanyId uniqueidentifier NOT NULL,
  MainDocumentId uniqueidentifier NULL,
  VersionNumber int NULL,
  ConfidentialityLevelId uniqueidentifier NULL,
  CONSTRAINT PK_NormativeDocument_Id PRIMARY KEY CLUSTERED (Id)
)
ON [PRIMARY]
GO

ALTER TABLE NDB.dbo.NormativeDocument
  ADD CONSTRAINT FK_NormativeDocument_DocumentTypeId FOREIGN KEY (DocumentTypeId) REFERENCES dbo.NormativeDocumentType (Id)
GO

ALTER TABLE NDB.dbo.NormativeDocument
  ADD CONSTRAINT FK_NormativeDocument_CreatedById FOREIGN KEY (CreatedById) REFERENCES dbo.Employee (Id)
GO

ALTER TABLE NDB.dbo.NormativeDocument
  ADD CONSTRAINT FK_NormativeDocument_CompanyId FOREIGN KEY (CompanyId) REFERENCES dbo.Company (Id)
GO

ALTER TABLE NDB.dbo.NormativeDocument
  ADD CONSTRAINT FK_NormativeDocument_MainDocumentId FOREIGN KEY (MainDocumentId) REFERENCES dbo.NormativeDocument (Id)
GO

ALTER TABLE NDB.dbo.NormativeDocument
  ADD CONSTRAINT FK_NormativeDocument_ConfidentialityLevelId FOREIGN KEY (ConfidentialityLevelId) REFERENCES dbo.NormativeDocumentConfidentialityLevel (Id)
GO

CREATE INDEX IDX_NormativeDocument_DocumentNumber
  ON NDB.dbo.NormativeDocument (DocumentNumber)
GO

CREATE INDEX IDX_NormativeDocument_CreatedOn
  ON NDB.dbo.NormativeDocument (CreatedOn)
GO

CREATE TABLE NDB.dbo.NormativeDocumentFile (
  Id uniqueidentifier NOT NULL,
  NormativeDocumentId uniqueidentifier NOT NULL,
  FileName varchar(250) NULL,
  CreatedOn datetime2 NOT NULL CONSTRAINT DF_NormativeDocumentFile_CreatedOn DEFAULT GETDATE(),
  CreatedById uniqueidentifier NOT NULL,
  Data varbinary(max) NOT NULL,
  CONSTRAINT PK_NormativeDocumentFile_Id PRIMARY KEY CLUSTERED (Id)
)
ON [PRIMARY]
GO

ALTER TABLE NDB.dbo.NormativeDocumentFile
  ADD CONSTRAINT FK_NormativeDocumentFile_CreatedById FOREIGN KEY (CreatedById) REFERENCES dbo.Employee (Id)
GO

CREATE TABLE NDB.dbo.NormativeDocumentVisa (
  Id uniqueidentifier NOT NULL,
  NormativeDocumentId uniqueidentifier NOT NULL,
  ApproverId uniqueidentifier NOT NULL,
  CreatedOn datetime2 NOT NULL CONSTRAINT DF_NormativeDocumentVisa_CreatedOn DEFAULT GETDATE(),
  Comment varchar(500) NULL,
  IsApproved bit NOT NULL,
  CONSTRAINT PK_NormativeDocumentVisa_Id PRIMARY KEY CLUSTERED (Id)
)
ON [PRIMARY]
GO

ALTER TABLE NDB.dbo.NormativeDocumentVisa
  ADD CONSTRAINT FK_NormativeDocumentVisa_NormativeDocumentId FOREIGN KEY (NormativeDocumentId) REFERENCES dbo.NormativeDocument (Id)
GO

ALTER TABLE NDB.dbo.NormativeDocumentVisa
  ADD CONSTRAINT FK_NormativeDocumentVisa_ApproverId FOREIGN KEY (ApproverId) REFERENCES dbo.Employee (Id)
GO

CREATE TABLE NDB.dbo.NormativeDocumentRelationType (
  Id uniqueidentifier NOT NULL,
  Name varchar(50) NOT NULL,
  ReverseName varchar(50) NOT NULL,
  CONSTRAINT PK_NormativeDocumentRelationType_Id PRIMARY KEY CLUSTERED (Id)
)
ON [PRIMARY]
GO

CREATE TABLE NDB.dbo.NormativeDocumentRelation (
  Id uniqueidentifier NOT NULL,
  RelationDocumentId uniqueidentifier NOT NULL,
  RelatedDocumentId uniqueidentifier NOT NULL,
  RelationTypeId uniqueidentifier NOT NULL,
  CONSTRAINT PK_NormativeDocumentRelation_Id PRIMARY KEY CLUSTERED (Id)
)
ON [PRIMARY]
GO

ALTER TABLE NDB.dbo.NormativeDocumentRelation
  ADD CONSTRAINT FK_NormativeDocumentRelation_RelationDocumentId FOREIGN KEY (RelationDocumentId) REFERENCES dbo.NormativeDocument (Id)
GO

ALTER TABLE NDB.dbo.NormativeDocumentRelation
  ADD CONSTRAINT FK_NormativeDocumentRelation_RelatedDocumentId FOREIGN KEY (RelatedDocumentId) REFERENCES dbo.NormativeDocument (Id)
GO

ALTER TABLE NDB.dbo.NormativeDocumentRelation
  ADD CONSTRAINT FK_NormativeDocumentRelation_RelationTypeId FOREIGN KEY (RelationTypeId) REFERENCES dbo.NormativeDocumentRelationType (Id)
GO

USE NDB
GO

CREATE VIEW dbo.VwNormativeDocumentRelation 
AS SELECT 
  RelationId = NormativeDocumentRelation.Id,
  DocumentA = RelationDocumentId,
  DocumentB = RelatedDocumentId,
  RelationName = NormativeDocumentRelationType.Name
FROM NormativeDocumentRelation
  INNER JOIN NormativeDocumentRelationType ON
    NormativeDocumentRelation.RelationTypeId = NormativeDocumentRelationType.Id
UNION ALL
SELECT 
  RelationId = NormativeDocumentRelation.Id,
  DocumentA = RelatedDocumentId,
  DocumentB = RelationDocumentId,
  RelationName = NormativeDocumentRelationType.ReverseName
FROM NormativeDocumentRelation
  INNER JOIN NormativeDocumentRelationType ON
    NormativeDocumentRelation.RelationTypeId = NormativeDocumentRelationType.Id
GO

INSERT Company(Id, Name, Address) VALUES ('038add12-04cc-4ddf-a6eb-5fdb405c8e42', 'Death Star', 'Somewhere in distant galaxy')
INSERT Company(Id, Name, Address) VALUES ('fa28eb7e-9a71-447b-9664-679898c5f2da', 'Rebelion army', 'Naboo')
INSERT Company(Id, Name, Address) VALUES ('23af38bb-24a5-4fe6-a728-1bd259fe91c8', 'Galactic empire', 'Not in a Milky Way')

INSERT INTO NormativeDocumentConfidentialityLevel(Id, Name, Description) VALUES ('1c94e800-30e4-4eb1-85bd-612017ec94a8', 'Top secret', 'No one should know about this!')
INSERT INTO NormativeDocumentConfidentialityLevel(Id, Name, Description) VALUES ('4ea68745-8df8-4ada-99d7-9b7f604496ed', 'Common usage', NULL)
INSERT INTO NormativeDocumentConfidentialityLevel(Id, Name, Description) VALUES ('2d51915e-32cf-457e-908c-a80b9cd09258', 'Public', NULL)

INSERT INTO Employee (Id, Name, Surname, CompanyId, Email, Password, ConfidentialityLevelId) 
VALUES (NEWID(), 'Darth', 'Vader', '038add12-04cc-4ddf-a6eb-5fdb405c8e42', 'darth.vader@deathstar.com', 'password123', '1c94e800-30e4-4eb1-85bd-612017ec94a8')

INSERT INTO Employee (Id, Name, Surname, CompanyId, Email, Password, ConfidentialityLevelId) 
VALUES (NEWID(), 'Darth', 'Sidious', '23af38bb-24a5-4fe6-a728-1bd259fe91c8', 'darth.sidious@galacticempire.com', 'password123', '1c94e800-30e4-4eb1-85bd-612017ec94a8')

INSERT INTO Employee (Id, Name, Surname, CompanyId, Email, Password, ConfidentialityLevelId) 
VALUES (NEWID(), 'Luke', 'Skywalker', 'fa28eb7e-9a71-447b-9664-679898c5f2da', 'luke.skywalker@rebellion.com', 'password123', '4ea68745-8df8-4ada-99d7-9b7f604496ed')

INSERT INTO Employee (Id, Name, Surname, CompanyId, Email, Password, ConfidentialityLevelId) 
VALUES (NEWID(), 'Han', 'Solo', 'fa28eb7e-9a71-447b-9664-679898c5f2da', 'han.solo@rebellion.com', 'password123', '4ea68745-8df8-4ada-99d7-9b7f604496ed')

INSERT INTO Employee (Id, Name, Surname, CompanyId, Email, Password, ConfidentialityLevelId) 
VALUES (NEWID(), 'Princess', 'Leya', 'fa28eb7e-9a71-447b-9664-679898c5f2da', 'princess@rebellion.com', 'password123', '2d51915e-32cf-457e-908c-a80b9cd09258')

INSERT INTO Employee (Id, Name, Surname, CompanyId, Email, Password, ConfidentialityLevelId) 
VALUES (NEWID(), 'Obe', 'Van Kenobe', 'fa28eb7e-9a71-447b-9664-679898c5f2da', 'obevan@rebellion.com', 'password123', '1c94e800-30e4-4eb1-85bd-612017ec94a8')

INSERT INTO Employee (Id, Name, Surname, CompanyId, Email, Password, ConfidentialityLevelId) 
VALUES (NEWID(), 'R2', 'D2', 'fa28eb7e-9a71-447b-9664-679898c5f2da', 'r2d2@rebellion.com', 'password123', '2d51915e-32cf-457e-908c-a80b9cd09258')


INSERT INTO NormativeDocumentType (Id, Name, Description) VALUES ('630379bb-ed17-48cd-bbb0-17bfcb02d071', 'Policy', 'Internal policies')
INSERT INTO NormativeDocumentType (Id, Name, Description) VALUES ('ba220bd8-0cc3-4e11-80a9-6d2fe38807e7', 'Rule', 'Rules that applies for company')
INSERT INTO NormativeDocumentType (Id, Name, Description) VALUES ('a7e06923-300f-4c23-99d4-91ac1180ca17', 'Plan', 'Company plans and strategies')
INSERT INTO NormativeDocumentType (Id, Name, Description) VALUES ('6c5fd7ea-3c7a-49d7-8e0f-b2a334acf1b8', 'Guideline', NULL)
INSERT INTO NormativeDocumentType (Id, Name, Description) VALUES ('9ccb7bf1-fb4f-4f8b-8dc7-6db8485e333d', 'Standard', 'Applies for company products')

INSERT NormativeDocumentRelationType(Id, Name, ReverseName) VALUES (NEWID(), 'Depends on', 'Relates to')
INSERT NormativeDocumentRelationType(Id, Name, ReverseName) VALUES (NEWID(), 'Mentions', 'Mentioned in')
INSERT NormativeDocumentRelationType(Id, Name, ReverseName) VALUES (NEWID(), 'Includes', 'Is part of')
INSERT NormativeDocumentRelationType(Id, Name, ReverseName) VALUES (NEWID(), 'Contains reference to', 'Is reference from')


--1. Normatīvu dokumentu saraksts, kurš pieder pie kompānijas, kura lietotājs strādā, ņemot vēra konfidencialitātes līmeni.

SELECT 
  nd.*
FROM NormativeDocument nd
  INNER JOIN Company c ON nd.CompanyId = c.Id
  INNER JOIN Employee e ON c.Id = e.CompanyId
WHERE e.Id = @userId AND nd.ConfidentialityLevelId = e.ConfidentialityLevelId

--2. Konkrēta normatīvā dokumenta versijas
SELECT * FROM NormativeDocument WHERE MainDocumentId = @documentId

--3. Normatīvu dokumentu saistības ar citiem dokumentiem
SELECT 
  relateddocument.DocumentNumber,
  relationdocument.DocumentNumber,
  vndr.RelationName 
FROM VwNormativeDocumentRelation vndr
  INNER JOIN NormativeDocument relateddocument ON
    vndr.DocumentA = relateddocument.Id
  INNER JOIN NormativeDocument relationdocument ON
    vndr.DocumentB = relationdocument.Id
WHERE DocumentA = @documentId

--4. Lietotāju saraksts, kuriem ir tiesības redzēt konkrētu dokumentu.
SELECT 
  e.*
FROM Employee e
  INNER JOIN NormativeDocument nd ON 
    e.ConfidentialityLevelId = nd.ConfidentialityLevelId
WHERE nd.Id = @documentId 

--5. Normatīvu dokumentu apstiprināšanas process
SELECT 
  ndv.*
FROM NormativeDocumentVisa ndv
WHERE ndv.NormativeDocumentId = @documentId

--6. Meklēšana pēc numura vai datuma.
SELECT 
  *
FROM NormativeDocument
WHERE DocumentNumber = @documentNumber OR DATEADD(dd, 0, DATEDIFF(dd, 0, CreatedOn)) = @date

