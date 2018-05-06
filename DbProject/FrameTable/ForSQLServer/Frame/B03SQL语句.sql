IF EXISTS (SELECT * FROM sysobjects WHERE type = 'U' AND name = 'JSqlInfo')
	BEGIN
	   DROP  Table JSqlInfo
	END
GO
 CREATE TABLE JSqlInfo
(
    num int,
    sqlID varchar(10) primary key,
	sqlText varchar(2000) ,
	author varchar (50) ,
	createtime datetime  ,
	modelID varchar(50),	
	remark varchar (200)  
)

/* drop table Jsqlinfo */