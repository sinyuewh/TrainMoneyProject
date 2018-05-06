 CREATE TABLE NewTrainQianYinFee				   /*----机车牵引费----*/
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
	
	Fee1   numeric(18,2),							/*-----牵引费用1------*/
	Fee2   numeric(18,2),							/*-----牵引费用2------*/
	Fee3   numeric(18,2)							/*-----牵引费用3------*/
)


drop table NewTrainQianYinFee