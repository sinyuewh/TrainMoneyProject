IF EXISTS (SELECT * FROM sysobjects WHERE type = 'U' AND name = 'JStrInfo')
	BEGIN
	   DROP  Table JStrInfo
	END
GO
  CREATE TABLE JStrInfo
(
    num int,
    strID varchar(10) primary key,
	strText text ,
	author varchar (50) ,
	createtime datetime  ,
	modelid varchar(50),	
	remark varchar (200)  
)

/* drop table Jstrinfo */