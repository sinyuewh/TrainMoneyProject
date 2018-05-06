  CREATE TABLE QianYinFeeProfile
(							
	QianYinType	int	not null primary key,
	QianYinName	varchar(50) not null unique,
	Fee1	number(18,2),
	Fee2	number(18,2)
)