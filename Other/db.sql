IF (EXISTS (select 1 from sys.tables where [name] = 'PERSON_ADDRESS')) 
  DROP TABLE PERSON_ADDRESS;

IF (EXISTS (select 1 from sys.tables where [name] = 'ADDRESS')) 
  DROP TABLE ADDRESS;

IF (EXISTS (select 1 from sys.tables where [name] = 'GROUP_MEMBER')) 
  DROP TABLE GROUP_MEMBER;

IF (EXISTS (select 1 from sys.tables where [name] = 'GROUP')) 
  DROP TABLE [GROUP];

IF (EXISTS (select 1 from sys.tables where [name] = 'TOWN')) 
  DROP TABLE TOWN;

IF (EXISTS (select 1 from sys.tables where [name] = 'COUNTRY')) 
  DROP TABLE COUNTRY;

IF (EXISTS (select 1 from sys.tables where [name] = 'PHONE')) 
  DROP TABLE PHONE;

IF (EXISTS (select 1 from sys.tables where [name] = 'MAIL')) 
  DROP TABLE MAIL;
  
IF (EXISTS (select 1 from sys.tables where [name] = 'PERSON')) 
  DROP TABLE PERSON;

IF (EXISTS (select 1 from sys.tables where [name] = 'TITLE')) 
  DROP TABLE TITLE;
  
IF (EXISTS (select 1 from sys.tables where [name] = 'GENDER')) 
  DROP TABLE GENDER;
  
IF (EXISTS (select 1 from sys.tables where [name] = 'PHONE_TYPE')) 
  DROP TABLE PHONE_TYPE;  
  
CREATE TABLE GENDER 
(
  ID INT NOT NULL,
  NAME VARCHAR(15),
  CONSTRAINT PK_GENEDER PRIMARY KEY (ID)
);

INSERT INTO GENDER (ID, [NAME])
VALUES (0, 'Unspecified');

INSERT INTO GENDER (ID, [NAME])
VALUES (1, 'Male');

INSERT INTO GENDER (ID, [NAME])
VALUES (2, 'Female');

CREATE TABLE TITLE 
(
  ID INT NOT NULL,
  NAME VARCHAR(10) NOT NULL,
  CONSTRAINT PK_TITLE PRIMARY KEY (ID)
);

INSERT INTO TITLE (ID, [NAME])
VALUES (0, 'Other');

INSERT INTO TITLE (ID, [NAME])
VALUES (1, 'Mr');

INSERT INTO TITLE (ID, [NAME])
VALUES (2, 'Mrs');

INSERT INTO TITLE (ID, [NAME])
VALUES (3, 'Miss');

INSERT INTO TITLE (ID, [NAME])
VALUES (4, 'Ms');

INSERT INTO TITLE (ID, [NAME])
VALUES (5, 'Dr');

INSERT INTO TITLE (ID, [NAME])
VALUES (6, 'Prof');

CREATE TABLE PHONE_TYPE 
(
  ID INT NOT NULL,
  TYPE VARCHAR(10) NOT NULL,
  CONSTRAINT PK_PHONE_TYPE PRIMARY KEY (ID)
);

INSERT INTO PHONE_TYPE (ID, [TYPE])
VALUES (0, 'Other');

INSERT INTO PHONE_TYPE (ID, [TYPE])
VALUES (1, 'Home');

INSERT INTO PHONE_TYPE (ID, [TYPE])
VALUES (2, 'Work');

INSERT INTO PHONE_TYPE (ID, [TYPE])
VALUES (3, 'Mobile');


CREATE TABLE PERSON
(
  ID BIGINT IDENTITY(1, 1) NOT NULL,
  TITLE_ID INT NOT NULL,
  FIRST_NAME NVARCHAR(100) NOT NULL,
  LAST_NAME NVARCHAR(100) NULL,
  GENDER_ID INT NOT NULL,
  AVATAR IMAGE NULL,
  CONSTRAINT PK_PERSON PRIMARY KEY (ID),
  CONSTRAINT FK_PERSON_GENDER FOREIGN KEY (GENDER_ID)
  REFERENCES GENDER(ID),
  CONSTRAINT FK_PERSON_TITLE FOREIGN KEY (TITLE_ID)
  REFERENCES TITLE(ID)
);

