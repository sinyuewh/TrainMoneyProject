 CREATE TABLE HeightTrainWeightProfile
(							
	num int,
	TrainType varchar(50) not null primary key,
	Weight	number(18,2),
	Cost1	number(18,2),
	Cost2	number(18,2),
	Cost3	number(18,2)
) 

drop table HeightTrainWeightProfile