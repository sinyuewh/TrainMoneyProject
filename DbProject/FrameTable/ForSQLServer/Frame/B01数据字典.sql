/*-------数据字典--------*/
IF EXISTS (SELECT * FROM sysobjects WHERE type = 'U' AND name = 'JItem')
	BEGIN
	   DROP  Table JItem
	END
GO
CREATE TABLE JItem
(
    num varchar(10),									
    ItemName varchar(50) primary key not null,			
    author varchar(50),								   
    createtime datetime ,								
    minvalue int,									
    maxvalue int,									
    intervalue int,									
    kind varchar(10),									
    modelID varchar(50),									
    remark varchar(200)								
)