CREATE TABLE MAIL
(
  ID BIGINT IDENTITY (1, 1) NOT NULL,
  PERSON_ID BIGINT NOT NULL,
  EMAIL nvarchar(200) NOT NULL,
  IS_PRIMARY BIT DEFAULT(0) NOT NULL,
  CONSTRAINT PK_MAIL PRIMARY KEY (ID),
  CONSTRAINT FK_MAIL_PERSON FOREIGN KEY (PERSON_ID)
  REFERENCES PERSON (ID) ON DELETE CASCADE
);

CREATE TABLE COUNTRY
(
  ID BIGINT IDENTITY(1, 1) NOT NULL, 
  NAME NVARCHAR(100) NOT NULL,
  CONSTRAINT PK_COUNTRY PRIMARY KEY (ID) 
);

CREATE TABLE TOWN 
(
  ID BIGINT IDENTITY(1, 1) NOT NULL, 
  NAME NVARCHAR(100) NOT NULL,
  COUNTRY_ID BIGINT NOT NULL,
  CONSTRAINT PK_TOWN PRIMARY KEY (ID),
  CONSTRAINT FK_TOWN_COUNTRY FOREIGN KEY (COUNTRY_ID)
  REFERENCES COUNTRY (ID) ON DELETE CASCADE
);
  
CREATE TABLE ADDRESS 
(
  ID BIGINT IDENTITY(1, 1) NOT NULL,
  TOWN_ID BIGINT NOT NULL,
  ADDRESS NVARCHAR(MAX) NULL,
  POST_CODE NVARCHAR(50) NULL,
  IS_PRIMARY BIT NOT NULL DEFAULT (0),
  CONSTRAINT PK_ADDRESS PRIMARY KEY (ID),
  CONSTRAINT FK_ADDRESS_TOWN FOREIGN KEY (TOWN_ID)
  REFERENCES TOWN(ID)
);

CREATE TABLE PERSON_ADDRESS 
(
   PERSON_ID BIGINT NOT NULL,
   ADDRESS_ID BIGINT NOT NULL,
   CONSTRAINT PK_PERSON_ADDRESS PRIMARY KEY (PERSON_ID, ADDRESS_ID),
   CONSTRAINT FK_PERSON_ADDRESS FOREIGN KEY (PERSON_ID)
   REFERENCES PERSON(ID) ON DELETE CASCADE,
   CONSTRAINT FK_ADDRESS_PERSON FOREIGN KEY (ADDRESS_ID)
   REFERENCES ADDRESS(ID) ON DELETE CASCADE
);

CREATE TABLE PHONE 
(
  ID BIGINT IDENTITY (1,1) NOT NULL,
  PERSON_ID BIGINT NOT NULL,
  NUMBER VARCHAR(50) NOT NULL,
  TYPE_ID INT NOT NULL,
  IS_PRIMARY BIT NOT NULL DEFAULT (0),
  CONSTRAINT PK_PHONE PRIMARY KEY (ID),
  CONSTRAINT FK_PHONE_PERSON FOREIGN KEY (PERSON_ID)
  REFERENCES PERSON (ID) ON DELETE CASCADE,
  CONSTRAINT FK_PHONE_TYPE FOREIGN KEY (TYPE_ID)
  REFERENCES PHONE_TYPE (ID)
);

CREATE TABLE [GROUP]
(
  ID BIGINT IDENTITY(1,1) NOT NULL,
  NAME NVARCHAR(100) NOT NULL,
  CONSTRAINT PK_GROUP PRIMARY KEY (ID)
);

CREATE TABLE GROUP_MEMBER 
(
  PERSON_ID BIGINT NOT NULL,
  GROUP_ID BIGINT NOT NULL,
  CONSTRAINT PK_GROUP_MEMBER PRIMARY KEY (PERSON_ID, GROUP_ID),
  CONSTRAINT FK_GROUP_PERSON FOREIGN KEY (PERSON_ID)
  REFERENCES PERSON(ID) ON DELETE CASCADE,
  CONSTRAINT FK_PERSON_GROUP FOREIGN KEY (GROUP_ID)
  REFERENCES [GROUP] (ID) ON DELETE CASCADE
);
