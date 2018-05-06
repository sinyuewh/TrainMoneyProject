CREATE TABLE bigStation								
(							
	num int not null primary key,		
	Name1 varchar(50) not null unique
)

CREATE TABLE bigStationList								
(							
	Dataid int primary key,							
	num int not null ,		
	parentnum int not null,
	name1 varchar(50) not null unique
)


drop table bigStation 
drop table bigStationList