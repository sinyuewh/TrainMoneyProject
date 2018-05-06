 CREATE TABLE NewTrainServerPeople					
(							
	num int not null primary key,		
	byear int not null,								/*----统计年----*/
	bmonth int not null,							/*----统计月----*/								
	TrainName varchar(50) not null ,				/*--车次---*/
	AStation varchar(50) not null,					/*---始发站----*/
	BStation varchar(50) not null,					/*---终到站----*/
		
	Pc1   int,										/*-----服务人数1------*/
	Pc2   int,										/*-----服务人数2------*/
	Pc3   int,										/*-----服务人数3------*/
	
	Price number(18,2),								/*-----服务人数单价----*/
	Fee1  number(18,2),								/*-----服务费1---------*/
	Fee2  number(18,2),								/*-----服务费2---------*/	
	Fee3  number(18,2)								/*-----服务费3---------*/
)


drop table NewTrainServerPeople

create sequence NewTrainServerPeople_Sequence increment by 1 
start with 1 minvalue 1 maxvalue 9999999999999 nocache order

/*----创建需要序列的表语句4----*/
create or replace trigger NewTrainServerPeople_trigger 
before insert on NewTrainServerPeople
for each row 
begin 
      select   NewTrainServerPeople_Sequence.nextval   into:new.num from sys.dual ; 
end;

