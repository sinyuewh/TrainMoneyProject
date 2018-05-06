  CREATE TABLE ChangJiaoQYFee
(							
	num int not null,	
	LineID int primary key,									
	LineName varchar(200) not null,			
	JiaoLu varchar(500) not null,	
	Fee1 int,
	Fee2 int,
	Fee3 int
)
/*-----创建序列-----------*/
create sequence ChangJiaoQYFee_Sequence increment by 1 
start with 1 minvalue 1 maxvalue 9999999999999 nocache order;

create or replace trigger ChangJiaoQYFee_trigger 
before insert on ChangJiaoQYFee
for each row 
begin 
      select  ChangJiaoQYFee_Sequence.nextval into:new.LineID from sys.dual ; 
end;


/*----------------------------------------------------------*/
  CREATE TABLE ChangJiaoQYFeeChild
(							
	num int not null primary key,	
	LineID int not null,		
	AStation varchar(50) not null,
	BStation varchar(50) not null,
	Fee1 int,
	Fee2 int,
	Fee3 int 							
)

create sequence ChangJiaoQYFeeChild_Sequence increment by 1 
start with 1 minvalue 1 maxvalue 9999999999999 nocache order;

create or replace trigger ChangJiaoQYFeeChild_trigger 
before insert on ChangJiaoQYFeeChild
for each row 
begin 
      select  ChangJiaoQYFeeChild_Sequence.nextval into:new.num from sys.dual ; 
end;


