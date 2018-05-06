
CREATE TABLE JErrorLog
(
    ID varchar(32) primary key,
	createtime date  ,
	author varchar(50),
	errorMessage varchar(2000),
	errorInfo clob 
)

/* drop table JErrorLog */