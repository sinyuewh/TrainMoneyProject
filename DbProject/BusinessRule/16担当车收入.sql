  CREATE TABLE NewTrainShouRou
(							
	num int not null primary key,		
	byear int not null,								/*----统计年----*/
	bmonth int not null,							/*----统计月----*/								
	TrainName varchar(50) not null ,				/*--车次---*/
	AStation varchar(50) not null,					/*---始发站----*/
	BStation varchar(50) not null,					/*---终到站----*/
	shouRou1 numeric(18,2),							/*-----旅客列车全程客票进款------*/
	shouRou2 numeric(18,2),							/*-----旅客票价进款------*/
	shouRou3 numeric(18,2),							/*-----软票费------*/
	shouRou4 numeric(18,2),							/*-----卧订费------*/
	shouRou5 numeric(18,2),							/*-----空调费------*/
	shouRou6 numeric(18,2),							/*-----应收客票进款------*/
	
	YinZuo int ,									/*---硬座-------*/
    RuanZuo int ,									/*---软座-------*/
    OpenYinWo int ,									/*---硬卧1-------*/
    CloseYinWo int ,								/*---硬卧2--------*/
    RuanWo int ,									/*---软卧1--------*/
    AdvanceRuanWo int ,								/*---软卧2--------*/
       
    CanChe int ,									/*----餐车--------*/
    FaDianChe int ,									/*----发电车-------*/
    ShuYinChe int,									/*----行李车-------*/
    YouZhengChe int									/*----邮政车-------*/
)


drop table NewTrainShouRou