 CREATE TABLE LineStation
(
    id int primary key,									
    LineId int,
    num int,
    Astation varchar(50) not null,
    Bstation varchar(50) not null,
    miles int,
    milesclass int,
    direction int						
)

drop table LineStation
 
