 CREATE TABLE SearchObjectList
(							
	num int primary key,
	AStation varchar(50) not null,
	BStation varchar(50) not null,
	ShenDu int not null,
	newLineFileName varchar(50) not null,
	middle varchar(500),
	Line1FileName varchar(50),
	Line2FileName varchar(50),
	Line3FileName varchar(50),
	Line4FileName varchar(50),
	SaveTime date default(sysdate),
	FENGDUAN varchar(1000)
) 

drop table SearchObjectList 

create sequence SearchObjectList_Sequence increment by 1 
start with 1 minvalue 1 maxvalue 9999999999999 nocache order;

create or replace trigger SearchObjectList_trigger 
before insert on SearchObjectList
for each row 
begin 
      select   SearchObjectList_Sequence.nextval   into:new.num from sys.dual ; 
end;