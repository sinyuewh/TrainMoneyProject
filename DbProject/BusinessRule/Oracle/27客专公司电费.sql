 CREATE TABLE GSCorpElecFee
(	
	num int not null primary key,									
	CorpName varchar(50) not null,			
	RWBureau varchar(50) not null,	
	NetFee int not null,
	ElecFee int not null
) 

create sequence GSCorpElecFee_Sequence increment by 1 
start with 1 minvalue 1 maxvalue 9999999999999 nocache order;

/*----创建需要序列的表语句4----*/
create or replace trigger GSCorpElecFee_trigger 
before insert on GSCorpElecFee
for each row 
begin 
      select   GSCorpElecFee_Sequence.nextval   into:new.num from sys.dual ; 
end;