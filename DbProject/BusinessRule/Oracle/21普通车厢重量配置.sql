CREATE TABLE CommTrainWeightProfile
(							
	num int,
	TrainType varchar(50) not null primary key,
	
	YZWeight	number(18,2),
	YWWeight	number(18,2),
	RZWeight	number(18,2),
	RWWeight	number(18,2),
	GRWWeight	number(18,2),
	CAWeight	number(18,2),
	KDWeight	number(18,2),
	SyWeight	number(18,2),
	
	YZPrice		number(18,2),
	YWPrice		number(18,2),
	RZPrice		number(18,2),
	RWPrice		number(18,2),
	GRWPrice	number(18,2),
	CAPrice		number(18,2),
	KDPrice		number(18,2),
	SyPrice		number(18,2),
	
	YZCost1		number(18,2),
	YWCost1		number(18,2),
	RZCost1		number(18,2),
	RWCost1		number(18,2),
	GRWCost1	number(18,2),
	CACost1		number(18,2),
	KDCost1		number(18,2),
	SyCost1		number(18,2),
	
	YZCost2		number(18,2),
	YWCost2		number(18,2),
	RZCost2		number(18,2),
	RWCost2		number(18,2),
	GRWCost2	number(18,2),
	CACost2		number(18,2),
	KDCost2		number(18,2),
	SyCost2		number(18,2),
	
	YZCost3		number(18,2),
	YWCost3		number(18,2),
	RZCost3		number(18,2),
	RWCost3		number(18,2),
	GRWCost3	number(18,2),
	CACost3		number(18,2),
	KDCost3		number(18,2),
	SyCost3		number(18,2),
	Oil			number(18,2)
	
) 

drop table CommTrainWeightProfile