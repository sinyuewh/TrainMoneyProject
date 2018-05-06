  CREATE TABLE NewTrainDianFee				     /*----电费和接触网使用费----*/
(							
	num int not null primary key,		
	byear int not null,								/*----统计年----*/
	bmonth int not null,							/*----统计月----*/								
	TrainName varchar(50) not null ,				/*--车次---*/
	AStation varchar(50) not null,					/*---始发站----*/
	BStation varchar(50) not null,					/*---终到站----*/
	
	zl1 numeric(18,2),								/*-----列车重量1------*/
	zl2 numeric(18,2),								/*-----列车重量2------*/
	zl3 numeric(18,2),								/*-----列车重量3------*/
	
	Fee1   numeric(18,2),							/*-----电费1------*/
	Fee2   numeric(18,2),							/*-----电费2------*/
	Fee3   numeric(18,2)							/*-----电费3------*/
)


drop table NewTrainDianFee

/*--------创建序列和触发器-------------*/
create sequence NewTrainDianFee_Sequence increment by 1 
start with 1 minvalue 1 maxvalue 9999999999999 nocache order;


/*------创建触发器---------------------*/
create or replace trigger NewTrainDianFee_trigger 
before insert on NewTrainDianFee
for each row 
begin 
      select   NewTrainDianFee_Sequence.nextval   into:new.num from sys.dual ; 
end;
