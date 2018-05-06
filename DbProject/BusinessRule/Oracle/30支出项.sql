create table PAYPROJ
(
    num int not null primary key,									
	PAYNAME varchar(50) not null
)

create sequence PAYPROJ_Sequence increment by 1 
start with 1 minvalue 1 maxvalue 9999999999999 nocache order;

/*----创建需要序列的表语句4----*/
create or replace trigger PAYPROJ_trigger 
before insert on PAYPROJ
for each row 
begin 
      select   PAYPROJ_Sequence.nextval   into:new.num from sys.dual ; 
end;