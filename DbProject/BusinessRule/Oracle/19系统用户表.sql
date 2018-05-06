  CREATE TABLE MyUserName
(							
	num int,
	UserName varchar(50) not null primary key,		
	PassWord varchar(50) not null,							
	IsAdmin varchar(1) not null,
	LastLogin varchar(50)
)


drop table MyUserName