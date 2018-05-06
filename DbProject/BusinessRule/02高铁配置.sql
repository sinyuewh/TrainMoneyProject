 CREATE TABLE HighTrainProfile
(
    id int not null,									
    MileType varchar(50) not null,
    HighTrainType varchar(50) not null primary key,
    PCount1	int not null,
    Rate1	number(18, 6) not null,	
    PCount2	int	not null,
    Rate2	number(18, 6) not null,		
    PCount3	int	not null,
    Rate3	number(18, 6) not null,
    PCount4	int	not null,
    Rate4	number(18, 6) not null					
)