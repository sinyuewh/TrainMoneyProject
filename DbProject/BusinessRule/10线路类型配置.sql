CREATE TABLE TrainLineKindProfile
(							
	LineID int not null primary key,
	LineName	varchar(50) not null,
	JieChuFee	number(18,2),
	DianFee		number(18,2)
)