 CREATE TABLE A2A3Fee								/*----客车A2、A3修明细表----*/
(							
	num int not null primary key,		
	TrainType varchar(50) unique,
	A2Fee number(18,2),
	A3Fee number(18,2)
)


drop table A2A3Fee 