  CREATE TABLE NewTrainLiLunZhi
(							
	num int primary key,										
	TrainName varchar(50) ,				/*--列车名称---*/
	byear int,
	bmonth int,
	
	Fee1 number(18,2),
	Fee2 number(18,2),
	Fee3 number(18,2),
	Fee4 number(18,2),
	Fee5 number(18,2),
	Fee6 number(18,2),
	Fee7 number(18,2),
	Fee8 number(18,2),
	Fee9 number(18,2),
	Fee10 number(18,2),
	Fee11 number(18,2),
	Fee12 number(18,2),
	Fee13 number(18,2),
	Fee14 number(18,2),
	ShouRu number(18,2),
	PCount int,
	
	SFee1 number(18,2),
	SFee2 number(18,2),
	SFee3 number(18,2),
	SFee4 number(18,2),
	SFee5 number(18,2),
	SFee6 number(18,2),
	SFee7 number(18,2),
	SFee8 number(18,2),
	SFee9 number(18,2),
	SFee10 number(18,2),
	SFee11 number(18,2),
	SFee12 number(18,2),
	SFee13 number(18,2),
	SFee14 number(18,2),
	SShouRu number(18,2),
	SPCount int
)


drop table NewTrainLiLunZhi

/*-----创建序列-----------*/
create sequence NewTrainLiLunZhi_Sequence increment by 1 
start with 1 minvalue 1 maxvalue 9999999999999 nocache order;


/*----创建需要序列的表语句1----*/
create or replace trigger NewTrainLiLunZhi_trigger 
before insert on NewTrainLiLunZhi
for each row 
begin 
      select   NewTrainLiLunZhi_Sequence.nextval   into:new.num from sys.dual ; 
end;


create or replace view newtrainlilunzhiview as
select  newtrainlilunzhi.num,newtrainlilunzhi.trainname,byear,bmonth,shouru,pcount,sshouru,spcount,traintype
,Fee1+Fee2+Fee3+Fee4+Fee5+Fee6+Fee7+Fee8+Fee9+Fee10+Fee11+Fee12+Fee13+Fee14 Fee,
sFee1+sFee2+sFee3+sFee4+sFee5+sFee6+sFee7+sFee8+sFee9+sFee10+sFee11+sFee12+sFee13+sFee14 SFee
from newtrainlilunzhi inner join newtrain on  newtrainlilunzhi.trainname = newtrain.trainname



