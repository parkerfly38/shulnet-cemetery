CREATE TABLE Cemetery (  
    id SERIAL NOT NULL PRIMARY KEY,
    Name VARCHAR(1000),
    GoogleMapUrl VARCHAR(1500)
);
CREATE TABLE Deed (
    id SERIAL NOT NULL PRIMARY KEY,
    SectionId INTEGER NOT NULL,
    Lot VARCHAR(255),
    LastName1 VARCHAR(255),
    FirstName1 VARCHAR(255),
    MiddleName1 VARCHAR(255),
    LastName2 VARCHAR(255),
    FirstName2 VARCHAR(255),
    MiddleName2 VARCHAR(255),
    IssueDate DATE,
    Notes VARCHAR(3000),
    CemeteryId int NOT NULL
);
CREATE TABLE Internment (
    id SERIAL NOT NULL PRIMARY KEY,
    SectionId INTEGER NOT NULL,
    Lot VARCHAR(255),
    Book VARCHAR(255),
    PageNumber INTEGER,
    DeceasedDate DATE,
    FirstName VARCHAR(255),
    LastName VARCHAR(255),
    MiddleName VARCHAR(255),
    BirthPlace VARCHAR(1000),
    LastResidence VARCHAR(1500),
    Age INTEGER,
    Sex VARCHAR(10),
    CemeteryId INTEGER NOT NULL,
    Notes VARCHAR(3000),
    Lot2 VARCHAR(255)
);
CREATE TABLE Person (
    id SERIAL NOT NULL PRIMARY KEY,
    FirstName VARCHAR(255),
    LastName VARCHAR(255),
    Email VARCHAR(255),
    UserId INTEGER NOT NULL
);
CREATE TABLE Role (
    id SERIAL NOT NULL PRIMARY KEY,
    Name VARCHAR(255)
);
CREATE TABLE Section (
    id SERIAL NOT NULL PRIMARY KEY,
    CemeteryId INTEGER NOT NULL,
    Code VARCHAR(255),
    Name VARCHAR(255)
);
CREATE TABLE UserData (
    id SERIAL NOT NULL PRIMARY KEY,
    PersonId INTEGER NOT NULL,
    UserName varchar(255),
    Password varchar(1500),
    AuthenticationMethod varchar(255)
);
CREATE TABLE UserRoles(
    UserId INTEGER NOT NULL,
    RoleId INTEGER NOT NULL
);

