CREATE TABLE CheXianBianZhu
(
    id int primary key,									
    kind varchar(50) not null,											
    name varchar(50) not null,															
    smallkind1 varchar(50),
    smallkind2 varchar(50),
    rate int not null,
    pcount int not null						
)