 
/*-----数据字典明细-------*/
IF EXISTS (SELECT * FROM sysobjects WHERE type = 'U' AND name = 'JItemDetail')
	BEGIN
	   DROP  Table JItemDetail
	END
GO
CREATE TABLE JItemDetail
(
    ID varchar(32) primary key,
    num int, 
    ItemName varchar(50) not null,
    itemText varchar(50) not null,
    itemValue varchar(50) not null
)
