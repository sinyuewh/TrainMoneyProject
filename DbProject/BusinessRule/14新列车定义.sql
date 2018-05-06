 CREATE TABLE NewTrain
(							
	num int,										
	TrainName varchar(50) not null primary key,		/*--列车名称---*/
	TrainBigKind int not null,						/*--列车大类---*/
	TrainType	varchar(50) not null,				/*--列车类型---*/
	AStation varchar(50) not null,					/*---始发站----*/
	BStation varchar(50) not null,					/*---终到站----*/
	Mile int,										/*---单程距离---*/
	LineID varchar(50) ,							/*---线路ID-----*/
	Kxts int,										/*---开行趟数---*/
	Cdzs number(18,2),								/*---车底组数---*/								

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


drop table NewTrain