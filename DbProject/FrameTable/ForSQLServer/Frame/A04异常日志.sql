
IF EXISTS (SELECT * FROM sysobjects WHERE type = 'U' AND name = 'JErrorLog')
	BEGIN
	   DROP  Table JErrorLog
	END
GO
CREATE TABLE JErrorLog
(
    ID varchar(32) primary key,
	createtime datetime  ,
	author varchar(50),
	errorMessage varchar(2000),
	errorInfo text
)

/* drop table JErrorLog */