CREATE TABLE HighTrainPriceRate							
(							
	num int not null primary key,		
	LiCheng int,
	Rate1 number(18,2),
	Rate2 number(18,2),
	Rate3 number(18,2)
)



create sequence HIGHTRAINPRICERATE_Sequence increment by 1 
start with 1 minvalue 1 maxvalue 9999999999999 nocache order;


create or replace trigger HIGHTRAINPRICERATE_trigger 
before insert on HIGHTRAINPRICERATE
for each row 
begin 
      select   HIGHTRAINPRICERATE_Sequence.nextval   into:new.num from sys.dual ; 
end;