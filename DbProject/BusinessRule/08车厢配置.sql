   CREATE TABLE CheXianWeightProfile
(							
	CheXianType int not null primary key,
	CheXianName	varchar(50) not null,
	Weight	number(18,2),
	UnitCost number(18,2),
	UnitFixCost number(18,2),
	UNITXHCOST number(18,2)
)