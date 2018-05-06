  CREATE TABLE TrainLine
(
    Lineid int primary key,							--线路ＩＤ	
    num int,										--序号				
    LineName varchar(100) not null,					--线路名称
    LineType varchar(100) not null,					--线路类型	
    LineClass varchar(50) not null,					--线路级别
    Remark varchar(200)								--备注
)

drop table TrainLine