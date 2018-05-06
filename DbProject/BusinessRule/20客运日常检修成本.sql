  CREATE TABLE CommonRiChangFee								/*----客车日常检修成本表----*/
(							
	num int not null primary key,		
	TrainType varchar(50) unique,
	RCFee1 number(18,2),
	RCFee2 number(18,2)
)


drop table CommonRiChangFee 