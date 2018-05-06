 CREATE TABLE NewTrainXianLuFee						/*----线路使用费----*/
(							
	num int not null primary key,		
	byear int not null,								/*----统计年----*/
	bmonth int not null,							/*----统计月----*/								
	TrainName varchar(50) not null ,				/*--车次---*/
	AStation varchar(50) not null,					/*---始发站----*/
	BStation varchar(50) not null,					/*---终到站----*/
	
	gongli1 numeric(18,2),							/*-----列车公里1------*/
	gongli2 numeric(18,2),							/*-----列车公里2------*/
	gongli3 numeric(18,2),							/*-----列车公里3------*/
	
	Fee1   numeric(18,2),							/*-----线路费用1------*/
	Fee2   numeric(18,2),							/*-----线路费用2------*/
	Fee3   numeric(18,2)							/*-----线路费用3------*/
)


drop table NewTrainXianLuFee