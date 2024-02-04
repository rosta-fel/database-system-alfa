CREATE DATABASE database_system_alfa_db;
USE database_system_alfa_db;

CREATE TABLE Address (
    ID INT NOT NULL PRIMARY KEY,
    Street VARCHAR(85) NOT NULL,
    City VARCHAR(55) NOT NULL,
    PostalCode INT NOT NULL,
    CHECK (PostalCode >= 10000 AND PostalCode <= 99999)
);

CREATE TABLE Publisher (
    ID INT NOT NULL PRIMARY KEY,
    PublisherName VARCHAR(125) NOT NULL UNIQUE,
    AdressID INT NOT NULL,
    FOREIGN KEY (AdressID) REFERENCES Address(ID)
);

CREATE TABLE DocumentType (
    ID INT NOT NULL PRIMARY KEY,
    TypeName VARCHAR(50) NOT NULL UNIQUE,
    TypeDescription VARCHAR(85) NOT NULL
);

CREATE TABLE Person (
    ID INT NOT NULL PRIMARY KEY,
    AdressID INT NOT NULL,
    FirstName VARCHAR(45) NOT NULL,
    LastName VARCHAR(45) NOT NULL,
    Birthday DATE NOT NULL,
    Sex VARCHAR(5) NOT NULL CHECK (Sex IN ('Men', 'Women')),
    BirthCertificate INT NOT NULL UNIQUE,
    FOREIGN KEY (AdressID) REFERENCES Address(ID)
);

CREATE TABLE Passport (
    ID INT NOT NULL PRIMARY KEY,
    PassportNumber INT NOT NULL,
    DocumentTypeID INT NOT NULL,
    PersonID INT NOT NULL,
    PublisherID INT NOT NULL,
    Issued DATE NOT NULL,
    Expiry DATE,
    CHECK (Issued <= Expiry),
    FOREIGN KEY (DocumentTypeID) REFERENCES DocumentType(ID),
    FOREIGN KEY (PersonID) REFERENCES Person(ID),
    FOREIGN KEY (PublisherID) REFERENCES Publisher(ID)
);

CREATE TABLE LostPassport (
    ID INT NOT NULL PRIMARY KEY,
    PassportID INT NOT NULL,
    WhenLost DATE NOT NULL,
    LostDescription VARCHAR(255),
    FOREIGN KEY (PassportID) REFERENCES Passport(ID)
);
